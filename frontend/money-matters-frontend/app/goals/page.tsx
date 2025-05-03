"use client"

import { useState, useEffect } from "react"
import Image from "next/image"
import { motion, AnimatePresence } from "framer-motion"
import { Plus, PieChart, X, Check, ArrowUp } from "lucide-react"
import BottomNavigation from "@/components/bottom-navigation"
import ExpenseTracker from "@/components/expense-tracker"

type Goal = {
  id: number
  name: string
  amount: number
  saved: number
  image: string
}

export default function Goals() {
  const [goals, setGoals] = useState<Goal[]>([
    {
      id: 1,
      name: "New Phone Case",
      amount: 2000,
      saved: 800,
      image: "/colorful-phone-case-display.png",
    },
    {
      id: 2,
      name: "Video Game",
      amount: 6000,
      saved: 1500,
      image: "/placeholder.svg?key=g9s4r",
    },
  ])

  const [showExpenseTracker, setShowExpenseTracker] = useState(false)
  const [selectedGoal, setSelectedGoal] = useState<Goal | null>(null)
  const [showAddProgress, setShowAddProgress] = useState(false)
  const [progressAmount, setProgressAmount] = useState("")

  useEffect(() => {
    setShowExpenseTracker(true);
  }, []);

  const handleAddProgress = () => {
    if (!selectedGoal || !progressAmount) return

    const amount = Number.parseFloat(progressAmount)
    if (isNaN(amount) || amount <= 0) return

    // Update the selected goal's saved amount
    const updatedGoals = goals.map((goal) => {
      if (goal.id === selectedGoal.id) {
        // Make sure we don't exceed the goal amount
        const newSaved = Math.min(goal.saved + amount, goal.amount)
        return { ...goal, saved: newSaved }
      }
      return goal
    })

    setGoals(updatedGoals)

    // Update the selected goal reference
    const updatedGoal = updatedGoals.find((g) => g.id === selectedGoal.id)
    if (updatedGoal) {
      setSelectedGoal(updatedGoal)
    }

    setProgressAmount("")
    setShowAddProgress(false)
  }

  const handleGoalClick = (goal: Goal) => {
    setSelectedGoal(goal)
  }

  const closeGoalDetail = () => {
    setSelectedGoal(null)
  }

  return (
    <div className="min-h-screen bg-gray-50 pb-20">
      {!showExpenseTracker ? (
        <>
          {/* Top Bar */}
          <div className="bg-[#2b2d33] text-white p-4 sticky top-0 z-20">
            <h1 className="text-lg font-bold text-center">My Savings Goals</h1>
          </div>

          {/* Main Content */}
          <div className="p-4">
            <div className="mb-6">
              <h2 className="text-xl font-bold mb-2">Your Goals</h2>
              <p className="text-gray-600">Track your progress towards your savings goals</p>
            </div>

            <div className="space-y-4">
              {goals.map((goal) => (
                <motion.div
                  key={goal.id}
                  initial={{ opacity: 0, y: 20 }}
                  animate={{ opacity: 1, y: 0 }}
                  transition={{ duration: 0.3, delay: goal.id * 0.1 }}
                  className="bg-white rounded-xl border-2 border-gray-200 p-4 cursor-pointer hover:border-[#ffcc33] transition-colors"
                  onClick={() => handleGoalClick(goal)}
                >
                  <div className="flex">
                    <div className="w-20 h-20 rounded-lg bg-gray-100 overflow-hidden mr-4 flex-shrink-0">
                      <Image
                        src={goal.image || "/placeholder.svg"}
                        alt={goal.name}
                        width={80}
                        height={80}
                        className="object-cover"
                      />
                    </div>
                    <div className="flex-1">
                      <h3 className="font-bold text-lg">{goal.name}</h3>
                      <div className="flex justify-between text-sm mb-1 mt-2">
                        <span>Progress</span>
                        <span>
                          {goal.saved.toLocaleString()} / {goal.amount.toLocaleString()} LEK
                        </span>
                      </div>
                      <div className="w-full bg-gray-200 rounded-full h-2.5 mb-2">
                        <div
                          className="bg-[#ffcc33] h-2.5 rounded-full"
                          style={{ width: `${(goal.saved / goal.amount) * 100}%` }}
                        ></div>
                      </div>
                      <div className="text-right text-sm text-gray-600">
                        {Math.round((goal.saved / goal.amount) * 100)}% saved
                      </div>
                    </div>
                  </div>
                </motion.div>
              ))}

              <button className="w-full bg-white rounded-xl border-2 border-dashed border-gray-300 p-4 flex items-center justify-center text-gray-500 hover:bg-gray-50 transition-colors">
                <Plus size={20} className="mr-2" />
                <span>Add New Goal</span>
              </button>

              {/* Expense Tracker Button */}
              <motion.div
                initial={{ opacity: 0, y: 20 }}
                animate={{ opacity: 1, y: 0 }}
                transition={{ duration: 0.3, delay: 0.3 }}
                className="bg-white rounded-xl border-2 border-gray-200 p-4"
                onClick={() => setShowExpenseTracker(true)}
              >
                <div className="flex items-center cursor-pointer">
                  <div className="w-12 h-12 rounded-full bg-[#2b2d33] text-white flex items-center justify-center mr-4">
                    <PieChart size={24} />
                  </div>
                  <div className="flex-1">
                    <h3 className="font-bold text-lg">Expense Tracker</h3>
                    <p className="text-gray-600">Track your spending habits</p>
                  </div>
                </div>
              </motion.div>
            </div>
          </div>

          <BottomNavigation />

          {/* Goal Detail Modal */}
          <AnimatePresence>
            {selectedGoal && (
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
                  className="bg-white rounded-xl border-2 border-gray-200 p-5 w-full max-w-sm relative"
                >
                  <button
                    onClick={closeGoalDetail}
                    className="absolute top-4 right-4 p-2 rounded-full hover:bg-gray-100"
                  >
                    <X size={20} />
                  </button>

                  <div className="flex items-center mb-6">
                    <div className="w-16 h-16 rounded-lg bg-gray-100 overflow-hidden mr-4 flex-shrink-0">
                      <Image
                        src={selectedGoal.image || "/placeholder.svg"}
                        alt={selectedGoal.name}
                        width={64}
                        height={64}
                        className="object-cover"
                      />
                    </div>
                    <div>
                      <h3 className="text-xl font-bold">{selectedGoal.name}</h3>
                      <p className="text-gray-600 text-sm">Savings Goal</p>
                    </div>
                  </div>

                  <div className="mb-6">
                    <div className="flex justify-between text-sm mb-1">
                      <span>Progress</span>
                      <span>
                        {selectedGoal.saved.toLocaleString()} / {selectedGoal.amount.toLocaleString()} LEK
                      </span>
                    </div>
                    <div className="w-full bg-gray-200 rounded-full h-3 mb-2">
                      <div
                        className="bg-[#ffcc33] h-3 rounded-full"
                        style={{ width: `${(selectedGoal.saved / selectedGoal.amount) * 100}%` }}
                      ></div>
                    </div>
                    <div className="text-center text-sm font-medium">
                      {Math.round((selectedGoal.saved / selectedGoal.amount) * 100)}% saved
                    </div>
                  </div>

                  <div className="mb-6">
                    <div className="flex justify-between mb-2">
                      <span className="text-gray-600">Target Amount:</span>
                      <span className="font-bold">{selectedGoal.amount.toLocaleString()} LEK</span>
                    </div>
                    <div className="flex justify-between mb-2">
                      <span className="text-gray-600">Saved So Far:</span>
                      <span className="font-bold">{selectedGoal.saved.toLocaleString()} LEK</span>
                    </div>
                    <div className="flex justify-between">
                      <span className="text-gray-600">Still Needed:</span>
                      <span className="font-bold">
                        {(selectedGoal.amount - selectedGoal.saved).toLocaleString()} LEK
                      </span>
                    </div>
                  </div>

                  <button
                    onClick={() => setShowAddProgress(true)}
                    className="w-full bg-[#ffcc33] text-[#2b2d33] font-bold py-3 rounded-lg flex items-center justify-center"
                  >
                    <ArrowUp size={20} className="mr-2" />
                    Add Progress
                  </button>
                </motion.div>
              </motion.div>
            )}
          </AnimatePresence>

          {/* Add Progress Modal */}
          <AnimatePresence>
            {showAddProgress && selectedGoal && (
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
                  className="bg-white rounded-xl border-2 border-gray-200 p-5 w-full max-w-sm relative"
                >
                  <h3 className="text-xl font-bold mb-4">Add Progress to Goal</h3>
                  <p className="text-gray-600 mb-4">How much have you saved towards your {selectedGoal.name} goal?</p>

                  <div className="mb-6">
                    <label className="block text-sm font-medium text-gray-700 mb-1">Amount (LEK)</label>
                    <div className="relative">
                      <input
                        type="number"
                        placeholder="e.g., 500"
                        className="w-full p-3 border border-gray-300 rounded-lg pr-12"
                        value={progressAmount}
                        onChange={(e) => setProgressAmount(e.target.value)}
                      />
                      <span className="absolute right-3 top-1/2 -translate-y-1/2 text-gray-500">LEK</span>
                    </div>
                    <p className="text-xs text-gray-500 mt-1">
                      You still need {(selectedGoal.amount - selectedGoal.saved).toLocaleString()} LEK to reach your
                      goal
                    </p>
                  </div>

                  <div className="flex space-x-3">
                    <button
                      onClick={() => setShowAddProgress(false)}
                      className="flex-1 py-3 rounded-lg border-2 border-gray-300 font-bold"
                    >
                      Cancel
                    </button>
                    <button
                      onClick={handleAddProgress}
                      disabled={!progressAmount || Number.parseFloat(progressAmount) <= 0}
                      className={`flex-1 py-3 rounded-lg font-bold flex items-center justify-center ${
                        !progressAmount || Number.parseFloat(progressAmount) <= 0
                          ? "bg-gray-300 text-gray-500"
                          : "bg-[#ffcc33] text-[#2b2d33]"
                      }`}
                    >
                      <Check size={20} className="mr-2" />
                      Add Progress
                    </button>
                  </div>
                </motion.div>
              </motion.div>
            )}
          </AnimatePresence>
        </>
      ) : (
        <ExpenseTracker onClose={() => setShowExpenseTracker(false)} />
      )}
    </div>
  )
}
