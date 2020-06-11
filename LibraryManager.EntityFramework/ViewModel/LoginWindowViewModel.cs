using Dragablz;
using LibraryManager.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            TabControlChanged = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
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

            LoginCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                p.Hide();
                MainWindow mainWindow = new MainWindow();
                mainWindow.DataContext = new MainWindowViewModel();
                mainWindow.Show();
                p.Close();
            });
        }
    }
}
