"use client";

import { motion } from "framer-motion";
import Image from "next/image";
import { useRouter } from "next/navigation";
import React, { useState } from "react";
import agent from "../api_calls/agent";

export default function AuthPage() {
  const router = useRouter();

  // form mode & fields
  const [isLogin, setIsLogin] = useState(false);
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [error, setError] = useState("");

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setError("");

    try {
      if (isLogin) {
        // Log in flow
        await agent.Auth.login({ email, password });
        router.push("/dashboard");
      } else {
        // Sign up flow
        const registeredUser = await agent.Auth.register({
          name,
          email,
          password,
          confirmPassword,
        });

        if (registeredUser.status === 201) {
          // Optionally, you can log in the user immediately after registration
          await agent.Auth.login({ email, password });
        }
      }
      router.push("/dashboard");
    } catch (err: any) {
      // you can introspect err.response?.data for more details
      setError(
        isLogin
          ? "Login failed. Please check your credentials."
          : "Registration failed. Please check your inputs."
      );
    }
  };

  const toggleMode = () => {
    setError("");
    setIsLogin(!isLogin);
  };

  const handleContinueAsGuest = () => {
    router.push("/dashboard");
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
              width={100}
              height={100}
            />
          </div>

          <h1 className="text-2xl font-bold text-center mb-6">
            {isLogin ? "Welcome Back" : "Create Your Account"}
          </h1>

          {error && (
            <div className="mb-4 text-center text-red-600">{error}</div>
          )}

          <form onSubmit={handleSubmit} className="space-y-4 mb-6">
            {!isLogin && (
              <div>
                <label htmlFor="name" className="block mb-2 font-medium">
                  Name
                </label>
                <input
                  type="text"
                  id="name"
                  className="input-field"
                  placeholder="Your full name"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                  required
                />
              </div>
            )}

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
                placeholder={
                  isLogin ? "Enter your password" : "Create a password"
                }
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                required
              />
            </div>

            {!isLogin && (
              <div>
                <label
                  htmlFor="confirmPassword"
                  className="block mb-2 font-medium"
                >
                  Confirm Password
                </label>
                <input
                  type="password"
                  id="confirmPassword"
                  className="input-field"
                  placeholder="Re-enter your password"
                  value={confirmPassword}
                  onChange={(e) => setConfirmPassword(e.target.value)}
                  required
                />
              </div>
            )}

            <button type="submit" className="btn-primary w-full text-white">
              {isLogin ? "Log In" : "Sign Up"}
            </button>
          </form>

          <div className="text-center space-y-2">
            <button
              onClick={toggleMode}
              className="text-sm text-blue-600 hover:underline"
            >
              {isLogin
                ? "Don't have an account? Sign up instead"
                : "Already have an account? Log in instead"}
            </button>

            {/* <div className="mt-2">
              <p className="text-gray-500 mb-2">or</p>
              <button
                onClick={handleContinueAsGuest}
                className="btn-secondary w-full"
              >
                Continue as Guest
              </button>
            </div> */}
          </div>
        </motion.div>
      </div>
    </main>
  );
}
