﻿using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.Utility.Enums;
using LibraryManager.Utility.Interfaces;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Ink;
using System.Windows.Media;

namespace LibraryManager.EntityFramework.Model.DataAccessLayer
{
    /// <summary>
    /// Class Data Access Layer for Book
    /// </summary>
    public class BookDAL : IDatabaseAccess<BookDTO, string>
    {
        public static BookDAL Instance { get => (instance == null) ? new BookDAL() : instance; }
        private BookDAL() { }
        public ObservableCollection<BookDTO> GetList()
        {
            var listRaw = DataProvider.Instance.Database.Books.ToList();
            var listBookDTO = new ObservableCollection<BookDTO>();

            foreach (var book in listRaw) { listBookDTO.Add(new BookDTO(book)); }

            return listBookDTO;
        }

        public ObservableCollection<BookDTO> GetList(int bookCategoryId, int publisherId)
        {
            var listRaw = DataProvider.Instance.Database.Books.ToList();
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

        public void Add(BookDTO newBook,int number)
        {
            var book = newBook.GetBaseModel();
            DataProvider.Instance.SaveEntity(book, EntityState.Added);
            BookItem bookItem = new BookItem() { BookId = book.Id, Number = number, Count = number, Status = true };
            DataProvider.Instance.SaveEntity(bookItem, EntityState.Added, true);
        }

        public void Update(BookDTO book)
        {
            //var bookUpdate = DataProvider.Instance.Database.Books.Where(x => x.Id == book.Id).SingleOrDefault();
            //if (bookUpdate != null)
            //{
            //    bookUpdate.LastName = book.LastName;
            //    bookUpdate.FirstName = book.FirstName;
            //    bookUpdate.Sex = book.Sex;
            //    bookUpdate.Birthday = book.Birthday;
            //    bookUpdate.SSN = book.SSN;
            //    bookUpdate.Address = book.Address;
            //    bookUpdate.Email = book.Email;
            //    bookUpdate.PhoneNumber = book.PhoneNumber;
            //    bookUpdate.StartDate = book.StartDate;
            //    bookUpdate.Salary = book.Salary;
            //}

            //DataProvider.Instance.SaveEntity(bookUpdate, EntityState.Modified);

            //DataProvider.Instance.Database.Entry(bookUpdate).State = EntityState.Modified;
            //DataProvider.Instance.Database.SaveChanges();
            //DataProvider.Instance.Database.Entry(bookUpdate).State = EntityState.Detached;
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

        public ObservableCollection<BookDTO> GetList(StatusFillter fillter)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(string objectId)
        {
            throw new System.NotImplementedException();
        }

        public void Add(BookDTO newObject)
        {
            throw new System.NotImplementedException();
        }

        private static BookDAL instance;
    }
}
