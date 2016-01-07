# dot-net-database-cleaner

## Overview

The purpose of this package is to assist in database cleaning for unit tests that persist data to a test database. Given a starting
table, `DatabaseCleaner` will build a tree of tables, where every child table has a foreign key constraint that points to its parent 
table. Once the tree is built, the tables will be emptied from the leaves on upward.

## Installation

This package is available on [NuGet](https://www.nuget.org/packages/DatabaseCleaner/1.0.0). Alternatively, you can clone this 
repository to a subfolder of your project.

## Usage

Usage is very simple: instantiate a `Scheduler` object, supplying the starting table name and a function that returns a new `DbContext`
derivative. Here is an example unit test.

```c#
using DatabaseCleaner;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProject.Models;

namespace Testing
{
    public class FooTest
    {
        [TestInitialize]
        public virtual void Before()
        {
            var scheduler = new Scheduler(() => new MyDbContext("name=MyConnectionString"), "Client");
            scheduler.Schedule();
        }
    }
}
```

## Contribution

If you wish to contribute to this project, please make a fork and submit a pull request.
