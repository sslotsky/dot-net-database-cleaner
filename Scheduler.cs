using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace DatabaseCleaner
{
    public class Scheduler
    {
        private TableRegistry registry;
        private IEnumerable<string> tableNames;
        private Func<DbContext> contextResolver;

        public Scheduler(Func<DbContext> contextResolver, params string[] tableNames)
        {
            this.tableNames = tableNames;
            this.contextResolver = contextResolver;
            registry = new TableRegistry();
        }

        public void Schedule()
        {
            using (var context = this.contextResolver())
            {
                foreach(var tableName in this.tableNames)
                {
                    this.Clean(context, tableName);
                }
                context.SaveChanges();
            }
        }

        private void Clean(DbContext context, string tableName)
        {
            if (!this.registry.IsRegistered(tableName))
            {
                this.registry.Register(tableName);
                var cleaner = new TableCleaner(tableName, context);
                foreach (string name in cleaner.TablesWithForeignKeys())
                {
                    this.Clean(context, name);
                }

                cleaner.Clean();
            }
        }
    }
}
