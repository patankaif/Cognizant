import { CommonModule } from '@angular/common';
import { Component, DestroyRef, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Store } from '@ngrx/store';
import { Book } from '../../../core/models/book.model';
import { HighlightDirective } from '../../../shared/directives/highlight.directive';
import { TruncatePipe } from '../../../shared/pipes/truncate.pipe';
import { addFavorite, removeFavorite } from '../../favorites/state/favorites.actions';
import { AppState } from '../../favorites/state/favorites.model';
import { selectIsFavorite } from '../../favorites/state/favorites.selectors';

@Component({
  selector: 'app-book-card',
  standalone: true,
  imports: [CommonModule, HighlightDirective, TruncatePipe],
  templateUrl: './book-card.html',
  styleUrl: './book-card.css'
})
export class BookCard implements OnChanges, OnInit {
  @Input({ required: true }) book!: Book;
  @Input() showActions = true;

  @Output() select = new EventEmitter<Book>();
  @Output() remove = new EventEmitter<number>();

  private readonly store = inject<Store<AppState>>(Store);
  private readonly destroyRef = inject(DestroyRef);

  changeCount = 0;
  isFavorite = false;

  ngOnInit(): void {
    this.store
      .select(selectIsFavorite(this.book.id))
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(isFavorite => (this.isFavorite = isFavorite));
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['book']) {
      this.changeCount++;
    }
  }

  onSelect(): void {
    this.select.emit(this.book);
  }

  onRemove(event: Event): void {
    event.stopPropagation();
    this.remove.emit(this.book.id);
  }

  onToggleFavorite(event: Event): void {
    event.stopPropagation();
    this.store.dispatch(
      this.isFavorite ? removeFavorite({ id: this.book.id }) : addFavorite({ id: this.book.id })
    );
  }
}
