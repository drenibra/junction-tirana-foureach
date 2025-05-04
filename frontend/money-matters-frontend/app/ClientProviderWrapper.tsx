"use client";

import { configureStore } from "@reduxjs/toolkit";
import { useMemo } from "react";
import { Provider } from "react-redux";
import userReducer, { UserState } from "./store/slices/userSlice";

const defaultUserState: UserState = {
  id: null,
  role: null,
  email: null,
  isLoggedIn: false,
};

function makeStore(initialUser: User | null) {
  // build a proper UserState
  const preloadedUser: UserState = initialUser
    ? {
        id: initialUser.id,
        role: initialUser.role ?? null,
        email: initialUser.email,
        isLoggedIn: true,
      }
    : defaultUserState;

  return configureStore({
    reducer: { user: userReducer /* …other slices… */ },
    preloadedState: {
      user: preloadedUser,
    },
  });
}

export default function ClientProviderWrapper({
  children,
  initialUser,
}: {
  children: React.ReactNode;
  initialUser: User | null;
}) {
  const store = useMemo(() => makeStore(initialUser), [initialUser]);
  return <Provider store={store}>{children}</Provider>;
}
