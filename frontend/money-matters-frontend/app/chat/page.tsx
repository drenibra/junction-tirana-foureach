"use client"

import type React from "react"

import { useState } from "react"
import Image from "next/image"
import { motion } from "framer-motion"
import { Send } from "lucide-react"
import BottomNavigation from "@/components/bottom-navigation"

export default function Chat() {
  const [message, setMessage] = useState("")
  const [chatHistory, setChatHistory] = useState([
    {
      id: 1,
      sender: "bot",
      text: "Hi there! I'm RaiBuddy. How can I help you with financial questions today?",
      timestamp: new Date(Date.now() - 60000),
    },
  ])

  const handleSendMessage = () => {
    if (!message.trim()) return

    // Add user message to chat
    const userMessage = {
      id: chatHistory.length + 1,
      sender: "user",
      text: message,
      timestamp: new Date(),
    }

    setChatHistory([...chatHistory, userMessage])
    setMessage("")

    // Simulate bot response after a short delay
    setTimeout(() => {
      const botResponse = {
        id: chatHistory.length + 2,
        sender: "bot",
        text: "That's a great question about money! Learning to save early is one of the best financial habits you can develop. Try setting aside a small amount each week.",
        timestamp: new Date(),
      }
      setChatHistory((prev) => [...prev, botResponse])
    }, 1000)
  }

  const handleKeyPress = (e: React.KeyboardEvent) => {
    if (e.key === "Enter" && !e.shiftKey) {
      e.preventDefault()
      handleSendMessage()
    }
  }

  return (
    <div className="min-h-screen bg-gray-50 pb-20">
      {/* Top Bar */}
      <div className="bg-[#2b2d33] text-white p-4 sticky top-0 z-20">
        <h1 className="text-lg font-bold text-center">RaiChat</h1>
      </div>

      {/* Chat Area */}
      <div className="flex flex-col h-[calc(100vh-132px)]">
        <div className="flex-1 overflow-y-auto p-4">
          {chatHistory.map((chat) => (
            <motion.div
              key={chat.id}
              initial={{ opacity: 0, y: 10 }}
              animate={{ opacity: 1, y: 0 }}
              transition={{ duration: 0.3 }}
              className={`mb-4 flex ${chat.sender === "user" ? "justify-end" : "justify-start"}`}
            >
              {chat.sender === "bot" && (
                <div className="w-8 h-8 mr-2 flex-shrink-0">
                  <Image src="/images/raibuddy.png" alt="RaiBuddy" width={32} height={32} />
                </div>
              )}
              <div
                className={`max-w-[75%] rounded-lg p-3 ${
                  chat.sender === "user" ? "bg-[#2b2d33] text-white" : "bg-white border border-gray-200"
                }`}
              >
                <p>{chat.text}</p>
                <p className="text-xs mt-1 opacity-70">
                  {chat.timestamp.toLocaleTimeString([], {
                    hour: "2-digit",
                    minute: "2-digit",
                  })}
                </p>
              </div>
            </motion.div>
          ))}
        </div>

        {/* Message Input */}
        <div className="bg-white border-t border-gray-200 p-3">
          <div className="flex items-center bg-gray-100 rounded-full px-4 py-2">
            <input
              type="text"
              placeholder="Ask RaiBuddy a question..."
              className="flex-1 bg-transparent outline-none"
              value={message}
              onChange={(e) => setMessage(e.target.value)}
              onKeyPress={handleKeyPress}
            />
            <button
              onClick={handleSendMessage}
              className="ml-2 w-8 h-8 flex items-center justify-center bg-[#ffcc33] text-[#2b2d33] rounded-full"
            >
              <Send size={16} />
            </button>
          </div>
        </div>
      </div>

      <BottomNavigation />
    </div>
  )
}
