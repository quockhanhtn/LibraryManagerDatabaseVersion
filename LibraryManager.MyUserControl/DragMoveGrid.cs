using LibraryManager.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LibraryManager.MyUserControl
{
    public class DragMoveGrid : Grid
    {
        public DragMoveGrid()
        {
            this.MouseLeftButtonDown += DragMoveGrid_MouseLeftButtonDown;
        }

        private void DragMoveGrid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var window = FrameworkElementExtend.GetWindowParent(this) as Window;
            if (window != null) {
                try { window.DragMove(); }
                catch (Exception) { }
            }
        }
    }
}
