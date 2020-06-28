using Dragablz;
using LibraryManager.EntityFramework.Model;
using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.View.AddWindow;
using LibraryManager.Utility;
using System.Linq;
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
                var tbxUsername = p.FindName("tbxUsername") as TextBox;
                var tbxPassWord = p.FindName("tbxPassWord") as PasswordBox;

                var (loginResult, idPerson) = (0,"");//AccountDAL.Instance.Login(tbxUsername.Text, tbxPassWord.Password);

                if (loginResult == -1)
                {
                    var tblLoginFail = p.FindName("tblLoginFail") as TextBlock;
                    tblLoginFail.Visibility = Visibility.Visible;
                    return;
                }
                else
                {
                    p.Hide();
                    if (loginResult == 0)
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                    }
                    else if (loginResult == 1)
                    {
                        LibrarianWindow librarianWindow = new LibrarianWindow() { DataContext = new LibrarianWindowViewModel(idPerson) };
                        librarianWindow.Show();
                    }
                    else if (loginResult == 2)
                    {
                        MessageBox.Show("Member");
                    }
                    p.Close();
                }
            });
        }
    }
}
