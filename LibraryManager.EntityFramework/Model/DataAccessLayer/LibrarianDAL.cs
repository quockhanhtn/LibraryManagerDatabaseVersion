using LibraryManager.EntityFramework.Model.DataTransferObject;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace LibraryManager.EntityFramework.Model.DataAccessLayer
{
    /// <summary>
    /// Class Data Access Layer for Librarian
    /// </summary>
    public class LibrarianDAL
    {
        public static LibrarianDAL Instance { get => (instance == null) ? new LibrarianDAL() : instance; }
        private LibrarianDAL() { }
        public ObservableCollection<LibrarianDTO> GetList(EnumStatus statusFillter = EnumStatus.AllStatus)
        {
            var listRaw = new List<Librarian>();
            switch (statusFillter)
            {
                case EnumStatus.AllStatus:
                    listRaw = DataProvider.Instance.Database.Librarians.Where(x => x.Id != "LIB000").ToList();
                    break;
                case EnumStatus.Active:
                    listRaw = DataProvider.Instance.Database.Librarians.Where(x => x.Id != "LIB000" && x.Status == true).ToList();
                    break;
                case EnumStatus.InActive:
                    listRaw = DataProvider.Instance.Database.Librarians.Where(x => x.Id != "LIB000" && x.Status == false).ToList();
                    break;
            }

            var listLibrarianDTO = new ObservableCollection<LibrarianDTO>();
            foreach (var lib in listRaw) { listLibrarianDTO.Add(new LibrarianDTO(lib)); }
            return listLibrarianDTO;
        }
        
        public void Add(LibrarianDTO newLibrarian)
        {
            var newLib = newLibrarian.GetBaseModel();
            DataProvider.Instance.SaveEntity(newLib, EntityState.Added);

            //DataProvider.Instance.Database.Entry(newLib).State = EntityState.Added;
            //DataProvider.Instance.Database.SaveChanges();
            //DataProvider.Instance.Database.Entry(newLib).State = EntityState.Detached;
        }

        public void Update(LibrarianDTO librarian)
        {
            var librarianUpdate = DataProvider.Instance.Database.Librarians.Where(x => x.Id == librarian.Id).SingleOrDefault();
            if (librarianUpdate != null)
            {
                librarianUpdate.LastName = librarian.LastName;
                librarianUpdate.FirstName = librarian.FirstName;
                librarianUpdate.Sex = librarian.Sex;
                librarianUpdate.Birthday = librarian.Birthday;
                librarianUpdate.SSN = librarian.SSN;
                librarianUpdate.Address = librarian.Address;
                librarianUpdate.Email = librarian.Email;
                librarianUpdate.PhoneNumber = librarian.PhoneNumber;
                librarianUpdate.StartDate = librarian.StartDate;
                librarianUpdate.Salary = librarian.Salary;

                DataProvider.Instance.SaveEntity(librarianUpdate, EntityState.Modified);
                //DataProvider.Instance.Database.Entry(librarianUpdate).State = EntityState.Modified;
                //DataProvider.Instance.Database.SaveChanges();
                //DataProvider.Instance.Database.Entry(librarianUpdate).State = EntityState.Detached;
            }
        }

        public void ChangeStatus(string idLibrarian)
        {
            var librarianUpdate = DataProvider.Instance.Database.Librarians.Where(x => x.Id == idLibrarian).SingleOrDefault();

            if (librarianUpdate != null)
            {
                librarianUpdate.Status = (librarianUpdate.Status == true) ? false : true;
                DataProvider.Instance.SaveEntity(librarianUpdate, EntityState.Modified);
                //DataProvider.Instance.Database.Entry(librarianUpdate).State = EntityState.Modified;
                //DataProvider.Instance.Database.SaveChanges();
                //DataProvider.Instance.Database.Entry(librarianUpdate).State = EntityState.Detached;
            }
        }

        public Librarian GetLibrarian(string idLibrarian)
        {
            return DataProvider.Instance.Database.Librarians.Where(x => x.Id == idLibrarian).SingleOrDefault();
        }

        private static LibrarianDAL instance;
    }
}
