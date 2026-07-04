USE Module3AdvancedSQL;
GO

IF OBJECT_ID('dbo.usp_GetEmployeesByDepartment', 'P') IS NOT NULL DROP PROCEDURE dbo.usp_GetEmployeesByDepartment;
GO

CREATE PROCEDURE dbo.usp_GetEmployeesByDepartment
    @DepartmentID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        EmployeeID,
        FirstName,
        LastName,
        Salary,
        HireDate
    FROM dbo.Employees
    WHERE DepartmentID = @DepartmentID
    ORDER BY LastName;
END
GO

EXEC dbo.usp_GetEmployeesByDepartment @DepartmentID = 2;
GO

IF OBJECT_ID('dbo.usp_AddEmployee', 'P') IS NOT NULL DROP PROCEDURE dbo.usp_AddEmployee;
GO

CREATE PROCEDURE dbo.usp_AddEmployee
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @DepartmentID INT,
    @ManagerID INT = NULL,
    @Salary DECIMAL(10,2),
    @HireDate DATE,
    @NewEmployeeID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Employees (FirstName, LastName, DepartmentID, ManagerID, Salary, HireDate)
    VALUES (@FirstName, @LastName, @DepartmentID, @ManagerID, @Salary, @HireDate);

    SET @NewEmployeeID = SCOPE_IDENTITY();
END
GO

DECLARE @NewID INT;
EXEC dbo.usp_AddEmployee
    @FirstName = 'Karen',
    @LastName = 'Adams',
    @DepartmentID = 1,
    @ManagerID = 1,
    @Salary = 80000,
    @HireDate = '2024-05-01',
    @NewEmployeeID = @NewID OUTPUT;
SELECT @NewID AS NewEmployeeID;
GO

IF OBJECT_ID('dbo.usp_UpdateEmployeeSalary', 'P') IS NOT NULL DROP PROCEDURE dbo.usp_UpdateEmployeeSalary;
GO

CREATE PROCEDURE dbo.usp_UpdateEmployeeSalary
    @EmployeeID INT,
    @NewSalary DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.Employees WHERE EmployeeID = @EmployeeID)
    BEGIN
        RAISERROR('Employee %d does not exist.', 16, 1, @EmployeeID);
        RETURN;
    END

    UPDATE dbo.Employees
    SET Salary = @NewSalary
    WHERE EmployeeID = @EmployeeID;
END
GO

EXEC dbo.usp_UpdateEmployeeSalary @EmployeeID = 2, @NewSalary = 85000;
GO

IF OBJECT_ID('dbo.usp_DeleteEmployee', 'P') IS NOT NULL DROP PROCEDURE dbo.usp_DeleteEmployee;
GO

CREATE PROCEDURE dbo.usp_DeleteEmployee
    @EmployeeID INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM dbo.Orders WHERE EmployeeID = @EmployeeID;
    DELETE FROM dbo.Employees WHERE EmployeeID = @EmployeeID;
END
GO

GRANT EXECUTE ON dbo.usp_GetEmployeesByDepartment TO PUBLIC;
GO
