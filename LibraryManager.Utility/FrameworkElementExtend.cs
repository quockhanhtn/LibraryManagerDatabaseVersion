using System.Windows;

namespace LibraryManager.Utility
{
    /// <summary>
    /// Hỗ trợ làm việc với các FrameworkElement
    /// </summary>
    public class FrameworkElementExtend
    {
        /// <summary>
        /// Trả về root parent của FrameworkElement
        /// </summary>
        /// <param name="f">FrameworkElement cần tìm root parrent</param>
        /// <returns></returns>
        public static FrameworkElement GetRootParent(FrameworkElement f)
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
