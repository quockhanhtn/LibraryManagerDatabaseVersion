using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.EntityFramework.Model.DataTransferObject
{
    public class ReturnBookDTO : ReturnBook
    {
        public ReturnBookDTO(BorrowDTO borrow)
        {
            this.BorrowId = borrow.Id;
            
        }
    }
}
