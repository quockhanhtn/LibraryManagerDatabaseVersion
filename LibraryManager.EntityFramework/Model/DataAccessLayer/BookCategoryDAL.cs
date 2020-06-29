using LibraryManager.EntityFramework.Model.DataTransferObject;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace LibraryManager.EntityFramework.Model.DataAccessLayer
{
    /// <summary>
    /// Class Data Access Layer for BookCategory
    /// </summary>
    public class BookCategoryDAL
    {
        public static BookCategoryDAL Instance { get => (instance == null) ? new BookCategoryDAL() : instance; }
        private BookCategoryDAL() { }

        public ObservableCollection<BookCategoryDTO> GetList()
        {
            var listRaw = DataProvider.Instance.Database.BookCategories.ToList();
            var listBookCategoryDTO = new ObservableCollection<BookCategoryDTO>();

            foreach (var bookCategory in listRaw) { listBookCategoryDTO.Add(new BookCategoryDTO(bookCategory)); }

            return listBookCategoryDTO;
        }

        public ObservableCollection<BookCategoryDTO> GetList(bool status)
        {
            var listRaw = DataProvider.Instance.Database.BookCategories.Where(x => x.Status == status).ToList();
            var listBookCategoryDTO = new ObservableCollection<BookCategoryDTO>();

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
