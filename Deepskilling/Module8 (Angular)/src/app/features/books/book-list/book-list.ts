import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Observable, Subject, combineLatest } from 'rxjs';
import { debounceTime, distinctUntilChanged, map, startWith, switchMap } from 'rxjs/operators';
import { Book } from '../../../core/models/book.model';
import { AuthService } from '../../../core/services/auth.service';
import { BookService } from '../../../core/services/book.service';
import { BookCard } from '../book-card/book-card';

@Component({
  selector: 'app-book-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, BookCard],
  templateUrl: './book-list.html',
  styleUrl: './book-list.css'
})
export class BookList implements OnInit {
  readonly searchControl = new FormControl('', { nonNullable: true });
  private readonly refresh$ = new Subject<void>();

  books$!: Observable<Book[]>;
  errorMessage: string | null = null;

  constructor(
    private readonly bookService: BookService,
    private readonly router: Router,
    readonly authService: AuthService
  ) {}

  ngOnInit(): void {
    const search$ = this.searchControl.valueChanges.pipe(
      startWith(this.searchControl.value),
      debounceTime(300),
      distinctUntilChanged()
    );

    this.books$ = combineLatest([search$, this.refresh$.pipe(startWith(void 0))]).pipe(
      map(([term]) => term),
      switchMap(term => this.bookService.getBooks(term))
    );
  }

  onSelectBook(book: Book): void {
    this.router.navigate(['/books', book.id]);
  }

  onDeleteBook(id: number): void {
    this.bookService.deleteBook(id).subscribe({
      next: () => this.refresh$.next(),
      error: () => (this.errorMessage = 'Failed to delete book.')
    });
  }

  trackByBookId(_index: number, book: Book): number {
    return book.id;
  }
}
