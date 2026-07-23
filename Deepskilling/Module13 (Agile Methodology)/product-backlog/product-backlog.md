# Product Backlog — Library Catalog System

This backlog is deliberately written for the same "Library Catalog"
domain used across Modules 5–8 (EF Core, Web API, Microservices,
Angular) — think of it as the backlog a real product team would have
written *before* any of that code existed. Every story below is small
enough to fit in one sprint, independently valuable, and testable — the
"I" and "T" of INVEST (see `AGILE_GUIDE.md` §4).

Story points use a Fibonacci-like scale (1, 2, 3, 5, 8, 13) — the
growing gaps between numbers reflect how estimation confidence naturally
gets fuzzier for bigger work, which is the whole reason Planning Poker
uses this scale instead of linear numbers.

## Epic 1: Book Catalog

### US-101: Browse the book catalog
**As a** reader
**I want** to see a list of all books in the catalog
**So that** I can discover what's available to read

**Acceptance Criteria:**
```
Given I am on the book catalog page
When the page loads
Then I see a list of books, each showing its title, author, and price

Given the catalog has no books yet
When I view the catalog page
Then I see a friendly "no books found" message instead of an empty page
```
**Story Points:** 3
**Priority:** High

---

### US-102: Search the catalog by title or author
**As a** reader
**I want** to search for books by title or author
**So that** I can quickly find a specific book instead of scrolling through everything

**Acceptance Criteria:**
```
Given I am on the book catalog page
When I type part of a book's title into the search box
Then only books whose title or author matches my search remain visible

Given I have typed a search term with no matches
When the search results update
Then I see a "no books match your search" message
```
**Story Points:** 3
**Priority:** High

---

### US-103: View full details of a single book
**As a** reader
**I want** to open a book and see its full details
**So that** I can decide whether I want to read it

**Acceptance Criteria:**
```
Given I click on a book in the catalog list
When the book detail page loads
Then I see its title, author, published year, price, and genre

Given I navigate directly to a book detail URL for a book that doesn't exist
When the page loads
Then I see a "book not found" message instead of an error
```
**Story Points:** 2
**Priority:** Medium

---

### US-104: Add a new book to the catalog (Admin)
**As an** admin
**I want** to add a new book to the catalog
**So that** readers can discover and order it

**Acceptance Criteria:**
```
Given I am logged in as an admin
When I fill out the "Add Book" form with valid details and submit
Then the book appears in the catalog immediately

Given I submit the "Add Book" form with a missing title
When I try to submit
Then I see a validation error and the book is not created

Given I am logged in as a reader (not an admin)
When I try to access the "Add Book" page
Then I am redirected away and cannot add a book
```
**Story Points:** 5
**Priority:** Medium
**Notes:** Depends on US-201 (login) being done first.

---

## Epic 2: Accounts & Access

### US-201: Log in to my account
**As a** registered user
**I want** to log in with my username and password
**So that** I can access features specific to my role

**Acceptance Criteria:**
```
Given I enter a valid username and password
When I submit the login form
Then I am logged in and redirected to the catalog page

Given I enter an incorrect password
When I submit the login form
Then I see an "invalid username or password" message and remain logged out

Given I am not logged in
When I try to visit a page that requires login
Then I am redirected to the login page
```
**Story Points:** 5
**Priority:** High

---

### US-202: Log out
**As a** logged-in user
**I want** to log out
**So that** my session ends and someone else using this device can't act as me

**Acceptance Criteria:**
```
Given I am logged in
When I click "Log Out"
Then my session ends and I am shown as logged out immediately
```
**Story Points:** 1
**Priority:** Medium

---

## Epic 3: Orders

### US-301: Place an order for a book
**As a** reader
**I want** to order a book
**So that** I can purchase it

**Acceptance Criteria:**
```
Given a book is in stock
When I submit an order for it with a valid quantity
Then the order is created and the book's stock is reduced by that quantity

Given a book does not have enough stock for the quantity I requested
When I submit the order
Then I see an "insufficient stock" message and no order is created

Given I submit an order for a book that doesn't exist
When I submit the order
Then I see an error and no order is created
```
**Story Points:** 8
**Priority:** High
**Notes:** Depends on US-101 (needs a book to exist) and, if we want
purchase history tied to a person, US-201.

---

### US-302: View my order history
**As a** reader
**I want** to see a list of orders I've placed
**So that** I can track what I've bought

**Acceptance Criteria:**
```
Given I have placed at least one order
When I view my order history page
Then I see each order's book title, quantity, and total price

Given I have never placed an order
When I view my order history page
Then I see a friendly empty state instead of a blank page
```
**Story Points:** 3
**Priority:** Medium

---

## Epic 4: Favorites

### US-401: Mark a book as a favorite
**As a** logged-in reader
**I want** to mark a book as a favorite
**So that** I can easily find it again later

**Acceptance Criteria:**
```
Given I am logged in and viewing the catalog
When I click "Add to Favorites" on a book
Then the button changes to show it's favorited, and the book appears on my Favorites page

Given I am not logged in
When I try to access the Favorites page
Then I am redirected to the login page
```
**Story Points:** 3
**Priority:** Low

---

### US-402: Remove a book from my favorites
**As a** logged-in reader
**I want** to remove a book from my favorites
**So that** my favorites list only shows books I still care about

**Acceptance Criteria:**
```
Given a book is in my favorites list
When I click "Remove" on it
Then it disappears from my favorites list immediately
```
**Story Points:** 2
**Priority:** Low

---

## Backlog summary (ordered by priority)

| ID | Story | Points | Priority |
|---|---|---|---|
| US-101 | Browse the book catalog | 3 | High |
| US-102 | Search the catalog | 3 | High |
| US-201 | Log in | 5 | High |
| US-301 | Place an order | 8 | High |
| US-103 | View book details | 2 | Medium |
| US-104 | Add a new book (Admin) | 5 | Medium |
| US-202 | Log out | 1 | Medium |
| US-302 | View order history | 3 | Medium |
| US-401 | Mark a favorite | 3 | Low |
| US-402 | Remove a favorite | 2 | Low |

**Total backlog size:** 35 story points across 10 stories.

See `sprint-planning-example.md` for how a team would actually pull from
this ordered list into a sprint, using a measured velocity.
