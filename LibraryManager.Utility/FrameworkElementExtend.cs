using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LibraryManager.Utility
{
    public class FrameworkElementExtend
    {
        public static FrameworkElement GetWindowParent(FrameworkElement f)
        {
            FrameworkElement parent = f;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }
    }
}
