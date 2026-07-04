USE Module3AdvancedSQL;
GO

IF OBJECT_ID('dbo.vw_EmployeeDirectory', 'V') IS NOT NULL DROP VIEW dbo.vw_EmployeeDirectory;
GO

CREATE VIEW dbo.vw_EmployeeDirectory
AS
SELECT
    e.EmployeeID,
    e.FirstName,
    e.LastName,
    d.DepartmentName,
    e.Salary,
    e.HireDate
FROM dbo.Employees e
JOIN dbo.Departments d ON d.DepartmentID = e.DepartmentID;
GO

SELECT * FROM dbo.vw_EmployeeDirectory ORDER BY DepartmentName, LastName;
GO

IF OBJECT_ID('dbo.vw_DepartmentSalarySummary', 'V') IS NOT NULL DROP VIEW dbo.vw_DepartmentSalarySummary;
GO

CREATE VIEW dbo.vw_DepartmentSalarySummary
WITH SCHEMABINDING
AS
SELECT
    DepartmentID,
    COUNT_BIG(*) AS EmployeeCount,
    SUM(Salary) AS TotalSalary
FROM dbo.Employees
GROUP BY DepartmentID;
GO

CREATE UNIQUE CLUSTERED INDEX IX_vw_DepartmentSalarySummary
ON dbo.vw_DepartmentSalarySummary (DepartmentID);
GO

SELECT * FROM dbo.vw_DepartmentSalarySummary ORDER BY DepartmentID;
GO

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Employees_DepartmentID' AND object_id = OBJECT_ID('dbo.Employees'))
    DROP INDEX IX_Employees_DepartmentID ON dbo.Employees;
GO

CREATE NONCLUSTERED INDEX IX_Employees_DepartmentID
ON dbo.Employees (DepartmentID)
INCLUDE (FirstName, LastName, Salary);
GO

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Orders_EmployeeID_OrderDate' AND object_id = OBJECT_ID('dbo.Orders'))
    DROP INDEX IX_Orders_EmployeeID_OrderDate ON dbo.Orders;
GO

CREATE NONCLUSTERED INDEX IX_Orders_EmployeeID_OrderDate
ON dbo.Orders (EmployeeID, OrderDate DESC);
GO

SELECT
    i.name AS IndexName,
    i.type_desc AS IndexType,
    OBJECT_NAME(i.object_id) AS TableName
FROM sys.indexes i
WHERE i.object_id IN (OBJECT_ID('dbo.Employees'), OBJECT_ID('dbo.Orders'))
    AND i.name IS NOT NULL
ORDER BY TableName, IndexName;
GO
