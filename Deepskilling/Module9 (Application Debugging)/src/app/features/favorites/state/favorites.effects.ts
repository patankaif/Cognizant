import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { map, tap } from 'rxjs/operators';
import { addFavorite, loadFavorites, loadFavoritesSuccess, removeFavorite } from './favorites.actions';

const STORAGE_KEY = 'module8_favorites';

function readStoredIds(): number[] {
  const raw = localStorage.getItem(STORAGE_KEY);
  if (!raw) return [];
  try {
    return JSON.parse(raw) as number[];
  } catch {
    return [];
  }
}

function writeStoredIds(ids: number[]): void {
  localStorage.setItem(STORAGE_KEY, JSON.stringify(ids));
}

@Injectable()
export class FavoritesEffects {
  loadFavorites$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadFavorites),
      map(() => loadFavoritesSuccess({ ids: readStoredIds() }))
    )
  );

  persistOnAdd$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(addFavorite),
        tap(({ id }) => {
          const ids = readStoredIds();
          if (!ids.includes(id)) {
            writeStoredIds([...ids, id]);
          }
        })
      ),
    { dispatch: false }
  );

  persistOnRemove$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(removeFavorite),
        tap(({ id }) => {
          writeStoredIds(readStoredIds().filter(existingId => existingId !== id));
        })
      ),
    { dispatch: false }
  );

  constructor(private readonly actions$: Actions) {}
}
