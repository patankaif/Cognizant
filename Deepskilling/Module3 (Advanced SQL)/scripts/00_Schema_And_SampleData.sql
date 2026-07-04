IF DB_ID('Module3AdvancedSQL') IS NULL
    CREATE DATABASE Module3AdvancedSQL;
GO

USE Module3AdvancedSQL;
GO

IF OBJECT_ID('dbo.Orders', 'U') IS NOT NULL DROP TABLE dbo.Orders;
IF OBJECT_ID('dbo.Employees', 'U') IS NOT NULL DROP TABLE dbo.Employees;
IF OBJECT_ID('dbo.Departments', 'U') IS NOT NULL DROP TABLE dbo.Departments;
IF OBJECT_ID('dbo.Sales', 'U') IS NOT NULL DROP TABLE dbo.Sales;
GO

CREATE TABLE dbo.Departments
(
    DepartmentID INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE dbo.Employees
(
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    DepartmentID INT NOT NULL FOREIGN KEY REFERENCES dbo.Departments(DepartmentID),
    ManagerID INT NULL FOREIGN KEY REFERENCES dbo.Employees(EmployeeID),
    Salary DECIMAL(10,2) NOT NULL,
    HireDate DATE NOT NULL
);
GO

CREATE TABLE dbo.Orders
(
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT NOT NULL FOREIGN KEY REFERENCES dbo.Employees(EmployeeID),
    OrderDate DATE NOT NULL,
    Amount DECIMAL(10,2) NOT NULL
);
GO

CREATE TABLE dbo.Sales
(
    SalesID INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(50) NOT NULL,
    Quarter NVARCHAR(2) NOT NULL,
    Amount DECIMAL(10,2) NOT NULL
);
GO

INSERT INTO dbo.Departments (DepartmentName) VALUES
('Engineering'),
('Sales'),
('Human Resources'),
('Finance');
GO

INSERT INTO dbo.Employees (FirstName, LastName, DepartmentID, ManagerID, Salary, HireDate) VALUES
('Alice', 'Nguyen', 1, NULL, 95000, '2018-03-01'),
('Bob', 'Smith', 1, 1, 82000, '2019-06-15'),
('Carol', 'Davis', 1, 1, 78000, '2020-01-10'),
('David', 'Lee', 2, NULL, 88000, '2017-09-23'),
('Eva', 'Martinez', 2, 4, 65000, '2021-02-14'),
('Frank', 'Wilson', 2, 4, 71000, '2019-11-05'),
('Grace', 'Taylor', 3, NULL, 60000, '2016-04-18'),
('Henry', 'Brown', 3, 7, 55000, '2022-07-01'),
('Ivy', 'Clark', 4, NULL, 99000, '2015-12-11'),
('Jack', 'Lewis', 4, 9, 73000, '2020-08-30');
GO

INSERT INTO dbo.Orders (EmployeeID, OrderDate, Amount) VALUES
(4, '2024-01-05', 1200.00),
(4, '2024-02-10', 800.00),
(5, '2024-01-20', 2200.00),
(5, '2024-03-02', 950.00),
(6, '2024-02-28', 1750.00),
(6, '2024-03-15', 500.00),
(2, '2024-01-11', 3000.00),
(3, '2024-02-19', 1450.00);
GO

INSERT INTO dbo.Sales (ProductName, Quarter, Amount) VALUES
('Widget', 'Q1', 12000),
('Widget', 'Q2', 15000),
('Widget', 'Q3', 11000),
('Widget', 'Q4', 17000),
('Gadget', 'Q1', 9000),
('Gadget', 'Q2', 9500),
('Gadget', 'Q3', 10200),
('Gadget', 'Q4', 12800);
GO
