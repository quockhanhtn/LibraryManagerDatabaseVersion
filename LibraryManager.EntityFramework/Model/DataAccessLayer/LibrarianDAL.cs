﻿using LibraryManager.EntityFramework.Model.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.EntityFramework.Model.DataAccessLayer
{

    public class LibrarianDAL
    {
        public static LibrarianDAL Instance { get => (instance == null) ? new LibrarianDAL() : instance; }
        private LibrarianDAL() { }

        public ObservableCollection<LibrarianDTO> GetList()
        {
            var listRaw = DataProvider.Instance.Database.Librarians.ToList();
            var listLibrarianDTO = new ObservableCollection<LibrarianDTO>();

            foreach (var lib in listRaw)
            {
                listLibrarianDTO.Add(new LibrarianDTO(lib));
            }

            return listLibrarianDTO;
        }
        public ObservableCollection<LibrarianDTO> GetListByFillter(bool status)
        {
            var listRaw = DataProvider.Instance.Database.Librarians.Where(x => x.Status == status).ToList();
            var listLibrarianDTO = new ObservableCollection<LibrarianDTO>();

            foreach (var lib in listRaw)
            {
                listLibrarianDTO.Add(new LibrarianDTO(lib));
            }

            return listLibrarianDTO;
        }

        public void Add(LibrarianDTO newLibrarian)
        {
            var newLib = newLibrarian.GetBaseModel();
            DataProvider.Instance.Database.Entry(newLib).State = EntityState.Added;
            DataProvider.Instance.Database.SaveChanges();
            DataProvider.Instance.Database.Entry(newLib).State = EntityState.Detached;
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
                librarianUpdate.Salary = librarian.Salary;
            }

            DataProvider.Instance.Database.Entry(librarianUpdate).State = EntityState.Modified;
            DataProvider.Instance.Database.SaveChanges();
            DataProvider.Instance.Database.Entry(librarianUpdate).State = EntityState.Detached;
        }

        private static LibrarianDAL instance;
    }
    
}
