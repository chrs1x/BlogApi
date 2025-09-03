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
`  
     git clone https://github.com/your-username/BlogApi.git
     cd BlogApi
`
3. Configure the database in appsettings.json.
4. Run migrations:
`
    dotnet ef migrations add Init
    dotnet ef database update
`
4. Launch the API:
`
    dotnet run
`
5. Navigate to https://localhost:5001/swagger to test endpoints.

## Project Structure
/Controllers — API endpoints  
/Models — Domain models  
/Services — Business logic  
/Data — EF DbContext  
/Migrations — EF migrations  
Program.cs, appsettings.json 

## API Endpoints
Resource:	    Endpoints:
Users	        GET /api/users, POST /api/users
Posts	        GET /api/posts, POST /api/posts, GET /api/posts/{id}
Comments	    GET /api/posts/{id}/comments, POST /api/posts/{id}/comments

## Learning Goals

To understand CRUD via EF Core
Practice building clean API layers (controllers, services, data)
Learn to manage schema changes with migrations
