using Dragablz;
using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.MyUserControl.MyBox;
using LibraryManager.Utility;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManager.EntityFramework.ViewModel
{
    public class LoginWindowViewModel : BaseViewModel
    {
        public ICommand TabControlChanged { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand SignUpCommand { get; set; }

        public LoginWindowViewModel()
        {
            TabControlChanged = new RelayCommand<Window>((p) => { return p != null; }, (p) =>
            {
                var tabControl = p.FindName("tabControl") as TabablzControl;
                var gridSignUp = p.FindName("gridSignUp") as Grid;
                var gridLogin = p.FindName("gridLogin") as Grid;

                if (tabControl.SelectedIndex == 0)
                {
                    gridLogin.Margin = new Thickness(0, 0, 0, 0);
                    gridSignUp.Margin = new Thickness(0, 0, 500, 0);
                }
                if (tabControl.SelectedIndex == 1)
                {
                    gridLogin.Margin = new Thickness(0, 0, 500, 0);
                    gridSignUp.Margin = new Thickness(0, 0, 0, 0);
                }
            });

            LoginCommand = new RelayCommand<Window>((p) => { return p != null; }, (p) =>
            {
                var txtUsername = p.FindName("txtUsername") as TextBox;
                var txtPassWord = p.FindName("txtPassWord") as PasswordBox;

                var accountLogin = AccountDAL.Instance.Login(txtUsername.Text, txtPassWord.Password);

                if (accountLogin == null)
                {
                    var tblLoginFail = p.FindName("tblLoginFail") as TextBlock;
                    tblLoginFail.Visibility = Visibility.Visible;
                    return;
                }
                else
                {
                    p.Hide();

                    switch (accountLogin.AccountType)
                    {
                        case 0:
                            var mainWindow = new MainWindow();
                            mainWindow.Show();
                            break;
                        case 1:
                            var librarianWindow = new LibrarianWindow() { DataContext = new LibrarianWindowViewModel(accountLogin) };
                            librarianWindow.Show();
                            break;
                        case 2:
                            var memberWindow = new MemberWindow() { DataContext = new MemberWindowViewModel(accountLogin) };
                            memberWindow.Show();
                            break;
                    }
                    p.Close();
                }
            });
        }
    }
}
