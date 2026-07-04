# Module 3 – Advanced SQL Using SQL Server

A set of runnable T-SQL scripts covering every sub-topic in **Module 3** of
the DN 5.0 Deep Skilling handbook, built against one shared sample schema
(Departments, Employees, Orders, Sales).

## Scripts (run in this order)

| # | Script | Covers |
|---|--------|--------|
| 00 | `00_Schema_And_SampleData.sql` | Creates the database, tables, and sample data used by every other script |
| 01 | `01_WindowFunctions.sql` | `OVER()`, `PARTITION BY`, `ROW_NUMBER`, `RANK`, `DENSE_RANK`, `NTILE`, running totals |
| 02 | `02_GroupingSetsCubeRollup.sql` | `GROUPING SETS`, `CUBE`, `ROLLUP`, `GROUPING()` |
| 03 | `03_CTE_And_RecursiveCTE.sql` | `WITH` statement, CTEs, chained CTEs, recursive CTE (employee/manager hierarchy) |
| 04 | `04_Merge.sql` | `MERGE` (upsert + delete-not-matched in one statement) |
| 05 | `05_Pivot_And_Unpivot.sql` | `PIVOT`, `UNPIVOT` |
| 06 | `06_Views_And_Indexes.sql` | Views, indexed (schema-bound) views, nonclustered indexes with `INCLUDE` |
| 07 | `07_StoredProcedures.sql` | Stored procedures with input/output parameters, `RAISERROR`, `GRANT EXECUTE` |
| 08 | `08_UserDefinedFunctions.sql` | Scalar functions, inline table-valued functions, multi-statement table-valued functions |
| 09 | `09_Triggers_And_Cursors.sql` | `AFTER UPDATE` trigger (audit log), `INSTEAD OF DELETE` trigger, a `CURSOR` |
| 10 | `10_ExceptionHandling.sql` | `TRY/CATCH`, nested `TRY/CATCH`, `THROW`, `ERROR_*()` functions, `XACT_ABORT` |
| 11 | `11_Transactions.sql` | Explicit transactions, `SAVE TRANSACTION` (savepoints), isolation levels, deadlock-aware retry pattern |

## How to run

**Option 1 – SQL Server Management Studio (SSMS)**
1. Connect to your SQL Server instance.
2. Open and execute `00_Schema_And_SampleData.sql` first (creates the
   `Module3AdvancedSQL` database).
3. Open and execute scripts `01` through `11` in numeric order.

**Option 2 – sqlcmd / bash**
```bash
cd scripts
chmod +x run_all.sh
./run_all.sh localhost sa YourPassword
```

## Notes

- All scripts target `Module3AdvancedSQL` — running `00_...` first is
  required since every later script does `USE Module3AdvancedSQL;`.
- Script `04_Merge.sql` and `10/11` depend on the `DepartmentBudgets`
  table, which `04_Merge.sql` creates — run it before `10` and `11`.
- Script `10_ExceptionHandling.sql` intentionally ends with a call that
  fails (insufficient budget) to demonstrate the `ROLLBACK`/`THROW`
  path — seeing that error in your output is expected, not a bug.
- I don't have a SQL Server instance available to execute these end to
  end, so please run them in SSMS/Azure Data Studio and let me know if
  anything needs adjusting for your specific SQL Server version.
