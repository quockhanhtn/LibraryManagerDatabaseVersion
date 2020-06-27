using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LibraryManager.MyUserControl
{
    public class YearPicker : ComboBox
    {
        public YearPicker()
        {
            var listYear = new List<int>();
            for (int i = 1900; i <= DateTime.Now.Year; i++)
            {
                listYear.Add(i);
            }
            this.ItemsSource = listYear;
            this.SelectedIndex = listYear.Count - 2;
        }
    }
}
