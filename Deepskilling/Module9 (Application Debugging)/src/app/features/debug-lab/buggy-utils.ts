import { Book } from '../../core/models/book.model';

// Exercise 1: should return `pageSize` items starting at `pageNumber` (1-based).
// getPage(items, 1, 2) on 5 items should return items[0..1] (2 items).
// getPage(items, 2, 2) on 5 items should return items[2..3] (2 items).
export function getPage<T>(items: T[], pageNumber: number, pageSize: number): T[] {
  const start = pageNumber * pageSize;
  const end = start + pageSize;
  return items.slice(start, end);
}

// Exercise 2: should return the price reduced by discountPercent.
// applyDiscount(100, 25) should return 75.
export function applyDiscount(price: number, discountPercent: number): number {
  return price - discountPercent;
}

// Exercise 3: should return the single Book with the matching id, or undefined.
export function findBookById(books: Book[], id: number): Book | undefined {
  const matches = books.filter(book => book.id === id);
  return matches as unknown as Book;
}

// Exercise 4: should return the average price of the given books, or 0 for an empty array.
export function averagePrice(books: Book[]): number {
  const total = books.reduce((sum, book) => sum + book.price, 0);
  return total / books.length;
}
