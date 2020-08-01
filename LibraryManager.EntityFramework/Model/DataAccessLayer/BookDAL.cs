using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.Utility.Enums;
using LibraryManager.Utility.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace LibraryManager.EntityFramework.Model.DataAccessLayer
{
    /// <summary>
    /// Class Data Access Layer for Book
    /// </summary>
    public class BookDAL : IDatabaseAccess<BookDTO, string>
    {
        public static BookDAL Instance { get => (instance == null) ? new BookDAL() : instance; }
        private BookDAL() { }
        public ObservableCollection<BookDTO> GetList(StatusFillter fillter = StatusFillter.AllStatus)
        {
            var listRaw = DataProvider.Instance.Database.Books.ToList();
            var listBookDTO = new ObservableCollection<BookDTO>();

            switch (fillter)
            {
                case StatusFillter.AllStatus:
                    listRaw = DataProvider.Instance.Database.Books.ToList();
                    break;
                case StatusFillter.Active:
                    listRaw = DataProvider.Instance.Database.Books.Where(x => x.Status == true).ToList();
                    break;
                case StatusFillter.InActive:
                    listRaw = DataProvider.Instance.Database.Books.Where(x => x.Status == false).ToList();
                    break;
            }

            foreach (var book in listRaw) { listBookDTO.Add(new BookDTO(book)); }
            return listBookDTO;
        }

        public ObservableCollection<BookDTO> GetList(int bookCategoryId, int publisherId)
        {
            var listRaw = DataProvider.Instance.Database.Books.Where(x => x.Status == true).ToList();
            var listBookDTO = new ObservableCollection<BookDTO>();

            foreach (var book in listRaw)
            {
                if (bookCategoryId == 0 && publisherId == 0)
                {
                    listBookDTO.Add(new BookDTO(book));
                }
                else if (bookCategoryId == 0)
                {
                    if (book.PublisherId == publisherId) { listBookDTO.Add(new BookDTO(book)); }
                }
                else if (publisherId == 0)
                {
                    if (book.BookCategoryId == bookCategoryId) { listBookDTO.Add(new BookDTO(book)); }
                }
                else
                {
                    if (book.PublisherId == publisherId && book.BookCategoryId == bookCategoryId) { listBookDTO.Add(new BookDTO(book)); }
                }
            }

            return listBookDTO;
        }

        public void Add(BookDTO newBook, int number)
        {
            var book = newBook.GetBaseModelWithoutAuthors();
            DataProvider.Instance.SaveEntity(book, EntityState.Added, true);

            //Lấy ra book vừa thêm vào Database
            var bookAdded = DataProvider.Instance.Database.Books.Where(x =>
                x.Title == book.Title && x.BookCategoryId == book.BookCategoryId
                && x.PublisherId == book.PublisherId && x.YearPublish == book.YearPublish
                && x.Size == book.Size).FirstOrDefault();

            //Cập nhật tác giả
            bookAdded.Authors = newBook.Authors;
            foreach (var item in bookAdded.Authors) { item.Books.Add(bookAdded); }
            DataProvider.Instance.SaveEntity(bookAdded, EntityState.Modified, true);

            //Cập nhật số lương
            var bookItem = new BookItem() { BookId = bookAdded.Id, Number = number, Count = number, Status = true };
            DataProvider.Instance.SaveEntity(bookItem, EntityState.Added, true);
        }

        public void Update(BookDTO bookUpdate, int number)
        {
            var book = DataProvider.Instance.Database.Books.Where(x => x.Id == bookUpdate.Id).FirstOrDefault();
            book.Title = bookUpdate.Title;
            book.BookCategoryId = bookUpdate.BookCategoryId;
            book.PublisherId = bookUpdate.PublisherId;
            book.YearPublish = bookUpdate.YearPublish;
            book.PageNumber = bookUpdate.PageNumber;
            book.Size = bookUpdate.Size;
            book.Price = bookUpdate.Price;
            book.Authors = bookUpdate.Authors;
            var distance = number - book.BookItem.Number;
            book.BookItem.Number += distance;
            book.BookItem.Count += distance;

            DataProvider.Instance.SaveEntity(book, EntityState.Modified, true);
        }

        public void ChangeStatus(string idBook)
        {
            var bookUpdate = DataProvider.Instance.Database.Books.Where(x => x.Id == idBook).SingleOrDefault();

            if (bookUpdate != null)
            {
                bookUpdate.Status = (bookUpdate.Status == true) ? false : true;
                DataProvider.Instance.SaveEntity(bookUpdate, EntityState.Modified);
            }
        }

        public Book GetBookById(string bookId)
        {
            return DataProvider.Instance.Database.Books.Where(x => x.Id == bookId).SingleOrDefault();
        }

        public void Remove(string bookId, int number)
        {
            var bookUpdate = GetBookById(bookId);
            bookUpdate.BookItem.Count -= number;
            bookUpdate.BookItem.Number -= number;

            DataProvider.Instance.SaveEntity(bookUpdate, EntityState.Modified, true);
        }

        public void Delete(string objectId) { throw new System.NotImplementedException(); }

        public void Add(BookDTO newObject) { throw new System.NotImplementedException(); }

        public void Update(BookDTO objectUpdate)
        {
            throw new System.NotImplementedException();
        }

        private static BookDAL instance;
    }
}
