using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace myCao.DatabaseService
{
    public interface IDBInterface
    {
        SQLiteAsyncConnection CreateConnection(string dbName);
    }
}
