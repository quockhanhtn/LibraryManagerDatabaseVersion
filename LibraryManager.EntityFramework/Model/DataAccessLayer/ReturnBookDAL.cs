using LibraryManager.EntityFramework.Model.DataTransferObject;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace LibraryManager.EntityFramework.Model.DataAccessLayer
{
    /// <summary>
    /// Class Data Access Layer for ReturnBook
    /// </summary>
    public class ReturnBookDAL
    {
        public static ReturnBookDAL Instance { get => (instance == null) ? new ReturnBookDAL() : instance; }
        private ReturnBookDAL() { }

        public ObservableCollection<ReturnDTO> GetListByDate(DateTime fromDate, DateTime toDate)
        {
            var result = new ObservableCollection<ReturnDTO>();
            foreach (var item in DataProvider.Instance.Database.ReturnBooks.ToList())
            {
                if (item.ReturnDate.Date >= fromDate.Date && item.ReturnDate.Date <= toDate.Date)
                {
                    result.Add(new ReturnDTO(item));
                }
            }
            return result;
        }

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
