# Prompt Refinement Case Study

A single realistic scenario, refined across four attempts, showing what
actually changes at each step and why. This mirrors how prompting really
works in practice — you rarely get the ideal prompt on the first try;
you notice what's missing from the output and add exactly that.

## Attempt 1 — too vague

**Prompt:**
> Write a function for orders.

**What you'd get back** — something plausible but generic, because
nothing in the prompt says what language, what "order" means, or what
operation on an order is even wanted:
```python
def process_order(order):
    order['status'] = 'processed'
    return order
```
Wrong language, wrong data shape, no business logic at all — this
response isn't *incorrect* given what it was told, it's just that it
was told almost nothing.

## Attempt 2 — add language, framework, and domain context

**Prompt:**
> In C#, write a method that places an order for a book in our library
> catalog system. The method should check that the book has enough
> stock before allowing the order.

**What you'd get back** — recognizably closer, but still guessing at
things a real prompt should specify:
```csharp
public Order PlaceOrder(int bookId, int quantity)
{
    var book = _books.FirstOrDefault(b => b.Id == bookId);
    if (book == null || book.Stock < quantity)
        throw new Exception("Cannot place order");

    book.Stock -= quantity;
    return new Order { BookId = bookId, Quantity = quantity };
}
```
Better, but: `throw new Exception(...)` is a generic exception type (bad
practice — see `security-and-ethics/ai-code-risks-checklist.md`), there's
no price/total calculation, and `_books` is invented with no idea whether
that's really how this codebase accesses book data.

## Attempt 3 — reference the actual existing code and dependencies

**Prompt:**
> In C#, write a method for our `OrdersService` microservice (see
> Module 7) that places an order. It should call an injected
> `ICatalogServiceClient` to look up the book by id (returns `null` if
> not found) and to reserve stock (returns `false` if there isn't
> enough). Throw specific, appropriate exception types rather than a
> generic `Exception`. The order should record the book's title and
> price at the time of the order.

**What you'd get back** — much closer to the real `OrdersController.cs`,
because the prompt now names the actual collaborator interface and its
real return-value contract instead of inventing one:
```csharp
public async Task<Order> PlaceOrderAsync(int bookId, int quantity)
{
    var book = await _catalogServiceClient.GetBookAsync(bookId);
    if (book is null)
        throw new InvalidOperationException($"Book with id {bookId} does not exist.");

    var reserved = await _catalogServiceClient.ReserveStockAsync(bookId, quantity);
    if (!reserved)
        throw new InvalidOperationException($"Insufficient stock for '{book.Title}'.");

    return new Order
    {
        BookId = book.Id,
        BookTitle = book.Title,
        Quantity = quantity,
        UnitPrice = book.Price,
        CreatedAtUtc = DateTime.UtcNow
    };
}
```
I compared this directly against the real `OrdersController.Create`
method in Module 7 — the logic sequence (look up book, check null,
reserve stock, check failure, construct the order with title/price
captured at order time) matches. The main remaining gap: the real code
also validates `quantity <= 0` before doing anything else, and returns
HTTP results (`BadRequest`/`Conflict`/`CreatedAtAction`) rather than
throwing exceptions, since it's a controller action, not a service
method called by one.

## Attempt 4 — close the remaining gap explicitly

**Prompt:**
> Same as before, but also validate that quantity is greater than zero
> before doing anything else, and structure it as an ASP.NET Core
> controller action returning `IActionResult` (`BadRequest`, `Conflict`,
> or `CreatedAtAction`) rather than throwing exceptions.

At this point the natural output converges almost exactly onto the real
`OrdersController.Create` method — which makes sense, since the prompt
now specifies essentially everything that method's signature and body
actually depend on: the collaborator's contract, the exception/response
strategy, and the validation order.

## What changed, attempt to attempt

| Attempt | What was added | What it fixed |
|---|---|---|
| 1 -> 2 | Language, framework, domain | Stopped guessing the language entirely |
| 2 -> 3 | The actual collaborator interface and its contract | Stopped inventing a data-access pattern that doesn't match the real code |
| 3 -> 4 | The exact validation order and response strategy | Closed the last gap between "plausible" and "matches this specific codebase" |

The general lesson: **vague output is almost always a signal about what
your prompt is missing, not a sign that the tool is bad at the task.**
Each refinement above was a direct response to a specific gap in the
previous output — not a random rewrite.
