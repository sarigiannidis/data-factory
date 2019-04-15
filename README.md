# data-factory
data-factory fills MS SQL databases with random data according to your configuration.
It respects foreign key relationships and primary keys, it can handle all value types, and it is very extensible.

data-factory is open source.
The code is published on github under the [MIT license](https://github.com/sarigiannidis/data-factory/blob/master/LICENSE). This tool WILL overwrite data in your database, so backup your data!

The binaries are only published for win-x64. Please check the [releases](https://github.com/sarigiannidis/data-factory/releases) for the latest version.

## Usage
To create a new project file given a connection string:
```
df new --name demo.json -c "Data Source=(localdb)\demo_server;Initial Catalog=demo_db;"
```

To auto-configure your project file:
```
df add alltables --project demo.json
```

To generate a file with all the SQL INSERT statements:
```
df generate file -n demo.json -o demo.sql
```

To write directly to the database named in the above connection string:
```
df generate database -n demo.json
```

## Compatibility
This tool will configure all MS SQL databases including, of course, those on LocalDB and Express servers, as long as their compatibility level is 2017 or higher.
While it may work with previous versions, this has not been tested.

This tool has been tested on Windows 10, and depends on the 2017 version of SQLLocalDB. You can download this [here](https://www.microsoft.com/en-us/sql-server/sql-server-downloads-free-trial). If you click on "Download Media" you will be given the option to only download SQLLocalDB.