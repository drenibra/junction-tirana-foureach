"use client"

import { useState, useEffect } from "react"
import { useRouter } from "next/navigation"
import { motion, AnimatePresence } from "framer-motion"
import { X } from "lucide-react"
import Image from "next/image"

// Define types for our quiz
type Option = {
  id: string
  text: string
  isCorrect: boolean
}

type Question = {
  id: number
  text: string
  options: Option[]
  explanation?: string
}

type Lesson = {
  id: number
  title: string
  questions: Question[]
}

export default function LessonPage({ params }: { params: { id: string } }) {
  const router = useRouter()
  const lessonId = Number.parseInt(params.id)

  const [showExitConfirmation, setShowExitConfirmation] = useState(false)
  const [currentQuestionIndex, setCurrentQuestionIndex] = useState(0)
  const [selectedOption, setSelectedOption] = useState<string | null>(null)
  const [isAnswerChecked, setIsAnswerChecked] = useState(false)
  const [isAnswerCorrect, setIsAnswerCorrect] = useState(false)
  const [progress, setProgress] = useState(0)
  const [lesson, setLesson] = useState<Lesson | null>(null)
  const [isLoading, setIsLoading] = useState(true)
  const [raiPoints] = useState(120)

  // Mock data - in a real app, this would come from an API
  useEffect(() => {
    // Simulate loading data
    setIsLoading(true)
    setTimeout(() => {
      const mockLesson: Lesson = {
        id: lessonId,
        title: "Budgeting 101",
        questions: [
          {
            id: 1,
            text: "What is a budget?",
            options: [
              { id: "a", text: "A list of things you want to buy", isCorrect: false },
              { id: "b", text: "A plan for how you will spend your money", isCorrect: true },
              { id: "c", text: "A type of bank account", isCorrect: false },
              { id: "d", text: "A way to borrow money", isCorrect: false },
            ],
            explanation: "A budget is a plan that helps you manage your money by tracking income and expenses.",
          },
          {
            id: 2,
            text: "Why is it important to save money?",
            options: [
              { id: "a", text: "So you can spend it all later", isCorrect: false },
              { id: "b", text: "It's not important to save money", isCorrect: false },
              { id: "c", text: "To have money for emergencies and future goals", isCorrect: true },
              { id: "d", text: "To make your parents happy", isCorrect: false },
            ],
            explanation: "Saving money helps you prepare for unexpected expenses and achieve your financial goals.",
          },
          {
            id: 3,
            text: "What is the difference between needs and wants?",
            options: [
              {
                id: "a",
                text: "Needs are things you must have, wants are things you would like to have",
                isCorrect: true,
              },
              { id: "b", text: "Needs are expensive, wants are cheap", isCorrect: false },
              { id: "c", text: "Needs are things your parents buy, wants are things you buy", isCorrect: false },
              { id: "d", text: "There is no difference", isCorrect: false },
            ],
            explanation:
              "Needs are essential for survival (like food and shelter), while wants are things that make life more enjoyable but aren't necessary.",
          },
          {
            id: 4,
            text: "What should you do first when creating a budget?",
            options: [
              { id: "a", text: "Start spending less", isCorrect: false },
              { id: "b", text: "Track your income and expenses", isCorrect: true },
              { id: "c", text: "Open a new bank account", isCorrect: false },
              { id: "d", text: "Ask for more pocket money", isCorrect: false },
            ],
            explanation: "Before making a budget, you need to know how much money you have coming in and going out.",
          },
          {
            id: 5,
            text: "What is the 50/30/20 rule in budgeting?",
            options: [
              { id: "a", text: "Spend 50% on food, 30% on fun, 20% on clothes", isCorrect: false },
              { id: "b", text: "Save 50%, spend 30%, give away 20%", isCorrect: false },
              { id: "c", text: "Spend 50% on needs, 30% on wants, save 20%", isCorrect: true },
              { id: "d", text: "Spend 50% now, save 30% for next month, save 20% for the future", isCorrect: false },
            ],
            explanation:
              "The 50/30/20 rule suggests spending 50% of your income on needs, 30% on wants, and saving 20%.",
          },
        ],
      }
      setLesson(mockLesson)
      setIsLoading(false)
    }, 1000)
  }, [lessonId])

  // Update progress when current question changes
  useEffect(() => {
    if (lesson) {
      setProgress((currentQuestionIndex / lesson.questions.length) * 100)
    }
  }, [currentQuestionIndex, lesson])

  const handleExit = () => {
    setShowExitConfirmation(true)
  }

  const confirmExit = () => {
    router.push("/dashboard")
  }

  const cancelExit = () => {
    setShowExitConfirmation(false)
  }

  const handleOptionSelect = (optionId: string) => {
    if (!isAnswerChecked) {
      setSelectedOption(optionId)
    }
  }

  const handleCheckAnswer = () => {
    if (!selectedOption || isAnswerChecked) return

    const currentQuestion = lesson?.questions[currentQuestionIndex]
    const selectedOptionObj = currentQuestion?.options.find((opt) => opt.id === selectedOption)

    setIsAnswerChecked(true)
    setIsAnswerCorrect(selectedOptionObj?.isCorrect || false)
  }

  const handleContinue = () => {
    if (lesson && currentQuestionIndex < lesson.questions.length - 1) {
      setCurrentQuestionIndex((prev) => prev + 1)
      setSelectedOption(null)
      setIsAnswerChecked(false)
      setIsAnswerCorrect(false)
    } else {
      // Lesson completed
      router.push("/dashboard")
    }
  }

  const currentQuestion = lesson?.questions[currentQuestionIndex]

  return (
    <div className="min-h-screen bg-gray-50 flex flex-col">
      {/* Custom Top Bar */}
      <div className="bg-[#2b2d33] text-white p-4 sticky top-0 z-20">
        <div className="flex items-center justify-between">
          <button onClick={handleExit} className="p-2 rounded-full hover:bg-gray-700 transition-colors">
            <X size={24} />
          </button>
          <div className="flex-1 mx-4">
            <div className="w-full bg-gray-700 rounded-full h-2.5">
              <motion.div
                className="bg-[#ffcc33] h-2.5 rounded-full"
                initial={{ width: 0 }}
                animate={{ width: `${progress}%` }}
                transition={{ duration: 0.5 }}
              ></motion.div>
            </div>
          </div>
          <div className="flex items-center space-x-1 bg-[#3a3c42] rounded-full px-3 py-1">
            <Image src="/images/star-coin.png" alt="RaiPoints" width={18} height={18} />
            <span className="font-medium">{raiPoints}</span>
          </div>
        </div>
      </div>

      {/* Main Content */}
      <div className="flex-1 p-4 flex flex-col">
        {isLoading ? (
          <div className="flex-1 flex items-center justify-center">
            <div className="animate-bounce">
              <Image src="/images/raibuddy.png" alt="Loading" width={80} height={80} />
            </div>
          </div>
        ) : (
          <>
            {/* Question */}
            <motion.div
              key={`question-${currentQuestionIndex}`}
              initial={{ opacity: 0, y: 20 }}
              animate={{ opacity: 1, y: 0 }}
              transition={{ duration: 0.5 }}
              className="mb-8 mt-4"
            >
              <h2 className="text-2xl font-bold mb-2">Question {currentQuestionIndex + 1}</h2>
              <p className="text-lg">{currentQuestion?.text}</p>
            </motion.div>

            {/* Options */}
            <div className="space-y-3 mb-8">
              <AnimatePresence mode="wait">
                {currentQuestion?.options.map((option) => (
                  <motion.div
                    key={option.id}
                    initial={{ opacity: 0, x: -20 }}
                    animate={{ opacity: 1, x: 0 }}
                    exit={{ opacity: 0, x: 20 }}
                    transition={{ duration: 0.3 }}
                    className={`border-2 rounded-lg p-4 cursor-pointer transition-all ${
                      selectedOption === option.id
                        ? isAnswerChecked
                          ? option.isCorrect
                            ? "border-green-500 bg-green-50"
                            : "border-red-500 bg-red-50"
                          : "border-[#ffcc33] bg-amber-50"
                        : isAnswerChecked && option.isCorrect
                          ? "border-green-500 bg-green-50"
                          : "border-gray-200 hover:border-gray-300"
                    }`}
                    onClick={() => handleOptionSelect(option.id)}
                  >
                    <div className="flex items-center">
                      <div
                        className={`w-6 h-6 rounded-full flex items-center justify-center mr-3 border-2 ${
                          selectedOption === option.id
                            ? isAnswerChecked
                              ? option.isCorrect
                                ? "border-green-500 bg-green-500 text-white"
                                : "border-red-500 bg-red-500 text-white"
                              : "border-[#ffcc33] bg-[#ffcc33] text-[#2b2d33]"
                            : isAnswerChecked && option.isCorrect
                              ? "border-green-500 bg-green-500 text-white"
                              : "border-gray-300"
                        }`}
                      >
                        {selectedOption === option.id && (
                          <motion.span
                            initial={{ scale: 0 }}
                            animate={{ scale: 1 }}
                            transition={{ type: "spring", stiffness: 500, damping: 30 }}
                          >
                            {isAnswerChecked ? (option.isCorrect ? "✓" : "✗") : ""}
                          </motion.span>
                        )}
                        {isAnswerChecked && option.isCorrect && selectedOption !== option.id && (
                          <motion.span
                            initial={{ scale: 0 }}
                            animate={{ scale: 1 }}
                            transition={{ type: "spring", stiffness: 500, damping: 30 }}
                          >
                            ✓
                          </motion.span>
                        )}
                      </div>
                      <span className={`${isAnswerChecked && option.isCorrect ? "font-bold" : ""}`}>{option.text}</span>
                    </div>
                  </motion.div>
                ))}
              </AnimatePresence>
            </div>

            {/* Feedback Area */}
            <AnimatePresence>
              {isAnswerChecked && (
                <motion.div
                  initial={{ opacity: 0, height: 0 }}
                  animate={{ opacity: 1, height: "auto" }}
                  exit={{ opacity: 0, height: 0 }}
                  transition={{ duration: 0.3 }}
                  className={`p-4 rounded-lg mb-4 ${
                    isAnswerCorrect
                      ? "bg-green-100 border-l-4 border-green-500"
                      : "bg-red-100 border-l-4 border-red-500"
                  }`}
                >
                  <p className="font-medium">
                    {isAnswerCorrect ? "Correct! Great job!" : "Not quite right. Let's learn from this!"}
                  </p>
                  <p className="mt-1 text-sm">{currentQuestion?.explanation}</p>
                </motion.div>
              )}
            </AnimatePresence>

            {/* Button */}
            <div className="mt-auto">
              <button
                onClick={isAnswerChecked ? handleContinue : handleCheckAnswer}
                disabled={!selectedOption && !isAnswerChecked}
                className={`w-full py-4 rounded-lg font-bold transition-all ${
                  !selectedOption && !isAnswerChecked
                    ? "bg-gray-300 text-gray-500 cursor-not-allowed"
                    : isAnswerChecked
                      ? isAnswerCorrect
                        ? "bg-[#ffcc33] text-[#2b2d33]"
                        : "bg-red-500 text-white"
                      : "bg-[#2b2d33] text-white"
                }`}
              >
                {!selectedOption ? "Continue" : isAnswerChecked ? (isAnswerCorrect ? "Continue" : "Got it") : "Check"}
              </button>
            </div>
          </>
        )}
      </div>

      {/* Exit Confirmation Modal */}
      <AnimatePresence>
        {showExitConfirmation && (
          <motion.div
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            exit={{ opacity: 0 }}
            className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 p-4"
          >
            <motion.div
              initial={{ scale: 0.9, y: 20 }}
              animate={{ scale: 1, y: 0 }}
              exit={{ scale: 0.9, y: 20 }}
              className="bg-white rounded-xl border-2 border-gray-200 p-5 w-full max-w-sm"
            >
              <h3 className="text-xl font-bold mb-4">Exit Lesson?</h3>
              <p className="mb-6">Are you sure you want to exit this lesson? Your progress will not be saved.</p>
              <div className="flex space-x-3">
                <button onClick={cancelExit} className="flex-1 py-3 rounded-lg border-2 border-gray-300 font-bold">
                  Cancel
                </button>
                <button onClick={confirmExit} className="flex-1 py-3 rounded-lg bg-[#2b2d33] text-white font-bold">
                  Exit
                </button>
              </div>
            </motion.div>
          </motion.div>
        )}
      </AnimatePresence>
    </div>
  )
}
