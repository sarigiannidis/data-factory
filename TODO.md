# TODO
* Allow nullable columns to be configured when adding all tables.
* Records per table, not per prescriptor.
* Use extensibility for foreign keys.
  * Consider uniqueness.
  * Consider probability distributions.
* Check that the database hasn't changed before writing to it.
* Re-design the options / handlers.
* The settings.json from df (application) overwrites the settings.json in df.tests. fix this.
* GenerateHandlerTest::GenerateDatabase depends on DF_TESTS_DB (as referenced in the connection string in TESTDB.json) having been attached to MSSQLLocalDB by another test. fix this.
* The df.tests generate files in the debug directory and these are not cleanup up after.
* Give Generator, IGenerator specific names so that they are consistent with other generator in the same namespace ie RecordGenerator.