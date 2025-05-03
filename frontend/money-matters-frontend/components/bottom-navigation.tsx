"use client"

import { usePathname, useRouter } from "next/navigation"
import { BookOpen, Trophy, Target, MessageCircle, User } from "lucide-react"

export default function BottomNavigation() {
  const pathname = usePathname()
  const router = useRouter()

  const navItems = [
    {
      name: "Lessons",
      icon: BookOpen,
      path: "/dashboard",
    },
    {
      name: "Challenges",
      icon: Trophy,
      path: "/challenges",
    },
    {
      name: "Goals",
      icon: Target,
      path: "/goals",
    },
    {
      name: "RaiChat",
      icon: MessageCircle,
      path: "/chat",
    },
    {
      name: "Profile",
      icon: User,
      path: "/profile",
    },
  ]

  return (
    <div className="fixed bottom-0 left-0 right-0 bg-white border-t border-gray-200 shadow-lg z-50">
      <div className="flex justify-around items-center h-16">
        {navItems.map((item) => {
          const isActive = pathname === item.path
          return (
            <button
              key={item.name}
              className={`flex flex-col items-center justify-center w-full h-full ${
                isActive ? "text-[#ffcc33]" : "text-gray-500"
              }`}
              onClick={() => router.push(item.path)}
            >
              <item.icon size={20} />
              <span className="text-xs mt-1">{item.name}</span>
            </button>
          )
        })}
      </div>
    </div>
  )
}
