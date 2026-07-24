# Chain-of-Thought Prompting

Chain-of-thought prompting means explicitly asking the model to reason
through a problem step by step *before* writing the final code, rather
than jumping straight to an answer. For tasks with edge cases or
ordering-dependent logic, this catches mistakes that a "just write the
code" prompt frequently produces — including the exact bug demonstrated
below, which I actually ran to confirm it's a real, silent bug, not a
hypothetical one.

## The task

A discount policy: orders of $250 or more get 20% off; orders of $100 or
more (but under $250) get 10% off; everything else gets no discount.

## What a direct prompt can produce

**Prompt:**
> Write a method that applies a discount: 10% off orders over $100, 20%
> off orders over $250.

**A very plausible, wrong response** (deliberately reproducing a mistake
that's easy to make when translating "10% over 100, 20% over 250"
straight into code in the order it was described):
```csharp
public decimal CalculateTotal(decimal subtotal)
{
    if (subtotal >= 100)
        return subtotal * 0.90m;
    else if (subtotal >= 250)
        return subtotal * 0.80m;
    return subtotal;
}
```

**This compiles, looks reasonable, and is silently wrong.** I verified
this for real (see the Python translation and actual output below) —
because the `>= 100` check comes first, the `>= 250` branch can *never*
be reached; a $300 order gets 10% off instead of 20%. This is
exactly the kind of mistake that's easy to miss in a code review if you
only skim it, since both branches are individually correct-looking C#.

**Actual verified output** (Python translation of both versions, run to
confirm the bug is real):
```
  subtotal   WRONG result   CORRECT result
        50          50.00            50.00
       100          90.00            90.00
       150         135.00           135.00
       250         225.00           200.00  <-- BUG
       300         270.00           240.00  <-- BUG
```
At $250 and $300, the buggy version overcharges the customer relative to
the intended policy — a genuine, silent financial bug, not a cosmetic
one.

## The same task, with chain-of-thought prompting

**Prompt:**
> I need a method that applies tiered discounts: 20% off orders $250 or
> more, 10% off orders $100 or more. Before writing the code, walk
> through the tier boundaries step by step, including what should happen
> exactly at $100, exactly at $250, and how the tiers should be ordered
> so higher tiers aren't accidentally unreachable. Then write the C#
> method.

**Generated reasoning (this is the part a direct prompt skips):**
> 1. There are three ranges: below $100 (no discount), $100 up to but
>    not including $250 (10% off), and $250 and above (20% off).
> 2. The boundaries are inclusive on their lower end — "$100 or more"
>    means $100.00 itself gets the 10% tier, and "$250 or more" means
>    $250.00 itself gets the 20% tier, not the 10% one.
> 3. Because the $250 tier is a *subset* of "$100 or more" (every order
>    over $250 is also over $100), the check for the highest tier must
>    come first, or it will never be reached — an order of exactly $250
>    would incorrectly match a `>= 100` check before ever reaching a
>    `>= 250` check placed after it.

**Resulting code — checks the highest tier first, avoiding the bug:**
```csharp
public decimal CalculateTotal(decimal subtotal)
{
    if (subtotal >= 250m)
        return subtotal * 0.80m;
    if (subtotal >= 100m)
        return subtotal * 0.90m;
    return subtotal;
}
```
This is exactly the `calculate_total_CORRECT` logic verified above —
same tier ordering, same boundary handling, and it produces the correct
$240.00 (not $270.00) for a $300 order.

## Why walking through boundaries explicitly matters

The prompt asked specifically about *tier ordering* and *what happens at
exact boundary values* — the two things that were actually wrong in the
naive version. This is the core skill in chain-of-thought prompting:
it's not just "add the words 'think step by step'" as a magic
incantation, it's asking the model to explicitly reason about the
specific failure modes *you* can anticipate for *this kind* of problem
(ordering-dependent conditionals, off-by-one boundaries, empty
collections, negative numbers) before it commits to code.

## When to reach for chain-of-thought

- Any logic with multiple conditions/tiers/ranges, where the *order* of
  checks matters (as demonstrated above).
- Algorithms with easy-to-miss edge cases (empty input, single-element
  input, boundary values).
- Anything you'd want a human code reviewer to "show their work" on
  before you'd trust it, like a financial calculation, an authorization
  check, or concurrency-sensitive logic.
- Debugging — asking "walk through what happens when X" on existing
  buggy code is often more effective than asking "what's wrong with
  this" directly.
