using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.EntityFramework.Model.DataTransferObject
{
    public class BookCategoryDTO : BookCategory
    {
        public string Note { get { return (this.Status != true) ? "Đã ẩn" : ""; } }
        public int NumberOfBook { get => Books.Count; }
        public BookCategoryDTO() : base() { }
        public BookCategoryDTO(BookCategory bookCategoryRaw) : base()
        {
            if (bookCategoryRaw != null)
            {
                this.Id = bookCategoryRaw.Id;
                this.Name = bookCategoryRaw.Name;
                this.LimitDays = bookCategoryRaw.LimitDays;
                this.Status = bookCategoryRaw.Status;

                this.Books = bookCategoryRaw.Books;
            }
        }

        public BookCategory GetBaseModel()
        {
            return new BookCategory()
            {
                Name = this.Name,
                LimitDays = this.LimitDays,
                Status = this.Status
            };
        }
    }
}
