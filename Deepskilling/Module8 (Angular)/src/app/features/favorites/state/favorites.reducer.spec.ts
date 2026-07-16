import { addFavorite, loadFavoritesSuccess, removeFavorite } from './favorites.actions';
import { favoritesReducer } from './favorites.reducer';
import { initialFavoritesState } from './favorites.model';

describe('favoritesReducer', () => {
  it('returns the initial state for an unknown action', () => {
    const state = favoritesReducer(undefined, { type: 'UNKNOWN' });
    expect(state).toEqual(initialFavoritesState);
  });

  it('sets ids and loaded on loadFavoritesSuccess', () => {
    const state = favoritesReducer(initialFavoritesState, loadFavoritesSuccess({ ids: [1, 2] }));
    expect(state.ids).toEqual([1, 2]);
    expect(state.loaded).toBeTrue();
  });

  it('adds an id on addFavorite without duplicating', () => {
    let state = favoritesReducer(initialFavoritesState, addFavorite({ id: 5 }));
    expect(state.ids).toEqual([5]);

    state = favoritesReducer(state, addFavorite({ id: 5 }));
    expect(state.ids).toEqual([5]);
  });

  it('removes an id on removeFavorite', () => {
    const seeded = favoritesReducer(initialFavoritesState, loadFavoritesSuccess({ ids: [1, 2, 3] }));
    const state = favoritesReducer(seeded, removeFavorite({ id: 2 }));
    expect(state.ids).toEqual([1, 3]);
  });
});
