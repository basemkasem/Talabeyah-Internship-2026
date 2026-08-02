# EShop — Internship Capstone

Incremental build: console app → Web API + SQL Server e-commerce backend.

See [Tasks](#tasks) below for the full task list and acceptance criteria.

## Prerequisites

- JetBrains Rider (installed/licensed or trial)
- .NET SDK
- SQL Server

## Repo setup

- Clone this repo (do not fork). `main` and `develop` branches already exist.

## Workflow

Everyone converges on one winning PR each round — a competition and a shared build.

1. Each round, everyone starts from the current `develop` (already contains the previous round's winning PR — except Round 1, where it's just this skeleton).
2. Create a fresh branch off latest `develop`: `feature/round<N>-<yourname>`.
3. At the end of the round, open a PR from your branch → `develop`.
4. PRs are judged against that round's acceptance criteria; one winner merges into `develop`.
5. Before the next round starts, everyone pulls the updated `develop` and branches off it again — including non-winners, who continue on top of the winning implementation rather than their own.

If you didn't win a round, your next branch is based on someone else's code, not yours. Skim the merged PR's diff before starting the next round's task.

## Judging order

1. Meets acceptance criteria (baseline — must pass to be eligible)
2. Clean code: clear variable/method names, SOLID, OOP principles
3. Tiebreaker: extra effort beyond the task requirement (e.g. added a test, handled an edge case)

## Scoring

- Winning PR owner: 1 point per round.
- Best non-merged attempt: 0.5 consolation point.
- Running leaderboard kept visible (spreadsheet / pinned Slack message).

## Domain model (reference — used every round)

- `Product` (Id, Name, Description, Price, StockQuantity, CategoryId)
- `Category` (Id, Name, ParentCategoryId — nullable, subcategories)
- `Customer` (Id, Name, Email, PasswordHash)
- `Cart` / `CartItem`
- `Order` / `OrderItem` (Id, CustomerId, Status, TotalAmount, CreatedAt)

## Structure

- `src/EShop.Console` — Round 1–2 console app (becomes ASP.NET Core Web API on Round 3, `src/EShop.Api`).
- `Entities/` — domain model classes (`Product`, `Category`, `Customer`, `Cart`, `Order`, ...).
- `Repositories/` — data access only. One interface + implementation per entity (e.g. `IExampleRepository` / `InMemoryExampleRepository`). No business rules here — just get/add/update.
- `Services/` — business logic. One interface + implementation per entity (e.g. `IExampleService` / `ExampleService`). Depends on a repository via constructor injection, applies validation/rules, then calls the repository.

`Entities/Example.cs`, `Repositories/IExampleRepository.cs` + `InMemoryExampleRepository.cs`, and `Services/IExampleService.cs` + `ExampleService.cs` are a worked example of this pattern — follow it for the real entities.
