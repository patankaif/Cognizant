USE Module3AdvancedSQL;
GO

BEGIN TRY
    DECLARE @Result INT = 10 / 0;
END TRY
BEGIN CATCH
    SELECT
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_SEVERITY() AS ErrorSeverity,
        ERROR_STATE() AS ErrorState,
        ERROR_PROCEDURE() AS ErrorProcedure,
        ERROR_LINE() AS ErrorLine,
        ERROR_MESSAGE() AS ErrorMessage;
END CATCH
GO

BEGIN TRY
    UPDATE dbo.Employees
    SET Salary = -1000
    WHERE EmployeeID = 999;

    IF @@ROWCOUNT = 0
        THROW 51000, 'No employee was found with the given ID.', 1;
END TRY
BEGIN CATCH
    PRINT 'Caught error: ' + ERROR_MESSAGE();
END CATCH
GO

BEGIN TRY
    BEGIN TRY
        INSERT INTO dbo.Employees (FirstName, LastName, DepartmentID, Salary, HireDate)
        VALUES ('Test', 'User', 9999, 50000, '2024-01-01');
    END TRY
    BEGIN CATCH
        PRINT 'Inner catch: ' + ERROR_MESSAGE();
        THROW;
    END CATCH
END TRY
BEGIN CATCH
    PRINT 'Outer catch: ' + ERROR_MESSAGE();
END CATCH
GO

CREATE OR ALTER PROCEDURE dbo.usp_TransferBudget
    @FromDepartmentID INT,
    @ToDepartmentID INT,
    @Amount DECIMAL(12,2)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE dbo.DepartmentBudgets
        SET Budget = Budget - @Amount
        WHERE DepartmentID = @FromDepartmentID;

        IF (SELECT Budget FROM dbo.DepartmentBudgets WHERE DepartmentID = @FromDepartmentID) < 0
            THROW 51001, 'Insufficient budget in source department.', 1;

        UPDATE dbo.DepartmentBudgets
        SET Budget = Budget + @Amount
        WHERE DepartmentID = @ToDepartmentID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END
GO

EXEC dbo.usp_TransferBudget @FromDepartmentID = 1, @ToDepartmentID = 2, @Amount = 1000000;
GO

SELECT * FROM dbo.DepartmentBudgets ORDER BY DepartmentID;
GO
