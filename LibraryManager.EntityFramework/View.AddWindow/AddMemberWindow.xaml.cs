using System.Windows;

namespace LibraryManager.EntityFramework.View.AddWindow
{
    /// <summary>
    /// Interaction logic for AddMemberWindow.xaml
    /// </summary>
    public partial class AddMemberWindow : Window
    {
        public AddMemberWindow()
        {
            InitializeComponent();
            tbxLastName.Focus();
        }
    }
}
