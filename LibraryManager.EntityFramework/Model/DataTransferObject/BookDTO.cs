﻿namespace LibraryManager.EntityFramework.Model.DataTransferObject
{
	public class BookDTO : Book
	{
        public string AuthorNames
		{
			get
			{
				string authorNames = "";
                foreach (var item in Authors) { authorNames += item.NickName + ", "; }
				return authorNames.Length > 2 ? authorNames.Substring(0, authorNames.Length - 2) : "";
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

        public Book GetBaseModelWithoutAuthors()
        {
            return new Book()
            {
                Id = "",
                Title = this.Title,
                BookCategoryId = this.BookCategoryId,
                PublisherId = this.PublisherId,
                YearPublish = this.YearPublish,
                PageNumber = this.PageNumber,
                Size = this.Size,
                Price = this.Price,
                //Authors = this.Authors,
                Status = true
            };
        }
	}
}
