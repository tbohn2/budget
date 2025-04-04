# Budget

## Description

**Budget** is an ASP.NET web application designed for personal budgeting. It helps users manage their finances by allowing them to track expenses. The application allows users to easily input transactions in categories, ensuring they stay on top of their spending and savings.

---

## Features

- **Expense Tracking**: Add and categorize expenses to monitor spending.
- **MSSQL Database**: Stores user data, transactions, and budget details.

---

## Prerequisites

To run this application locally, you will need:

- **Visual Studio** (or another compatible IDE)
- **.NET SDK** (version 6.0 or higher)
- **MSSQL Server** (or any compatible SQL Server instance)
- **SQL Server Management Studio** (optional for database management)

---

## Setup Instructions

1. **Clone the repository**:
   ```bash
   git clone https://github.com/yourusername/Budget.git
    ```

2. **Install dependencies**:
* Run in terminal at root of project:
   ```bash
    dotnet restore
    ```

3. **Set up the MSSQL database**:
* Create a new MSSQL database in your SQL Server instance. You can name it BudgetDB or any name of your choosing.
* Inside the appsettings.json file, locate the ConnectionStrings section and update it with your own database connection string:
   ```json
   "ConnectionStrings": {
        "DefaultConnection": "Server=your-server;Database=BudgetDB;User Id=your-username;Password=your-password;"
    }
    ```

4. **Migrate the database:**
* Open Package Manager Console in Visual Studio and run the following commands:
    ```bash
    dotnet ef migrations add MigrationName
    dotnet ef database update
    ```

5. **Start the App:**
* Run the application in the terminal:
     ```bash
    dotnet run    
    ```

---

## Usage

* Add expenses and categorize them under different categories.
* Track budget a year at a time.