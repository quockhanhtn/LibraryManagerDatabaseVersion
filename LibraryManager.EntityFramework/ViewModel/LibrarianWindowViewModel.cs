using LibraryManager.EntityFramework.Model;
using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.EntityFramework.View.PageUC;
using LibraryManager.EntityFramework.ViewModel.PageUC;
using LibraryManager.MyUserControl.MyBox;
using LibraryManager.Utility;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManager.EntityFramework.ViewModel
{
    public class LibrarianWindowViewModel : BaseViewModel
    {
        public ICommand LoadedWindow { get; set; }
        public ICommand MenuSelectionChangedCommand { get; set; }
        public Librarian LibrarianLogin { get; set; }
        public Grid GridMain { get; set; }
        public UserControl PageLibrarianInfor { get; set; }
        public UserControl PageMemberManager { get; set; }
        public UserControl PageBookManager { get; set; }
        public UserControl PagePublisherManager { get; set; }
        public UserControl PageBookCategoryManager { get; set; }
        public UserControl PageAuthorManager { get; set; }
        public UserControl PageAboutSoftware { get; set; }

        public LibrarianWindowViewModel(Account accountLogin)
        {
            LibrarianLogin = LibrarianDAL.Instance.GetLibrarian(accountLogin.PersonId);

            MenuSelectionChangedCommand = new RelayCommand<Window>((p) => { return (p != null); }, (p) =>
            {
                var gridCursor = p.FindName("gridCursor") as Grid;
                var listViewMenu = p.FindName("ListViewMenu") as ListView;
                var listViewSelectedItem = (listViewMenu).SelectedItem as ListViewItem;

                gridCursor.Margin = new Thickness(0, 60 * listViewMenu.SelectedIndex, 0, 0);

                GridMain.Children.Clear();
                switch (listViewSelectedItem.Name)
                {
                    case "AccountInfo":
                        GridMain.Children.Add(this.PageLibrarianInfor);
                        break;
                    case "MemberManager":
                        GridMain.Children.Add(this.PageMemberManager);
                        break;
                    case "BookManager":
                        GridMain.Children.Add(this.PageBookManager);
                        break;
                    case "PublisherManager":
                        GridMain.Children.Add(this.PagePublisherManager);
                        break;
                    case "BookCategoryManager":
                        GridMain.Children.Add(this.PageBookCategoryManager);
                        break;
                    case "AuthorManager":
                        GridMain.Children.Add(this.PageAuthorManager);
                        break;
                    case "ChangePassword":
                        var dataContext = new ChangePasswordWindowViewModel(LibrarianLogin.Id);
                        var changePasswordWindow = new ChangePasswordWindow() { DataContext = dataContext };
                        changePasswordWindow.Show();
                        break;
                    case "AboutSoftware":
                        GridMain.Children.Add(this.PageAboutSoftware);
                        break;
                }
            });

            LoadedWindow = new RelayCommand<Window>((p) => { return (p != null); }, (p) =>
            {
                if (LibrarianLogin.Status != true)
                {
                    MyMessageBox.Show("Tài khoản của bạn đã bị khóa!\n\rLiên hệ với quản trị viên để mở lại", "Thông báo", "OK", "", MessageBoxImage.Error);
                    p.Close();
                }

                InitPage(accountLogin);
                GridMain = p.FindName("gridMain") as Grid;
                GridMain.Children.Add(this.PageBookManager);

                var icoAccount = p.FindName("icoAccount") as PackIcon;
                int firstChar = char.ToUpper(LibrarianLogin.FirstName[0]);

                if (firstChar == 'A') { icoAccount.Kind = PackIconKind.AlphaACircle; }
                else if (firstChar == 'B') { icoAccount.Kind = PackIconKind.AlphaBCircle; }
                else { icoAccount.Kind = (PackIconKind)(158 + 5 * (firstChar - (int)'A' + 2)); }
            });
        }

        void InitPage(Account accountLogin)
        {
            this.PageLibrarianInfor = new PageLibrarianInfor() { DataContext = new PageLibrarianInforVM(new LibrarianDTO(LibrarianLogin), accountLogin) };
            this.PageMemberManager = new PageMemberManager() { DataContext = new PageMemberManagerVM() };
            this.PageBookManager = new PageBookManager() { DataContext = new PageBookManagerVM(new LibrarianDTO(LibrarianLogin)) };
            this.PagePublisherManager = new PagePublisherManager() { DataContext = new PagePublisherManagerVM() };
            this.PageBookCategoryManager = new PageBookCategoryManager() { DataContext = new PageBookCategoryManagerVM() };
            this.PageAuthorManager = new PageAuthorManager() { DataContext = new PageAuthorManagerVM() };
            this.PageAboutSoftware = new PageAboutSoftware();
        }
    }
}
