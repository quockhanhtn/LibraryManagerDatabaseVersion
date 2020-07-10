using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.Utility;
using System.Collections.ObjectModel;

namespace LibraryManager.EntityFramework.ViewModel.PageUC
{
    public class PageMemberBorrowListVM : BaseViewModel
    {
        public ObservableCollection<BorrowDTO> ListBookBorrow { get => listBookBorrow; set { listBookBorrow = value; OnPropertyChanged(); } }

        public PageMemberBorrowListVM(string memberId)
        {
            ListBookBorrow = BorrowDAL.Instance.GetListByMemberId(memberId);
        }

        ObservableCollection<BorrowDTO> listBookBorrow;
    }
}
