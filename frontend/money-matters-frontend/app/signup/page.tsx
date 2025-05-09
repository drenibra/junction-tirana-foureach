"use client";

import { motion } from "framer-motion";
import Image from "next/image";
import { useRouter } from "next/navigation";
import React, { useState } from "react";
import { useDispatch } from "react-redux";
import agent from "../api_calls/agent";
import { setUser } from "../store/slices/userSlice";

export default function AuthPage() {
  const router = useRouter();
  const dispatch = useDispatch();

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
        // ---- existing login flow ----
        const response = await agent.Auth.login({ email, password });
        console.log(response);

        dispatch(setUser({ id: response.id, email: response.username }));
      } else {
        // ---- sign-up flow ----
        await agent.Auth.register({ name, email, password, confirmPassword });
        // immediately log in, no status check needed
        const response = await agent.Auth.login({ email, password });
        dispatch(setUser(response));
      }

      router.push("/dashboard");
    } catch (err: any) {
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

  const handleContinueAsGuest = async () => {
    setError("");
    try {
      await agent.Auth.login({
        email: "rrezart.hetemi@outlook.com",
        password: "Pa$$w0rd",
      });
      router.push("/dashboard");
    } catch (err: any) {
      setError("Guest login failed. Please try again later.");
    }
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
              src="images/greeting-raistar.gif"
              alt="RaiBuddy"
              width={350}
              height={350}
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

            <div className="mt-4">
              <button
                onClick={handleContinueAsGuest}
                className="btn-secondary w-full"
              >
                Continue as Guest
              </button>
            </div>
          </div>
        </motion.div>
      </div>
    </main>
  );
}
