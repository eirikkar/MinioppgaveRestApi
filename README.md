# MinioppgaveRestApi

This is a .NET program designed to provide a REST API for managing tasks. The program uses Entity Framework for database operations, and a sqlite database.

## Installation

Make sure that dotnet is installed and follow these steps to install and set up the api:

1. **Clone the repository:**

   ```bash
   git clone <https://github.com/eirikkar/MinioppgaveRestApi>
   cd MinioppgaveRestApi
   ```

2. **Create a Database folder:**

   ```bash
   mkdir Database
   ```

3. **Restore the .NET project:**

   ```bash
   dotnet restore
   ```

4. **Apply Entity Framework migrations:**

   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

5. **Build the project:**

   ```bash
   dotnet build
   ```

6. **Run the application:**
   ```bash
   dotnet run
   ```
