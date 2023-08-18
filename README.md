
# MoviezLand - Movie Management System

MoviezLand is a web-based Movie Management System built using ASP.NET MVC and Entity Framework Core. It allows users to create accounts, explore movie information, write reviews, and manage movie genres. The project follows a three-layer architecture and utilizes the Repository design pattern with the Unit of Work pattern for efficient data access.

## Project Overview

MoviezLand is developed using the following technologies:
- C#
- HTML, CSS, JavaScript
- ASP.NET MVC
- Entity Framework Core
- LINQ
- Repository Design Pattern
- Three-Layer Architecture (Presentation Layer, Business Logic Layer, Data Access Layer)

## Installation

To run the project locally, ensure you have at least .NET 7 installed. Additionally, make sure to install the required NuGet packages by running the following commands:

```bash
dotnet add package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.AspNetCore.Identity.UI
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package NToastNotify

After installing the packages, you need to update the database and run the migrations. The initial migration will create a default manager account with the following credentials:
Username: manager
Password: Adel.Wageeh.1
#Features

#User Management:

1-Users can create accounts and log in.
2-Users can explore movie information and write reviews.

#Admin Rights:

1-Admins can manage movies (add, edit, delete).
2-Admins can write reviews on behalf of their username.
3-Admins can manage movie genres (add, edit, delete).
4-Admins can manage users (add, edit profile, delete).

#Manager Rights:

1-Managers have all rights of users and admins.
2-Managers can manage user roles (make users admins, managers, or users).

#Getting Started
#Clone the repository:

bash
Copy code
git clone https://github.com/yourusername/MoviezLand.git
cd MoviezLand
Install dependencies and run migrations:

dotnet restore
dotnet ef database update


#Default Manager Account
Username: manager
Password: Adel.Wageeh.1

