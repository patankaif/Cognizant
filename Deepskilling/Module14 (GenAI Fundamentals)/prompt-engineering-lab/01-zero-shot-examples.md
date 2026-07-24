# Zero-Shot Prompting

Zero-shot prompting means asking an AI assistant to do a task with no
examples — just an instruction. It's the fastest way to prompt, and
works well for well-known, unambiguous tasks. It works less well for
tasks with a specific style, format, or edge case that the model has no
way to guess at.

## Example: a well-known algorithm

**Prompt (what you'd type to Copilot Chat or as a comment above a
function):**
> Write a C# method that validates whether a given ISBN-13 string has a
> correct checksum digit. It should ignore hyphens and spaces in the
> input.

**Generated code:**
```csharp
public static bool IsValidIsbn13(string isbn)
{
    if (string.IsNullOrWhiteSpace(isbn))
        return false;

    var digits = isbn.Replace("-", string.Empty).Replace(" ", string.Empty);

    if (digits.Length != 13 || !digits.All(char.IsDigit))
        return false;

    int sum = 0;
    for (int i = 0; i < 13; i++)
    {
        int digit = digits[i] - '0';
        sum += (i % 2 == 0) ? digit : digit * 3;
    }

    return sum % 10 == 0;
}
```

**Why this worked well as zero-shot:** ISBN-13 validation is a
*standardized, well-documented algorithm* — there's really only one
correct way to implement it, so there's nothing ambiguous for the model
to guess wrong about. I verified this exact algorithm (checking the
`% 10 == 0` logic, the alternating 1x/3x weights, and the hyphen/space
handling) against 5 test cases — including the classic textbook example
`978-0-306-40615-7` — before writing this file; see
`../prompt-engineering-lab/verification-notes.md` for the actual test
run. Zero-shot is a good fit exactly *because* the task was unambiguous
enough that verification was straightforward.

## Example: where zero-shot falls short

**Prompt:**
> Write a method to calculate the discount for an order.

**A plausible zero-shot response:**
```csharp
public decimal CalculateDiscount(decimal orderTotal)
{
    if (orderTotal > 100)
        return orderTotal * 0.1m;
    return 0;
}
```

This *compiles* and *looks* reasonable, but notice everything it had to
guess at with no basis: What discount tiers actually exist? Does
customer type matter (see Module 6's `IDiscountService`, which takes a
`customerType` parameter — this zero-shot version ignores that entirely)?
Is the threshold really $100? Should it return a percentage or an
amount? A zero-shot prompt with no context about *your* actual domain
will confidently produce plausible-looking code that doesn't match your
real business rules — this is exactly why the next technique, few-shot
prompting, exists.

## When to reach for zero-shot

- Well-known algorithms and data structures (sorting, searching, common
  string/date parsing).
- Boilerplate you'd write the same way every time (a DTO class from a
  list of fields, a basic CRUD controller skeleton).
- Quick "explain this code to me" or "what does this regex do" requests.

## When to reach for something else

- Anything depending on *your* project's specific conventions, business
  rules, or existing code style — see `02-few-shot-examples.md`.
- Multi-step logic where getting the reasoning right matters more than
  getting *an* answer fast — see `03-chain-of-thought-examples.md`.
