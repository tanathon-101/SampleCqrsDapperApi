# SampleCqrsDapperApi

## Project Overview

SampleCqrsDapperApi เป็นตัวอย่างโปรเจกต์ .NET 8 ที่ออกแบบด้วย Clean Architecture + CQRS Pattern และใช้ Dapper เป็น ORM หลักสำหรับเชื่อมต่อกับ SQL Server Database

---

## Tech Stack

- .NET 8
- Dapper
- MediatR
- SQL Server
- CQRS Pattern
- Clean Architecture
- VSCode (Dev Environment)

---

## Architecture Structure

- **Domain**: Entity, Value Object, Base Types
- **Application**: Commands, Queries, DTOs, Handlers
- **Infrastructure**: Dapper Repository, SQL Connection
- **WebAPI**: Controllers, Program.cs, Middlewares

---

## How to Run

### 1. Clone Repo

```bash
git clone https://github.com/tanathon-101/SampleCqrsDapperApi.git
cd SampleCqrsDapperApi

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


{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SampleCQRSDB;User Id=sa;Password=YourStrongPassword123;TrustServerCertificate=True;"
  }
}

