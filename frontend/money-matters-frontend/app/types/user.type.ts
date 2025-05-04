interface User {
  id: string;
  email: string;
  role: number | null;
  firstName: string;
  lastName: string;
  phoneNumber?: string;
}
interface UserState extends User {
  isLoggedIn: boolean;
}

interface LoginResponse {
  user: {
    id: string;
    email: string;
    role: number;
  };
}

interface ValidationError {
  errors: { msg: string; path: string }[];
}

interface GeneralError {
  message: string;
}

type UserFormState = Omit<User, "id" | "role">;
