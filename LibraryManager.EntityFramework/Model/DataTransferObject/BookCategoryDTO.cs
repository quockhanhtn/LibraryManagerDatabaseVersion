using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.EntityFramework.Model.DataTransferObject
{
    public class BookCategoryDTO : View_BookCategory
    {
        public string Note { get { return (this.Status != true) ? "Đã ẩn" : ""; } }
        public BookCategoryDTO() : base() { }
        public BookCategoryDTO(View_BookCategory bookCategoryView) : base()
        {
            this.Id = bookCategoryView.Id;
            this.Name = bookCategoryView.Name;
            this.LimitDays = bookCategoryView.LimitDays;
            this.NumberOfBook = bookCategoryView.NumberOfBook;
            this.Status = bookCategoryView.Status;
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
