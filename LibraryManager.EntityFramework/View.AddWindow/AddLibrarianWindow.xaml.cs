using System.Windows;

namespace LibraryManager.EntityFramework.View.AddWindow
{
    /// <summary>
    /// Interaction logic for AddLibrarianWindow.xaml
    /// </summary>
    public partial class AddLibrarianWindow : Window
    {
        public AddLibrarianWindow()
        {
            InitializeComponent();
            tbxLastName.Focus();
        }
    }
}
