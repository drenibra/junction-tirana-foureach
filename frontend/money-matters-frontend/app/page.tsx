"use client";

import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import agent from "./api_calls/agent";

export default function Home() {
  const [user, setUser] = useState(null);
  const router = useRouter();
  useEffect(() => {
    const fetchUser = async () => {
      try {
        const response = agent.Auth.current();
        if (!response) {
          throw new Error("Network response was not ok");
        }
        console.log(response);
      } catch (error) {
        console.error("Error fetching user:", error);
      }
    };
  });

  if (!user) {
    router.push("/questionnaire");
  } else {
    router.push("/dashboard");
  }
}
