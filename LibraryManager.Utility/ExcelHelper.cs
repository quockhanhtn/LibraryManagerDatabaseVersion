using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace LibraryManager.Utility
{
    /// <summary>
    /// Hỗ trợ làm việc với file Excell
    /// </summary>
    public static class ExcelHelper
    {
        /// <summary>
        /// Tạo boder xung quanh một Cell
        /// </summary>
        /// <param name="excelWorksheet">Sheet cần tạo boder</param>
        /// <param name="rowIndex">Chỉ số hàng</param>
        /// <param name="colIndex">Chỉ số cột</param>
        /// <param name="excelBorderStyle">Kiểu đường viền</param>
        public static void FormatCellBorder(ExcelWorksheet excelWorksheet, int rowIndex, int colIndex, ExcelBorderStyle excelBorderStyle = ExcelBorderStyle.Thin)
        {
            var cell = excelWorksheet.Cells[rowIndex, colIndex];

            //căn chỉnh các border
            var border = cell.Style.Border;
            border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = excelBorderStyle;
        }

        /// <summary>
        /// Mở file Excel
        /// </summary>
        /// <param name="path">Đường dẫn đến file</param>
        public static void OpenFile(string path)
        {
            var excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Visible = true;
            Microsoft.Office.Interop.Excel.Workbooks workbooks = excelApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook sheet = workbooks.Open(path);
        }
    }
}
