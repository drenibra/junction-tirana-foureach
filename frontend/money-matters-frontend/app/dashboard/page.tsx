"use client";

import { AnimatePresence, motion } from "framer-motion";
import { useEffect, useRef, useState } from "react";

import BottomNavigation from "@/components/bottom-navigation";
import { FlameIcon as Fire } from "lucide-react";
import Image from "next/image";
import { useRouter } from "next/navigation";

export default function Dashboard() {
  const router = useRouter();
  const [streakCount] = useState(3);
  const [raiPoints] = useState(120);
  const [selectedLesson, setSelectedLesson] = useState<number | null>(null);
  const popupRef = useRef<HTMLDivElement>(null);

  // Refs and state for drawing roadmap lines
  const containerRef = useRef<HTMLDivElement>(null);
  const lessonRefs = useRef<(HTMLDivElement | null)[]>([]);
  const [points, setPoints] = useState<string[]>([]);

  // Close popup when clicking outside
  useEffect(() => {
    function handleClickOutside(event: MouseEvent) {
      if (
        popupRef.current &&
        !popupRef.current.contains(event.target as Node)
      ) {
        setSelectedLesson(null);
      }
    }
    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);

  // Calculate points for segments
  useEffect(() => {
    if (!containerRef.current) return;
    const pts = lessonRefs.current
      .map((ref) => {
        if (!ref) return null;
        const rect = ref.getBoundingClientRect();
        const cr = containerRef.current!.getBoundingClientRect();
        const x = rect.left - cr.left + rect.width / 2;
        const y = rect.top - cr.top + rect.height / 2;
        return `${x},${y}`;
      })
      .filter((p): p is string => p !== null);
    setPoints(pts);
  }, []);

  // Define lessons with positions for a backward S trail
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
    {
      id: 9,
      title: "Investing Basics",
      description: "Get started with investing and grow your wealth",
      completed: false,
      locked: true,
      xOffset: 50,
      yOffset: 640,
      points: 60,
    },
    {
      id: 10,
      title: "Debt Management",
      description: "Learn how to manage and reduce debt",
      completed: false,
      locked: true,
      xOffset: -50,
      yOffset: 720,
      points: 65,
    },
    {
      id: 11,
      title: "Credit Scores",
      description: "Understanding and improving your credit score",
      completed: false,
      locked: true,
      xOffset: 0,
      yOffset: 800,
      points: 70,
    },
    {
      id: 12,
      title: "Insurance 101",
      description: "An introduction to different types of insurance",
      completed: false,
      locked: true,
      xOffset: 70,
      yOffset: 880,
      points: 75,
    },
    {
      id: 13,
      title: "Retirement Planning",
      description: "Preparing for a secure retirement from a young age",
      completed: false,
      locked: true,
      xOffset: -70,
      yOffset: 960,
      points: 80,
    },
  ];

  const handleStartLesson = (lessonId: number) =>
    router.push(`/lesson/${lessonId}`);

  return (
    <div className="min-h-screen bg-gradient-to-b from-blue-100 to-blue-50 pb-20">
      {/* Top Bar */}
      <div className="bg-[#2b2d33] text-white p-4 sticky top-0 z-50">
        <div className="flex justify-between items-center">
          <div className="flex items-center space-x-2">
            <div className="flex items-center bg-[#3a3c42] rounded-full px-3 py-1">
              <Fire className="text-orange-500 mr-1" size={24} />
              <span className="font-medium">{streakCount}</span>
            </div>
          </div>
          <h1 className="text-lg font-bold">RaiBuddy</h1>
          <div className="flex items-center space-x-1 bg-[#3a3c42] rounded-full px-3 py-1">
            <Image
              src="/images/star-coin.png"
              alt="RaiPoints"
              width={24}
              height={24}
            />
            <span className="font-medium">{raiPoints}</span>
          </div>
        </div>
      </div>

      {/* Main Content */}
      <div className="p-4">
        {/* Character */}
        <div className="relative h-20 mb-4">
          <div className="absolute left-4 top-0">
            <motion.div
              initial={{ y: 0 }}
              animate={{ y: [0, -10, 0] }}
              transition={{ repeat: Infinity, duration: 2, ease: "easeInOut" }}
              className="transform scale-x-[-1]"
            >
              <Image
                src="images/raibuddy.png"
                alt="RaiBuddy"
                width={100}
                height={100}
              />
            </motion.div>
          </div>
          <div className="pl-28 pt-2">
            <h2 className="text-xl font-bold mb-2">Continue Learning</h2>
            <p className="text-gray-600">Keep up your daily streak!</p>
          </div>
        </div>

        {/* Lessons Path */}
        <motion.div
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          exit={{ opacity: 0 }}
        >
          <div className="relative -ms-[40px] mt-14 pb-80" ref={containerRef}>
            {/* SVG segments for dotted lines */}
            <svg className="absolute inset-0 w-full h-full pointer-events-none">
              {points.map((pt, i) =>
                i < points.length - 1 ? (
                  <path
                    key={i}
                    d={`M ${pt} L ${points[i + 1]}`}
                    fill="none"
                    stroke="#888888"
                    strokeDasharray="8 8"
                    strokeWidth={4}
                    strokeLinecap="round"
                  />
                ) : null
              )}
            </svg>

            <div
              className="relative z-20 flex justify-center"
              style={{ height: "600px" }}
            >
              <div className="relative w-full">
                {lessons.map((lesson, idx) => (
                  <motion.div
                    key={lesson.id}
                    ref={(el) => {
                      lessonRefs.current[idx] = el;
                    }}
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
                      <Image
                        src={
                          lesson.completed || lesson.current
                            ? "/images/path-active.png"
                            : "/images/path-deactivated.png"
                        }
                        alt={`Lesson ${lesson.id}`}
                        width={80}
                        height={80}
                        className={lesson.locked ? "" : ""}
                      />
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
        </motion.div>
      </div>

      {/* Popup */}
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
                      <div className="ml-3">
                        <h3 className="text-xl font-bold">{lesson.title}</h3>
                        <p className="text-gray-600 text-sm">
                          {lesson.description}
                        </p>
                      </div>
                    </div>
                    {lesson.locked ? (
                      <div className="bg-gray-100 rounded-lg p-4 text-center mb-3">
                        <p className="text-gray-600">
                          Complete previous lessons to unlock
                        </p>
                      </div>
                    ) : (
                      <button
                        onClick={() => handleStartLesson(lesson.id)}
                        className={`w-full ${
                          lesson.completed
                            ? "bg-[#ffcc33] text-[#2b2d33]"
                            : "bg-[#2b2d33] text-white"
                        } font-bold py-3 rounded-lg mb-3`}
                      >
                        {lesson.completed ? "REVIEW" : "START"} +{lesson.points}{" "}
                        RaiPoints
                      </button>
                    )}
                  </div>
                ))}
            </motion.div>
          </motion.div>
        )}
      </AnimatePresence>

      <div className="shadow-[0_35px_60px_-15px_rgba(0,0,0,0.3)]">
        <BottomNavigation />
      </div>
    </div>
  );
}
