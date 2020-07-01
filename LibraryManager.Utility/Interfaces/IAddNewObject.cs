using System.Windows.Input;

namespace LibraryManager.Utility.Interfaces
{
    public interface IAddNewObject<ObjectType>
    {
        /// <summary>
        /// Kết quả
        ///     return new object được thêm vào
        ///     null nếu không có object nào được thêm
        /// </summary>
        ObjectType Result { get; set; }
        ICommand OKCommand { get; set; }
        ICommand CancelCommand { get; set; }
    }
}
