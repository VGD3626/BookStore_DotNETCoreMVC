# BookStore MVC

BookStore MVC is a web application built with ASP.NET Core MVC that allows users to manage their personal book library, browse available books, and maintain user-specific features such as favorites and password management.

## Features

- **User Authentication and Authorization**: Supports user login, registration, and role-based access (Admin and regular users).
- **Book Management**: Add, view, and manage books in the library.
- **User Library**: Users can view their own book library.
- **Favorite Books**: Mark books as favorites and manage the list of favorite books.
- **Admin Features**: Admin users have additional management functionalities.
- **Password Management**: Change password functionality for authenticated users.

## Technologies Used

- **ASP.NET Core MVC**: Framework used to build the web application.
- **Entity Framework Core**: ORM for interacting with the database.
- **Microsoft Identity**: Handles authentication and authorization.
- **SQL Server**: Database used for storing user and book information.
- **Bootstrap**: Front-end framework for styling the application.

## Prerequisites

Before you begin, ensure you have met the following requirements:

- .NET Core 3.1 SDK or later
- Visual Studio 2019/2022 or Visual Studio Code
- SQL Server (LocalDB or full installation)
- Node.js (optional, if you want to use npm for client-side libraries)

## Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/your-username/BookStore_MVC.git
   ```
2. **Navigate to the project directory**:
   ```bash
   cd BookStore_MVC
   ```
3. **Set up the database**:
   - Update the connection string in `appsettings.json` to match your SQL Server configuration.
   - Run database migrations:
     ```bash
     dotnet ef database update
     ```

4. **Run the application**:
   ```bash
   dotnet run
   ```
   - Open your web browser and navigate to `https://localhost:5001` or `http://localhost:5000`.

## Usage

### User Authentication

- Users can sign up, log in, and log out.
- Admin users can only be created by seeding the database during the initial setup.
- Regular users can register through the signup form.

### Book Management

- **Admins**: Can add, edit, and delete any books in the library.
- **Regular Users**: Can add books to their personal library, view their books, and mark books as favorites.

### User Roles and Navigation

- The navigation bar will show different options based on whether the user is logged in, and if they have an Admin role.
- Admin users see additional management options in the dropdown.


### Entity Configuration

Make sure that the foreign key relationship between `Book` and `User` is correctly set up in your `DbContext`:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Book>()
        .HasOne(b => b.User)
        .WithMany(u => u.Books)
        .HasForeignKey(b => b.UserId);
}
```

## Contributing

If you would like to contribute to this project:

1. **Fork the repository**.
2. **Create a new branch**:
   ```bash
   git checkout -b feature/YourFeatureName
   ```
3. **Make your changes** and commit them:
   ```bash
   git commit -m "Add some feature"
   ```
4. **Push to the branch**:
   ```bash
   git push origin feature/YourFeatureName
   ```
5. **Create a Pull Request**.

## License

This project is licensed under the MIT License.

## Contact

CE106
CE123
Batch=2026
---
