"use client";

import BottomNavigation from "@/components/bottom-navigation";
import { motion } from "framer-motion";
import { Award, LogOut, Settings } from "lucide-react";
import Image from "next/image";
import { useState } from "react";

export default function Profile() {
  const [user] = useState({
    nickname: "Dren",
    age: 15,
    streakDays: 3,
    raiPoints: 120,
    level: 2,
    achievements: [
      { id: 1, name: "First Lesson", icon: "🎓", date: "2 days ago" },
      { id: 2, name: "Saving Star", icon: "💰", date: "1 day ago" },
    ],
  });

  return (
    <div className="min-h-screen bg-gray-50 pb-20">
      {/* Top Bar */}
      <div className="bg-[#2b2d33] text-white p-4 sticky top-0 z-20">
        <h1 className="text-lg font-bold text-center">My Profile</h1>
      </div>

      {/* Main Content */}
      <div className="p-4">
        {/* Profile Header */}
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ duration: 0.5 }}
          className="bg-white rounded-xl border-2 border-gray-200 p-6 mb-6"
        >
          <div className="flex items-center">
            <div className="w-16 h-16 bg-[#ffcc33] rounded-full flex items-center justify-center text-[#2b2d33] text-2xl font-bold mr-4">
              {user.nickname.charAt(0)}
            </div>
            <div>
              <h2 className="text-xl font-bold">{user.nickname}</h2>
              <p className="text-gray-600">Age: {user.age}</p>
              <div className="flex items-center mt-1">
                <div className="flex items-center mr-3">
                  <span className="mr-1">🔥</span>
                  <span className="font-medium">
                    {user.streakDays} day streak
                  </span>
                </div>
                <div className="flex items-center">
                  <Image
                    src="/images/star-coin.png"
                    alt="RaiPoints"
                    width={16}
                    height={16}
                    className="mr-1"
                  />
                  <span className="font-medium">{user.raiPoints} points</span>
                </div>
              </div>
            </div>
          </div>

          <div className="mt-4">
            <div className="flex justify-between text-sm mb-1">
              <span>Level {user.level}</span>
              <span>Level {user.level + 1}</span>
            </div>
            <div className="w-full bg-gray-200 rounded-full h-2.5">
              <div
                className="bg-[#ffcc33] h-2.5 rounded-full"
                style={{ width: "45%" }}
              ></div>
            </div>
            <p className="text-center text-sm text-gray-600 mt-1">
              {user.raiPoints} / 200 points to next level
            </p>
          </div>
        </motion.div>

        {/* Achievements */}
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ duration: 0.5, delay: 0.1 }}
          className="bg-white rounded-xl border-2 border-gray-200 p-4 mb-6"
        >
          <h3 className="font-bold text-lg mb-3 flex items-center">
            <Award size={20} className="mr-2 text-[#ffcc33]" />
            Achievements
          </h3>

          <div className="space-y-3">
            {user.achievements.map((achievement) => (
              <div key={achievement.id} className="flex items-center">
                <div className="w-10 h-10 rounded-full bg-amber-100 flex items-center justify-center text-xl mr-3">
                  {achievement.icon}
                </div>
                <div>
                  <h4 className="font-medium">{achievement.name}</h4>
                  <p className="text-xs text-gray-500">{achievement.date}</p>
                </div>
              </div>
            ))}
          </div>
        </motion.div>

        {/* Actions */}
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ duration: 0.5, delay: 0.2 }}
          className="space-y-3"
        >
          <button className="w-full flex items-center p-3 bg-white rounded-lg border border-gray-200">
            <Settings size={20} className="mr-3 text-gray-500" />
            <span>Settings</span>
          </button>

          <button className="w-full flex items-center p-3 bg-white rounded-lg border border-gray-200">
            <LogOut size={20} className="mr-3 text-gray-500" />
            <span>Sign Out</span>
          </button>
        </motion.div>
      </div>

      <BottomNavigation />
    </div>
  );
}
