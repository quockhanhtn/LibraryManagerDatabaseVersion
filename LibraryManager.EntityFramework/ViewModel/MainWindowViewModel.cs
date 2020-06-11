using LibraryManager.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LibraryManager.EntityFramework.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ICommand MenuSelectionChangedCommand { get; set; }
        public MainWindowViewModel()
        {
            MenuSelectionChangedCommand = new RelayCommand<Window>((p) => { return (p != null); }, (p) =>
            {
                var gridMain = p.FindName("gridMain") as Grid;
                var gridCursor = p.FindName("gridCursor") as Grid;
                var listViewMenu = p.FindName("ListViewMenu") as ListView;
                gridMain.Children.Clear();

                var listViewItem = (listViewMenu).SelectedItem as ListViewItem;

                gridCursor.Margin = new Thickness(0, 60 * listViewMenu.SelectedIndex, 0, 0);

                switch (listViewItem.Name)
                {

                    case "LibrarianManager":
                        break;
                    case "MemberManager":
                        break;
                    case "BookManager":
                        break;
                    case "BookCategoryManager":
                        break;
                    default:
                        break;
                }
            });
        }
    }
}
