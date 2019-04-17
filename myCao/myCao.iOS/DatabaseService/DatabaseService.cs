using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Foundation;
using myCao.DatabaseService;
using myCao.iOS.DatabaseService;
using SQLite;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseService))]
namespace myCao.iOS.DatabaseService
{
    class DatabaseService : IDBInterface
    {
        public SQLiteAsyncConnection CreateConnection(string dbName)
        {
            var sqlFilename = dbName + ".db";
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            string path = Path.Combine(libFolder, sqlFilename);

          
            if(!File.Exists(path))
            {
                var existingDb = NSBundle.MainBundle.PathForResource(dbName, "db");
                File.Copy(existingDb, path);
            }
            
            var connection = new SQLite.SQLiteAsyncConnection(path);

            // Return the database connection 
            return connection;

        }
    }
}