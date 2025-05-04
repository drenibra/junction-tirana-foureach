import { createSlice, PayloadAction } from "@reduxjs/toolkit";

// Export the UserState interface so it can be imported elsewhere
export interface UserState {
  id: string | null;
  role: number | null;
  email: string | null;
  isLoggedIn: boolean;
}

const initialState: UserState = {
  id: null,
  role: null,
  email: null,
  isLoggedIn: false,
};

const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    setUser: (
      state,
      action: PayloadAction<{ id: string; role?: number; email: string }>
    ) => {
      state.id = action.payload.id;
      state.role = action.payload.role ?? null;
      state.email = action.payload.email;
      state.isLoggedIn = true;
    },
    logoutUser: (state) => {
      state.id = null;
      state.role = null;
      state.email = null;
      state.isLoggedIn = false;
    },
    initializeUser: (state, action: PayloadAction<UserState>) => {
      state.id = action.payload.id;
      state.role = action.payload.role;
      state.email = action.payload.email;
      state.isLoggedIn = action.payload.isLoggedIn;
    },
  },
});

export const { setUser, logoutUser, initializeUser } = userSlice.actions;
export default userSlice.reducer;
