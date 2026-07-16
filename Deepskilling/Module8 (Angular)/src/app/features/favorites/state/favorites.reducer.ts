import { createReducer, on } from '@ngrx/store';
import { addFavorite, loadFavoritesSuccess, removeFavorite } from './favorites.actions';
import { FavoritesState, initialFavoritesState } from './favorites.model';

export const favoritesReducer = createReducer(
  initialFavoritesState,
  on(loadFavoritesSuccess, (state, { ids }): FavoritesState => ({ ids, loaded: true })),
  on(addFavorite, (state, { id }): FavoritesState => ({
    ...state,
    ids: state.ids.includes(id) ? state.ids : [...state.ids, id]
  })),
  on(removeFavorite, (state, { id }): FavoritesState => ({
    ...state,
    ids: state.ids.filter(existingId => existingId !== id)
  }))
);
