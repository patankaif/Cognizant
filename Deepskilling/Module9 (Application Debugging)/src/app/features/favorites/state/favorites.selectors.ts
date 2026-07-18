import { createFeatureSelector, createSelector } from '@ngrx/store';
import { FAVORITES_FEATURE_KEY, FavoritesState } from './favorites.model';

export const selectFavoritesState = createFeatureSelector<FavoritesState>(FAVORITES_FEATURE_KEY);

export const selectFavoriteIds = createSelector(selectFavoritesState, state => state.ids);

export const selectFavoriteCount = createSelector(selectFavoriteIds, ids => ids.length);

export const selectIsFavorite = (id: number) =>
  createSelector(selectFavoriteIds, ids => ids.includes(id));
