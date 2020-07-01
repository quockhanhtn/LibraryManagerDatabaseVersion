using System;

namespace LibraryManager.EntityFramework.Model.DataTransferObject
{
    public class BorrowDTO : Borrow
    {
        public DateTime TermDate { get { return this.BorrowDate.AddDays((double)this.Book.BookCategory.LimitDays); } }
        public string LibrarianName { get { return this.Librarian.LastName + " " + this.Librarian.FirstName; } }

        public BorrowDTO() : base() { }
        public BorrowDTO(Borrow borrowRaw) : base()
        {
            if (borrowRaw != null)
            {
                this.Id = borrowRaw.Id;
                this.Book = borrowRaw.Book;
                this.BookId = borrowRaw.BookId;
                this.BorrowDate = borrowRaw.BorrowDate;
                this.Librarian = borrowRaw.Librarian;
                this.LibrarianId = borrowRaw.LibrarianId;
                this.Member = borrowRaw.Member;
                this.MemberId = borrowRaw.MemberId;
            }
        }
    }
}
