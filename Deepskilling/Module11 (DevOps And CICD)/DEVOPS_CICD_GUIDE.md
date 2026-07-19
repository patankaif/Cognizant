# Module 11 – DevOps and CI/CD

Covers every learning objective in **Module 11** of the DN 5.0 Deep
Skilling handbook: DevOps concepts, CI/CD components, and the popular
tools used to implement them — paired with real pipeline configs written
against the actual projects built in Modules 4, 6, 7, and 8, not
hypothetical examples.

The `npm ci` and `ng build --configuration production` steps used in the
Angular pipelines were run against the real Module 8 project in this
sandbox to confirm they work exactly as written (see the Notes section at
the end for what I could and couldn't verify).

## 1. What is DevOps?

**DevOps** is a set of practices and a culture that combines **Dev**elopment
and IT **Op**erations, aiming to shorten the software delivery lifecycle
and deliver high-quality software continuously. It breaks down the
traditional wall between "the people who write the code" and "the people
who run it in production" — instead of throwing a finished build over the
fence to Ops, the same team owns build, test, deployment, and monitoring
end to end.

**Goals and benefits:**
- **Faster delivery** — smaller, more frequent releases instead of large,
  risky ones.
- **Improved collaboration** — shared ownership and shared tooling between
  developers, QA, and operations.
- **Higher quality, fewer failures** — automated testing and consistent
  environments catch problems before they reach production.
- **Faster recovery** — when something does break, small frequent changes
  make it far easier to identify which change caused it, and to roll back.
- **Automation over manual toil** — repetitive tasks (builds, tests,
  deployments, provisioning) are scripted and repeatable instead of
  done by hand, which also removes "it worked on my machine" variance.

**Key DevOps practices:**
- **Continuous Integration (CI)** — see §2.
- **Continuous Delivery / Deployment (CD)** — see §2.
- **Infrastructure as Code (IaC)** — defining servers, networks, and
  environments in version-controlled config files (e.g. the
  `docker-compose.yml` and Kubernetes manifests built in Module 7) instead
  of manually clicking through a cloud console.
- **Automated testing** — unit, integration, and end-to-end tests running
  automatically on every change (Module 4's NUnit/Moq tests, Module 8's
  Jasmine/Karma tests).
- **Monitoring and logging** — knowing a deployment is healthy (or isn't)
  without having to guess; see Module 7's `/health` endpoints and
  Serilog logging.
- **Configuration management** — keeping environment-specific settings
  (connection strings, feature flags, API keys) outside the codebase and
  consistent across environments.

## 2. Understanding CI/CD

**Continuous Integration (CI)** — developers merge their changes back to
a shared branch **frequently** (ideally several times a day), and every
merge automatically triggers a build and a test run. The goal is to catch
integration problems — two people's changes conflicting, or one change
breaking another part of the system — within minutes of it happening,
not weeks later when it's much harder to untangle.

**Continuous Delivery** — every change that passes CI is automatically
prepared for release (built, tested, packaged) and is *always in a
deployable state*, but the actual deployment to production requires a
manual approval step (a person clicks "deploy").

**Continuous Deployment** — the same as Continuous Delivery, but without
the manual gate: every change that passes all automated checks is
deployed to production automatically, with no human in the loop. This is
the most mature/aggressive end of the spectrum and requires a lot of
confidence in your automated test coverage.

| | Continuous Integration | Continuous Delivery | Continuous Deployment |
|---|---|---|---|
| Automatically build & test on every commit | Yes | Yes | Yes |
| Automatically package a release-ready artifact | — | Yes | Yes |
| Deploy to production automatically | — | — | Yes |
| Human approval required to release | N/A | Yes | No |

**Benefits of CI/CD:**
- Bugs are caught close to when they're introduced, when they're cheapest
  to fix.
- Releases become routine, low-drama events instead of rare, high-risk
  "release day" events.
- Manual, error-prone deployment steps are replaced with a repeatable,
  scripted process — the exact same steps run every time.
- Feedback loops shrink: a developer knows within minutes whether their
  change is good, not days later.

**A typical CI/CD pipeline, stage by stage** (this is the shape every
pipeline in `pipelines/` follows, just with different tool-specific
syntax):

```
Source  →  Build  →  Test  →  Package  →  Deploy  →  Monitor
(push/PR)  (compile)  (run     (produce    (ship to    (health checks,
                       tests)   an          an          logs, alerts —
                                artifact)   environment) Module 7 territory)
```

## 3. CI/CD Tools and Platforms

Four real pipelines live in `pipelines/`, each doing roughly the same
job (build → test → deploy) for one of our earlier modules, so you can
directly compare the syntax and philosophy of each tool side by side.

| Tool | Hosting | Config format | Notes |
|---|---|---|---|
| **Jenkins** | Self-hosted (you run the server) | Groovy DSL (`Jenkinsfile`) | Oldest and most flexible of the four; huge plugin ecosystem; you're responsible for maintaining the server itself. Popular in enterprises that need full control or have on-prem/air-gapped requirements. |
| **GitHub Actions** | Hosted by GitHub, built into the repo | YAML (`.github/workflows/*.yml`) | Zero infrastructure to manage; tightly integrated with GitHub (PRs, issues, releases); huge marketplace of reusable "actions". The natural default if your code is already on GitHub. |
| **GitLab CI/CD** | Hosted by GitLab (or self-hosted GitLab) | YAML (`.gitlab-ci.yml`, one file per repo) | Built directly into GitLab, similar philosophy to GitHub Actions; strong built-in support for Docker/Kubernetes deployments and its own container registry. |
| **CircleCI** | Cloud-hosted (or self-hosted "runners") | YAML (`.circleci/config.yml`) | Cloud-first, fast parallel execution, "orbs" (reusable config packages, similar concept to GitHub's marketplace actions). Works with any Git host, not just one platform. |

**How to read the comparison pipelines in this module:**
- `pipelines/github-actions/dotnet-nunit-ci.yml` and
  `pipelines/jenkins/Jenkinsfile` do the *same job* — restore, build, test,
  publish — for the Module 4 .NET solution. Compare how each expresses
  "run this shell command" and "only do this on the main branch."
- `pipelines/github-actions/angular-ci-cd.yml` and
  `pipelines/gitlab-ci/.gitlab-ci.yml` do the same job for the Module 8
  Angular app — install, test, build, deploy.
- `pipelines/circleci/config.yml` shows the same .NET pipeline again in
  CircleCI's syntax, including its distinctive `orbs`-free Docker-image
  based job definition and explicit dependency caching.

**Picking one, in practice:** if your code already lives on GitHub or
GitLab, use that platform's built-in CI (Actions or GitLab CI) — it's the
path of least friction, since you don't need a separate account, a
separate server, or to wire up webhooks by hand. Reach for Jenkins
specifically when you need something that platform-hosted CI can't do
easily: full control over the build server's environment, complex
multi-repo orchestration, or on-premises/air-gapped infrastructure.
CircleCI is a reasonable pick when you want a dedicated CI product that's
independent of whichever Git host you happen to use, or if your team
already has expertise there.

## Try it yourself

None of these pipelines need a real Jenkins/GitLab/CircleCI account to
*read and understand* — they're meant to be studied side by side. To
actually run one:

- **GitHub Actions**: copy a file from `pipelines/github-actions/` into
  `.github/workflows/` in a real GitHub repo containing the corresponding
  module's code, push, and watch it run in the "Actions" tab — no extra
  setup needed.
- **GitLab CI/CD**: copy `pipelines/gitlab-ci/.gitlab-ci.yml` to the root
  of a GitLab repo containing `Module8-Angular/`, push, and check the
  "CI/CD → Pipelines" section.
- **Jenkins**: requires a running Jenkins server with the .NET SDK
  plugin/tool configured (see the `tools { dotnet ... }` block) — point a
  Pipeline job at the repo and Jenkins will pick up the `Jenkinsfile`
  automatically if it's at the repo root.
- **CircleCI**: copy `pipelines/circleci/config.yml` to `.circleci/config.yml`
  at the repo root and connect the repo in the CircleCI dashboard.

## Notes

- I verified the exact shell commands used in the Angular pipelines
  (`npm ci` and `npx ng build --configuration production`) by running
  them against the real Module 8 project in this sandbox — both
  succeeded. I don't have the .NET SDK available here, so I couldn't
  run the equivalent `dotnet restore`/`build`/`test` commands, but they
  match exactly what was already verified working when Module 4 was
  built and tested via `dotnet test` there.
- I don't have live Jenkins, GitHub Actions, GitLab CI, or CircleCI
  environments available in this sandbox, so I couldn't trigger a real
  pipeline run end-to-end. I did validate that every YAML file parses
  correctly and checked the Jenkinsfile's Groovy syntax (brace/paren
  balance) by hand. Please try these against a real repo and let me
  know if any tool-specific detail needs adjusting for your exact
  Jenkins plugin versions or GitLab/CircleCI account setup.
- The `deploy` stages in every pipeline are intentionally left as a
  placeholder `echo`/comment — the actual deploy target (Azure, AWS, a
  Kubernetes cluster as in Module 7, an on-prem IIS server, etc.) is
  something only you can wire up, since it depends entirely on where
  you're actually hosting the application.
