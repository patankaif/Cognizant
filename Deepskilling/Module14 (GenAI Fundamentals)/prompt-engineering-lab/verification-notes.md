# Verification Notes

This file documents what was actually executed and checked while
building this module, so the claims made in the lab files aren't just
assertions.

## ISBN-13 checksum (used in `01-zero-shot-examples.md`)

Algorithm verified in Python before being presented as C#:

```python
def is_valid_isbn13(isbn):
    digits = isbn.replace("-", "").replace(" ", "")
    if len(digits) != 13 or not digits.isdigit():
        return False
    total = 0
    for i, ch in enumerate(digits):
        d = int(ch)
        total += d if i % 2 == 0 else d * 3
    return total % 10 == 0
```

Test run and actual output:
```
'978-0-306-40615-7'       -> True (expected True) [OK]
'9780306406157'           -> True (expected True) [OK]
'978-0-306-40615-8'       -> False (expected False) [OK]
'not-an-isbn'             -> False (expected False) [OK]
'978030640615'            -> False (expected False) [OK]
```
All 5 cases matched. The C# version in `01-zero-shot-examples.md`
implements the identical algorithm (same alternating 1x/3x weights, same
`% 10 == 0` check, same hyphen/space stripping) — I translated it after
confirming the logic itself was correct, not before.

## Chain-of-thought example (used in `03-chain-of-thought-examples.md`)

The "detect the longest run of consecutive discounted days" problem was
implemented and tested in Python first — see that file for the full
test run and output, included inline rather than duplicated here.

## Why Python first, then C#

This program is C#/.NET-heavy, but this sandbox doesn't have the .NET
SDK installed (confirmed when Module 1 was built), while Python is
available. Verifying algorithmic *logic* in Python and then translating
a confirmed-correct algorithm to idiomatic C# is a reasonable substitute
for compiling and running the C# directly — the risk in these examples
is almost always in the *logic* (off-by-one errors, wrong operator,
wrong edge case), not in C#-specific syntax, so this catches the
mistakes that actually matter. Where a detail is genuinely C#-specific
(nullability, LINQ method chaining, `async`/`await` usage), that's
called out explicitly in context rather than assumed correct.
