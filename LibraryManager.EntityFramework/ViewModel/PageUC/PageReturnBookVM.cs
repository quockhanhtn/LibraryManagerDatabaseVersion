using LibraryManager.EntityFramework.Model;
using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.EntityFramework.View.PageUC;
using LibraryManager.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManager.EntityFramework.ViewModel.PageUC
{
    public class PageReturnBookVM : BaseViewModel
    {
        public MemberDTO MemberBorrow { get => memberBorrow; set { memberBorrow = value; OnPropertyChanged(); } }
        public LibrarianDTO LibrarianBorrow { get => librarianBorrow; set { librarianBorrow = value; OnPropertyChanged(); } }
        public ObservableCollection<BorrowDTO> ListBookBorrow { get => listBookBorrow; set { listBookBorrow = value; OnPropertyChanged(); } }
        public ObservableCollection<BorrowDTO> ListBookReturn { get => listBookReturn; set { listBookReturn = value; OnPropertyChanged(); } }
        public BorrowDTO SelectedBorrow { get => selectedBorrow; set { selectedBorrow = value; OnPropertyChanged(); } }
        public BorrowDTO SelectedReturn { get => selectedReturn; set { selectedReturn = value; OnPropertyChanged(); } }
        public ICommand ReturnBookCommand { get; set; }
        public ICommand LostBookCommand { get; set; }
        public ICommand UndoReturnBookCommand { get; set; }
        public ICommand SaveChangeCommand { get; set; }
        public ICommand DiscardChangeCommand { get; set; }
        

        public PageReturnBookVM(MemberDTO member, LibrarianDTO librarian)
        {
            MemberBorrow = member;
            LibrarianBorrow = librarian;

            ListBookBorrow = BorrowDAL.Instance.GetList(member.Id);
            ListBookReturn = new ObservableCollection<BorrowDTO>();

            var newBorrowBook = new ObservableCollection<BorrowDTO>();

            ReturnBookCommand = new RelayCommand<object>((p) => { return SelectedBorrow != null; }, (p) =>
            {
                ListBookReturn.Add(SelectedBorrow);
                ListBookBorrow.Remove(SelectedBorrow);
                
            });

            LostBookCommand = new RelayCommand<object>((p) => { return SelectedBorrow != null; }, (p) =>
            {
                ListBookReturn.Add(SelectedBorrow);
                ListBookBorrow.Remove(SelectedBorrow);
            });

            UndoReturnBookCommand = new RelayCommand<object> ((p) => { return SelectedReturn != null; }, (p) =>
            {
                ListBookBorrow.Add(SelectedReturn);
                ListBookReturn.Remove(SelectedReturn);
            });
        }

        MemberDTO memberBorrow;
        LibrarianDTO librarianBorrow;
        ObservableCollection<BorrowDTO> listBookBorrow;
        ObservableCollection<BorrowDTO> listBookReturn;
        BorrowDTO selectedBorrow;
        BorrowDTO selectedReturn;
    }
}
