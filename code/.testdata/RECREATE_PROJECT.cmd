@ECHO OFF

SETLOCAL

SET ROOT=%~dp0
df new -n %ROOT%\TESTDB.json -c "Data Source=(localdb)\DF_SERVER;Initial Catalog=DF_TEST_DB;"
df add alltables --project %ROOT%\TESTDB.json

ENDLOCAL