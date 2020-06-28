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
    public class PageBorrowBookVM : BaseViewModel
    {
        public MemberDTO MemberBorrow { get => memberBorrow; set { memberBorrow = value; OnPropertyChanged(); } }

        public LibrarianDTO LibrarianBorrow { get => librarianBorrow; set { librarianBorrow = value; OnPropertyChanged(); } }

        public ObservableCollection<BookDTO> ListBook { get => listBook; set { listBook = value; OnPropertyChanged(); } }

        public BookDTO NewBookBorrow { get => newBookBorrow; set { newBookBorrow = value; OnPropertyChanged(); } }

        public ObservableCollection<BorrowDTO> ListBookBorrow { get => listBookBorrow; set { listBookBorrow = value; OnPropertyChanged(); } }

        public ICommand BorrowBookCommand { get; set; }
        public ICommand SaveChangeCommand { get; set; }
        public ICommand DiscardChangeCommand { get; set; }


        public PageBorrowBookVM(MemberDTO member, LibrarianDTO librarian)
        {
            MemberBorrow = member;
            LibrarianBorrow = librarian;
            ListBook = BookDAL.Instance.GetList();
            ListBookBorrow = MemberDAL.Instance.GetListBookBorrow(member.Id);

            var newBorrowBook = new ObservableCollection<BorrowDTO>();

            BorrowBookCommand = new RelayCommand<object>((p) =>
            {
                if (NewBookBorrow == null || NewBookBorrow.Count < 1) { return false; }
                var listBookIdBorrow = new List<string>();
                foreach (var item in ListBookBorrow) { listBookIdBorrow.Add(item.BookId); }
                return !listBookIdBorrow.Contains(NewBookBorrow.Id);
            }, (p) =>
            {
                var br = new BorrowDTO()
                {
                    BookId = NewBookBorrow.Id,
                    Book = BookDAL.Instance.GetBookById(NewBookBorrow.Id),
                    LibrarianId = librarian.Id,
                    Librarian = librarian.GetEntityModel(),
                    BorrowDate = DateTime.Now
                };
                newBorrowBook.Add(br);
                ListBookBorrow.Add(br);
            });
            
            SaveChangeCommand = new RelayCommand<UserControl>((p) => { return (p != null); }, (p) =>
            {
                foreach (var newBr in newBorrowBook)
                {
                    BorrowDAL.Instance.Add(member.Id, librarian.Id, newBr.BookId);
                }
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
        LibrarianDTO librarianBorrow;
        BookDTO newBookBorrow;
        ObservableCollection<BookDTO> listBook;
        ObservableCollection<BorrowDTO> listBookBorrow;
    }
}
