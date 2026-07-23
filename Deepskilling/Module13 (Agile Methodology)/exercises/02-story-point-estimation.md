# Exercise 2 — Story Point Estimation (Planning Poker)

## Setup

You'll need at least 3 people for a real Planning Poker session (can be
done solo by role-playing multiple perspectives, but it's much more
useful with real people who genuinely see the work differently).

Cards: 1, 2, 3, 5, 8, 13, 20, 40, 100, ? (unknown/needs more info), ☕
(let's take a break).

## Part A — run a real session

Using the three review-feature stories you wrote in Exercise 1 (or the
US-401/US-402 favorites stories from the product backlog, if you didn't
do Exercise 1):

1. Read the story and its acceptance criteria aloud to the group.
2. Everyone privately picks a card — no discussion yet, no looking at
   others' choices.
3. Reveal all cards at the same time.
4. If everyone's within one step of each other on the scale (e.g. 3 and
   5), just average/round and move on.
5. If there's a big spread (e.g. 2 and 13, as in the Sprint Planning
   example's Round 1), the highest and lowest estimator each briefly
   explain their reasoning — often the gap is really a **scope**
   disagreement (someone assumed something's in scope that isn't), not
   an estimation skill difference.
6. Re-vote. Repeat until converged or the team agrees to split the story
   because it's clearly too big/uncertain to estimate as one piece.

## Part B — reflect on why the gap happened

For any round where estimates diverged significantly, write 2–3
sentences: what assumption was different between the high and low
estimators? Compare this to the real example in
`product-backlog/sprint-planning-example.md` §2, where the 13-point
estimate included handling a race condition that the 5-point estimate
didn't.

## Part C — relative sizing warm-up

Before estimating anything from your own backlog, calibrate as a group
using the *already-estimated* stories in `product-backlog.md` as
reference points:
- US-202 (Log out) = 1 point — the smallest, simplest thing in the
  backlog.
- US-301 (Place an order) = 8 points — one of the largest.

For any new story, ask "is this closer to the Log Out story, or closer
to the Place an Order story?" before picking an exact number. This is
often faster and more accurate than estimating from nothing, because
you're comparing to something the whole team already has a shared
understanding of.

## Check yourself

- Did discussion ever change someone's estimate after Round 1? What did
  they learn that they didn't know before speaking up?
- Story points measure *effort/complexity/uncertainty*, not just raw
  time. Can you think of a task that would take a long time but still
  deserve a *low* point value (simple, just tedious)? Can you think of
  one that's fast to build but deserves a *high* point value (small
  amount of code, but very risky/uncertain)?

## Stretch goal

Try estimating the same story twice, a week apart, without looking at
your first answer. How close are the two estimates? Perfect
repeatability isn't the goal (some drift is normal and expected) — but
wildly different answers on the same story with the same information is
a sign the team needs more calibration practice, similar to Part C.
