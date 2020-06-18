using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LibraryManager.MyUserControl
{
    public class SenderSelectionCombobox : ComboBox
    {
        public SenderSelectionCombobox()
        {
            this.ItemsSource = new List<string>() { "Nam", "Nữ", "Khác" };
            this.Style = Application.Current.FindResource("MaterialDesignFloatingHintComboBox") as Style;
        }
    }
}
