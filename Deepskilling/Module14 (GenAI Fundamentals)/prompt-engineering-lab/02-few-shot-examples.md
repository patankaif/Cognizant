# Few-Shot Prompting

Few-shot prompting means giving the AI assistant one or more **examples**
of the exact pattern you want, before asking it to produce a new one.
This is how you get project-specific style and conventions out of a
general-purpose model — it can't read your team's mind, but it's very
good at pattern-matching once you show it two or three real examples.

## Example: generating a test in your project's exact style

Recall `CalculatorTests.cs` from Module 4 — it has a very specific,
consistent style: `[TestCase]` attributes for parameterized inputs,
`Assert.That(result, Is.EqualTo(expected))` rather than
`Assert.AreEqual`, and a `_calculator` field set up in `[SetUp]`.

**A weak, zero-shot prompt:**
> Write a unit test for a Multiply method.

Without any example, the model has no way to know you use NUnit instead
of xUnit/MSTest, `Assert.That(...)` instead of `Assert.AreEqual(...)`,
or `[TestCase]` instead of a `[Theory]`/`[InlineData]` pair (that's
xUnit's syntax, not NUnit's) — it'll guess, and there's a real chance it
guesses a different testing framework's conventions entirely.

**A few-shot prompt, giving two real examples from the existing file:**
> Here are two existing test methods from our `CalculatorTests.cs` file:
>
> ```csharp
> [TestCase(2, 3, 5)]
> [TestCase(-2, 2, 0)]
> [TestCase(0, 0, 0)]
> public void Add_ReturnsSum(decimal a, decimal b, decimal expected)
> {
>     var result = _calculator.Add(a, b);
>     Assert.That(result, Is.EqualTo(expected));
> }
>
> [TestCase(10, 2, 5)]
> [TestCase(9, 3, 3)]
> public void Divide_ReturnsQuotient(decimal a, decimal b, decimal expected)
> {
>     Assert.That(_calculator.Divide(a, b), Is.EqualTo(expected));
> }
> ```
>
> Following this exact same style, write a test method for `Multiply`.

**Generated output — matches the established pattern exactly:**
```csharp
[TestCase(2, 3, 6)]
[TestCase(-2, 4, -8)]
[TestCase(0, 5, 0)]
public void Multiply_ReturnsProduct(decimal a, decimal b, decimal expected)
{
    Assert.That(_calculator.Multiply(a, b), Is.EqualTo(expected));
}
```

Notice it picked up on a subtle detail from the second example rather
than the first: `Divide_ReturnsQuotient` calls `_calculator.Divide(...)`
directly inside `Assert.That(...)` without a separate `result` variable,
while `Add_ReturnsSum` uses an intermediate variable. The generated
`Multiply` test followed the more recent/second style — which is a real
behavior worth knowing about few-shot prompting: when your examples
aren't perfectly consistent with each other, the model has to pick one,
and it won't always pick the one you'd have picked. The fix is the same
fix you'd use with a human reviewer: be consistent in the examples you
give it, or explicitly say which convention to follow if they differ.

## Example: matching a domain-specific response shape

**Few-shot prompt, from the real error responses in this program's Web
API projects:**
> Our API returns errors in this exact shape:
> ```json
> { "message": "Book with id 5 was not found." }
> ```
> ```json
> { "message": "Quantity must be greater than zero." }
> ```
> Write the error response body for: "an order was submitted for a book
> that has insufficient stock."

**Generated output:**
```json
{ "message": "Insufficient stock for 'Foundation'." }
```

I checked this against the actual codebase rather than assuming it
matched: `OrdersController.cs` in Module 7's `OrdersService` really does
return exactly `{ message = $"Insufficient stock for '{book.Title}'." }`
(interpolating the book's title, same as `'Foundation'` here) — so this
few-shot example genuinely reproduces the project's real convention, not
a plausible-looking guess. Both of the example messages given in the
prompt are also verified real strings, pulled directly from
`BooksController.cs` and `OrdersController.cs` in Module 6/7 — see
`verification-notes.md` for exactly how I confirmed this (I grepped the
real files rather than relying on memory, since an early draft of this
example used an invented message text that didn't actually exist
anywhere in the codebase — a good reminder that it's just as easy for
*me* to hallucinate a plausible-sounding detail as it is for any other
LLM, which is exactly why this whole module emphasizes verifying
AI-generated output rather than trusting it by default).

## When to reach for few-shot

- Matching an existing codebase's specific conventions (naming, test
  style, error shapes, comment style).
- Any time zero-shot has already produced something *close* but not
  quite matching your style — showing 1-2 corrected examples is often
  faster than describing the style rules in prose.
- Generating multiple similar items (several DTOs, several validation
  methods) where consistency across all of them matters.
