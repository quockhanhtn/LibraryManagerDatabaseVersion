using System.Windows;

namespace LibraryManager.EntityFramework.View.AddWindow
{
    /// <summary>
    /// Interaction logic for AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        public AddBookWindow()
        {
            InitializeComponent();
            tbxTitle.Focus();
        }
    }
}
