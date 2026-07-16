import { HttpInterceptorFn, HttpResponse } from '@angular/common/http';
import { of, throwError } from 'rxjs';
import { delay, mergeMap } from 'rxjs/operators';
import { Book } from '../models/book.model';

let books: Book[] = [
  { id: 1, title: 'Pride and Prejudice', author: 'Jane Austen', publishedYear: 1813, price: 9.99, genre: 'Fiction' },
  { id: 2, title: 'Foundation', author: 'Isaac Asimov', publishedYear: 1951, price: 8.75, genre: 'Science Fiction' },
  { id: 3, title: '1984', author: 'George Orwell', publishedYear: 1949, price: 8.99, genre: 'Fiction' },
  { id: 4, title: 'I, Robot', author: 'Isaac Asimov', publishedYear: 1950, price: 7.99, genre: 'Science Fiction' }
];
let nextId = 5;

export const mockBackendInterceptor: HttpInterceptorFn = (req, next) => {
  if (!req.url.startsWith('/api/books')) {
    return next(req);
  }

  const segments = req.url.replace('/api/books', '').split('/').filter(Boolean);
  const id = segments.length > 0 ? Number(segments[0]) : null;

  return of(null).pipe(
    delay(300),
    mergeMap(() => {
      switch (req.method) {
        case 'GET': {
          if (id === null) {
            const search = req.params.get('search')?.toLowerCase();
            const result = search
              ? books.filter(b =>
                  b.title.toLowerCase().includes(search) || b.author.toLowerCase().includes(search))
              : books;
            return of(new HttpResponse({ status: 200, body: result }));
          }

          const book = books.find(b => b.id === id);
          return book
            ? of(new HttpResponse({ status: 200, body: book }))
            : throwError(() => new HttpResponse({ status: 404, body: { message: `Book ${id} not found` } }));
        }

        case 'POST': {
          const newBook: Book = { ...(req.body as Omit<Book, 'id'>), id: nextId++ };
          books = [...books, newBook];
          return of(new HttpResponse({ status: 201, body: newBook }));
        }

        case 'PUT': {
          if (id === null) {
            return throwError(() => new HttpResponse({ status: 400, body: { message: 'Missing id' } }));
          }
          books = books.map(b => (b.id === id ? { ...(req.body as Book), id } : b));
          return of(new HttpResponse({ status: 200, body: books.find(b => b.id === id) }));
        }

        case 'DELETE': {
          if (id === null) {
            return throwError(() => new HttpResponse({ status: 400, body: { message: 'Missing id' } }));
          }
          books = books.filter(b => b.id !== id);
          return of(new HttpResponse({ status: 204 }));
        }

        default:
          return next(req);
      }
    })
  );
};
