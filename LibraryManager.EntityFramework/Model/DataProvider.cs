using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Windows;

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

        public void SaveEntity(object entity, EntityState entityState, bool reloadDatabase = false)
        {
            if (entity == null) { return; }

            try
            {
                Database.Entry(entity).State = entityState;
                Instance.Database.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    MessageBox.Show("Entity of type \"" + eve.Entry.Entity.GetType().Name + "\" in state \"" + eve.Entry.State + "\" has the following validation errors:");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        MessageBox.Show("- Property: \"" + ve.PropertyName + "\", Error: \"" + ve.ErrorMessage + "\"");
                    }
                }
                throw;
            }

            Instance.Database.Entry(entity).State = EntityState.Detached;

            if (reloadDatabase) { Reload(); }
        }
    }
}