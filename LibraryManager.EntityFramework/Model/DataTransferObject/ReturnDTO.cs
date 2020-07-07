using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.EntityFramework.Model.DataTransferObject
{
    public class ReturnDTO : ReturnBook
    {
        public string LibrarianBorrow { get { return this.Borrow.Librarian.LastName + " " + this.Borrow.Librarian.FirstName; } }
        public string LibrarianReturn { get { return this.Librarian.LastName + " " + this.Librarian.FirstName; } }
        public string MemberName { get { return this.Borrow.Member.LastName + " " + this.Borrow.Member.FirstName; } }

        public ReturnDTO() : base() { }
        public ReturnDTO(ReturnBook returnRaw) : base()
        {
            if (returnRaw != null)
            {
                this.Id = returnRaw.Id;
                this.BorrowId = returnRaw.BorrowId;
                this.ReturnDate = returnRaw.ReturnDate;
                this.LibrarianId = returnRaw.LibrarianId;

                this.Borrow = returnRaw.Borrow;
                this.Librarian = returnRaw.Librarian;
            }
        }
    }
}
