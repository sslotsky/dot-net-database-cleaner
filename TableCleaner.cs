using DatabaseCleaner.Properties;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DatabaseCleaner
{
    public class TableCleaner
    {
        private DbContext context;
        private string tableName;

        public TableCleaner(string tableName, DbContext context) 
        {
            this.context = context;
            this.tableName = tableName;
        }

        public List<string> TablesWithForeignKeys()
        {
            string sql = string.Format(Resources.TablesWithKeys, this.tableName);
            return this.context.Database.SqlQuery<string>(sql).ToList();
        }

        public void Clean()
        {
            context.Database.ExecuteSqlCommand(string.Format("DELETE FROM [{0}]", this.tableName));
        }
    }
}
