using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.EntityFramework.ViewModel
{
    public class PayFineInfoWindowViewModel : BaseViewModel
    {
        public string MemberInfo { get; set; }
        public string LibrarianInfo { get; set; }
        public PayFineInfoDTO PayFineInfoSelected { get => payFineInfoSelected; set { payFineInfoSelected = value; OnPropertyChanged(); } }
        public ObservableCollection<PayFineInfoDTO> ListPayFineInfo { get => listPayFineInfo; set { listPayFineInfo = value; OnPropertyChanged(); } }

        public PayFineInfoWindowViewModel(LibrarianDTO librarianDTO, MemberDTO memberDTO, ObservableCollection<BorrowDTO> listBookReturn)
        {
            LibrarianInfo = librarianDTO.FullName + " - " + librarianDTO.Id;
            MemberInfo = memberDTO.FullName + " - " + memberDTO.Id;
        }

        PayFineInfoDTO payFineInfoSelected;
        ObservableCollection<PayFineInfoDTO> listPayFineInfo;
    }
}
