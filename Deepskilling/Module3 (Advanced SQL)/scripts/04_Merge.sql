USE Module3AdvancedSQL;
GO

IF OBJECT_ID('dbo.DepartmentBudgets', 'U') IS NOT NULL DROP TABLE dbo.DepartmentBudgets;
GO

CREATE TABLE dbo.DepartmentBudgets
(
    DepartmentID INT PRIMARY KEY,
    Budget DECIMAL(12,2) NOT NULL,
    LastUpdated DATETIME NOT NULL DEFAULT GETDATE()
);
GO

INSERT INTO dbo.DepartmentBudgets (DepartmentID, Budget) VALUES
(1, 500000),
(2, 300000);
GO

CREATE TABLE #IncomingBudgets
(
    DepartmentID INT,
    Budget DECIMAL(12,2)
);
GO

INSERT INTO #IncomingBudgets (DepartmentID, Budget) VALUES
(1, 550000),
(2, 300000),
(3, 150000),
(4, 275000);
GO

MERGE dbo.DepartmentBudgets AS target
USING #IncomingBudgets AS source
ON target.DepartmentID = source.DepartmentID
WHEN MATCHED AND target.Budget <> source.Budget THEN
    UPDATE SET target.Budget = source.Budget, target.LastUpdated = GETDATE()
WHEN NOT MATCHED BY TARGET THEN
    INSERT (DepartmentID, Budget, LastUpdated)
    VALUES (source.DepartmentID, source.Budget, GETDATE())
WHEN NOT MATCHED BY SOURCE THEN
    DELETE
OUTPUT $action, inserted.DepartmentID, inserted.Budget, deleted.Budget AS PreviousBudget;
GO

SELECT * FROM dbo.DepartmentBudgets ORDER BY DepartmentID;
GO

DROP TABLE #IncomingBudgets;
GO
