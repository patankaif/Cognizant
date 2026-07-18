# Module 8 – Angular (v20.0)

A real, buildable Angular 20 application (standalone components, the
current Angular CLI defaults) covering every learning objective in
**Module 8** of the DN 5.0 Deep Skilling handbook. It's themed as a small
book catalog, tying together with the "Books" domain used in earlier
modules.

I scaffolded this with the actual Angular CLI (`ng new`), installed real
npm packages (`@ngrx/store`, `@ngrx/effects`, `@ngrx/store-devtools`), and
**verified it with real builds** — `ng build` (development) and
`ng build --configuration production` both succeed. I could not run the
Karma/Jasmine test suite itself, since this sandbox has no Chrome/Chromium
available to run headless — but the test bundle compiled cleanly (all
`.spec.ts` files type-check and bundle with zero errors), which is a
strong signal they'll pass; please run `ng test` yourself to confirm.

## Running it

```bash
npm install
ng serve
```
Then open `http://localhost:4200`. Demo logins: `admin` / `Admin@123`
(can create/delete books) or `reader` / `Reader@123` (read-only).

```bash
ng test     # unit tests (needs Chrome/Chromium installed locally)
ng build    # production build
```

There's no real backend — an `HttpInterceptorFn` (`mock-backend.interceptor.ts`)
intercepts calls to `/api/books` and simulates a REST API with an
in-memory array, so `ng serve` works with zero setup.

## Where each learning objective is demonstrated

| Topic | File(s) |
|---|---|
| Angular architecture, CLI, project structure | Whole project — generated with `ng new`, standalone components (no NgModules) |
| Components, data binding, component lifecycle | `book-card.ts` (`@Input`/`@Output`, `OnChanges`), `book-list.ts`/`book-detail.ts` (`OnInit`) |
| Property/event/two-way binding | `book-card.html` (`(click)`), `login.html` (`[(ngModel)]`), `book-list.html` (`[formControl]`) |
| Directives & pipes | `*ngIf`/`*ngFor`/`*ngSwitch` throughout; custom attribute directive `highlight.directive.ts`; custom structural directive `unless.directive.ts`; custom pipe `truncate.pipe.ts`; built-in `currency` pipe |
| Template-driven forms | `login.ts`/`login.html` — `ngModel`, `NgForm`, validation messages |
| Reactive forms | `book-form.ts`/`book-form.html` — `FormBuilder`, `FormGroup`, `Validators`, a custom validator (`futureYearValidator`) |
| Dependency Injection & services | `book.service.ts`, `auth.service.ts`, `logger.service.ts` — all `providedIn: 'root'`, injected via `inject()` or constructors |
| Routing, navigation, guards | `app.routes.ts` — lazy-loaded routes (`loadComponent`), route params (`book-detail.ts`), functional guards (`auth.guard.ts`) |
| HttpClient & Observables | `book.service.ts` (typed HTTP calls), `mock-backend.interceptor.ts` (simulated backend) |
| HTTP Interceptors | `auth.interceptor.ts` (adds a bearer token), `mock-backend.interceptor.ts` |
| State management with services and NgRx | `features/favorites/state/` — full Store/Actions/Reducer/Effects/Selectors slice, connected from `favorites.ts` and `book-card.ts` |
| RxJS operators | `book-list.ts` — `debounceTime`, `distinctUntilChanged`, `switchMap`, `combineLatest`, `startWith` for a live search box |
| Testing (Jasmine/Karma) | `truncate.pipe.spec.ts`, `auth.service.spec.ts`, `book.service.spec.ts` (`HttpTestingController`), `book-card.spec.ts` (component + store), `favorites.reducer.spec.ts` |

## Module 9 — Application Debugging

This project also includes a **Debugging Lab** (`/debug-lab` route) with
five intentionally buggy functions/components to practice Chrome DevTools
and VS Code debugging on, plus enhanced `.vscode/launch.json` configs and
a full walkthrough. See **`DEBUGGING_GUIDE.md`** in this same folder.

## Project layout

```
src/app/
  app.ts / app.html / app.css        root component: nav bar, favorite count badge
  app.routes.ts                      lazy-loaded routes + guards
  app.config.ts                      HttpClient interceptors, NgRx Store/Effects/DevTools
  core/
    models/                          Book, User
    services/
      book.service.ts                HttpClient CRUD
      auth.service.ts                signal-based login/logout state
      logger.service.ts
    guards/auth.guard.ts             authGuard, adminGuard (functional CanActivateFn)
    interceptors/auth.interceptor.ts adds Authorization header
    mock/mock-backend.interceptor.ts simulates a REST API, no real server needed
  shared/
    directives/highlight.directive.ts   attribute directive
    directives/unless.directive.ts      structural directive (inverse of *ngIf)
    pipes/truncate.pipe.ts
  features/
    books/
      book-list/    search box (RxJS), list of book-card, delete
      book-detail/  route param lookup, *ngSwitch price tier
      book-form/    reactive form, custom validator
      book-card/    @Input/@Output, OnChanges, favorite toggle (NgRx)
    auth/login/     template-driven form
    favorites/      NgRx-connected list
      state/        actions, reducer, effects, selectors, model
```

## Notes

- Angular 20's CLI defaults to standalone components and drops the
  `.component` file suffix (e.g. `book-list.ts`, class `BookList`) — that's
  not a shortcut I took, it's what `ng generate` produces on this version.
- `mock-backend.interceptor.ts` exists purely so this runs standalone; in
  a real app you'd point `BookService` at an actual backend (e.g. the
  Module 6 Web API) and delete the mock interceptor — nothing else would
  need to change, since `BookService` only knows about HTTP, not about
  where the data actually comes from.
- The Favorites feature is intentionally the "full NgRx slice" example
  (Actions, Reducer, Effects persisting to `localStorage`, Selectors)
  since it's small enough to review end-to-end in a few files, while
  `AuthService` shows the simpler "state in a service, using signals"
  approach the syllabus also asks for — both patterns are represented.
- I don't have Chrome/Chromium in this sandbox to actually execute
  `ng test`, so please run it locally and let me know if anything needs
  adjusting — though the fact that Karma successfully compiled every
  spec file into a test bundle (zero TypeScript/template errors) is a
  good sign.
