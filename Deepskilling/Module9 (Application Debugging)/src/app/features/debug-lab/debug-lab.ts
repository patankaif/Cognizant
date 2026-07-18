import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Book } from '../../core/models/book.model';
import { BookService } from '../../core/services/book.service';
import { applyDiscount, averagePrice, findBookById, getPage } from './buggy-utils';

interface ExerciseResult {
  title: string;
  expected: string;
  actual: string;
}

@Component({
  selector: 'app-debug-lab',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './debug-lab.html',
  styleUrl: './debug-lab.css'
})
export class DebugLab {
  private readonly bookService = inject(BookService);

  results: ExerciseResult[] = [];
  asyncBugMessage = '';

  private readonly sampleBooks: Book[] = [
    { id: 1, title: 'Book A', author: 'Author A', publishedYear: 2001, price: 10, genre: 'Fiction' },
    { id: 2, title: 'Book B', author: 'Author B', publishedYear: 2002, price: 20, genre: 'Fiction' },
    { id: 3, title: 'Book C', author: 'Author C', publishedYear: 2003, price: 30, genre: 'Fiction' },
    { id: 4, title: 'Book D', author: 'Author D', publishedYear: 2004, price: 40, genre: 'Fiction' },
    { id: 5, title: 'Book E', author: 'Author E', publishedYear: 2005, price: 50, genre: 'Fiction' }
  ];

  runPaginationExercise(): void {
    const page1 = getPage(this.sampleBooks, 1, 2);
    this.results.push({
      title: 'Exercise 1: getPage(books, pageNumber=1, pageSize=2)',
      expected: 'Book A, Book B',
      actual: page1.map(b => b.title).join(', ') || '(empty)'
    });
  }

  runDiscountExercise(): void {
    const result = applyDiscount(200, 25);
    this.results.push({
      title: 'Exercise 2: applyDiscount(price=200, discountPercent=25)',
      expected: '150',
      actual: String(result)
    });
  }

  runFindByIdExercise(): void {
    const book = findBookById(this.sampleBooks, 3);
    let actual: string;
    try {
      actual = String(book!.title);
    } catch (error) {
      actual = `threw: ${(error as Error).message}`;
    }
    this.results.push({
      title: 'Exercise 3: findBookById(books, id=3).title',
      expected: 'Book C',
      actual
    });
  }

  runAveragePriceExercise(): void {
    const result = averagePrice([]);
    this.results.push({
      title: 'Exercise 4: averagePrice([]) (no books)',
      expected: '0',
      actual: String(result)
    });
  }

  runAsyncTimingExercise(): void {
    this.asyncBugMessage = '';
    let books: Book[] = [];

    this.bookService.getBooks().subscribe(loadedBooks => {
      books = loadedBooks;
    });

    try {
      this.asyncBugMessage = `First book title: ${books[0].title}`;
    } catch (error) {
      this.asyncBugMessage = `threw: ${(error as Error).message}`;
    }
  }

  clearResults(): void {
    this.results = [];
    this.asyncBugMessage = '';
  }
}
