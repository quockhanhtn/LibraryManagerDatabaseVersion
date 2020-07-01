using LibraryManager.EntityFramework.Model.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.EntityFramework.Model.DataAccessLayer
{
    /// <summary>
    /// Class Data Access Layer for PayFineInfo
    /// </summary>
    public class PayFineInfoDAL
    {
        public static PayFineInfoDAL Instance { get => (instance == null) ? new PayFineInfoDAL() : instance; }
        private PayFineInfoDAL() { }

        public ObservableCollection<PayFineInfoDTO> CreateList(ObservableCollection<BorrowDTO> listBookReturn)
        {
            var result = new ObservableCollection<PayFineInfoDTO>();


            return result;
        }

        private static PayFineInfoDAL instance;
    }

}
