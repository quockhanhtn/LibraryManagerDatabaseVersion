using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.Utility;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManager.EntityFramework.ViewModel.PageUC
{
    public class PageStatisticBookBorrowVM : BaseViewModel
    {
        public ObservableCollection<BorrowDTO> ListBookBorrow { get => listBookBorrow; set { listBookBorrow = value; OnPropertyChanged(); } }
        public BookDTO BookStatistic { get => bookStatistic; set { bookStatistic = value; OnPropertyChanged(); } }

        public ICommand BackCommand { get; set; }

        public PageStatisticBookBorrowVM(BookDTO bookStatistic)
        {
            BookStatistic = bookStatistic;
            ListBookBorrow = BorrowDAL.Instance.GetListByBookId(bookStatistic.Id);

            BackCommand = new RelayCommand<UserControl>((p) => { return p != null; }, (p) =>
            {
                try
                {
                    var w = FrameworkElementExtend.GetRootParent(p) as Window;
                    var gridMain = w.FindName("gridMain") as Grid;
                    gridMain.Children.Remove(p);
                }
                catch (System.Exception) { }
            });
        }

        ObservableCollection<BorrowDTO> listBookBorrow;
        BookDTO bookStatistic;
    }
}
