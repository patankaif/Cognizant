import { TestBed } from '@angular/core/testing';
import { provideStore } from '@ngrx/store';
import { BookCard } from './book-card';
import { Book } from '../../../core/models/book.model';
import { FAVORITES_FEATURE_KEY } from '../../favorites/state/favorites.model';
import { favoritesReducer } from '../../favorites/state/favorites.reducer';

describe('BookCard', () => {
  const testBook: Book = {
    id: 1,
    title: 'Test Book',
    author: 'Test Author',
    publishedYear: 2020,
    price: 12.5,
    genre: 'Fiction'
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookCard],
      providers: [provideStore({ [FAVORITES_FEATURE_KEY]: favoritesReducer })]
    }).compileComponents();
  });

  it('creates the component and renders the book title', () => {
    const fixture = TestBed.createComponent(BookCard);
    fixture.componentInstance.book = testBook;
    fixture.detectChanges();

    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('h3')?.textContent).toContain('Test Book');
  });

  it('emits select when clicked', () => {
    const fixture = TestBed.createComponent(BookCard);
    fixture.componentInstance.book = testBook;
    fixture.detectChanges();

    let emitted: Book | undefined;
    fixture.componentInstance.select.subscribe((book: Book) => (emitted = book));

    (fixture.nativeElement as HTMLElement).querySelector('.book-card')?.dispatchEvent(new Event('click'));

    expect(emitted).toEqual(testBook);
  });

  it('increments changeCount when the book input changes', () => {
    const fixture = TestBed.createComponent(BookCard);
    fixture.componentInstance.book = testBook;
    fixture.detectChanges();

    const initialCount = fixture.componentInstance.changeCount;

    fixture.componentInstance.book = { ...testBook, title: 'Updated Title' };
    fixture.componentInstance.ngOnChanges({
      book: {
        previousValue: testBook,
        currentValue: { ...testBook, title: 'Updated Title' },
        firstChange: false,
        isFirstChange: () => false
      }
    });

    expect(fixture.componentInstance.changeCount).toBe(initialCount + 1);
  });
});
