import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class LoggerService {
  info(message: string, ...params: unknown[]): void {
    console.log(`[INFO] ${message}`, ...params);
  }

  warn(message: string, ...params: unknown[]): void {
    console.warn(`[WARN] ${message}`, ...params);
  }

  error(message: string, ...params: unknown[]): void {
    console.error(`[ERROR] ${message}`, ...params);
  }
}
