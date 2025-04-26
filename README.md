# SampleCqrsDapperApi

## 1. Project Overview

SampleCqrsDapperApi เป็นตัวอย่างโปรเจกต์ .NET 8 ที่ออกแบบด้วย Clean Architecture + CQRS Pattern และใช้ Dapper เป็น ORM หลักสำหรับเชื่อมต่อกับ SQL Server Database

---

## 2. Tech Stack

- .NET 8
- Dapper
- MediatR
- SQL Server
- CQRS Pattern
- Clean Architecture
- VSCode (Dev Environment)

---

## 3. Architecture Structure

- **Domain**: Entity, Value Object, Base Types
- **Application**: Commands, Queries, DTOs, Handlers
- **Infrastructure**: Dapper Repository, SQL Connection
- **WebAPI**: Controllers, Program.cs, Middlewares

---

## 4. Database Schema

```sql
CREATE DATABASE SampleCQRSDB;
GO

USE SampleCQRSDB;
GO

CREATE TABLE Customers (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Orders (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CustomerId INT NOT NULL,
    OrderDate DATETIME NOT NULL,
    TotalAmount DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
);
GO
```

---

## 5. How to Run

### 5.1 Clone the Repository

```bash
git clone https://github.com/tanathon-101/SampleCqrsDapperApi.git
cd SampleCqrsDapperApi
```

### 5.2 Config Connection String

แก้ไขไฟล์ `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SampleCQRSDB;User Id=sa;Password=YourStrongPassword123;TrustServerCertificate=True;"
  }
}
```

### 5.3 Restore Dependencies

```bash
dotnet restore
```

### 5.4 Run the Application

```bash
dotnet run --project SampleCqrsDapperApi.WebAPI
```

---

## 6. API Usage

### POST `/api/customers`

**Request Body:**

```json
{
  "name": "Josh Aspire",
  "email": "josh@sample.com"
}
```

**Response:**

```json
{
  "message": "Customer created successfully"
}
```

### GET `/api/customers`

**Response:**

```json
[
  {
    "id": 1,
    "name": "Josh Aspire",
    "email": "josh@sample.com"
  }
]
```
