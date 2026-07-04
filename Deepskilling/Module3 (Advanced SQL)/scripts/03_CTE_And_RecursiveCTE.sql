USE Module3AdvancedSQL;
GO

WITH DepartmentSalary AS
(
    SELECT
        DepartmentID,
        AVG(Salary) AS AvgSalary,
        COUNT(*) AS EmployeeCount
    FROM dbo.Employees
    GROUP BY DepartmentID
)
SELECT
    d.DepartmentName,
    ds.AvgSalary,
    ds.EmployeeCount
FROM DepartmentSalary ds
JOIN dbo.Departments d ON d.DepartmentID = ds.DepartmentID
ORDER BY ds.AvgSalary DESC;
GO

WITH HighEarners AS
(
    SELECT EmployeeID, FirstName, LastName, DepartmentID, Salary
    FROM dbo.Employees
    WHERE Salary > 75000
),
HighEarnerOrders AS
(
    SELECT h.EmployeeID, h.FirstName, h.LastName, o.OrderID, o.Amount
    FROM HighEarners h
    JOIN dbo.Orders o ON o.EmployeeID = h.EmployeeID
)
SELECT * FROM HighEarnerOrders
ORDER BY EmployeeID, OrderID;
GO

WITH EmployeeHierarchy AS
(
    SELECT
        EmployeeID,
        FirstName,
        LastName,
        ManagerID,
        0 AS HierarchyLevel
    FROM dbo.Employees
    WHERE ManagerID IS NULL

    UNION ALL

    SELECT
        e.EmployeeID,
        e.FirstName,
        e.LastName,
        e.ManagerID,
        eh.HierarchyLevel + 1
    FROM dbo.Employees e
    JOIN EmployeeHierarchy eh ON e.ManagerID = eh.EmployeeID
)
SELECT
    EmployeeID,
    FirstName,
    LastName,
    ManagerID,
    HierarchyLevel
FROM EmployeeHierarchy
ORDER BY HierarchyLevel, EmployeeID
OPTION (MAXRECURSION 100);
GO
