using System.Windows;

namespace LibraryManager.EntityFramework.View.AddWindow
{
    /// <summary>
    /// Interaction logic for AddBookCategoryWindow.xaml
    /// </summary>
    public partial class AddBookCategoryWindow : Window
    {
        public AddBookCategoryWindow()
        {
            InitializeComponent();
            txtName.Focus();
        }
    }
}
