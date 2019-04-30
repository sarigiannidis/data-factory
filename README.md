[![CodeFactor](https://www.codefactor.io/repository/github/sarigiannidis/data-factory/badge)](https://www.codefactor.io/repository/github/sarigiannidis/data-factory)
![latest release](https://img.shields.io/github/release-pre/sarigiannidis/data-factory.svg)
![MIT license](https://img.shields.io/github/license/sarigiannidis/data-factory.svg)

# Data Factory

```data-factory``` fills MS SQL databases with random data according to your configuration.
It respects foreign key relationships and primary keys, it can handle all value types, and it is very extensible.

This tool **WILL** overwrite data in your database, so backup your data!

## Downloads

The binaries are only published for win-x64. Please check the [releases](https://github.com/sarigiannidis/data-factory/releases) for the latest version.

## Usage

To create a new project file given a connection string:
```batchfile
df new --name demo.json -c "Data Source=(localdb)\demo_server;Initial Catalog=demo_db;"
```

To auto-configure your project file:
```batchfile
df add alltables --project demo.json
```

To generate a file with all the SQL INSERT statements:
```batchfile
df generate file -n demo.json -o demo.sql
```

To write directly to the database named in the above connection string:
```batchfile
df generate database -n demo.json
```

## Extensibility

Developers may create their own configurable value factories if the ones provided do not meet their needs. Please see the projects ```df.valuefactories``` and ```df.valuefactories.spatial``` for examples.

## Compatibility

This tool will configure all MS SQL databases including, of course, those on SQLLocalDB and Express servers, as long as their compatibility level is 2017 or higher.
While it may work with previous versions, this has not been tested.

This tool has been tested on Windows 10, and depends on the 2017 version of SQLLocalDB. You can download this [here](https://www.microsoft.com/en-us/sql-server/sql-server-downloads-free-trial). If you click on "Download Media" you will be given the option to only download SQLLocalDB.

## Licensing

The ```data-factory``` project is open source. The code is published on [github](https://github.com/sarigiannidis/data-factory) under the [MIT license](https://github.com/sarigiannidis/data-factory/blob/master/LICENSE).

The Xeger code in ```Df.Stochastic.Fare``` has been copied from [Fare](https://github.com/moodmosaic/Fare) by [moodmosaic](https://github.com/moodmosaic/) - many thanks to [moodmosaic](https://github.com/moodmosaic/) for maintaining such a great port. Most -but not all!- of [Fare](https://github.com/moodmosaic/Fare) is covered by an MIT license. Please review the licenses there if you are considering incorporating any impacted part of the ```data-factory``` code into your project.

The ```data-factory``` project also depends on numerous NuGet packages which all come with their own licenses. Please visit the project's [dependencies](https://github.com/sarigiannidis/data-factory/network/dependencies) to view an extensive and current list of all referenced packages.

A good developer always reviews their licenses.

## Contributing

If you would like to contribute, submit a bug, or suggest a feature, please see [CONTRIBUTING.md](CONTRIBUTING.md).
