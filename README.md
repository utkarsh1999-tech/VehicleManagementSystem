# Vehicle Management System

A full-stack Vehicle Management System developed using Angular 14 and ASP.NET Core Web API.

---

# Tech Stack

## Frontend
- Angular 14
- Bootstrap
- RxJS
- JWT Authentication
- Route Guards
- HTTP Interceptors

## Backend
- ASP.NET Core Web API
- JWT Authentication
- Repository Pattern
- Strategy Pattern
- Middleware Exception Handling

---

# Features

## Authentication
- JWT Login Authentication
- Role Based Authorization
- Admin & Manager Roles

## Car Model Module
- Add Car Model
- Edit Car Model
- Delete Car Model
- Search by Model Name
- Search by Model Code
- Sort by Manufacturing Date
- Image Upload Support
- CKEditor Integration

## Commission Module
- Dynamic Commission Calculation
- Brand Based Strategy Pattern
- Admin Only Access

## Security
- Angular Route Guards
- JWT Token Validation
- Role Based API Authorization

---

# Project Structure

```bash
VehicleManagementSystem
 ├── api
 └── UI
```

---

# Login Credentials

## Admin

```text
Username: admin
Password: 1234
```

Access:
- Car Models
- Commission Report

---

## Manager

```text
Username: manager
Password: 1234
```

Access:
- Car Models Only

---

# Run Backend

```bash
cd api
dotnet run
```

Backend URL:

```text
http://localhost:5000
```

---

# Run Frontend

```bash
cd UI
npm install
ng serve
```

Frontend URL:

```text
http://localhost:4200
```

---

# API Authentication

JWT token is automatically attached using Angular HTTP Interceptor.

---

# Design Patterns Used

- Repository Pattern
- Strategy Pattern
- Dependency Injection

---

# Author

Utkarsh Sharma
