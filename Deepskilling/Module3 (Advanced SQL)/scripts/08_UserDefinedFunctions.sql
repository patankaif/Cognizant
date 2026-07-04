USE Module3AdvancedSQL;
GO

IF OBJECT_ID('dbo.ufn_GetAnnualBonus', 'FN') IS NOT NULL DROP FUNCTION dbo.ufn_GetAnnualBonus;
GO

CREATE FUNCTION dbo.ufn_GetAnnualBonus
(
    @Salary DECIMAL(10,2),
    @YearsOfService INT
)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @BonusRate DECIMAL(4,3);

    SET @BonusRate =
        CASE
            WHEN @YearsOfService >= 10 THEN 0.15
            WHEN @YearsOfService >= 5 THEN 0.10
            WHEN @YearsOfService >= 2 THEN 0.05
            ELSE 0.02
        END;

    RETURN @Salary * @BonusRate;
END
GO

SELECT
    EmployeeID,
    FirstName,
    LastName,
    Salary,
    DATEDIFF(YEAR, HireDate, GETDATE()) AS YearsOfService,
    dbo.ufn_GetAnnualBonus(Salary, DATEDIFF(YEAR, HireDate, GETDATE())) AS AnnualBonus
FROM dbo.Employees
ORDER BY AnnualBonus DESC;
GO

IF OBJECT_ID('dbo.ufn_GetEmployeesHiredAfter', 'IF') IS NOT NULL DROP FUNCTION dbo.ufn_GetEmployeesHiredAfter;
GO

CREATE FUNCTION dbo.ufn_GetEmployeesHiredAfter
(
    @CutoffDate DATE
)
RETURNS TABLE
AS
RETURN
(
    SELECT
        EmployeeID,
        FirstName,
        LastName,
        DepartmentID,
        HireDate
    FROM dbo.Employees
    WHERE HireDate > @CutoffDate
);
GO

SELECT * FROM dbo.ufn_GetEmployeesHiredAfter('2020-01-01') ORDER BY HireDate;
GO

IF OBJECT_ID('dbo.ufn_GetDepartmentSalaryStats', 'TF') IS NOT NULL DROP FUNCTION dbo.ufn_GetDepartmentSalaryStats;
GO

CREATE FUNCTION dbo.ufn_GetDepartmentSalaryStats
(
    @DepartmentID INT
)
RETURNS @Stats TABLE
(
    MinSalary DECIMAL(10,2),
    MaxSalary DECIMAL(10,2),
    AvgSalary DECIMAL(10,2),
    EmployeeCount INT
)
AS
BEGIN
    INSERT INTO @Stats (MinSalary, MaxSalary, AvgSalary, EmployeeCount)
    SELECT
        MIN(Salary),
        MAX(Salary),
        AVG(Salary),
        COUNT(*)
    FROM dbo.Employees
    WHERE DepartmentID = @DepartmentID;

    RETURN;
END
GO

SELECT * FROM dbo.ufn_GetDepartmentSalaryStats(1);
GO
