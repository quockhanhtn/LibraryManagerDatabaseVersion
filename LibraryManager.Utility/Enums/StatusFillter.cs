namespace LibraryManager.Utility.Enums
{
    /// <summary>
    /// Trạng thái khi lấy dữ liệu từ Database
    /// </summary>
    public enum StatusFillter
    {
        /// <summary>
        /// Tất cả trạng thái
        /// </summary>
        AllStatus,
        /// <summary>
        /// Chỉ những object có Status == true
        /// </summary>
        Active,
        /// <summary>
        /// Các object có Status != true
        /// </summary>
        InActive
    }
}
