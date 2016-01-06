using System.Collections.Generic;

namespace DatabaseCleaner
{
    public class TableRegistry
    {
        private List<string> registeredTables = new List<string>();

        public bool IsRegistered(string tableName)
        {
            return this.registeredTables.Contains(tableName);
        }

        public void Register(string tableName)
        {
            this.registeredTables.Add(tableName);
        }
    }
}
