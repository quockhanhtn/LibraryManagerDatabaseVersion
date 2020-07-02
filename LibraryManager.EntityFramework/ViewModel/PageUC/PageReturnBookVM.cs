using LibraryManager.EntityFramework.Model;
using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.EntityFramework.View.PageUC;
using LibraryManager.Utility;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManager.EntityFramework.ViewModel.PageUC
{
    public class PageReturnBookVM : BaseViewModel
    {
        public MemberDTO MemberBorrow { get => memberBorrow; set { memberBorrow = value; OnPropertyChanged(); } }
        public LibrarianDTO LibrarianReturn { get => librarianReturn; set { librarianReturn = value; OnPropertyChanged(); } }
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
            LibrarianReturn = librarian;

            ListBookBorrow = BorrowDAL.Instance.GetListByMemberId(member.Id);
            ListBookReturn = new ObservableCollection<BorrowDTO>();

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

            SaveChangeCommand = new RelayCommand<UserControl>((p) => { return (p != null); }, (p) =>
            {
                var listBookReturnLate = ListBookReturn.Where(x => x.TermDate < DateTime.Now.Date) as ObservableCollection<BorrowDTO>;

                //var payFineVM = new PayFineInfoWindowViewModel(librarian, member, listBookReturnLate);
                //var payFineInfoWindow = new PayFineInfoWindow() { DataContext = payFineVM };
                //payFineInfoWindow.ShowDialog();

                ReturnBookDAL.Instance.Add(ListBookReturn, librarian.Id);

                try
                {
                    var w = FrameworkElementExtend.GetWindowParent(p) as Window;
                    var gridMain = w.FindName("gridMain") as Grid;
                    gridMain.Children.Remove(p);
                    var dt = (gridMain.Children[0] as PageBookManager).DataContext as PageBookManagerVM;
                    dt.ReloadList();
                }
                catch (Exception) { }
            });

            DiscardChangeCommand = new RelayCommand<UserControl>((p) => { return (p != null); }, (p) =>
            {
                try
                {
                    var w = FrameworkElementExtend.GetWindowParent(p) as Window;
                    (w.FindName("gridMain") as Grid).Children.Remove(p);
                }
                catch (Exception) { }
            });
        }

        MemberDTO memberBorrow;
        LibrarianDTO librarianReturn;
        ObservableCollection<BorrowDTO> listBookBorrow;
        ObservableCollection<BorrowDTO> listBookReturn;
        BorrowDTO selectedBorrow;
        BorrowDTO selectedReturn;
    }
}
