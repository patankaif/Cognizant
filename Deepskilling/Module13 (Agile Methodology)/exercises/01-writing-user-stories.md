# Exercise 1 — Writing User Stories

## Setup

Read `templates/user-story-template.md` and
`templates/acceptance-criteria-template.md` first if you haven't already.

## Part A — write three new stories for the Library Catalog

The product backlog (`product-backlog/product-backlog.md`) doesn't yet
cover book **reviews**. Write three user stories for this feature, in
proper "As a / I want / So that" format:

1. A reader leaving a star rating and a written review on a book.
2. A reader viewing all reviews for a book (with an empty state for
   books with none yet).
3. An admin removing an inappropriate review.

For each, also write at least 2 Given-When-Then acceptance criteria
blocks — at minimum, one happy path and one edge case (validation
failure, empty state, or permission boundary — see the "Covering the
edges" section of the acceptance criteria template for the categories to
draw from).

## Part B — run the INVEST checklist

For each of your three stories, go through all six letters of INVEST
(see the table in `templates/user-story-template.md`) and answer, in one
sentence each: does this story pass, or not? If any story fails
**Small**, split it into two smaller stories and redo this check on the
split versions.

## Part C — critique a bad story

Here's a deliberately bad "story":

> As a user, I want the system to be fast and handle all my book-related
> needs so that I'm happy.

1. List every INVEST letter this fails, and why.
2. Rewrite it as 2–3 properly-scoped stories that could actually go into
   a sprint.

## Check yourself

- Could someone unfamiliar with the Library Catalog project read your
  three review stories and know exactly what to build, without asking
  you any follow-up questions?
- For each acceptance criteria block you wrote, could a tester turn it
  directly into a test case?

## Stretch goal

Pick one of your three review stories and estimate it using the same
reasoning shown in `product-backlog/sprint-planning-example.md` §2 — not
just a number, but a sentence explaining *why* that number, referencing
what's in scope and out of scope for this sprint.
