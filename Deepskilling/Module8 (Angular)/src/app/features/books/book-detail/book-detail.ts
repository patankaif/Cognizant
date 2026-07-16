import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Observable, switchMap } from 'rxjs';
import { Book } from '../../../core/models/book.model';
import { BookService } from '../../../core/services/book.service';

@Component({
  selector: 'app-book-detail',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './book-detail.html',
  styleUrl: './book-detail.css'
})
export class BookDetail implements OnInit {
  book$!: Observable<Book>;

  constructor(private readonly route: ActivatedRoute, private readonly bookService: BookService) {}

  ngOnInit(): void {
    this.book$ = this.route.paramMap.pipe(
      switchMap(params => this.bookService.getBook(Number(params.get('id'))))
    );
  }

  priceTier(price: number): string {
    if (price < 8) return 'budget';
    if (price < 10) return 'standard';
    return 'premium';
  }
}
