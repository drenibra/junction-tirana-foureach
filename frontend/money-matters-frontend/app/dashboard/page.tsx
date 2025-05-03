"use client"

import { useState, useRef, useEffect } from "react"
import Image from "next/image"
import { motion, AnimatePresence } from "framer-motion"
import { FlameIcon as Fire } from "lucide-react"
import { useRouter } from "next/navigation"
import BottomNavigation from "@/components/bottom-navigation"

export default function Dashboard() {
  const router = useRouter()
  const [streakCount] = useState(3)
  const [raiPoints] = useState(120)
  const [selectedLesson, setSelectedLesson] = useState<number | null>(null)
  const popupRef = useRef<HTMLDivElement>(null)

  // Close popup when clicking outside
  useEffect(() => {
    function handleClickOutside(event: MouseEvent) {
      if (popupRef.current && !popupRef.current.contains(event.target as Node)) {
        setSelectedLesson(null)
      }
    }
    document.addEventListener("mousedown", handleClickOutside)
    return () => {
      document.removeEventListener("mousedown", handleClickOutside)
    }
  }, [])

  // Define lessons with positions for a backward S trail
  // Adjusted xOffset values to span more of the screen width
  const lessons = [
    {
      id: 1,
      title: "Money Basics",
      description: "Learn what money is and how it works",
      completed: true,
      xOffset: -120,
      yOffset: -20,
      points: 20,
    },
    {
      id: 2,
      title: "Saving Habits",
      description: "Discover smart ways to save money",
      completed: true,
      xOffset: -20,
      yOffset: 80,
      points: 25,
    },
    {
      id: 3,
      title: "Budgeting 101",
      description: "Create your first budget",
      completed: false,
      current: true,
      xOffset: 100,
      yOffset: 160,
      points: 30,
    },
    {
      id: 4,
      title: "Smart Spending",
      description: "Learn to spend wisely",
      completed: false,
      locked: true,
      xOffset: -20,
      yOffset: 240,
      points: 35,
    },
    {
      id: 5,
      title: "Earning Money",
      description: "Explore ways to earn money as a teen",
      completed: false,
      locked: true,
      xOffset: -120,
      yOffset: 320,
      points: 40,
    },
    {
      id: 6,
      title: "Saving Goals",
      description: "Setting and achieving financial goals",
      completed: false,
      locked: true,
      xOffset: -20,
      yOffset: 400,
      points: 45,
    },
    {
      id: 7,
      title: "Banking Basics",
      description: "Understanding how banks work",
      completed: false,
      locked: true,
      xOffset: 100,
      yOffset: 480,
      points: 50,
    },
    {
      id: 8,
      title: "Financial Future",
      description: "Planning for your financial future",
      completed: false,
      locked: true,
      xOffset: -20,
      yOffset: 560,
      points: 55,
    },
  ]

  const handleStartLesson = (lessonId: number) => {
    router.push(`/lesson/${lessonId}`)
  }

  return (
    <div className="min-h-screen bg-gray-50 pb-20">
      {/* Top Bar */}
      <div className="bg-[#2b2d33] text-white p-4 sticky top-0 z-20">
        <div className="flex justify-between items-center">
          <div className="flex items-center space-x-2">
            <div className="flex items-center bg-[#3a3c42] rounded-full px-3 py-1">
              <Fire className="text-orange-500 mr-1" size={24} />
              <span className="font-medium">{streakCount}</span>
            </div>
          </div>
          <h1 className="text-lg font-bold">RaiBuddy</h1>
          <div className="flex items-center space-x-1 bg-[#3a3c42] rounded-full px-3 py-1">
            <Image src="/images/star-coin.png" alt="RaiPoints" width={24} height={24} />
            <span className="font-medium">{raiPoints}</span>
          </div>
        </div>
      </div>

      {/* Main Content */}
      <div className="p-4">
        {/* RaiBuddy Character */}
        <div className="relative h-20 mb-4">
          <div className="absolute left-4 top-0">
            <motion.div
              initial={{ y: 0 }}
              animate={{ y: [0, -10, 0] }}
              transition={{
                repeat: Number.POSITIVE_INFINITY,
                duration: 2,
                ease: "easeInOut",
              }}
              className="transform scale-x-[-1]" // This flips the character horizontally
            >
              <Image src="/images/raibuddy.png" alt="RaiBuddy" width={80} height={80} />
            </motion.div>
          </div>

          <div className="pl-28 pt-2">
            <h2 className="text-xl font-bold mb-2">Continue Learning</h2>
            <p className="text-gray-600">Keep up your daily streak!</p>
          </div>
        </div>

        {/* Lessons Path - Backward S Trail */}
        <div className="relative mt-10 pb-80">
          <div className="relative z-10 flex justify-center" style={{ height: "600px" }}>
            <div className="relative w-full">
              {lessons.map((lesson) => (
                <motion.div
                  key={lesson.id}
                  initial={{ opacity: 0, y: 20 }}
                  animate={{ opacity: 1, y: 0 }}
                  transition={{ duration: 0.3, delay: lesson.id * 0.1 }}
                  className="absolute"
                  style={{
                    left: `calc(50% + ${lesson.xOffset}px)`,
                    top: lesson.yOffset,
                    transform: "translateX(-50%)",
                  }}
                >
                  <motion.div
                    onClick={() => setSelectedLesson(lesson.id)}
                    whileHover={{ scale: 1.1 }}
                    whileTap={{ scale: 0.95 }}
                    className="relative cursor-pointer"
                  >
                    <div className="lesson-icon-wrapper">
                      <Image
                        src={
                          lesson.completed || lesson.current
                            ? "/images/path-active.png"
                            : "/images/path-deactivated.png"
                        }
                        alt={`Lesson ${lesson.id}`}
                        width={80}
                        height={80}
                        className={`lesson-icon ${lesson.locked ? "opacity-70" : ""}`}
                      />
                    </div>

                    {lesson.completed && (
                      <div className="absolute -top-2 -right-2 w-8 h-8 bg-[#ffcc33] border-2 border-white rounded-full flex items-center justify-center">
                        <span className="text-sm text-white">✓</span>
                      </div>
                    )}

                    {lesson.current && (
                      <div className="absolute -top-2 -right-2 w-8 h-8 bg-[#ffcc33] border-2 border-white rounded-full flex items-center justify-center">
                        <span className="text-sm text-white">!</span>
                      </div>
                    )}
                  </motion.div>
                </motion.div>
              ))}
            </div>
          </div>
        </div>
      </div>

      {/* Lesson Popup */}
      <AnimatePresence>
        {selectedLesson !== null && (
          <motion.div
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            exit={{ opacity: 0 }}
            className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 p-4"
          >
            <motion.div
              ref={popupRef}
              initial={{ scale: 0.9, y: 20 }}
              animate={{ scale: 1, y: 0 }}
              exit={{ scale: 0.9, y: 20 }}
              className="bg-white rounded-xl border-2 border-gray-200 p-5 w-full max-w-sm relative"
            >
              {lessons
                .filter((lesson) => lesson.id === selectedLesson)
                .map((lesson) => (
                  <div key={lesson.id}>
                    <div className="flex items-center mb-4">
                      <div className="w-16 h-16 mr-4 flex-shrink-0 flex items-center justify-center">
                        <Image
                          src={
                            lesson.completed || lesson.current
                              ? "/images/path-active.png"
                              : "/images/path-deactivated.png"
                          }
                          alt={`Lesson ${lesson.id}`}
                          width={64}
                          height={64}
                          className={lesson.locked ? "opacity-70" : ""}
                        />
                      </div>
                      <div>
                        <h3 className="text-xl font-bold">{lesson.title}</h3>
                        <p className="text-gray-600 text-sm">{lesson.description}</p>
                      </div>
                    </div>

                    {lesson.locked ? (
                      <div className="bg-gray-100 rounded-lg p-4 text-center mb-3">
                        <p className="text-gray-600">Complete previous lessons to unlock</p>
                      </div>
                    ) : null}

                    {!lesson.locked && (
                      <button
                        onClick={() => handleStartLesson(lesson.id)}
                        className={`w-full ${lesson.completed ? "bg-[#ffcc33] text-[#2b2d33]" : "bg-[#2b2d33] text-white"} font-bold py-3 rounded-lg mb-3`}
                      >
                        {lesson.completed ? "REVIEW" : "START"} +{lesson.points} XP
                      </button>
                    )}
                  </div>
                ))}
            </motion.div>
          </motion.div>
        )}
      </AnimatePresence>

      <BottomNavigation />
    </div>
  )
}
