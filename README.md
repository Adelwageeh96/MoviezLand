# MoviezLand - Movie Management System

MoviezLand is a comprehensive movie management system built using C#, HTML, CSS, JavaScript, LINQ, Entity Framework Core, ASP.NET MVC, and ORM technologies. It follows a three-layer architecture and employs the Repository design pattern with the Unit of Work concept for efficient data management. This system allows users to create accounts, explore movie information, write reviews, and manage movies and genres. Administrators have enhanced privileges to manage users, movies, and genres.

Note: You can watch video and summary in arabic from here (https://www.linkedin.com/feed/update/urn:li:activity:7098379650346758144/)

## Table of Contents

- [Requirements](#requirements)
- [Installation](#installation)
- [Usage](#usage)
- [Features](#features)
- [Default Manager Account](#default-manager-account)

## Requirements

To run the MoviezLand project, you need:

- .NET 7 or later
- The following packages:
  1. Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
  2. Microsoft.AspNetCore.Identity.EntityFrameworkCore
  3. Microsoft.AspNetCore.Identity.UI
  4. Microsoft.EntityFrameworkCore
  5. Microsoft.EntityFrameworkCore.SqlServer
  6. Microsoft.EntityFrameworkCore.Tools
  7. Microsoft.VisualStudio.Web.CodeGeneration.Design
  8. NToastNotify

## Installation

1. Clone the repository to your local machine.
2. Open the solution using Visual Studio or your preferred IDE.
3. Install the required NuGet packages mentioned above.
4. Open the Package Manager Console and run migrations using the command: "Update-Database"
This will create the necessary database structure.
5. Run the application.

## Usage

MoviezLand is a user-friendly movie management system with the following key features:

- **User Accounts**: Users can create accounts and log in to access the system.
- **Movie Information**: Users can view detailed information about movies.
- **Movie Reviews**: Users can write their own reviews for movies they've watched.
- **Admin Privileges**: Administrators have extended rights, including:
- Managing Movies: Adding, editing, and deleting movie details.
- Managing Genres: Adding, editing, and deleting movie genres (all associated movies in a genre must be removed first).
- User Management: Viewing, adding, editing, and deleting user profiles.
- **Manager Role**: Managers possess all user and admin privileges, including role management.

## Features

- Three-Layer Architecture: The system is organized into presentation, business logic, and data access layers for maintainability.
- Repository Design Pattern: The Repository pattern is employed for efficient data storage and retrieval.
- Unit of Work: The Unit of Work pattern ensures consistency and atomicity in database operations.
- Multi-Genre Movies: Movies can belong to multiple genres simultaneously.
- User Reviews: Users can share their thoughts by writing reviews for movies.
- Role Management: Administrators and managers can assign different roles to users.

## Default Manager Account

When you run the migrations, a default manager account is created with the following credentials:

- Username: manager
- Password: Adel.Wageeh.1

This manager account has full access to the system's functionalities.

Feel free to explore, contribute, and enhance the MoviezLand project as needed. If you encounter any issues or have questions, please refer to the project's documentation or reach out to the project maintainers.

