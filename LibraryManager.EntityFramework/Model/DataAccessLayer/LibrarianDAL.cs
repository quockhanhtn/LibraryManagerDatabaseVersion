using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.Utility.Enums;
using LibraryManager.Utility.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace LibraryManager.EntityFramework.Model.DataAccessLayer
{
    /// <summary>
    /// Class Data Access Layer for Librarian
    /// </summary>
    public class LibrarianDAL : IDatabaseAccess<LibrarianDTO, string>
    {
        public static LibrarianDAL Instance { get => (instance == null) ? new LibrarianDAL() : instance; }
        private LibrarianDAL() { }

        public ObservableCollection<LibrarianDTO> GetList(StatusFillter fillter = StatusFillter.AllStatus)
        {
            var listLibrarianDTO = new ObservableCollection<LibrarianDTO>();
            var listRaw = new List<Librarian>();

            switch (fillter)
            {
                case StatusFillter.AllStatus:
                    listRaw = DataProvider.Instance.Database.Librarians.Where(x => x.Id != "LIB000").ToList();
                    break;
                case StatusFillter.Active:
                    listRaw = DataProvider.Instance.Database.Librarians.Where(x => x.Id != "LIB000" && x.Status == true).ToList();
                    break;
                case StatusFillter.InActive:
                    listRaw = DataProvider.Instance.Database.Librarians.Where(x => x.Id != "LIB000" && x.Status == false).ToList();
                    break;
            }

            foreach (var lib in listRaw) { listLibrarianDTO.Add(new LibrarianDTO(lib)); }
            return listLibrarianDTO;
        }

        public ObservableCollection<LibrarianDTO> GetList(DateTime fromDate, DateTime toDate)
        {
            var listLibrarianDTO = new ObservableCollection<LibrarianDTO>();
            var listRaw = DataProvider.Instance.Database.Librarians.Where(x => x.Id != "LIB000").ToList();

            foreach (var item in listRaw)
            {
                if (item.StartDate.Value.Date >= fromDate.Date && item.StartDate.Value.Date <= toDate.Date)
                {
                    listLibrarianDTO.Add(new LibrarianDTO(item));
                }
            }
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

        public void Delete(string objectId) { throw new System.NotImplementedException(); }

        private static LibrarianDAL instance;
    }
}
