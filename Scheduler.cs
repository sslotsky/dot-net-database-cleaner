using System;
using System.Data.Entity;

namespace DatabaseCleaner
{
    public class Scheduler
    {
        private TableRegistry registry;
        private string startingTableName;
        private Func<DbContext> contextResolver;

        public Scheduler(string tableName, Func<DbContext> contextResolver)
        {
            this.startingTableName = tableName;
            this.contextResolver = contextResolver;
            registry = new TableRegistry();
        }

        public void Schedule()
        {
            using (var context = this.contextResolver())
            {
                this.Clean(context, startingTableName);
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
