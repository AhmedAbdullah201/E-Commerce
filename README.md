# 🛒 E-Commerce Backend API – ASP.NET Core (.NET 8)

This is a scalable, feature-rich **E-Commerce backend system** built using **ASP.NET Core**, designed following **Clean Architecture** and **Domain-Driven Design (DDD)** principles. The API supports full order management, secure authentication, and efficient data handling with a production-ready architecture.

---

## 🚀 Features

- ✅ **Clean Architecture** + **Domain-Driven Design (DDD)**
- ✅ **JWT Authentication** with Role-Based Authorization
- ✅ **Basket to Checkout to Payment** Order Flow
- ✅ **CRUD Operations** for Products, Brands, and Types
- ✅ **Redis Caching** for High Performance ⚡
- ✅ **Repository + Unit of Work Patterns**
- ✅ **Specification Pattern** for Flexible Filtering
- ✅ **AutoMapper** for DTO Mapping
- ✅ **Centralized Exception Handling**
- ✅ **Stripe Integration** for Secure Payments

---

## 🧰 Tech Stack

- **Backend:** ASP.NET Core (.NET 8)
- **ORM:** Entity Framework Core
- **Database:** Microsoft SQL Server
- **Caching:** Redis
- **Authentication:** JWT (JSON Web Token)
- **Payment:** Stripe
- **Utilities:** AutoMapper, Swagger, FluentValidation

---

## 📁 Project Structure

The project follows Clean Architecture with clearly separated layers:

- `API`: Presentation layer (Controllers, Middleware, etc.)
- `Application`: Business logic and interfaces
- `Domain`: Core entities and value objects
- `Infrastructure`: Database, Repositories, External Services (Stripe, Redis)
- `Persistence`: EF Core DbContext and Configurations

---

## 🔐 Authentication & Authorization

- Implements **JWT-based Authentication**
- Supports **Role-Based Authorization** for admin/user roles
- Secure endpoints with `Authorize` attributes

---

## 💳 Payment Integration

- Integrated with **Stripe API** for real payment processing
- Handles customer payment intent creation and order finalization

---

## 🛒 Order Workflow

1. Add items to **Basket**
2. Proceed to **Checkout**
3. Process payment via **Stripe**
4. Save Order and trigger response

---

## 🔄 Caching

- Utilizes **Redis** to cache product data
- Reduces DB load and improves response times

---

## 🚦 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [Redis](https://redis.io/)
- [Stripe Account](https://stripe.com/)

