using System.Windows;

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
