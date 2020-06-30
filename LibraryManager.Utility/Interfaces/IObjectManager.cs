using System.Windows.Input;

namespace LibraryManager.Utility.Interfaces
{
    public interface IObjectManager
    {
        /// <summary>
        /// Thay đổi object được chọn trên listview
        /// </summary>
        ICommand ObjectSelectedChangedCommand { get; set; }
        /// <summary>
        /// Thêm đối tượng mới
        /// </summary>
        ICommand AddCommand { get; set; }
        /// <summary>
        /// Cập nhật thông tin đối tượng
        /// </summary>
        ICommand UpdateCommand { get; set; }
        /// <summary>
        /// Thay đổi trạng thái (status) của đối tượng
        /// </summary>
        ICommand StatusChangeCommand { get; set; }
        /// <summary>
        /// Xóa đối tượng
        /// </summary>
        ICommand DeleteCommand { get; set; }
    }
}
