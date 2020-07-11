using LibraryManager.Utility;
using System;
using System.Windows;
using System.Windows.Controls;

namespace LibraryManager.MyUserControl
{
    /// <summary>
    /// Gird có thể di chuyển window chứa nó bằng kéo thả chuột
    /// </summary>
    public class DragMoveGrid : Grid
    {
        public DragMoveGrid()
        {
            this.MouseLeftButtonDown += DragMoveGrid_MouseLeftButtonDown;
        }

        private void DragMoveGrid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var window = FrameworkElementExtend.GetRootParent(this) as Window;
            if (window != null) {
                try { window.DragMove(); }
                catch (Exception) { }
            }
        }
    }
}
