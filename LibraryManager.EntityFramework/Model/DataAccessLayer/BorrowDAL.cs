﻿using LibraryManager.EntityFramework.Model.DataTransferObject;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace LibraryManager.EntityFramework.Model.DataAccessLayer
{
    /// <summary>
    /// Class Data Access Layer for Borrow
    /// </summary>
    public class BorrowDAL
    {
        public static BorrowDAL Instance { get => (instance == null) ? new BorrowDAL() : instance; }
        private BorrowDAL() { }
        public ObservableCollection<BorrowDTO> GetList(string memberId)
        {
            var result = new ObservableCollection<BorrowDTO>();
            foreach (var item in DataProvider.Instance.Database.Borrows.Where(x => x.MemberId == memberId).ToList())
            {
                result.Add(new BorrowDTO(item));
            }
            return result;
        }
        public void Add(string memberId, string librarianId, string bookId)
        {
            var br = new Borrow() { BookId = bookId, MemberId = memberId, LibrarianId = librarianId, BorrowDate = DateTime.Now };
            DataProvider.Instance.SaveEntity(br, EntityState.Added, true);
        }

        private static BorrowDAL instance;
    }
}