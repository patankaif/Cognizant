import { TestBed } from '@angular/core/testing';
import { AuthService } from './auth.service';

describe('AuthService', () => {
  let service: AuthService;

  beforeEach(() => {
    localStorage.clear();
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthService);
  });

  afterEach(() => {
    localStorage.clear();
  });

  it('starts logged out', () => {
    expect(service.isLoggedIn()).toBeFalse();
    expect(service.currentUser()).toBeNull();
  });

  it('logs in with valid admin credentials', () => {
    const success = service.login({ username: 'admin', password: 'Admin@123' });

    expect(success).toBeTrue();
    expect(service.isLoggedIn()).toBeTrue();
    expect(service.isAdmin()).toBeTrue();
    expect(service.currentUser()?.username).toBe('admin');
  });

  it('rejects invalid credentials', () => {
    const success = service.login({ username: 'admin', password: 'wrong-password' });

    expect(success).toBeFalse();
    expect(service.isLoggedIn()).toBeFalse();
  });

  it('treats a reader as logged in but not admin', () => {
    service.login({ username: 'reader', password: 'Reader@123' });

    expect(service.isLoggedIn()).toBeTrue();
    expect(service.isAdmin()).toBeFalse();
  });

  it('clears the session on logout', () => {
    service.login({ username: 'admin', password: 'Admin@123' });
    service.logout();

    expect(service.isLoggedIn()).toBeFalse();
    expect(service.getToken()).toBeNull();
  });
});
