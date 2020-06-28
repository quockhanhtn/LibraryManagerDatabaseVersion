using LibraryManager.EntityFramework.Model.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.EntityFramework.Model.DataTransferObject
{
	public class BookDTO : View_Book
	{
		public string Note { get { return (this.Status == true) ? "Đang làm" : "Đã nghỉ"; } }
        public BookCategoryDTO BookCategory { get; set; }
		public PublisherDTO Publisher { get; set; }
		public List<AuthorDTO> ListAuthor { get; set; } = new List<AuthorDTO>();
        public string AuthorNames
		{
			get
			{
				string authorNames = "";
                foreach (var item in ListAuthor)
                {
					authorNames += item.NickName + ", ";
                }
				return authorNames.Substring(0, authorNames.Length - 2);
			}
		}
        public BookDTO() : base() { }
		public BookDTO(List<View_Book> bookRaw) : base()
		{
			if (bookRaw != null)
			{
				this.Id = bookRaw[0].Id;
				this.Title = bookRaw[0].Title;

				this.BookCategoryId = bookRaw[0].BookCategoryId;
				this.BookCategory = new BookCategoryDTO(DataProvider.Instance.Database.View_BookCategory.Where(x => x.Id == BookCategoryId).SingleOrDefault());

				this.PublisherId = bookRaw[0].PublisherId;
				this.Publisher = new PublisherDTO(DataProvider.Instance.Database.View_Publisher.Where(x => x.Id == PublisherId).SingleOrDefault());

				this.YearPublish = bookRaw[0].YearPublish;
				this.AuthorId = bookRaw[0].AuthorId;
				this.Price = bookRaw[0].Price;
				this.PageNumber = bookRaw[0].PageNumber;
				this.Size = bookRaw[0].Size;
				this.NumberOfBook = bookRaw[0].NumberOfBook;
				this.Status = bookRaw[0].Status;

                foreach (var book in bookRaw)
                {
					var a = DataProvider.Instance.Database.View_Author.Where(x => x.AuthorId == this.AuthorId).GroupBy(u => u.AuthorId).Select(grp => grp.ToList()).SingleOrDefault();
					this.ListAuthor.Add(new AuthorDTO(a));
                }
			}
		}
	}
}
