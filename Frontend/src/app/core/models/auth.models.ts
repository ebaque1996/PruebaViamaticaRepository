export interface LoginRequest {
  emailOrUsername: string;
  passwordHash: string; // O simplemente 'password' según lo que reciba tu API
}

export interface User {
  userId: number;
  username: string;
  email: string;
  fullName: string;
  roleName: 'Admin' | 'Gestor' | 'Cajero';
  isApproved?: boolean;
}

export interface MenuItem {
  id: number;
  title: string;
  route: string;
  icon?: string;
  children?: MenuItem[];
}

export interface LoginResponse {
  token: string;
  user: User;
  menu: MenuItem[];
}

export interface RecoverPasswordRequest {
  emailOrIdentification: string;
}
