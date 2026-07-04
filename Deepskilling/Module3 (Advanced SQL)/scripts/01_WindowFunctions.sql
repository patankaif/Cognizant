USE Module3AdvancedSQL;
GO

SELECT
    EmployeeID,
    FirstName,
    LastName,
    DepartmentID,
    Salary,
    ROW_NUMBER() OVER (PARTITION BY DepartmentID ORDER BY Salary DESC) AS RowNum,
    RANK() OVER (PARTITION BY DepartmentID ORDER BY Salary DESC) AS SalaryRank,
    DENSE_RANK() OVER (PARTITION BY DepartmentID ORDER BY Salary DESC) AS SalaryDenseRank
FROM dbo.Employees
ORDER BY DepartmentID, Salary DESC;
GO

SELECT
    EmployeeID,
    FirstName,
    LastName,
    DepartmentID,
    Salary,
    AVG(Salary) OVER (PARTITION BY DepartmentID) AS DepartmentAvgSalary,
    Salary - AVG(Salary) OVER (PARTITION BY DepartmentID) AS DiffFromDeptAvg,
    SUM(Salary) OVER (PARTITION BY DepartmentID) AS DepartmentTotalSalary
FROM dbo.Employees
ORDER BY DepartmentID, Salary DESC;
GO

SELECT
    OrderID,
    EmployeeID,
    OrderDate,
    Amount,
    SUM(Amount) OVER (PARTITION BY EmployeeID ORDER BY OrderDate
                       ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS RunningTotal
FROM dbo.Orders
ORDER BY EmployeeID, OrderDate;
GO

SELECT
    EmployeeID,
    DepartmentID,
    Salary,
    NTILE(4) OVER (ORDER BY Salary DESC) AS SalaryQuartile
FROM dbo.Employees
ORDER BY Salary DESC;
GO
