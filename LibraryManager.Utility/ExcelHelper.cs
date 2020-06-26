using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media;

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

        /// <summary>
        /// Set Column width cho sheet
        /// </summary>
        /// <param name="excelWorksheet">Sheet cần gán</param>
        /// <param name="columnWidth">List colum width, bắt đầu từ column 1</param>
        public static void SetColumWidth(ExcelWorksheet excelWorksheet, int[] columnWidth)
        {
            for (int i = 0; i < columnWidth.Length; i++)
            {
                excelWorksheet.Column(i + 1).Width = columnWidth[i];
            }
        }

        /// <summary>
        /// Gán thông tin cho ExcelPackage
        /// </summary>
        /// <param name="excelPackage">ExcelPackage cần gán thông tin</param>
        /// <param name="author">Tác giả</param>
        /// <param name="title">Tiêu đề</param>
        /// <param name="wookSheetName">Danh sách tên các Sheets</param>
        public static void SetExcelPackageInfo(ExcelPackage excelPackage, string author, string title, List<string> wookSheetName)
        {
            // đặt tên người tạo file
            excelPackage.Workbook.Properties.Author = author;

            // đặt tiêu đề cho file
            excelPackage.Workbook.Properties.Title = title;

            //Tạo các sheet
            foreach (var sheet in wookSheetName)
            {
                excelPackage.Workbook.Worksheets.Add(sheet);
            }
        }

        public static void SetSheetInfo(ExcelWorksheet excelWorksheet, string sheetName, string sheetFontName = "Segoe UI", float sheetFontSize = 14)
        {
            // đặt tên cho sheet
            excelWorksheet.Name = sheetName;

            // fontsize mặc định cho cả sheet
            excelWorksheet.Cells.Style.Font.Size = sheetFontSize;

            // font family mặc định cho cả sheet
            excelWorksheet.Cells.Style.Font.Name = sheetFontName;
        }

        /// <summary>
        /// Gộp các ô lại thành một và căn giữa
        /// </summary>
        /// <param name="excelWorksheet">Sheet chứa các cell cần gộp</param>
        /// <param name="fromRow">Hàng bắt đầu</param>
        /// <param name="formCol">Cột bắt đầu</param>
        /// <param name="toRow">Hàng kết thúc</param>
        /// <param name="toCol">Cột kết trúc</param>
        /// <param name="fontBold">In đâm hay không</param>
        /// <param name="fontItalic">In nghiêng hay không</param>
        public static void MergeAndCenter(ExcelWorksheet excelWorksheet, int fromRow, int formCol, int toRow, int toCol, bool fontBold = false, bool fontItalic=false)
        {
            excelWorksheet.Cells[fromRow, formCol, toRow, toCol].Merge = true;
            excelWorksheet.Cells[fromRow, formCol, toRow, toCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            excelWorksheet.Cells[fromRow, formCol, toRow, toCol].Style.Font.Bold = fontBold;
            excelWorksheet.Cells[fromRow, formCol, toRow, toCol].Style.Font.Italic = fontItalic;
        }

        public static void SaveExcelPackage(ExcelPackage excelPackage, string filePath)
        {
            //Lưu file lại
            Byte[] bin = excelPackage.GetAsByteArray();
            File.WriteAllBytes(filePath, bin);
        }
    }
}
