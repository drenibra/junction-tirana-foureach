"use client";

import BottomNavigation from "@/components/bottom-navigation";
import DOMPurify from "dompurify";
import { motion } from "framer-motion";
import { Send } from "lucide-react";
import { marked } from "marked";
import Image from "next/image";
import { useEffect, useRef, useState } from "react";

interface ChatMessage {
  id: number;
  sender: "user" | "bot";
  text: string;
  timestamp: string;
}

export default function Chat() {
  const [message, setMessage] = useState("");
  const [chatHistory, setChatHistory] = useState<ChatMessage[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [idCounter, setIdCounter] = useState(1);
  const chatContainerRef = useRef<HTMLDivElement>(null);

  const loadingGifs = ["/images/loading-raistar.gif"];
  const getRandomGif = () =>
    loadingGifs[Math.floor(Math.random() * loadingGifs.length)];

  const systemPrompt = `
    You are RaiBuddy, an AI financial education companion designed for teenagers aged 12–17, created for Raiffeisen Bank's financial literacy platform. Your purpose is to empower teens to make smart financial decisions through engaging, accessible, and interactive guidance. Base your responses on insights from a Financial Education Bootcamp with 60 teens, focusing on their needs, struggles, and dreams.

    **Your Role and Tone**:
    - Be friendly, relatable, and encouraging, using a conversational tone that resonates with teens.
    - Use simple language to explain financial concepts like budgeting, saving, investing, and managing needs vs. wants.
    - Incorporate fun elements like pop culture references, gamification (e.g., challenges or scenarios), and relatable examples (e.g., saving for a PS5 or concert tickets).
    - Encourage critical thinking and provide actionable tips without overwhelming the user.
    - Ensure responses are safe, age-appropriate, and avoid complex jargon.

    **Key Goals**:
    - Help teens understand and manage money through simplified budgeting and expense tracking.
    - Teach the value of money and guide wise financial decisions (e.g., balancing essentials like food with wants like entertainment).
    - Gamify financial education with playful challenges or scenarios (e.g., "Imagine you have €500, how would you split it?").
    - Provide personalized advice for saving (e.g., for a laptop, travel, or emergency fund) and introduce basic investment concepts (e.g., stocks, bonds) in a teen-friendly way.
    - Address common teen challenges: balancing needs vs. wants, avoiding debt, understanding cost of living, and generating income (e.g., side hustles like babysitting).
    - Inspire with ideas like AI-powered budgeting tools, social sharing of financial milestones, or connecting financial education to hobbies (e.g., gaming, sports).

    **Response Guidelines**:
    - Use Markdown for formatting (e.g., **bold** for emphasis, - for lists) to make responses clear and engaging.
    - Tailor advice to the user's question, incorporating bootcamp insights (e.g., teens want to save for tech, travel, or experiences).
    - Suggest practical steps or tools (e.g., "Try splitting your allowance: 50% needs, 30% wants, 20% savings").
    - When relevant, include a fun challenge or question to keep the user engaged (e.g., "What’s one thing you’d love to save for?").
    - If the user’s question is vague, ask clarifying questions to provide relevant advice (e.g., "Are you thinking about saving, spending, or investing?").

    **Example Context**:
    - Teens want to save for goals like laptops, sneakers, or trips but struggle with budgeting €2,500 for a month.
    - They’re curious about investments (e.g., stocks, crypto) but lack basic knowledge.
    - They love gamified learning, pop culture, and social features like sharing achievements.

    Make every response educational, fun, and empowering, turning financial literacy into an exciting journey for teens!
  `;

  useEffect(() => {
    const savedChats = localStorage.getItem("chatHistory");
    console.log(savedChats);
    if (savedChats) {
      const parsed = JSON.parse(savedChats) as ChatMessage[];
      setChatHistory(parsed);
      setIdCounter(
        parsed.length > 0 ? Math.max(...parsed.map((c) => c.id)) + 1 : 1
      );
    } else {
      const welcome: ChatMessage = {
        id: idCounter,
        sender: "bot",
        text: "Hi there! I'm **RaiBuddy**, your financial education buddy! 😎...",
        timestamp: new Date().toISOString(),
      };
      setChatHistory([welcome]);
      setIdCounter(idCounter + 1);
    }
  }, []);

  useEffect(() => {
    if (chatHistory != null && chatHistory.length > 0)
      localStorage.setItem("chatHistory", JSON.stringify(chatHistory));
  }, [chatHistory]);

  useEffect(() => {
    if (chatContainerRef.current) {
      chatContainerRef.current.scrollTo({
        top: chatContainerRef.current.scrollHeight,
        behavior: "smooth",
      });
    }
  }, [chatHistory, isLoading]);

  const handleSendMessage = async () => {
    if (!message.trim()) return;
    setIsLoading(true);
    const userMsg: ChatMessage = {
      id: idCounter,
      sender: "user",
      text: message,
      timestamp: new Date().toISOString(),
    };
    setChatHistory((prev) => [...prev, userMsg]);
    setIdCounter((prev) => prev + 1);

    const conversation = [
      { role: "system", content: systemPrompt },
      ...chatHistory.map((c) => ({
        role: c.sender === "user" ? "user" : "assistant",
        content: c.text,
      })),
      { role: "user", content: message },
    ];

    try {
      const res = await fetch("/api/chat", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ messages: conversation }),
      });
      const data = await res.json();
      const botText = data.content || "Sorry, something went wrong.";
      const botMsg: ChatMessage = {
        id: idCounter + 1,
        sender: "bot",
        text: botText,
        timestamp: new Date().toISOString(),
      };
      setChatHistory((prev) => [...prev, botMsg]);
      setIdCounter((prev) => prev + 2);
    } catch (err) {
      console.error(err);
      const errorMsg: ChatMessage = {
        id: idCounter + 1,
        sender: "bot",
        text: "Sorry, I'm having trouble responding right now.",
        timestamp: new Date().toISOString(),
      };
      setChatHistory((prev) => [...prev, errorMsg]);
      setIdCounter((prev) => prev + 2);
    } finally {
      setMessage("");
      setIsLoading(false);
    }
  };

  const handleTextareaChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
    setMessage(e.target.value);
    e.target.style.height = "auto";
    e.target.style.height = `${e.target.scrollHeight}px`;
  };

  const handleKeyDown = (e: React.KeyboardEvent<HTMLTextAreaElement>) => {
    if (e.key === "Enter" && !e.shiftKey) {
      e.preventDefault();
      handleSendMessage();
    }
  };

  const renderMarkdown = (text: string) => {
    const raw = marked.parse(text) as string;
    const sanitized = DOMPurify.sanitize(raw);
    return { __html: sanitized };
  };

  return (
    <div className="min-h-screen bg-gray-50 pb-20">
      <div className="bg-[#2b2d33] text-white p-4 sticky top-0 z-20">
        <h1 className="text-lg font-bold text-center">RaiChat</h1>
      </div>
      <div className="flex flex-col h-[calc(100vh-132px)]">
        <div className="flex-1 overflow-y-auto p-4" ref={chatContainerRef}>
          {chatHistory.map((chat) => (
            <motion.div
              key={chat.id}
              initial={{ opacity: 0, y: 10 }}
              animate={{ opacity: 1, y: 0 }}
              transition={{ duration: 0.3 }}
              className={`mb-4 flex ${
                chat.sender === "user" ? "justify-end" : "justify-start"
              }`}
            >
              {chat.sender === "bot" && (
                <div className="w-16 h-16 mr-2 flex-shrink-0">
                  <Image
                    src="/images/raibuddy.png"
                    alt="RaiBuddy"
                    width={64}
                    height={64}
                  />
                </div>
              )}
              <div
                className={`max-w-[75%] rounded-lg p-3 ${
                  chat.sender === "user"
                    ? "bg-[#2b2d33] text-white"
                    : "bg-white border border-gray-200"
                }`}
              >
                <div dangerouslySetInnerHTML={renderMarkdown(chat.text)} />
                <p className="text-xs mt-1 opacity-70">
                  {new Date(chat.timestamp).toLocaleTimeString([], {
                    hour: "2-digit",
                    minute: "2-digit",
                  })}
                </p>
              </div>
            </motion.div>
          ))}
          {isLoading && (
            <motion.div
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
              transition={{ duration: 0.3 }}
              className="flex justify-start mb-4"
            >
              <Image
                src={getRandomGif()}
                alt="Loading"
                width={200}
                height={200}
                className="rounded-lg"
              />
            </motion.div>
          )}
        </div>
        <div className="bg-white border-t border-gray-200 p-3">
          <div className="flex items-center bg-gray-100 rounded-full px-4 py-2">
            <textarea
              placeholder="Ask RaiBuddy a question..."
              className="flex-1 bg-transparent outline-none resize-none max-h-40 overflow-y-auto"
              value={message}
              onChange={handleTextareaChange}
              onKeyDown={handleKeyDown}
              disabled={isLoading}
              rows={1}
            />
            <button
              onClick={handleSendMessage}
              disabled={isLoading}
              className={`ml-2 w-8 h-8 flex items-center justify-center rounded-full ${
                isLoading ? "bg-gray-400" : "bg-[#ffcc33] text-[#2b2d33]"
              }`}
            >
              <Send size={16} />
            </button>
          </div>
        </div>
      </div>
      <BottomNavigation />
    </div>
  );
}
