# Module 9 – Application Debugging

A hands-on debugging module built on top of the Module 8 Angular app.
Rather than more application code, this adds:

1. A **Debugging Lab** (`/debug-lab` route) — five small, real bugs to
   practice finding with breakpoints, the Sources panel, and Watch
   expressions, instead of reading about debugging in the abstract.
2. VS Code debug configs (`.vscode/launch.json`, already present from the
   Angular CLI, lightly enhanced) so you can set breakpoints directly in
   `.ts` files and hit them from VS Code, not just Chrome.
3. This guide, covering both tools plus debugging Angular-specific things
   (component state, services, the NgRx store).

## 1. Debugging Angular with Chrome DevTools

**Setup:** `npm install`, then `ng serve`, then open `http://localhost:4200`
and press **F12** (or right-click → Inspect) to open DevTools.

### Elements panel — inspecting the DOM tree
- The Elements panel shows the *rendered* DOM, not your template source —
  useful for confirming a `*ngIf` actually rendered, or that a class
  binding (`[class.mismatch]` in `debug-lab.html`) applied correctly.
- Click the inspect icon (top-left of the panel) then click any element on
  the page to jump straight to its markup.
- The **Styles** pane on the right shows exactly which CSS rules apply
  and lets you toggle them live — handy for layout bugs.

### Sources panel — breakpoints, call stack, source maps
- Open the **Sources** tab, then `Ctrl+P` (or `Cmd+P`) and type a filename,
  e.g. `buggy-utils.ts`. Because Angular's dev build emits source maps,
  you're setting breakpoints in your actual TypeScript, not the compiled
  JS.
- Click a line number to set a breakpoint (blue flag). Click again to
  remove it.
- **Conditional breakpoints**: right-click a line number → "Add
  conditional breakpoint" → e.g. `pageNumber === 2` — only pauses when
  true. Useful when a bug only shows up on the 2nd call, the 10th item,
  etc.
- Once paused: the right-hand panel shows **Call Stack** (who called this
  function, and who called *them*), **Scope** (local variables in the
  current function, live), and **Watch** (expressions you pin so you can
  track them across steps).
- Stepping controls: **Step over** (F10) runs the current line without
  entering function calls; **Step into** (F11) enters the function call
  on the current line; **Step out** (Shift+F11) finishes the current
  function and returns to its caller.
- You can also type `debugger;` directly in your TypeScript — the
  debugger will pause there automatically whenever DevTools is open,
  without needing to click a line number first. (Remove it before
  committing — it's a debugging aid, not something to ship.)

### Console — quick checks without breakpoints
- While paused at a breakpoint, the Console has access to every variable
  currently in scope — type `books`, `books[0]`, `books.length`, etc. to
  inspect them without adding a Watch.
- `console.table(someArray)` renders an array of objects as an actual
  table — much easier to scan than nested `console.log` output.

## 2. Debugging Angular with Visual Studio Code

**Prerequisite:** the built-in `Debugger for Chrome` / `js-debug`
extension (bundled with VS Code by default since 1.46+) — no extra
install needed on a recent VS Code.

The Angular CLI already generated `.vscode/launch.json` and
`.vscode/tasks.json` for this project; I added `webRoot`/`sourceMaps` and
an extra "attach" configuration. To use it:

1. Open this folder in VS Code.
2. Set a breakpoint by clicking in the gutter next to a line number in
   any `.ts` file (e.g. `buggy-utils.ts`, line inside `getPage`).
3. Open the **Run and Debug** panel (`Ctrl+Shift+D`), pick **"ng serve"**
   from the dropdown, and press **F5**. VS Code will start `ng serve` for
   you (via the `preLaunchTask`), launch Chrome, and attach.
4. Trigger the code path (e.g. click "Run Exercise 1" in the Debug Lab
   page) — VS Code will pause on your breakpoint, right inside the editor,
   with the same Variables/Watch/Call Stack panels Chrome DevTools has,
   but without leaving your editor.
5. If you already have `ng serve` running in a terminal and just want to
   attach: launch Chrome yourself with remote debugging enabled
   (`chrome --remote-debugging-port=9222`), then pick **"Attach to
   running Chrome"** from the same dropdown.

The third configuration, **"ng test"**, does the same thing but against
the Karma test runner — set a breakpoint inside a `.spec.ts` file, launch
that config, and it pauses inside your test the same way.

## 3. Debugging Angular state — services and the NgRx store

- **Component/service state**: any `@Input`, plain field, or service
  property is just a JS value — pause on a breakpoint inside a component
  method (e.g. `BookCard.onToggleFavorite`) and inspect `this` in the
  Scope panel to see the live component instance, including injected
  services.
- **NgRx store**: install the free **Redux DevTools** browser extension.
  This project calls `provideStoreDevtools(...)` in `app.config.ts`, so
  it's already wired up — open the Redux DevTools panel (a new tab
  appears in Chrome DevTools once the extension is installed) and you'll
  see every dispatched action (`[Favorites] Add Favorite`, `[Favorites]
  Load Favorites Success`, etc.), a full state tree at each step, and a
  time-travel slider to jump back to any previous state.
- This is usually faster than a breakpoint for state bugs: instead of
  guessing where to pause, watch the action log to see exactly which
  action fired (or didn't), and diff the state before/after.

## 4. Debug Lab — five exercises

Run `ng serve`, open `/debug-lab`, and try to find each bug **before**
reading the solution below. For each one: click the button, see the
Expected/Actual mismatch in the table, then set a breakpoint (Chrome
Sources panel or a VS Code gutter breakpoint) in the referenced file and
step through.

<details>
<summary><b>Exercise 1 — Pagination (click to reveal the fix)</b></summary>

`getPage()` in `buggy-utils.ts` treats `pageNumber` as 0-based, but every
caller (and the exercise description) treats it as 1-based (page 1 = the
first page). Breakpoint on the `start` line, call with
`getPage(books, 1, 2)`, watch `start` — it evaluates to `2` instead of
`0`.

```ts
export function getPage<T>(items: T[], pageNumber: number, pageSize: number): T[] {
  const start = (pageNumber - 1) * pageSize;
  const end = start + pageSize;
  return items.slice(start, end);
}
```
</details>

<details>
<summary><b>Exercise 2 — Discount math</b></summary>

`applyDiscount()` subtracts `discountPercent` as if it were a flat
amount, instead of treating it as a percentage of `price`.

```ts
export function applyDiscount(price: number, discountPercent: number): number {
  return price - (price * discountPercent) / 100;
}
```
</details>

<details>
<summary><b>Exercise 3 — findBookById returns the wrong shape</b></summary>

`Array.prototype.filter()` always returns an array (even with one match),
but the function is typed and used as if it returns a single `Book`. The
`as unknown as Book` cast hides this from the compiler — TypeScript
won't catch it, only a breakpoint (or the runtime mismatch) will. Fix: use
`.find()` instead of `.filter()`.

```ts
export function findBookById(books: Book[], id: number): Book | undefined {
  return books.find(book => book.id === id);
}
```
</details>

<details>
<summary><b>Exercise 4 — averagePrice on an empty array</b></summary>

`total / books.length` divides by zero when the array is empty,
producing `NaN` instead of `0`. Watch `books.length` at a breakpoint on
the `return` line to see it's `0`.

```ts
export function averagePrice(books: Book[]): number {
  if (books.length === 0) return 0;
  const total = books.reduce((sum, book) => sum + book.price, 0);
  return total / books.length;
}
```
</details>

<details>
<summary><b>Exercise 5 — async timing bug</b></summary>

`runAsyncTimingExercise()` in `debug-lab.ts` reads `books[0]` immediately
after calling `.subscribe(...)`, but the mock backend responds after a
300ms delay (see `mock-backend.interceptor.ts`) — the callback hasn't run
yet, so `books` is still `[]` and `books[0]` is `undefined`. Set a
breakpoint on the `try` line vs. inside the `subscribe` callback and
compare how many times each one is hit, and in what order, to see this
directly. Fix: only read the data inside the callback (or, in a real
component, drive the template off an `Observable` with the `async` pipe,
as `BookList` and `BookDetail` already do elsewhere in this app).

```ts
runAsyncTimingExercise(): void {
  this.asyncBugMessage = '';

  this.bookService.getBooks().subscribe(books => {
    this.asyncBugMessage = books.length
      ? `First book title: ${books[0].title}`
      : 'No books loaded.';
  });
}
```
</details>

## Notes

- I verified this builds cleanly with `ng build` (both development and
  production configurations) in a real Angular 20 environment — the bugs
  in the Debug Lab are logical, not syntax errors, so they compile fine
  and only reveal themselves at runtime, which is the point of the
  exercise.
- I don't have Chrome/VS Code available to interactively drive in this
  sandbox, so I couldn't record real breakpoint screenshots — this guide
  describes the exact steps and panel names to use. Please try it
  live and let me know if any step doesn't match what you see.
