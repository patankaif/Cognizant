export interface Book {
  id: number;
  title: string;
  author: string;
  publishedYear: number;
  price: number;
  genre: string;
}

export type NewBook = Omit<Book, 'id'>;
