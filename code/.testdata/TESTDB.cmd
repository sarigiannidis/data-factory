df new -n TESTDB.json -c "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DF_TESTS_DB;AttachDbFilename=%cd%\TESTDB.mdf"
df add alltables --project TESTDB.json