USE Module3AdvancedSQL;
GO

SELECT
    ProductName,
    [Q1], [Q2], [Q3], [Q4]
FROM
(
    SELECT ProductName, Quarter, Amount
    FROM dbo.Sales
) AS SourceTable
PIVOT
(
    SUM(Amount)
    FOR Quarter IN ([Q1], [Q2], [Q3], [Q4])
) AS PivotTable
ORDER BY ProductName;
GO

IF OBJECT_ID('dbo.SalesPivoted', 'U') IS NOT NULL DROP TABLE dbo.SalesPivoted;
GO

SELECT
    ProductName,
    [Q1], [Q2], [Q3], [Q4]
INTO dbo.SalesPivoted
FROM
(
    SELECT ProductName, Quarter, Amount
    FROM dbo.Sales
) AS SourceTable
PIVOT
(
    SUM(Amount)
    FOR Quarter IN ([Q1], [Q2], [Q3], [Q4])
) AS PivotTable;
GO

SELECT
    ProductName,
    Quarter,
    Amount
FROM dbo.SalesPivoted
UNPIVOT
(
    Amount FOR Quarter IN ([Q1], [Q2], [Q3], [Q4])
) AS UnpivotTable
ORDER BY ProductName, Quarter;
GO

DROP TABLE dbo.SalesPivoted;
GO
