#!/usr/bin/env bash
set -e

SERVER="${1:-localhost}"
USER="${2:-sa}"
PASSWORD="${3:-YourPassword}"

SCRIPTS=(
  "00_Schema_And_SampleData.sql"
  "01_WindowFunctions.sql"
  "02_GroupingSetsCubeRollup.sql"
  "03_CTE_And_RecursiveCTE.sql"
  "04_Merge.sql"
  "05_Pivot_And_Unpivot.sql"
  "06_Views_And_Indexes.sql"
  "07_StoredProcedures.sql"
  "08_UserDefinedFunctions.sql"
  "09_Triggers_And_Cursors.sql"
  "10_ExceptionHandling.sql"
  "11_Transactions.sql"
)

for script in "${SCRIPTS[@]}"; do
  echo "Running $script ..."
  sqlcmd -S "$SERVER" -U "$USER" -P "$PASSWORD" -i "$script"
done
