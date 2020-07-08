using System.Windows;

namespace LibraryManager.EntityFramework.View.AddWindow
{
    /// <summary>
    /// Interaction logic for AddPublisherWindow.xaml
    /// </summary>
    public partial class AddPublisherWindow : Window
    {
        public AddPublisherWindow()
        {
            InitializeComponent();
            tbxName.Focus();
        }
    }
}
