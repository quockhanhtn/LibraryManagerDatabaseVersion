using System.Windows;

namespace LibraryManager.EntityFramework
{
    /// <summary>
    /// Interaction logic for FindMemberWindow.xaml
    /// </summary>
    public partial class FindMemberWindow : Window
    {
        public FindMemberWindow()
        {
            InitializeComponent();
            cmbMemberId.Focus();
        }
    }
}
