USE Module3AdvancedSQL;
GO

SELECT
    d.DepartmentName,
    YEAR(o.OrderDate) AS OrderYear,
    SUM(o.Amount) AS TotalAmount
FROM dbo.Orders o
JOIN dbo.Employees e ON e.EmployeeID = o.EmployeeID
JOIN dbo.Departments d ON d.DepartmentID = e.DepartmentID
GROUP BY GROUPING SETS
(
    (d.DepartmentName, YEAR(o.OrderDate)),
    (d.DepartmentName),
    ()
)
ORDER BY d.DepartmentName, OrderYear;
GO

SELECT
    d.DepartmentName,
    YEAR(o.OrderDate) AS OrderYear,
    SUM(o.Amount) AS TotalAmount
FROM dbo.Orders o
JOIN dbo.Employees e ON e.EmployeeID = o.EmployeeID
JOIN dbo.Departments d ON d.DepartmentID = e.DepartmentID
GROUP BY CUBE (d.DepartmentName, YEAR(o.OrderDate))
ORDER BY d.DepartmentName, OrderYear;
GO

SELECT
    d.DepartmentName,
    YEAR(o.OrderDate) AS OrderYear,
    SUM(o.Amount) AS TotalAmount
FROM dbo.Orders o
JOIN dbo.Employees e ON e.EmployeeID = o.EmployeeID
JOIN dbo.Departments d ON d.DepartmentID = e.DepartmentID
GROUP BY ROLLUP (d.DepartmentName, YEAR(o.OrderDate))
ORDER BY d.DepartmentName, OrderYear;
GO

SELECT
    d.DepartmentName,
    YEAR(o.OrderDate) AS OrderYear,
    SUM(o.Amount) AS TotalAmount,
    GROUPING(d.DepartmentName) AS IsDeptSubtotal,
    GROUPING(YEAR(o.OrderDate)) AS IsYearSubtotal
FROM dbo.Orders o
JOIN dbo.Employees e ON e.EmployeeID = o.EmployeeID
JOIN dbo.Departments d ON d.DepartmentID = e.DepartmentID
GROUP BY ROLLUP (d.DepartmentName, YEAR(o.OrderDate))
ORDER BY d.DepartmentName, OrderYear;
GO
