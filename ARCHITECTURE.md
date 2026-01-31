# Architecture Overview

## Project Structure

The project follows Domain-Driven Design (DDD) principles and is organized into distinct layers:

- **Domain**: Contains the core business entities and logic (e.g., `Sale`, `User`, `SaleItem`).
- **Application**: Implements use cases, commands, queries, and business rules using MediatR for CQRS pattern.
- **ORM (Infrastructure)**: Handles data persistence using Entity Framework Core with PostgreSQL.
- **WebApi (Presentation)**: Exposes RESTful endpoints using ASP.NET Core Web API and Swagger for documentation.

## Key Technologies

- **.NET 8**: Modern, high-performance framework for building APIs.
- **Entity Framework Core**: ORM for database access and migrations.
- **PostgreSQL**: Relational database, containerized with Docker for portability.
- **MediatR**: Implements CQRS pattern, separating commands and queries for better maintainability.
- **FluentValidation**: Validates incoming requests and enforces business rules.
- **Docker & Docker Compose**: Containerize the API and database for consistent development and deployment environments.
- **Swagger**: Auto-generated API documentation and testing UI.

## Domain-Driven Design Highlights

- Entities like `Sale` and `SaleItem` encapsulate business logic and validation.
- Business rules for discounts and item limits are enforced in the Application layer.
- Repositories abstract data access, promoting loose coupling and testability.

## Database Migrations

- Migrations are managed in the ORM project.
- The `DefaultContext` DbContext applies entity configurations and manages the connection to PostgreSQL.
- Migrations are versioned and applied via CLI commands or Docker entrypoints to ensure database schema consistency.

## Error Handling and Validation

- ValidationBehavior pipeline ensures requests meet business rules before processing.
- Custom middleware captures exceptions and returns meaningful error responses.

## Future Improvements

- Add JWT authentication and authorization.
- Implement unit and integration tests.
- Enhance logging and monitoring.
- Develop a frontend client for user interaction.

---

# Summary

This architecture promotes separation of concerns, scalability, and maintainability, making it suitable for real-world enterprise applications.