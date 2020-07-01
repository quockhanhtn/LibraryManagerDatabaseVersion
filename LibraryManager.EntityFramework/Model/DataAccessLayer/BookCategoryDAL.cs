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
    /// Class Data Access Layer for BookCategory
    /// </summary>
    public class BookCategoryDAL : IDatabaseAccess<BookCategoryDTO, int>
    {
        public static BookCategoryDAL Instance { get => (instance == null) ? new BookCategoryDAL() : instance; }
        private BookCategoryDAL() { }

        public ObservableCollection<BookCategoryDTO> GetList(StatusFillter fillter = StatusFillter.AllStatus)
        {
            var listRaw = new List<BookCategory>();
            var listBookCategoryDTO = new ObservableCollection<BookCategoryDTO>();

            switch (fillter)
            {
                case StatusFillter.AllStatus:
                    listRaw = DataProvider.Instance.Database.BookCategories.ToList();
                    break;
                case StatusFillter.Active:
                    listRaw = DataProvider.Instance.Database.BookCategories.Where(x => x.Status == true).ToList();
                    break;
                case StatusFillter.InActive:
                    listRaw = DataProvider.Instance.Database.BookCategories.Where(x => x.Status == false).ToList();
                    break;
            }

            foreach (var bookCategory in listRaw) { listBookCategoryDTO.Add(new BookCategoryDTO(bookCategory)); }
            return listBookCategoryDTO;
        }

        public void Add(BookCategoryDTO newBookCategory)
        {
            var newBC = newBookCategory.GetBaseModel();
            DataProvider.Instance.SaveEntity(newBC, EntityState.Added, true);
        }

        public void Update(BookCategoryDTO bookCategory)
        {
            var bookCategoryUpdate = DataProvider.Instance.Database.BookCategories.Where(x => x.Id == bookCategory.Id).SingleOrDefault();
            if (bookCategoryUpdate != null)
            {
                bookCategoryUpdate.Name = bookCategory.Name;
                bookCategoryUpdate.LimitDays = bookCategory.LimitDays;

                DataProvider.Instance.SaveEntity(bookCategoryUpdate, EntityState.Modified, true);
            }
        }

        public void ChangeStatus(int idBookCategory)
        {
            var bookCategoryUpdate = DataProvider.Instance.Database.BookCategories.Where(x => x.Id == idBookCategory).SingleOrDefault();

            if (bookCategoryUpdate != null)
            {
                bookCategoryUpdate.Status = (bookCategoryUpdate.Status == true) ? false : true;
                DataProvider.Instance.SaveEntity(bookCategoryUpdate, EntityState.Modified, true);
            }
        }

        public void Delete(int idBookCategory)
        {
            var bookCategoryDelete = DataProvider.Instance.Database.BookCategories.Where(x => x.Id == idBookCategory).SingleOrDefault();

            if (bookCategoryDelete != null)
            {
                DataProvider.Instance.Database.BookCategories.Remove(bookCategoryDelete);
                DataProvider.Instance.Database.SaveChanges();
            }
        }

        private static BookCategoryDAL instance;
    }
}
