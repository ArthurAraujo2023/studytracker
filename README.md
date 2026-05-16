# StudyTracker

StudyTracker is a learning project built with C# and ASP.NET Core.

The goal of this project is to practice backend architecture, separation of responsibilities, and REST API fundamentals.

## Project Status

This project is currently in development.

## Technologies

- C#
- .NET
- ASP.NET Core
- Console Application
- REST API
- Git
- GitHub

## Project Structure

- `StudyTracker.Core`  
  Contains the main business logic, Models, DTOs, and Services.

- `StudyTracker.Console`  
  Console application used to test the application flow.

- `StudyTracker.Api`  
  ASP.NET Core Web API with Controllers and HTTP endpoints.

## Concepts Practiced

- Models
- DTOs
- Services
- Controllers
- Dependency Injection
- HTTP GET
- HTTP POST
- HTTP PUT
- HTTP DELETE
- ActionResult
- HTTP status codes
- Git and GitHub workflow

## Current Features

- Create study sessions
- List study sessions
- Search study session by Id
- Update study session
- Remove study session

## Learning Purpose

This project was created as part of my learning journey in C# and ASP.NET Core.

The main focus is not only to make the code work, but to understand how each layer is responsible for a specific part of the application.

- Model represents the final object stored by the system.
- DTO represents the data received from the outside.
- Service contains the business rules.
- Controller receives HTTP requests and returns HTTP responses.
- Console is used to test the application flow during development.

## Next Steps

- Improve validation handling
- Return created objects from the Service
- Improve HTTP responses
- Add Swagger testing
- Add persistence with a database
- Improve project documentation
