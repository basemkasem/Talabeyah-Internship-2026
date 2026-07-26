# EShop — Internship Capstone

5-day incremental build: console app to Web API + SQL Server e-commerce backend.

## Branching workflow

- `main` — stable, only updated from `develop` at wrap-up.
- `develop` — daily trunk. Contains each day's winning PR.
- Interns: branch off latest `develop` each morning as `feature/dayN-<yourname>`, PR back into `develop` by end of day.

## Domain model (reference — build these across Day 1)

- `Product` (Id, Name, Description, Price, StockQuantity, CategoryId)
- `Category` (Id, Name, ParentCategoryId — nullable, subcategories)
- `Customer` (Id, Name, Email, PasswordHash)
- `Cart` / `CartItem`
- `Order` / `OrderItem` (Id, CustomerId, Status, TotalAmount, CreatedAt)

## Structure

- `src/EShop.Console` — Day 1–2 console app (becomes ASP.NET Core Web API on Day 3, `src/EShop.Api`).

## Day 1 tasks

1. Git & branching workflow (rebase, cherry-pick) — see task sheet.
2. Domain model with encapsulation, `Notification` hierarchy, `IDiscountable`.
3. SOLID refactor of `OrderProcessor`.
