using LibraryManager.EntityFramework.Model;
using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.EntityFramework.View.PageUC;
using LibraryManager.EntityFramework.ViewModel.PageUC;
using LibraryManager.Utility;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManager.EntityFramework.ViewModel
{
    public class MemberWindowViewModel : BaseViewModel
    {
        public ICommand LoadedWindow { get; set; }
        public ICommand MenuSelectionChangedCommand { get; set; }
        public Member MemberLogin { get; set; }
        public Grid GridMain { get; set; }
        public UserControl PageAccountInfor { get; set; }
        public UserControl PageListBookBorrow { get; set; }
        public UserControl PageAboutSoftware { get; set; }

        public MemberWindowViewModel(string idMember)
        {
            MemberLogin = MemberDAL.Instance.GetMember(idMember);

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
                        GridMain.Children.Add(this.PageAccountInfor);
                        break;
                    case "BorrowBookList":
                        break;
                    case "ChangePassword":
                        var dataContext = new ChangePasswordWindowViewModel(MemberLogin.Id);
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
                InitPage();
                GridMain = p.FindName("gridMain") as Grid;
                GridMain.Children.Add(this.PageAccountInfor);

                var icoAccount = p.FindName("icoAccount") as PackIcon;
                int firstChar = char.ToUpper(MemberLogin.FirstName[0]);
                icoAccount.Kind = (PackIconKind)(158 + 5 * (firstChar - (int)'A' + 2));

            });
        }

        void InitPage()
        {
            this.PageAccountInfor = new PageMemberInfor() { DataContext = new PageMemberInforVM(new MemberDTO(MemberLogin)) };
            this.PageAboutSoftware = new PageAboutSoftware();
        }
    }
}
