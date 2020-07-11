using Dragablz;
using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.MyUserControl.MyBox;
using LibraryManager.Utility;
using MaterialDesignThemes.Wpf;
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
        public ICommand LostPasswordCommand { get; set; }
        public ICommand ShowPasswordCommand { get; set; }

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
                var txtPassword = p.FindName("txtPassword") as PasswordBox;
                var txtPasswordShow = p.FindName("txtPasswordShow") as TextBox;
                var icoEye = p.FindName("icoEye") as PackIcon;

                var passwordInput = (icoEye.Kind == PackIconKind.Visibility) ? txtPassword.Password : txtPasswordShow.Text;
                var accountLogin = AccountDAL.Instance.Login(txtUsername.Text, passwordInput);

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

            SignUpCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MyMessageBox.Show("Comming soon !", "Sorry", "OK", "", MessageBoxImage.Error);
            });

            LostPasswordCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MyMessageBox.Show("Comming soon !", "Sorry", "OK", "", MessageBoxImage.Error);
            });

            ShowPasswordCommand = new RelayCommand<Window>((p) => { return p != null; }, (p) =>
            {
                var txtPassword = p.FindName("txtPassword") as PasswordBox;
                var txtPasswordShow = p.FindName("txtPasswordShow") as TextBox;
                var icoEye = p.FindName("icoEye") as PackIcon;

                if (icoEye.Kind == PackIconKind.Visibility)
                {
                    icoEye.Kind = PackIconKind.VisibilityOff;
                    txtPassword.Visibility = Visibility.Hidden;
                    txtPasswordShow.Text = txtPassword.Password;
                    txtPasswordShow.Visibility = Visibility.Visible;

                    txtPasswordShow.Focus();
                    txtPasswordShow.SelectionStart = txtPasswordShow.Text.Length;
                }
                else
                {
                    icoEye.Kind = PackIconKind.Visibility;
                    txtPassword.Visibility = Visibility.Visible;
                    txtPassword.Password = txtPasswordShow.Text;
                    txtPasswordShow.Visibility = Visibility.Hidden;

                    txtPassword.Focus();
                }
            });
        }
    }
}
