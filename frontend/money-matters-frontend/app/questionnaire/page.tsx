"use client";

import type React from "react";

import { motion } from "framer-motion";
import Image from "next/image";
import { useRouter } from "next/navigation";
import { useState } from "react";

export default function Questionnaire() {
  const router = useRouter();
  const [step, setStep] = useState(1);
  const [nickname, setNickname] = useState("");
  const [age, setAge] = useState("");
  const [showAgeWarning, setShowAgeWarning] = useState(false);
  const [selectedGoal, setSelectedGoal] = useState("");
  const [customGoal, setCustomGoal] = useState("");
  const [customAmount, setCustomAmount] = useState("");

  const goals = [
    {
      name: "New Phone Case",
      description: "A stylish case for your phone",
      amount: 2000,
    },
    {
      name: "Video Game",
      description: "That new game everyone's talking about",
      amount: 6000,
    },
    {
      name: "Sneakers",
      description: "Cool new kicks for school",
      amount: 8000,
    },
    {
      name: "Concert Ticket",
      description: "See your favorite artist live",
      amount: 5000,
    },
    {
      name: "Ice Cream Treats",
      description: "Treats for you and your friends",
      amount: 1000,
    },
    { name: "Other", description: "Set your own savings goal", amount: 0 },
  ];

  const handleContinue = () => {
    if (step === 1) {
      if (nickname.trim()) {
        setStep(2);
      }
    } else if (step === 2) {
      const ageNum = Number.parseInt(age);
      if (age && !isNaN(ageNum)) {
        setShowAgeWarning(ageNum < 12 || ageNum > 17);
        setStep(3);
      }
    } else if (step === 3) {
      router.push("/signup");
    }
  };

  const handleLogin = () => {
    router.push("/signup");
  };

  const handleKeyPress = (e: React.KeyboardEvent) => {
    if (e.key === "Enter") {
      handleContinue();
    }
  };

  return (
    <main className="flex min-h-screen flex-col items-center justify-center p-6 bg-gradient-to-b from-white to-gray-100">
      <div className="w-full max-w-md">
        <motion.div
          className="card p-6"
          initial={{ opacity: 0, y: 20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ duration: 0.5 }}
        >
          <div className="flex justify-center mb-6">
            <Image
              src="/images/raibuddy.png"
              alt="RaiBuddy"
              width={120}
              height={120}
              className="animate-bounce-slow"
            />
          </div>

          {step === 1 && (
            <motion.div
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
              transition={{ delay: 0.3 }}
            >
              <h1 className="text-2xl font-bold text-center mb-6">
                👋 Welcome! I'm RaiBuddy
              </h1>
              <p className="text-center mb-6">
                I'll help you learn about money in a fun way!
              </p>
              <div className="mb-6">
                <label htmlFor="nickname" className="block mb-2 font-medium">
                  What should I call you?
                </label>
                <input
                  type="text"
                  id="nickname"
                  className="input-field"
                  placeholder="Your nickname"
                  value={nickname}
                  onChange={(e) => setNickname(e.target.value)}
                  onKeyPress={handleKeyPress}
                  autoFocus
                />
              </div>
            </motion.div>
          )}

          {step === 2 && (
            <motion.div
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
              transition={{ delay: 0.3 }}
            >
              <h1 className="text-2xl font-bold text-center mb-6">
                Hi {nickname}! 👋
              </h1>
              <div className="mb-6">
                <label htmlFor="age" className="block mb-2 font-medium">
                  How old are you?
                </label>
                <input
                  type="number"
                  id="age"
                  className="input-field"
                  placeholder="Your age"
                  value={age}
                  onChange={(e) => setAge(e.target.value)}
                  onKeyPress={handleKeyPress}
                  autoFocus
                />
              </div>
            </motion.div>
          )}

          {step === 3 && (
            <motion.div
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
              transition={{ delay: 0.3 }}
            >
              <h1 className="text-2xl font-bold text-center mb-4">
                Choose a Savings Goal
              </h1>

              {showAgeWarning && (
                <div className="bg-amber-100 border-l-4 border-amber-500 text-amber-700 p-4 mb-6 rounded">
                  ⚠️ Note: Typical age range is 12–17, but you can continue
                </div>
              )}

              <div className="grid grid-cols-1 gap-3 mb-6">
                {goals.map((goal) => (
                  <div
                    key={goal.name}
                    className={`border-2 rounded-lg p-3 cursor-pointer transition-all ${
                      selectedGoal === goal.name
                        ? "border-[#ffcc33] bg-amber-50"
                        : "border-gray-200 hover:border-gray-300"
                    }`}
                    onClick={() => setSelectedGoal(goal.name)}
                  >
                    <div className="flex justify-between items-center">
                      <div>
                        <h3 className="font-medium">{goal.name}</h3>
                        <p className="text-sm text-gray-600">
                          {goal.description}
                        </p>
                      </div>
                      {goal.amount > 0 && (
                        <div className="text-right">
                          <span className="font-bold">
                            {goal.amount.toLocaleString()} LEK
                          </span>
                        </div>
                      )}
                    </div>
                  </div>
                ))}
              </div>

              {selectedGoal === "Other" && (
                <div className="mb-6 space-y-4">
                  <div>
                    <label
                      htmlFor="customGoal"
                      className="block mb-2 font-medium"
                    >
                      What's your goal?
                    </label>
                    <input
                      type="text"
                      id="customGoal"
                      className="input-field"
                      placeholder="e.g., New Headphones"
                      value={customGoal}
                      onChange={(e) => setCustomGoal(e.target.value)}
                    />
                  </div>
                  <div>
                    <label
                      htmlFor="customAmount"
                      className="block mb-2 font-medium"
                    >
                      How much do you need? (in LEK)
                    </label>
                    <input
                      type="number"
                      id="customAmount"
                      className="input-field"
                      placeholder="e.g., 5000"
                      value={customAmount}
                      onChange={(e) => setCustomAmount(e.target.value)}
                    />
                  </div>
                </div>
              )}
            </motion.div>
          )}

          <button
            className="btn-secondary w-full mt-4"
            onClick={handleContinue}
          >
            Continue
          </button>

          <p className="text-center text-gray-600 mt-4">
            Have an account already?
          </p>
          <button
            className="btn-primary text-white w-full mt-4"
            onClick={handleLogin}
          >
            Log In
          </button>
        </motion.div>
      </div>
    </main>
  );
}
