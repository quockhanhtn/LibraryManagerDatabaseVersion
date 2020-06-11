using Dragablz;
using LibraryManager.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LibraryManager.EntityFramework.ViewModel
{
    public class LoginWindowViewModel : BaseViewModel
    {
        public ICommand TabControlChanged { get; set; }
        public ICommand LoginCommand { get; set; }
        public LoginWindowViewModel()
        {
            TabControlChanged = new RelayCommand<TabablzControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                MessageBox.Show(p.SelectedIndex.ToString());
            });

            LoginCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                p.Close();
            });
        }
    }
}
