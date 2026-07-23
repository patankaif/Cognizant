# User Story Template

```
As a [type of user]
I want [some goal]
So that [some reason / benefit]
```

## Why this format works

- **"As a [type of user]"** forces you to be specific about *who* —
  "a reader" and "an admin" need different things from the catalog, and
  writing them as separate stories (see US-101 vs. US-104 in the product
  backlog) makes that difference visible instead of hidden inside one
  vague story.
- **"I want [goal]"** describes the *what*, but deliberately not the
  *how* — a good user story stays out of implementation details (it
  doesn't say "add a `GET /api/books` endpoint"), leaving the team free
  to figure out the best technical approach during planning.
- **"So that [benefit]"** is the most often skipped part, and the most
  valuable — it's the reason the story exists at all. If you can't fill
  this in convincingly, that's a signal the story might not be worth
  doing, or that you don't yet understand the real need behind it.

## The INVEST checklist

Before a story is "ready" to bring into a sprint, check it against all
six letters:

| Letter | Means | Ask yourself |
|---|---|---|
| **I**ndependent | Doesn't hard-depend on another unstarted story | Could this be built and shipped on its own? |
| **N**egotiable | Not a rigid contract — details can be discussed | Is there room to discuss the *how* during planning? |
| **V**aluable | Delivers real value to a user or the business | Who benefits, and how? (This is the "So that" clause.) |
| **E**stimable | The team has enough information to size it | Could the team put a story-point number on this right now? |
| **S**mall | Fits comfortably in one sprint | Could this realistically be finished in days, not weeks? |
| **T**estable | Has a clear way to confirm it's done | Do you have (or can you write) acceptance criteria for it? |

A story that fails "Small" is usually really an **Epic** — a larger body
of work that should be broken down into several smaller stories before
it's ready for a sprint. (See Epic 3: Orders in `product-backlog.md` —
"Orders" itself is the epic; US-301 and US-302 are the actual stories.)

A story that fails "Independent" isn't necessarily wrong — sometimes
real dependencies exist (US-104 genuinely does need login/US-201 to make
sense) — but it should be called out explicitly, the way the "Notes"
field does in the backlog, so sprint planning can sequence around it.
