import { jwtVerify } from "jose";
import type { Metadata } from "next";
import { Inter } from "next/font/google";
import { cookies } from "next/headers";
import type React from "react";
import ClientProviderWrapper from "./ClientProviderWrapper";
import "./globals.css";

const inter = Inter({ subsets: ["latin"] });

export const metadata: Metadata = {
  title: "RaiBuddy - Financial Literacy for Teens",
  description: "Learn financial skills in a fun, gamified way with RaiBuddy!",
  generator: "v0.dev",
};

export default async function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  const token = (await cookies()).get("token")?.value;
  let user = null;
  if (token) {
    try {
      const { payload } = await jwtVerify(
        token,
        new TextEncoder().encode(process.env.APP_SECRET!)
      );
      user = {
        id: payload.nameid as string,
        email: payload.email as string,
        firstName: (payload.given_name as string) || "",
        lastName: (payload.family_name as string) || "",
        role: 0,
      };
    } catch {}
  }

  return (
    <html lang="en">
      <body className={`${inter.className} bg-white text-gray-900`}>
        <ClientProviderWrapper initialUser={user}>
          {children}
        </ClientProviderWrapper>
      </body>
    </html>
  );
}
