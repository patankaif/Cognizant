import { createAction, props } from '@ngrx/store';

export const loadFavorites = createAction('[Favorites] Load Favorites');

export const loadFavoritesSuccess = createAction(
  '[Favorites] Load Favorites Success',
  props<{ ids: number[] }>()
);

export const addFavorite = createAction('[Favorites] Add Favorite', props<{ id: number }>());

export const removeFavorite = createAction('[Favorites] Remove Favorite', props<{ id: number }>());
