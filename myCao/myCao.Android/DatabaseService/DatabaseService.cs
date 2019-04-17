using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using myCao.Droid.DatabaseService;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseService))]
namespace myCao.Droid.DatabaseService
{
    class DatabaseService:myCao.DatabaseService.IDBInterface
    {
        public DatabaseService()
        {

        }

        public SQLiteAsyncConnection CreateConnection(string dbName)
        {
            var sqlFileName = dbName + ".db";
            string documentsDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsDir, sqlFileName);
            if(File.Exists(path))
            {
                var tempConnection = new SQLiteAsyncConnection(path);
                int version = tempConnection.ExecuteScalarAsync<int>("PRAGMA user_version;").Result;

                if(version !=1)
                {
                    File.Delete(path);
                }
                

                tempConnection.CloseAsync();
            }

            if(!File.Exists(path))
            {
                WriteSQLDB(sqlFileName, path);
            }
            
            var connection = new SQLiteAsyncConnection(path);

            return connection;
        }

        private void WriteSQLDB(string sqlFileName, string path)
        {
            using (var binaryReader = new BinaryReader(Application.Context.Assets.Open(sqlFileName)))
            {
                using (var binaryWriter = new BinaryWriter(new FileStream(path, FileMode.Create)))
                {
                    byte[] buffer = new byte[2048];
                    int length = 0;
                    while ((length = binaryReader.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        binaryWriter.Write(buffer, 0, length);
                    }
                }
            }
        }


    }
}