# My Blog API

A simple backend API built with ASP.NET Core, EF Core, and SQL Server. Supports CRUD operations for Users, Posts, and Comments.

## Features
- RESTful API endpoints for Users, Posts, Comments
- Entity Framework Core with SQL Server
- Database schema managed via migrations
- Interactive API documentation with Swagger

## Tech Stack
- ASP.NET Core Web API (.NET 7)
- Entity Framework Core
- SQL Server Express / LocalDB
- Swashbuckle (Swagger)

## Getting Started

### Prerequisites
- .NET SDK 7.0+
- SQL Server Express or LocalDB

### Setup
1. Clone the repo:
```bash  
git clone https://github.com/chrs1x/BlogApi.git
cd BlogApi
```
3. Configure the database in appsettings.json.
4. Run migrations:
```bash
dotnet ef migrations add Init
dotnet ef database update
```
4. Launch the API:
```bash
dotnet run
```
5. Navigate to https://localhost:7119/swagger to test endpoints.

## Project Structure
/Controllers - API endpoints  
/Models - Domain models & DTOs
/Services - Business logic  
/Data - EF Core DbContext  
/Migrations - EF Core migrations  
/Middleware - Error Handling </br>
/Properties - Project Settings </br>
Program.cs, appsettings.json

## API Endpoints
### Users

GET  `/api/users` - Get all users

GET  `/api/users/{id}` - Get user by ID

POST  `/api/users` - Create a new user

PUT  `/api/users/{id}` - Update an existing user

GET  `/api/users/search` - Search for a user by name/email

DELETE  `/api/users/{id}` - Delete a user

### Posts

GET `/api/posts` - Get all posts

GET `/api/posts/{id}` - Get post by ID

GET `/api/posts/user/{userId}` - Get all posts by a specific user

POST `/api/posts` - Create a new post

PUT `/api/posts/{id}` - Update an existing post

GET `/api/posts/search` - Search for a post by title/content

DELETE `/api/posts/{id}` - Delete a post

### Comments

GET `/api/comments` - Get all comments

GET `/api/comments/{id}` - Get comment by ID

GET `/api/comments/posts/{postId}` - Get all comments for a specific post

POST `/api/comments` - Add a new comment

PUT `/api/comments/{id}` - Update an existing comment

GET `/api/comments/search` - Search for a comment by text content

DELETE `/api/comments/{id}` - Delete a comment

### Swagger UI

<img width="970" height="914" alt="image" src="https://github.com/user-attachments/assets/eaaf7f0d-80b8-41ae-a991-8379980b7105" />


## Learning Goals

Get hands-on practice building a REST API with ASP.NET Core and EF Core

Learn how to keep code organized by splitting it into controllers, services, and data layers

Understand how to update and manage the database using EF Core migrations

Add basic error handling so the API gives clear, consistent responses

Try out Swagger to make the API easy to test and document
