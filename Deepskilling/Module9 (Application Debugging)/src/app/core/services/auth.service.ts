import { Injectable, computed, signal } from '@angular/core';
import { LoginCredentials, User } from '../models/user.model';

const STORAGE_KEY = 'module8_auth_token';

interface StoredSession {
  token: string;
  user: User;
}

const DEMO_USERS: Array<{ username: string; password: string; role: User['role'] }> = [
  { username: 'admin', password: 'Admin@123', role: 'Admin' },
  { username: 'reader', password: 'Reader@123', role: 'Reader' }
];

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly currentUserSignal = signal<User | null>(this.readStoredUser());
  private readonly tokenSignal = signal<string | null>(this.readStoredToken());

  readonly currentUser = this.currentUserSignal.asReadonly();
  readonly isLoggedIn = computed(() => this.currentUserSignal() !== null);
  readonly isAdmin = computed(() => this.currentUserSignal()?.role === 'Admin');

  login(credentials: LoginCredentials): boolean {
    const match = DEMO_USERS.find(
      u => u.username === credentials.username && u.password === credentials.password
    );

    if (!match) {
      return false;
    }

    const user: User = { username: match.username, role: match.role };
    const token = btoa(`${user.username}:${Date.now()}`);

    this.currentUserSignal.set(user);
    this.tokenSignal.set(token);

    const session: StoredSession = { token, user };
    localStorage.setItem(STORAGE_KEY, JSON.stringify(session));

    return true;
  }

  logout(): void {
    this.currentUserSignal.set(null);
    this.tokenSignal.set(null);
    localStorage.removeItem(STORAGE_KEY);
  }

  getToken(): string | null {
    return this.tokenSignal();
  }

  private readStoredSession(): StoredSession | null {
    const raw = localStorage.getItem(STORAGE_KEY);
    if (!raw) return null;

    try {
      return JSON.parse(raw) as StoredSession;
    } catch {
      return null;
    }
  }

  private readStoredUser(): User | null {
    return this.readStoredSession()?.user ?? null;
  }

  private readStoredToken(): string | null {
    return this.readStoredSession()?.token ?? null;
  }
}
