import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { BookService } from './book.service';
import { Book } from '../models/book.model';

describe('BookService', () => {
  let service: BookService;
  let httpMock: HttpTestingController;

  const sampleBook: Book = {
    id: 1,
    title: 'Test Book',
    author: 'Test Author',
    publishedYear: 2000,
    price: 9.99,
    genre: 'Fiction'
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting()]
    });

    service = TestBed.inject(BookService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('fetches all books', () => {
    service.getBooks().subscribe(books => {
      expect(books.length).toBe(1);
      expect(books[0].title).toBe('Test Book');
    });

    const req = httpMock.expectOne(req => req.url === '/api/books');
    expect(req.request.method).toBe('GET');
    req.flush([sampleBook]);
  });

  it('fetches a single book by id', () => {
    service.getBook(1).subscribe(book => {
      expect(book).toEqual(sampleBook);
    });

    const req = httpMock.expectOne('/api/books/1');
    expect(req.request.method).toBe('GET');
    req.flush(sampleBook);
  });

  it('sends a POST request to create a book', () => {
    const { id, ...newBook } = sampleBook;

    service.createBook(newBook).subscribe(created => {
      expect(created.id).toBe(1);
    });

    const req = httpMock.expectOne('/api/books');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(newBook);
    req.flush(sampleBook);
  });

  it('sends a DELETE request', () => {
    service.deleteBook(1).subscribe();

    const req = httpMock.expectOne('/api/books/1');
    expect(req.request.method).toBe('DELETE');
    req.flush(null);
  });
});
