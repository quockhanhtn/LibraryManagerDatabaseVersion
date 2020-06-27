using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.EntityFramework.Model
{
    public class DataProvider
    {
        private static DataProvider instance;

        public static DataProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataProvider();
                }
                return instance;
            }
            set => instance = value;
        }

        public LibraryManagerEntities Database { get; set; }

        private DataProvider()
        {
            Database = new LibraryManagerEntities();
        }

        public void Reload()
        {
            Database.Dispose();
            Database = new LibraryManagerEntities();
        }

        public void SaveEntity(object entity, EntityState entityState, bool reloadDatabase=false)
        {
            Database.Entry(entity).State = entityState;
            Instance.Database.SaveChanges();
            Instance.Database.Entry(entity).State = EntityState.Detached;

            if (reloadDatabase) { Reload(); }
        }
    }
}
