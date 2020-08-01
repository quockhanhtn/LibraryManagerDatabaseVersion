using LibraryManager.EntityFramework.Model;
using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.EntityFramework.View.PageUC;
using LibraryManager.EntityFramework.ViewModel.PageUC;
using LibraryManager.MyUserControl;
using LibraryManager.MyUserControl.MyBox;
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
        public UserControl PageMemberBorrowList { get; set; }
        public UserControl PageAboutSoftware { get; set; }

        public MemberWindowViewModel(Account accountLogin)
        {
            MemberLogin = MemberDAL.Instance.GetMember(accountLogin.PersonId);

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
                        GridMain.Children.Add(this.PageMemberBorrowList);
                        break;
                    case "AboutSoftware":
                        GridMain.Children.Add(this.PageAboutSoftware);
                        break;
                    case "Logout":
                        var messageboxResult = MyMessageBox.Show("Bạn có muốn đăng xuất khỏi phần mềm ?", "Cảnh báo", "Không", "Có", MessageBoxImage.Warning);
                        if (messageboxResult == true)
                        {
                            listViewMenu.SelectedIndex = 0;
                            this.MenuSelectionChangedCommand.Execute(p);
                        }
                        else
                        {
                            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                            Application.Current.Shutdown();
                        }
                        break;
                }
            });

            LoadedWindow = new RelayCommand<Window>((p) => { return (p != null); }, (p) =>
            {
                var titleBar = p.FindName("titleBar") as TitleBar;
                titleBar.Tag = "Library Manager - Member:" + MemberLogin.LastName + " " + MemberLogin.FirstName;

                if (MemberLogin.Status != true)
                {
                    MyMessageBox.Show("Tài khoản của bạn đã bị khóa!\n\rLiên hệ quản trị viên để được hỗ trợ", "Thông báo", "OK", "", MessageBoxImage.Error);
                    p.Close();
                }

                InitPage(accountLogin);
                GridMain = p.FindName("gridMain") as Grid;
                GridMain.Children.Add(this.PageAccountInfor);

                var icoAccount = p.FindName("icoAccount") as PackIcon;
                int firstChar = char.ToUpper(MemberLogin.FirstName[0]);

                if (firstChar == 'A') { icoAccount.Kind = PackIconKind.AlphaACircle; }
                else if (firstChar == 'B') { icoAccount.Kind = PackIconKind.AlphaBCircle; }
                else { icoAccount.Kind = (PackIconKind)(158 + 5 * (firstChar - (int)'A' + 2)); }
            });
        }

        void InitPage(Account accountLogin)
        {
            this.PageAccountInfor = new PageMemberInfor() { DataContext = new PageMemberInforVM(new MemberDTO(MemberLogin), accountLogin) };
            this.PageMemberBorrowList = new PageMemberBorrowList() { DataContext = new PageMemberBorrowListVM(MemberLogin.Id) };
            this.PageAboutSoftware = new PageAboutSoftware();
        }
    }
}
