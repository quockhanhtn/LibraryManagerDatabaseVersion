using LibraryManager.EntityFramework.Model.DataTransferObject;
using System;
using System.Collections.ObjectModel;

namespace LibraryManager.EntityFramework.Model.DataAccessLayer
{
    /// <summary>
    /// Class Data Access Layer for ReturnBook
    /// </summary>
    public class ReturnBookDAL
    {
        public static ReturnBookDAL Instance { get => (instance == null) ? new ReturnBookDAL() : instance; }
        private ReturnBookDAL() { }

        public void Add(ObservableCollection<BorrowDTO> listBookReturn, string librarianId)
        {
            foreach (var borrow in listBookReturn)
            {
                var returnBook = new ReturnBook() { BorrowId = borrow.Id, ReturnDate = DateTime.Now , LibrarianId = librarianId };
                DataProvider.Instance.SaveEntity(returnBook, System.Data.Entity.EntityState.Added, true);
            }
        }

        private static ReturnBookDAL instance;
    }
}
