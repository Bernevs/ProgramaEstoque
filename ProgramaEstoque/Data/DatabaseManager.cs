using System;
using System.Data;
using System.Data.SQLite;
using ProgramaEstoque.Models;

namespace ProgramaEstoque.Data
{
    public class DatabaseManager
    {   
        private const string DatabaseFilename = "C:\\Users\\berne\\OneDrive\\Área de Trabalho\\Projetos\\ProgramaEstoque\\ProgramaEstoque\\Data\\banco.db";
        private static SQLiteConnection db;

        public static SQLiteConnection GetConnection()
        {
            if (db == null)
            {
                db = new SQLiteConnection("Data Source=" + DatabaseFilename);
                db.Open();
            }
            return db;
        }

        public static void CloseConnection()
        {
            if (db != null)
            {
                db.Close();
                db = null;
            }
        }

    }

}
