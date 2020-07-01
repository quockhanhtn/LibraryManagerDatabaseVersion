using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LibraryManager.MyUserControl
{
    public class NumericPicker : ComboBox
    {
        public int MaxValue { get => maxValue; set { maxValue = value; SetItemSource(); } }
        public int MinValue { get => minValue; set { minValue = value; SetItemSource(); } }

        public NumericPicker()
        {
            this.IsEditable = true;
        }

        void SetItemSource()
        {
            var listValue = new ObservableCollection<int>();
            for (int i = MinValue; i <= MaxValue; i++)
            {
                listValue.Add(i);
            }
            this.ItemsSource = listValue;
        }

        int minValue = 0;
        int maxValue = 100;
    }
}
