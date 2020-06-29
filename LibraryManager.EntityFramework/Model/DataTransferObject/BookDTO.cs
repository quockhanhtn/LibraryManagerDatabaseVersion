using LibraryManager.EntityFramework.Model.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.EntityFramework.Model.DataTransferObject
{
	public class BookDTO : Book
	{
        public string AuthorNames
		{
			get
			{
				string authorNames = "";
                foreach (var item in Authors) { authorNames += item.NickName + ", "; }
				return authorNames.Substring(0, authorNames.Length - 2);
			}
		}
        public BookDTO() : base() { }
		public BookDTO(Book bookRaw) : base()
		{
            if (bookRaw != null)
            {
                this.Id = bookRaw.Id;
                this.Title = bookRaw.Title;
                this.PublisherId = bookRaw.PublisherId;
                this.YearPublish = bookRaw.YearPublish;
                this.BookCategoryId = bookRaw.BookCategoryId;
                this.PageNumber = bookRaw.PageNumber;
                this.Size = bookRaw.Size;
                this.Price = bookRaw.Price;
                this.Status = bookRaw.Status;

                this.BookCategory = bookRaw.BookCategory;
                this.Publisher = bookRaw.Publisher;
                this.BookItem = bookRaw.BookItem;
                this.Borrows = bookRaw.Borrows;
                this.Authors = bookRaw.Authors;
            }
		}
	}
}
