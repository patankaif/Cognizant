export interface FavoritesState {
  ids: number[];
  loaded: boolean;
}

export const initialFavoritesState: FavoritesState = {
  ids: [],
  loaded: false
};

export const FAVORITES_FEATURE_KEY = 'favorites';

export interface AppState {
  [FAVORITES_FEATURE_KEY]: FavoritesState;
}
