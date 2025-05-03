"use client"

import { useState } from "react"
import { motion, AnimatePresence } from "framer-motion"
import { ArrowLeft, Plus, X, Check } from "lucide-react"

type Expense = {
  id: number
  name: string
  amount: number
  category: "want" | "need"
  date: Date
}

type ExpenseTrackerProps = {
  onClose: () => void
}

export default function ExpenseTracker({ onClose }: ExpenseTrackerProps) {
  const [expenses, setExpenses] = useState<Expense[]>([
    {
      id: 1,
      name: "Lunch",
      amount: 500,
      category: "need",
      date: new Date(2023, 4, 1),
    },
    {
      id: 2,
      name: "Movie Ticket",
      amount: 800,
      category: "want",
      date: new Date(2023, 4, 2),
    },
    {
      id: 3,
      name: "Bus Fare",
      amount: 200,
      category: "need",
      date: new Date(2023, 4, 3),
    },
  ])

  const [showAddExpense, setShowAddExpense] = useState(false)
  const [newExpense, setNewExpense] = useState<{
    name: string
    amount: string
    category: "want" | "need"
  }>({
    name: "",
    amount: "",
    category: "need",
  })

  const totalExpenses = expenses.reduce((sum, expense) => sum + expense.amount, 0)
  const needsTotal = expenses
    .filter((expense) => expense.category === "need")
    .reduce((sum, expense) => sum + expense.amount, 0)
  const wantsTotal = expenses
    .filter((expense) => expense.category === "want")
    .reduce((sum, expense) => sum + expense.amount, 0)

  const handleAddExpense = () => {
    if (!newExpense.name || !newExpense.amount) return

    const amount = Number.parseFloat(newExpense.amount)
    if (isNaN(amount)) return

    const expense: Expense = {
      id: Date.now(),
      name: newExpense.name,
      amount,
      category: newExpense.category,
      date: new Date(),
    }

    setExpenses([expense, ...expenses])
    setNewExpense({
      name: "",
      amount: "",
      category: "need",
    })
    setShowAddExpense(false)
  }

  const formatDate = (date: Date) => {
    return date.toLocaleDateString("en-US", {
      month: "short",
      day: "numeric",
    })
  }

  return (
    <div className="min-h-screen bg-gray-50 relative">
      {/* Custom Header */}
      <div className="bg-[#2b2d33] text-white p-4 sticky top-0 z-20">
        <div className="flex items-center">
          <button onClick={onClose} className="p-2 rounded-full hover:bg-gray-700 transition-colors">
            <ArrowLeft size={20} />
          </button>
          <h1 className="text-lg font-bold ml-2">Expense Tracker</h1>
        </div>
      </div>

      {/* Main Content */}
      <div className="p-4">
        {/* Summary */}
        <div className="bg-white rounded-xl border-2 border-gray-200 p-4 mb-6">
          <h2 className="text-lg font-bold mb-3">Summary</h2>
          <div className="flex justify-between mb-2">
            <span className="text-gray-600">Total Expenses:</span>
            <span className="font-bold">{totalExpenses.toLocaleString()} LEK</span>
          </div>
          <div className="flex justify-between mb-2">
            <div className="flex items-center">
              <div className="w-3 h-3 rounded-full bg-green-500 mr-2"></div>
              <span className="text-gray-600">Needs:</span>
            </div>
            <span>{needsTotal.toLocaleString()} LEK</span>
          </div>
          <div className="flex justify-between">
            <div className="flex items-center">
              <div className="w-3 h-3 rounded-full bg-blue-500 mr-2"></div>
              <span className="text-gray-600">Wants:</span>
            </div>
            <span>{wantsTotal.toLocaleString()} LEK</span>
          </div>

          {/* Simple Chart */}
          <div className="mt-4">
            <div className="w-full h-4 bg-gray-200 rounded-full overflow-hidden flex">
              {totalExpenses > 0 && (
                <>
                  <div
                    className="h-full bg-green-500"
                    style={{ width: `${(needsTotal / totalExpenses) * 100}%` }}
                  ></div>
                  <div className="h-full bg-blue-500" style={{ width: `${(wantsTotal / totalExpenses) * 100}%` }}></div>
                </>
              )}
            </div>
            <div className="flex justify-between mt-1 text-xs text-gray-500">
              <span>Needs: {totalExpenses > 0 ? Math.round((needsTotal / totalExpenses) * 100) : 0}%</span>
              <span>Wants: {totalExpenses > 0 ? Math.round((wantsTotal / totalExpenses) * 100) : 0}%</span>
            </div>
          </div>
        </div>

        {/* Expenses List */}
        <h2 className="text-lg font-bold mb-3">Recent Expenses</h2>
        <div className="space-y-3">
          {expenses.length === 0 ? (
            <div className="text-center py-8 text-gray-500">
              <p>No expenses yet. Add your first expense!</p>
            </div>
          ) : (
            expenses.map((expense) => (
              <motion.div
                key={expense.id}
                initial={{ opacity: 0, y: 10 }}
                animate={{ opacity: 1, y: 0 }}
                className="bg-white rounded-lg border border-gray-200 p-3 flex items-center"
              >
                <div
                  className={`w-10 h-10 rounded-full flex items-center justify-center mr-3 ${
                    expense.category === "need" ? "bg-green-100 text-green-600" : "bg-blue-100 text-blue-600"
                  }`}
                >
                  {expense.category === "need" ? "N" : "W"}
                </div>
                <div className="flex-1">
                  <div className="flex justify-between">
                    <h3 className="font-medium">{expense.name}</h3>
                    <span className="font-bold">{expense.amount.toLocaleString()} LEK</span>
                  </div>
                  <div className="flex justify-between text-xs text-gray-500 mt-1">
                    <span>{expense.category === "need" ? "Need" : "Want"}</span>
                    <span>{formatDate(expense.date)}</span>
                  </div>
                </div>
              </motion.div>
            ))
          )}
        </div>
      </div>

      {/* Add Expense Button */}
      {!showAddExpense && (
        <button
          onClick={() => setShowAddExpense(true)}
          className="fixed bottom-6 right-6 w-14 h-14 rounded-full bg-[#ffcc33] text-[#2b2d33] flex items-center justify-center shadow-lg"
        >
          <Plus size={24} />
        </button>
      )}

      {/* Add Expense Modal */}
      <AnimatePresence>
        {showAddExpense && (
          <motion.div
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            exit={{ opacity: 0 }}
            className="fixed inset-0 bg-black bg-opacity-50 flex items-end z-50"
          >
            <motion.div
              initial={{ y: "100%" }}
              animate={{ y: 0 }}
              exit={{ y: "100%" }}
              transition={{ type: "spring", damping: 25 }}
              className="bg-white w-full rounded-t-xl p-5"
            >
              <div className="flex justify-between items-center mb-4">
                <h2 className="text-lg font-bold">Add Expense</h2>
                <button onClick={() => setShowAddExpense(false)} className="p-2 rounded-full hover:bg-gray-100">
                  <X size={20} />
                </button>
              </div>

              <div className="space-y-4">
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">Expense Name</label>
                  <input
                    type="text"
                    placeholder="e.g., Lunch, Movie Ticket"
                    className="w-full p-3 border border-gray-300 rounded-lg"
                    value={newExpense.name}
                    onChange={(e) => setNewExpense({ ...newExpense, name: e.target.value })}
                  />
                </div>

                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">Amount (LEK)</label>
                  <div className="relative">
                    <input
                      type="number"
                      placeholder="e.g., 500"
                      className="w-full p-3 border border-gray-300 rounded-lg pr-12"
                      value={newExpense.amount}
                      onChange={(e) => setNewExpense({ ...newExpense, amount: e.target.value })}
                    />
                    <span className="absolute right-3 top-1/2 -translate-y-1/2 text-gray-500">LEK</span>
                  </div>
                </div>

                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">Category</label>
                  <div className="flex space-x-3">
                    <button
                      className={`flex-1 py-3 px-4 rounded-lg border-2 flex items-center justify-center ${
                        newExpense.category === "need" ? "border-green-500 bg-green-50" : "border-gray-300"
                      }`}
                      onClick={() => setNewExpense({ ...newExpense, category: "need" })}
                    >
                      <div className="w-4 h-4 rounded-full border-2 border-green-500 mr-2 flex items-center justify-center">
                        {newExpense.category === "need" && <div className="w-2 h-2 rounded-full bg-green-500"></div>}
                      </div>
                      <span>Need</span>
                    </button>
                    <button
                      className={`flex-1 py-3 px-4 rounded-lg border-2 flex items-center justify-center ${
                        newExpense.category === "want" ? "border-blue-500 bg-blue-50" : "border-gray-300"
                      }`}
                      onClick={() => setNewExpense({ ...newExpense, category: "want" })}
                    >
                      <div className="w-4 h-4 rounded-full border-2 border-blue-500 mr-2 flex items-center justify-center">
                        {newExpense.category === "want" && <div className="w-2 h-2 rounded-full bg-blue-500"></div>}
                      </div>
                      <span>Want</span>
                    </button>
                  </div>
                </div>

                <button
                  onClick={handleAddExpense}
                  disabled={!newExpense.name || !newExpense.amount}
                  className={`w-full py-3 rounded-lg font-bold flex items-center justify-center ${
                    !newExpense.name || !newExpense.amount ? "bg-gray-300 text-gray-500" : "bg-[#ffcc33] text-[#2b2d33]"
                  }`}
                >
                  <Check size={20} className="mr-2" />
                  Add Expense
                </button>
              </div>
            </motion.div>
          </motion.div>
        )}
      </AnimatePresence>
    </div>
  )
}
