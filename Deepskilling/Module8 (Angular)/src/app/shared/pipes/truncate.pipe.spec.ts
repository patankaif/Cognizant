import { TruncatePipe } from './truncate.pipe';

describe('TruncatePipe', () => {
  let pipe: TruncatePipe;

  beforeEach(() => {
    pipe = new TruncatePipe();
  });

  it('creates an instance', () => {
    expect(pipe).toBeTruthy();
  });

  it('returns the original string when shorter than maxLength', () => {
    expect(pipe.transform('short text', 50)).toBe('short text');
  });

  it('truncates and appends an ellipsis when longer than maxLength', () => {
    const result = pipe.transform('This is a fairly long sentence to truncate', 10);
    expect(result.endsWith('...')).toBeTrue();
    expect(result.length).toBeLessThanOrEqual(13);
  });

  it('returns an empty string for falsy input', () => {
    expect(pipe.transform('', 10)).toBe('');
  });
});
