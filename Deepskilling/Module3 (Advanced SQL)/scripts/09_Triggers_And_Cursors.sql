USE Module3AdvancedSQL;
GO

IF OBJECT_ID('dbo.EmployeeSalaryAudit', 'U') IS NOT NULL DROP TABLE dbo.EmployeeSalaryAudit;
GO

CREATE TABLE dbo.EmployeeSalaryAudit
(
    AuditID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT NOT NULL,
    OldSalary DECIMAL(10,2) NOT NULL,
    NewSalary DECIMAL(10,2) NOT NULL,
    ChangedAt DATETIME NOT NULL DEFAULT GETDATE()
);
GO

IF OBJECT_ID('dbo.trg_Employees_AfterUpdate_Salary', 'TR') IS NOT NULL DROP TRIGGER dbo.trg_Employees_AfterUpdate_Salary;
GO

CREATE TRIGGER dbo.trg_Employees_AfterUpdate_Salary
ON dbo.Employees
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF UPDATE(Salary)
    BEGIN
        INSERT INTO dbo.EmployeeSalaryAudit (EmployeeID, OldSalary, NewSalary)
        SELECT
            i.EmployeeID,
            d.Salary,
            i.Salary
        FROM inserted i
        JOIN deleted d ON d.EmployeeID = i.EmployeeID
        WHERE i.Salary <> d.Salary;
    END
END
GO

UPDATE dbo.Employees SET Salary = 90000 WHERE EmployeeID = 3;
GO

SELECT * FROM dbo.EmployeeSalaryAudit ORDER BY AuditID;
GO

IF OBJECT_ID('dbo.trg_Employees_InsteadOfDelete', 'TR') IS NOT NULL DROP TRIGGER dbo.trg_Employees_InsteadOfDelete;
GO

CREATE TRIGGER dbo.trg_Employees_InsteadOfDelete
ON dbo.Employees
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM deleted d JOIN dbo.Orders o ON o.EmployeeID = d.EmployeeID)
    BEGIN
        RAISERROR('Cannot delete an employee who still has orders on file.', 16, 1);
        RETURN;
    END

    DELETE e
    FROM dbo.Employees e
    JOIN deleted d ON d.EmployeeID = e.EmployeeID;
END
GO

DELETE FROM dbo.Employees WHERE EmployeeID = 8;
SELECT EmployeeID, FirstName, LastName FROM dbo.Employees WHERE EmployeeID = 8;
GO

DECLARE @EmployeeID INT;
DECLARE @FirstName NVARCHAR(50);
DECLARE @Salary DECIMAL(10,2);
DECLARE @Report NVARCHAR(MAX) = '';

DECLARE employee_cursor CURSOR FOR
    SELECT EmployeeID, FirstName, Salary
    FROM dbo.Employees
    WHERE DepartmentID = 1
    ORDER BY Salary DESC;

OPEN employee_cursor;
FETCH NEXT FROM employee_cursor INTO @EmployeeID, @FirstName, @Salary;

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @Report = @Report + @FirstName + ': ' + CAST(@Salary AS NVARCHAR(20)) + CHAR(13);
    FETCH NEXT FROM employee_cursor INTO @EmployeeID, @FirstName, @Salary;
END

CLOSE employee_cursor;
DEALLOCATE employee_cursor;

PRINT @Report;
GO
