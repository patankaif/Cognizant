import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { Store } from '@ngrx/store';
import { AuthService } from './core/services/auth.service';
import { loadFavorites } from './features/favorites/state/favorites.actions';
import { AppState } from './features/favorites/state/favorites.model';
import { selectFavoriteCount } from './features/favorites/state/favorites.selectors';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  readonly authService = inject(AuthService);
  private readonly store = inject<Store<AppState>>(Store);

  readonly favoriteCount$ = this.store.select(selectFavoriteCount);

  ngOnInit(): void {
    this.store.dispatch(loadFavorites());
  }

  logout(): void {
    this.authService.logout();
  }
}
