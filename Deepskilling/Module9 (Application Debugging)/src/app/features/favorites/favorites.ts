import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { Book } from '../../core/models/book.model';
import { BookService } from '../../core/services/book.service';
import { addFavorite, loadFavorites, removeFavorite } from './state/favorites.actions';
import { AppState } from './state/favorites.model';
import { selectFavoriteIds } from './state/favorites.selectors';

@Component({
  selector: 'app-favorites',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './favorites.html',
  styleUrl: './favorites.css'
})
export class Favorites implements OnInit {
  favoriteIds$: Observable<number[]>;
  favoriteBooks$!: Observable<Book[]>;

  constructor(private readonly store: Store<AppState>, private readonly bookService: BookService) {
    this.favoriteIds$ = this.store.select(selectFavoriteIds);
  }

  ngOnInit(): void {
    this.store.dispatch(loadFavorites());

    this.favoriteBooks$ = this.favoriteIds$.pipe(
      switchMap(ids =>
        this.bookService
          .getBooks()
          .pipe(map(books => books.filter(book => ids.includes(book.id))))
      )
    );
  }

  removeFromFavorites(id: number): void {
    this.store.dispatch(removeFavorite({ id }));
  }
}
