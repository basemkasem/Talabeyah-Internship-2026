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

1. Each round, everyone starts from the current `main` (already contains the previous round's winning PR — except Round 1, where it's just this skeleton).
2. Create a fresh branch off latest `main`: `feature/round<N>-<yourname>`.
3. At the end of the round, open a PR from your branch → `main` and `develop`.
4. PRs are judged against that round's acceptance criteria; one winner merges into `develop`.
5. Before the next round starts, everyone pulls the updated `main` and `develop` and branches off it again — including non-winners, who continue on top of the winning implementation rather than their own.

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

`Entities/Example.cs`, `Repositories/IExampleRepository.cs` + `InMemoryExampleRepository.cs`, and `S# Task 1 — Git/GitHub, OOP & SOLID

## Task 1.1 — Repo Setup & Branching (Git/GitHub)

Clone the shared `eshop` repository. The repository is an empty skeleton with `main` and `develop` branches already created.

### Requirements

* Clone the repository.
* Checkout `main`.
* Create a feature branch:

  ```text
  feature/round1-<yourname>
  ```

  based on `main`.
* Commit the domain model in small, logical commits.

  * Minimum: **one entity per commit**.
* Implement one entity (e.g. `Category`) on a separate branch:

  ```text
  feature/round1-<yourname>-category
  ```
* Commit the `Category` implementation on that branch.
* Cherry-pick the `Category` commit into your main feature branch.
* Open a Pull Request into:

  * `main`
  * `develop`
* Add a clear description to the PR. This PR is your competition entry.

### Acceptance Criteria

* The PR is open.
* The cherry-picked commit is visible in the Git log.

---

## Task 1.2 — Domain Model with OOP (OOP)

Model the following domain entities as C# classes:

* `Product`
* `Category`
* `Customer`
* `Cart`
* `Order`

### Requirements

Apply proper OOP principles:

* Use constructors to guarantee valid object creation.
* Use encapsulation.
* Control state mutation through meaningful methods.
* Avoid exposing raw mutable state.
* Keep responsibilities within the appropriate entities.

### Notification Hierarchy

Create an abstract `Notification` base class containing:

```csharp
SendConfirmation()
```

Create two subclasses:

* `EmailNotification`
* `SmsNotification`

For now, both implementations should be stubs that simply use:

```csharp
Console.WriteLine(...)
```

No real email or SMS sending is required.

### ISummarizable

Create an interface:

```csharp
ISummarizable
```

It should define a `Summarize` method.

Implement `ISummarizable` in different entities where it makes sense.

### Acceptance Criteria

* Entities demonstrate proper OOP principles.
* Encapsulation is properly applied.
* Inheritance and polymorphism are meaningful rather than artificial.
* Responsibilities are appropriately distributed.
* The design follows SOLID principles.

---

## Task 1.3 — SOLID 

Implement an `OrderProcessor` responsible for placing an order.

The order-processing workflow should:

1. Validate stock.
2. Calculate the order total.

   * Apply any applicable discount.
3. Save the order.
4. Send a confirmation notification.

### Requirements

Design the supporting class structure yourself.

Apply SOLID principles throughout the implementation.

`OrderProcessor` must:

* Depend on abstractions rather than concrete implementations.
* Receive its dependencies through **constructor injection**.
* Avoid directly creating infrastructure dependencies such as repositories or notification implementations.

For example, dependencies should be represented through interfaces/abstractions where appropriate.

### Acceptance Criteria

* SOLID principles are clearly applied.
* `OrderProcessor` depends on abstractions, not concrete classes.
* Dependencies are provided through constructor injection.
* Each component has a clear and focused responsibility.
* The design is easy to extend and test.
ervices/IExampleService.cs` + `ExampleService.cs` are a worked example of this pattern — follow it for the real entities.

