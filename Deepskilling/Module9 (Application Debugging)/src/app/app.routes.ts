import { Routes } from '@angular/router';
import { adminGuard, authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'books' },
  {
    path: 'books',
    loadComponent: () => import('./features/books/book-list/book-list').then(m => m.BookList)
  },
  {
    path: 'books/new',
    loadComponent: () => import('./features/books/book-form/book-form').then(m => m.BookForm),
    canActivate: [authGuard, adminGuard]
  },
  {
    path: 'books/:id',
    loadComponent: () => import('./features/books/book-detail/book-detail').then(m => m.BookDetail)
  },
  {
    path: 'favorites',
    loadComponent: () => import('./features/favorites/favorites').then(m => m.Favorites),
    canActivate: [authGuard]
  },
  {
    path: 'login',
    loadComponent: () => import('./features/auth/login/login').then(m => m.Login)
  },
  {
    path: 'debug-lab',
    loadComponent: () => import('./features/debug-lab/debug-lab').then(m => m.DebugLab)
  },
  { path: '**', redirectTo: 'books' }
];
