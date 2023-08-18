MoviezLand - Movie Management System
MoviezLand is a comprehensive movie management system built using C#, HTML, CSS, JavaScript, LINQ, Entity Framework Core, ASP.NET MVC, and ORM technologies. It follows a three-layer architecture and employs the Repository design pattern with the Unit of Work concept for efficient data management. This system allows users to create accounts, explore movie information, write reviews, and manage movies and genres. Administrators have enhanced privileges to manage users, movies, and genres.

Table of Contents
Requirements
Installation
Usage
Features
Default Manager Account
Requirements
To run the MoviezLand project, you need:

.NET 7 or later
The following packages:
Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
Microsoft.AspNetCore.Identity.EntityFrameworkCore
Microsoft.AspNetCore.Identity.UI
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.VisualStudio.Web.CodeGeneration.Design
NToastNotify
Installation
Clone the repository to your local machine.
Open the solution using Visual Studio or your preferred IDE.
Install the required NuGet packages mentioned above.
Open the Package Manager Console and run migrations using the command:
mathematica
Copy code
Update-Database
This will create the necessary database structure.
Run the application.
Usage
MoviezLand is a user-friendly movie management system with the following key features:

User Accounts: Users can create accounts and log in to access the system.
Movie Information: Users can view detailed information about movies.
Movie Reviews: Users can write their own reviews for movies they've watched.
Admin Privileges: Administrators have extended rights, including:
Managing Movies: Adding, editing, and deleting movie details.
Managing Genres: Adding, editing, and deleting movie genres (all associated movies in a genre must be removed first).
User Management: Viewing, adding, editing, and deleting user profiles.
Manager Role: Managers possess all user and admin privileges, including role management.
Features
Three-Layer Architecture: The system is organized into presentation, business logic, and data access layers for maintainability.
Repository Design Pattern: The Repository pattern is employed for efficient data storage and retrieval.
Unit of Work: The Unit of Work pattern ensures consistency and atomicity in database operations.
Multi-Genre Movies: Movies can belong to multiple genres simultaneously.
User Reviews: Users can share their thoughts by writing reviews for movies.
Role Management: Administrators and managers can assign different roles to users.
Default Manager Account
When you run the migrations, a default manager account is created with the following credentials:

Username: manager
Password: Adel.Wageeh.1
This manager account has full access to the system's functionalities.

Feel free to explore, contribute, and enhance the MoviezLand project as needed. If you encounter any issues or have questions, please refer to the project's documentation or reach out to the project maintainers.
