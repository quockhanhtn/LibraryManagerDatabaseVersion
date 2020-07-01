using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.EntityFramework.Model.DataTransferObject
{
    public class PayFineInfoDTO : PayFineInfo
    {
        public PayFineInfoDTO() : base()
        {
        }

        public PayFineInfoDTO(BorrowDTO borrow) : base()
        {

        }
    }
}
