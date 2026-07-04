USE Module3AdvancedSQL;
GO

BEGIN TRANSACTION;

UPDATE dbo.Employees SET Salary = Salary * 1.05 WHERE DepartmentID = 1;
UPDATE dbo.Employees SET Salary = Salary * 1.03 WHERE DepartmentID = 2;

COMMIT TRANSACTION;
GO

SELECT DepartmentID, AVG(Salary) AS AvgSalary FROM dbo.Employees GROUP BY DepartmentID ORDER BY DepartmentID;
GO

BEGIN TRANSACTION;

UPDATE dbo.Employees SET Salary = Salary * 1.10 WHERE DepartmentID = 3;

IF EXISTS (SELECT 1 FROM dbo.Employees WHERE DepartmentID = 3 AND Salary > 1000000)
BEGIN
    ROLLBACK TRANSACTION;
    PRINT 'Rolled back: salary exceeded sanity threshold.';
END
ELSE
BEGIN
    COMMIT TRANSACTION;
    PRINT 'Committed successfully.';
END
GO

BEGIN TRANSACTION;

UPDATE dbo.Employees SET Salary = Salary + 1000 WHERE EmployeeID = 2;
SAVE TRANSACTION AfterFirstRaise;

UPDATE dbo.Employees SET Salary = Salary + 5000 WHERE EmployeeID = 3;

IF (SELECT Salary FROM dbo.Employees WHERE EmployeeID = 3) > 150000
BEGIN
    ROLLBACK TRANSACTION AfterFirstRaise;
    PRINT 'Rolled back to savepoint AfterFirstRaise; first raise for EmployeeID 2 is kept.';
END

COMMIT TRANSACTION;
GO

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
GO

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
GO

BEGIN TRANSACTION;
SELECT * FROM dbo.Employees WHERE DepartmentID = 1;
COMMIT TRANSACTION;
GO

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
GO

CREATE OR ALTER PROCEDURE dbo.usp_PlaceOrder
    @EmployeeID INT,
    @Amount DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET DEADLOCK_PRIORITY LOW;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO dbo.Orders (EmployeeID, OrderDate, Amount)
        VALUES (@EmployeeID, CAST(GETDATE() AS DATE), @Amount);

        UPDATE dbo.Employees
        SET Salary = Salary
        WHERE EmployeeID = @EmployeeID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0
            ROLLBACK TRANSACTION;

        IF ERROR_NUMBER() = 1205
            PRINT 'Deadlock detected; caller should retry usp_PlaceOrder.';

        THROW;
    END CATCH
END
GO

EXEC dbo.usp_PlaceOrder @EmployeeID = 5, @Amount = 1300.00;
GO

SELECT TOP 5 * FROM dbo.Orders ORDER BY OrderID DESC;
GO
