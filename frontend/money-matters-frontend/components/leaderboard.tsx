"use client"

import { useState } from "react"
import { motion } from "framer-motion"
import { ArrowLeft, Trophy, Medal, Award } from "lucide-react"
import Image from "next/image"

type User = {
  id: number
  name: string
  avatar?: string
  streakDays: number
  raiPoints: number
  city: string
}

type LeaderboardProps = {
  onClose: () => void
  currentUserId: number
}

export default function Leaderboard({ onClose, currentUserId }: LeaderboardProps) {
  const [activeTab, setActiveTab] = useState<"streaks" | "points">("streaks")

  // Mock data for Albanian users
  const users: User[] = [
    { id: 1, name: "Andi", streakDays: 21, raiPoints: 1250, city: "Tirana" },
    { id: 2, name: "Elisa", streakDays: 18, raiPoints: 1420, city: "Durrës" },
    { id: 3, name: "Genti", streakDays: 15, raiPoints: 980, city: "Vlorë" },
    { id: 4, name: "Bora", streakDays: 12, raiPoints: 870, city: "Shkodër" },
    { id: 5, name: "Dritan", streakDays: 10, raiPoints: 760, city: "Elbasan" },
    { id: 6, name: "Teuta", streakDays: 9, raiPoints: 690, city: "Korçë" },
    { id: 7, name: "Arben", streakDays: 8, raiPoints: 580, city: "Fier" },
    { id: 8, name: "Mirela", streakDays: 7, raiPoints: 510, city: "Berat" },
    { id: 9, name: "Alban", streakDays: 14, raiPoints: 120, city: "Tirana" }, // Current user
    { id: 10, name: "Klajdi", streakDays: 2, raiPoints: 90, city: "Lushnjë" },
  ]

  // Sort users based on active tab
  const sortedUsers = [...users].sort((a, b) =>
    activeTab === "streaks" ? b.streakDays - a.streakDays : b.raiPoints - a.raiPoints,
  )

  // Find current user's rank
  const currentUserRank = sortedUsers.findIndex((user) => user.id === currentUserId) + 1

  // Get medal icon based on rank
  const getMedalIcon = (rank: number) => {
    switch (rank) {
      case 1:
        return <Trophy className="text-yellow-500" size={28} />
      case 2:
        return <Medal className="text-gray-400" size={28} />
      case 3:
        return <Award className="text-amber-700" size={28} />
      default:
        return (
          <span className="w-6 h-6 rounded-full bg-gray-200 flex items-center justify-center text-sm font-bold">
            {rank}
          </span>
        )
    }
  }

  // Get background style for top 3 ranks
  const getTopRankStyle = (rank: number) => {
    switch (rank) {
      case 1:
        return {
          background: "linear-gradient(135deg, #fef9c3 0%, #fef08a 100%)",
          borderColor: "#eab308",
          shadow: "0 4px 6px -1px rgba(234, 179, 8, 0.3)",
        }
      case 2:
        return {
          background: "linear-gradient(135deg, #f3f4f6 0%, #e5e7eb 100%)",
          borderColor: "#9ca3af",
          shadow: "0 4px 6px -1px rgba(156, 163, 175, 0.3)",
        }
      case 3:
        return {
          background: "linear-gradient(135deg, #fef3c7 0%, #fcd34d 100%)",
          borderColor: "#d97706",
          shadow: "0 4px 6px -1px rgba(217, 119, 6, 0.3)",
        }
      default:
        return {}
    }
  }

  return (
    <div className="min-h-screen bg-gray-50">
      {/* Custom Header */}
      <div className="bg-[#2b2d33] text-white p-4 sticky top-0 z-20">
        <div className="flex items-center justify-between">
          <button onClick={onClose} className="p-2 rounded-full hover:bg-gray-700 transition-colors">
            <ArrowLeft size={20} />
          </button>
          <h1 className="text-lg font-bold">Leaderboard</h1>
          <div className="w-10"></div> {/* Spacer for alignment */}
        </div>
      </div>

      {/* Tabs */}
      <div className="flex border-b border-gray-200 bg-white">
        <button
          className={`flex-1 py-3 text-center font-medium ${activeTab === "streaks" ? "text-[#ffcc33] border-b-2 border-[#ffcc33]" : "text-gray-500"}`}
          onClick={() => setActiveTab("streaks")}
        >
          Streaks
        </button>
        <button
          className={`flex-1 py-3 text-center font-medium ${activeTab === "points" ? "text-[#ffcc33] border-b-2 border-[#ffcc33]" : "text-gray-500"}`}
          onClick={() => setActiveTab("points")}
        >
          RaiPoints
        </button>
      </div>

      {/* Leaderboard List */}
      <div className="p-4">
        <div className="mb-4">
          <h2 className="text-lg font-bold">Top Players in Albania</h2>
          <p className="text-sm text-gray-600">Your rank: #{currentUserRank}</p>
        </div>

        {/* Top 3 Podium */}
        <div className="mb-6">
          <div className="flex justify-center items-end h-44 relative">
            {/* Second Place */}
            {sortedUsers.length > 1 && (
              <motion.div
                initial={{ opacity: 0, y: 20 }}
                animate={{ opacity: 1, y: 0 }}
                transition={{ delay: 0.1 }}
                className="w-24 z-10 mx-1"
              >
                <div
                  className="rounded-t-lg border-2 border-gray-300 flex flex-col items-center pt-3 pb-2 relative"
                  style={{
                    background: "linear-gradient(135deg, #f3f4f6 0%, #e5e7eb 100%)",
                    height: "100px",
                    boxShadow: "0 4px 6px -1px rgba(156, 163, 175, 0.3)",
                  }}
                >
                  <div className="absolute -top-3 left-1/2 transform -translate-x-1/2">
                    <Medal className="text-gray-400" size={24} />
                  </div>
                  <div className="w-12 h-12 bg-white rounded-full border-2 border-gray-300 flex items-center justify-center text-lg font-bold mb-1 mt-2">
                    {sortedUsers[1].name.charAt(0)}
                  </div>
                  <p className="text-xs font-bold">{sortedUsers[1].name}</p>
                  <div className="flex items-center mt-1">
                    {activeTab === "streaks" ? (
                      <div className="flex items-center">
                        <span className="mr-1">🔥</span>
                        <span className="text-xs font-bold">{sortedUsers[1].streakDays}</span>
                      </div>
                    ) : (
                      <div className="flex items-center">
                        <Image src="/images/star-coin.png" alt="RaiPoints" width={12} height={12} className="mr-1" />
                        <span className="text-xs font-bold">{sortedUsers[1].raiPoints}</span>
                      </div>
                    )}
                  </div>
                </div>
                <div className="text-center bg-gray-300 text-xs py-1 font-bold rounded-b-lg">2</div>
              </motion.div>
            )}

            {/* First Place */}
            {sortedUsers.length > 0 && (
              <motion.div initial={{ opacity: 0, y: 20 }} animate={{ opacity: 1, y: 0 }} className="w-28 z-20 mx-1">
                <div
                  className="rounded-t-lg border-2 border-yellow-500 flex flex-col items-center pt-3 pb-2 relative"
                  style={{
                    background: "linear-gradient(135deg, #fef9c3 0%, #fef08a 100%)",
                    height: "130px",
                    boxShadow: "0 4px 6px -1px rgba(234, 179, 8, 0.3)",
                  }}
                >
                  <div className="absolute -top-4 left-1/2 transform -translate-x-1/2">
                    <div className="relative">
                      <Trophy className="text-yellow-500" size={32} />
                      <motion.div
                        animate={{ y: [0, -3, 0] }}
                        transition={{ repeat: Number.POSITIVE_INFINITY, duration: 1.5 }}
                        className="absolute -top-1 -right-1 text-xs"
                      >
                        ✨
                      </motion.div>
                      <motion.div
                        animate={{ y: [0, -2, 0] }}
                        transition={{ repeat: Number.POSITIVE_INFINITY, duration: 2, delay: 0.5 }}
                        className="absolute -top-1 -left-1 text-xs"
                      >
                        ✨
                      </motion.div>
                    </div>
                  </div>
                  <div className="w-16 h-16 bg-white rounded-full border-2 border-yellow-500 flex items-center justify-center text-xl font-bold mb-2 mt-3">
                    {sortedUsers[0].name.charAt(0)}
                  </div>
                  <p className="text-sm font-bold">{sortedUsers[0].name}</p>
                  <div className="flex items-center mt-1">
                    {activeTab === "streaks" ? (
                      <div className="flex items-center">
                        <span className="mr-1">🔥</span>
                        <span className="font-bold">{sortedUsers[0].streakDays}</span>
                      </div>
                    ) : (
                      <div className="flex items-center">
                        <Image src="/images/star-coin.png" alt="RaiPoints" width={14} height={14} className="mr-1" />
                        <span className="font-bold">{sortedUsers[0].raiPoints}</span>
                      </div>
                    )}
                  </div>
                </div>
                <div className="text-center bg-yellow-400 text-xs py-1 font-bold rounded-b-lg">1</div>
              </motion.div>
            )}

            {/* Third Place */}
            {sortedUsers.length > 2 && (
              <motion.div
                initial={{ opacity: 0, y: 20 }}
                animate={{ opacity: 1, y: 0 }}
                transition={{ delay: 0.2 }}
                className="w-24 z-10 mx-1"
              >
                <div
                  className="rounded-t-lg border-2 border-amber-600 flex flex-col items-center pt-3 pb-2 relative"
                  style={{
                    background: "linear-gradient(135deg, #fef3c7 0%, #fcd34d 100%)",
                    height: "80px",
                    boxShadow: "0 4px 6px -1px rgba(217, 119, 6, 0.3)",
                  }}
                >
                  <div className="absolute -top-3 left-1/2 transform -translate-x-1/2">
                    <Award className="text-amber-700" size={24} />
                  </div>
                  <div className="w-12 h-12 bg-white rounded-full border-2 border-amber-600 flex items-center justify-center text-lg font-bold mb-1 mt-2">
                    {sortedUsers[2].name.charAt(0)}
                  </div>
                  <p className="text-xs font-bold">{sortedUsers[2].name}</p>
                  <div className="flex items-center mt-1">
                    {activeTab === "streaks" ? (
                      <div className="flex items-center">
                        <span className="mr-1">🔥</span>
                        <span className="text-xs font-bold">{sortedUsers[2].streakDays}</span>
                      </div>
                    ) : (
                      <div className="flex items-center">
                        <Image src="/images/star-coin.png" alt="RaiPoints" width={12} height={12} className="mr-1" />
                        <span className="text-xs font-bold">{sortedUsers[2].raiPoints}</span>
                      </div>
                    )}
                  </div>
                </div>
                <div className="text-center bg-amber-600 text-white text-xs py-1 font-bold rounded-b-lg">3</div>
              </motion.div>
            )}
          </div>
        </div>

        {/* Rest of the Leaderboard */}
        <h3 className="font-medium text-gray-500 mb-3">All Players</h3>
        <div className="space-y-3">
          {sortedUsers.map((user, index) => {
            const rank = index + 1
            const isCurrentUser = user.id === currentUserId
            const isTopThree = rank <= 3
            const topStyle = getTopRankStyle(rank)

            return (
              <motion.div
                key={user.id}
                initial={{ opacity: 0, y: 10 }}
                animate={{ opacity: 1, y: 0 }}
                transition={{ delay: index * 0.05 }}
                className={`flex items-center p-3 rounded-lg ${
                  isCurrentUser
                    ? "bg-amber-50 border-2 border-[#ffcc33]"
                    : isTopThree
                      ? "border-2"
                      : "bg-white border border-gray-200"
                }`}
                style={
                  isTopThree && !isCurrentUser
                    ? {
                        background: topStyle.background,
                        borderColor: topStyle.borderColor,
                        boxShadow: topStyle.shadow,
                      }
                    : {}
                }
              >
                <div className="mr-3">{getMedalIcon(rank)}</div>

                <div
                  className={`w-10 h-10 rounded-full flex items-center justify-center text-lg font-bold mr-3 ${
                    isTopThree
                      ? rank === 1
                        ? "bg-white border-2 border-yellow-500"
                        : rank === 2
                          ? "bg-white border-2 border-gray-400"
                          : "bg-white border-2 border-amber-600"
                      : "bg-gray-200"
                  }`}
                >
                  {user.avatar ? (
                    <Image
                      src={user.avatar || "/placeholder.svg"}
                      alt={user.name}
                      width={40}
                      height={40}
                      className="rounded-full"
                    />
                  ) : (
                    user.name.charAt(0)
                  )}
                </div>

                <div className="flex-1">
                  <div className="flex justify-between">
                    <div>
                      <h3 className="font-medium">{user.name}</h3>
                      <p className="text-xs text-gray-500">{user.city}</p>
                    </div>
                    <div className="text-right">
                      <div className="font-bold">
                        {activeTab === "streaks" ? (
                          <div className="flex items-center">
                            <span className="mr-1">🔥</span>
                            {user.streakDays} days
                          </div>
                        ) : (
                          <div className="flex items-center">
                            <Image
                              src="/images/star-coin.png"
                              alt="RaiPoints"
                              width={16}
                              height={16}
                              className="mr-1"
                            />
                            {user.raiPoints}
                          </div>
                        )}
                      </div>
                    </div>
                  </div>
                </div>

                {isCurrentUser && (
                  <div className="ml-2 px-2 py-1 bg-[#ffcc33] rounded-full text-xs font-bold text-[#2b2d33]">You</div>
                )}
              </motion.div>
            )
          })}
        </div>
      </div>
    </div>
  )
}
