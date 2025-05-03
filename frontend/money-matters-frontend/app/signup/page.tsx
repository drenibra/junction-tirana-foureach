"use client"

import type React from "react"

import { useState } from "react"
import Image from "next/image"
import { useRouter } from "next/navigation"
import { motion } from "framer-motion"

export default function SignUp() {
  const router = useRouter()
  const [email, setEmail] = useState("")
  const [password, setPassword] = useState("")

  const handleSignUp = (e: React.FormEvent) => {
    e.preventDefault()
    // In a real app, you would handle authentication here
    router.push("/dashboard")
  }

  const handleContinueAsGuest = () => {
    router.push("/dashboard")
  }

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
            <Image src="/images/raibuddy.png" alt="RaiBuddy" width={100} height={100} />
          </div>

          <h1 className="text-2xl font-bold text-center mb-6">Create Your Account</h1>

          <form onSubmit={handleSignUp} className="space-y-4 mb-6">
            <div>
              <label htmlFor="email" className="block mb-2 font-medium">
                Email
              </label>
              <input
                type="email"
                id="email"
                className="input-field"
                placeholder="your.email@example.com"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                required
              />
            </div>
            <div>
              <label htmlFor="password" className="block mb-2 font-medium">
                Password
              </label>
              <input
                type="password"
                id="password"
                className="input-field"
                placeholder="Create a password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                required
              />
            </div>
            <button type="submit" className="btn-primary w-full">
              Sign Up
            </button>
          </form>

          <div className="text-center">
            <p className="text-gray-500 mb-4">or</p>
            <button onClick={handleContinueAsGuest} className="btn-secondary w-full">
              Continue as Guest
            </button>
          </div>
        </motion.div>
      </div>
    </main>
  )
}
