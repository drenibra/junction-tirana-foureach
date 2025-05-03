"use client"
import Image from "next/image"
import { motion } from "framer-motion"
import BottomNavigation from "@/components/bottom-navigation"

export default function Challenges() {
  const challenges = [
    {
      id: 1,
      title: "Budget Master",
      description: "Track your spending for 7 days straight",
      reward: 50,
      progress: 3,
      total: 7,
      icon: "📊",
    },
    {
      id: 2,
      title: "Saving Star",
      description: "Save 500 LEK this week",
      reward: 75,
      progress: 300,
      total: 500,
      icon: "💰",
    },
    {
      id: 3,
      title: "Quiz Champion",
      description: "Complete 5 financial quizzes",
      reward: 100,
      progress: 2,
      total: 5,
      icon: "🧠",
    },
  ]

  return (
    <div className="min-h-screen bg-gray-50 pb-20">
      {/* Top Bar */}
      <div className="bg-[#2b2d33] text-white p-4 sticky top-0 z-20">
        <h1 className="text-lg font-bold text-center">Challenges</h1>
      </div>

      {/* Main Content */}
      <div className="p-4">
        <div className="mb-6">
          <h2 className="text-xl font-bold mb-2">Active Challenges</h2>
          <p className="text-gray-600">Complete challenges to earn RaiPoints!</p>
        </div>

        <div className="space-y-4">
          {challenges.map((challenge) => (
            <motion.div
              key={challenge.id}
              initial={{ opacity: 0, y: 20 }}
              animate={{ opacity: 1, y: 0 }}
              transition={{ duration: 0.3, delay: challenge.id * 0.1 }}
              className="bg-white rounded-xl border-2 border-gray-200 p-4"
            >
              <div className="flex items-center mb-3">
                <div className="w-12 h-12 rounded-full bg-[#ffcc33] text-[#2b2d33] flex items-center justify-center text-2xl mr-3">
                  {challenge.icon}
                </div>
                <div>
                  <h3 className="font-bold">{challenge.title}</h3>
                  <p className="text-gray-600 text-sm">{challenge.description}</p>
                </div>
              </div>

              <div className="mb-3">
                <div className="flex justify-between text-sm mb-1">
                  <span>Progress</span>
                  <span>
                    {typeof challenge.progress === "number" && typeof challenge.total === "number"
                      ? `${challenge.progress}/${challenge.total}`
                      : `${Math.round((challenge.progress / challenge.total) * 100)}%`}
                  </span>
                </div>
                <div className="w-full bg-gray-200 rounded-full h-2.5">
                  <div
                    className="bg-[#ffcc33] h-2.5 rounded-full"
                    style={{ width: `${(challenge.progress / challenge.total) * 100}%` }}
                  ></div>
                </div>
              </div>

              <div className="flex justify-between items-center">
                <div className="flex items-center">
                  <Image src="/images/star-coin.png" alt="RaiPoints" width={20} height={20} />
                  <span className="ml-1 font-bold">{challenge.reward} points</span>
                </div>
                <button className="text-sm bg-[#2b2d33] text-white py-1 px-3 rounded-full">Update</button>
              </div>
            </motion.div>
          ))}
        </div>
      </div>

      <BottomNavigation />
    </div>
  )
}
