# AuthDemo

A full-stack user authentication application built with C# and ASP.NET Core.

The application provides user registration and login functionality, with user data stored in SQL Server and passwords securely hashed using ASP.NET Core Identity.

## How to Run

1) Make sure you have Docker Desktop installed.
2) Run the following command from the project folder:
    `docker compose up --build -d`
3) Visit the main page at:
    `http://localhost:5280/`

You can access the Swagger dashboard at:
    `http://localhost:5280/swagger`

## Features

- User registration and login
- Password hashing with ASP.NET Core Identity
- REST API built with ASP.NET Core
- Entity Framework Core database operations
- SQL Server database
- Swagger API documentation
- Docker Compose setup for the API and database

## Tech Stack

**Frontend**
- HTML
- CSS
- JavaScript

**Backend**
- C#
- ASP.NET Core
- Entity Framework Core
- ASP.NET Core Identity

**Database**
- SQL Server

**Tools**
- Docker
- Docker Compose
- Swagger
- Git

## Notes

While this project was initially created to familiarize myself with the stack, I am planning to expand on it in the future. I would love to create a music player software out of it.