# Module 11 – DevOps and CI/CD

Covers every learning objective in **Module 11** of the DN 5.0 Deep
Skilling handbook: DevOps concepts, CI vs. CD, and the four CI/CD tools
the handbook names by name — Jenkins, GitHub Actions, GitLab CI/CD, and
CircleCI.

**Start with `DEVOPS_CICD_GUIDE.md`** — that's the main content. The
`pipelines/` folder holds real, working pipeline configs for each tool,
written against the actual .NET (Module 4) and Angular (Module 8)
projects from earlier in this program, so you can compare how the same
build → test → deploy job looks in each tool's syntax.

## Contents

```
DEVOPS_CICD_GUIDE.md              main guide: DevOps, CI/CD, tool comparison
pipelines/
  github-actions/
    dotnet-nunit-ci.yml           builds & tests Module 4 (.NET/NUnit/Moq)
    angular-ci-cd.yml             tests, builds & deploys Module 8 (Angular)
  jenkins/
    Jenkinsfile                   same job as dotnet-nunit-ci.yml, in Jenkins' Groovy DSL
  gitlab-ci/
    .gitlab-ci.yml                same job as angular-ci-cd.yml, in GitLab's YAML
  circleci/
    config.yml                    same job as dotnet-nunit-ci.yml, in CircleCI's YAML
```

## Verification

- Every YAML pipeline file was parsed with a YAML parser to confirm it's
  syntactically valid.
- The Jenkinsfile's brace/paren balance was checked by hand (it's Groovy,
  not YAML, so a YAML parser doesn't apply).
- The two shell commands the Angular pipelines actually run — `npm ci`
  and `npx ng build --configuration production` — were run for real
  against the Module 8 project in this sandbox and both succeeded.
- I don't have the .NET SDK, or live Jenkins/GitHub/GitLab/CircleCI
  environments, available here, so the .NET pipelines and the actual
  triggering of any pipeline could not be end-to-end verified. See the
  Notes section in `DEVOPS_CICD_GUIDE.md` for specifics.
