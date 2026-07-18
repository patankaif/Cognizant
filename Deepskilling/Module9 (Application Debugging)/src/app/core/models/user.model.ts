export interface User {
  username: string;
  role: 'Admin' | 'Reader';
}

export interface LoginCredentials {
  username: string;
  password: string;
}
