using System.Collections.ObjectModel;

namespace LibraryManager.Utility.Interfaces
{
    /// <summary>
    /// Lấy dữ liệu từ database và chuyển sang DTO
    /// </summary>
    /// <typeparam name="ObjectDTO">Kiểu dữ liệu object lấy ra</typeparam>
    /// <typeparam name="IdType">Kiểu dữ liệu Id của object</typeparam>
    public interface IDatabaseAccess<ObjectDTO, IdType>
    {
        /// <summary>
        /// Lấy dữ liệu từ database, chuyển đổi thành ObservableCollection<>
        /// </summary>
        /// <param name="fillter"></param>
        /// <returns></returns>
        ObservableCollection<ObjectDTO> GetList(Enums.StatusFillter fillter = Enums.StatusFillter.AllStatus);
        /// <summary>
        /// Thêm "newObject" vào database
        /// </summary>
        /// <param name="newObject"></param>
        void Add(ObjectDTO newObject);
        /// <summary>
        /// Cập nhật datase theo "objectUpdate"
        /// </summary>
        /// <param name="objectUpdate"></param>
        void Update(ObjectDTO objectUpdate);
        /// <summary>
        /// Thay đổi trạng thái (Status)
        /// </summary>
        /// <param name="objectId">Mã của object</param>
        void ChangeStatus(IdType objectId);
        /// <summary>
        /// Xóa object
        /// </summary>
        /// <param name="objectId">Mã của object</param>
        void Delete(IdType objectId);
    }
}
