﻿using Dragablz;
using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.MyUserControl.MyBox;
using LibraryManager.Utility;
using Microsoft.Win32;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManager.EntityFramework.ViewModel.PageUC
{
    public class PageStatisticVM : BaseViewModel
    {
        public ICommand TabControlChanged { get; set; }
        public ICommand ViewBorrowCommand { get; set; }
        public ICommand ViewReturnCommand { get; set; }
        public ICommand ViewMemberCommand { get; set; }
        public ICommand ViewLibrarianCommand { get; set; }
        public ICommand ExportToExcelBorrowCommand { get; set; }
        public ICommand ExportToExcelReturnCommand { get; set; }
        public ICommand ExportToExcelMemberCommand { get; set; }
        public ICommand ExportToExcelLibrarianCommand { get; set; }

        public ObservableCollection<BorrowDTO> ListBookBorrow { get => listBookBorrow; set { listBookBorrow = value; OnPropertyChanged(); } }
        public ObservableCollection<ReturnDTO> ListBookReturn { get => listBookReturn; set { listBookReturn = value; OnPropertyChanged(); } }

        public DateTime FromDateBorrow { get => fromDateBorrow; set { fromDateBorrow = value; OnPropertyChanged(); } }
        public DateTime FromDateReturn { get => fromDateReturn; set { fromDateReturn = value; OnPropertyChanged(); } }
        public DateTime FromDateMember { get => fromDateMember; set { fromDateMember = value; OnPropertyChanged(); } }
        public DateTime FromDateLibrarian { get => fromDateLibrarian; set { fromDateLibrarian = value; OnPropertyChanged(); } }
        public DateTime ToDateBorrow { get => toDateBorrow; set { toDateBorrow = value; OnPropertyChanged(); } }
        public DateTime ToDateReturn { get => toDateReturn; set { toDateReturn = value; OnPropertyChanged(); } }
        public DateTime ToDateMember { get => toDateMember; set { toDateMember = value; OnPropertyChanged(); } }
        public DateTime ToDateLibrarian { get => toDateLibrarian; set { toDateLibrarian = value; OnPropertyChanged(); } }

        public PageStatisticVM()
        {
            ListBookBorrow = BorrowDAL.Instance.GetListByDate(FromDateBorrow, ToDateBorrow);
            ListBookReturn = ReturnBookDAL.Instance.GetListByDate(FromDateReturn, ToDateReturn);

            TabControlChanged = new RelayCommand<UserControl>((p) => { return p != null; }, (p) =>
            {
                var tabControl = p.FindName("tabControl") as TabablzControl;
                var gridBorrow = p.FindName("gridBorrow") as Grid;
                var gridReturn = p.FindName("gridReturn") as Grid;
                var gridMember = p.FindName("gridMember") as Grid;
                var gridLibrarian = p.FindName("gridLibrarian") as Grid;

                var listGrid = new List<Grid>() { gridBorrow, gridReturn, gridMember, gridLibrarian };

                listGrid.ForEach(g => g.Margin = new Thickness(0, 0, 2000, 0));
                listGrid[tabControl.SelectedIndex].Margin = new Thickness(0, 0, 0, 0);
            });

            ViewBorrowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
             {
                 ListBookBorrow = BorrowDAL.Instance.GetListByDate(FromDateBorrow, ToDateBorrow);
             });

            ViewReturnCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
             {
                 ListBookReturn = ReturnBookDAL.Instance.GetListByDate(FromDateReturn, ToDateReturn);
             });

            ExportToExcelBorrowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                string filePath = "";
                // tạo SaveFileDialog để lưu file excel
                SaveFileDialog dialog = new SaveFileDialog();

                dialog.Title = "Xuất thống kê mượn sách";

                // chỉ lọc ra các file có định dạng Excel
                dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";

                // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
                if (dialog.ShowDialog() == true) { filePath = dialog.FileName; }

                // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
                if (string.IsNullOrEmpty(filePath)) { return; }

                try
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (var excelPackage = new ExcelPackage())
                    {
                        ExcelHelper.SetExcelPackageInfo(excelPackage, "Library Manger", "Thống kê mượn sách", new List<string>() { "Borrow Sheet" });

                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

                        ExcelHelper.SetSheetInfo(worksheet, "Borrow Sheet");

                        // set column width
                        ExcelHelper.SetColumWidth(worksheet, new double[] { 15, 50, 26, 30, 10, 22, 22, 14, 14 });

                        // Tạo danh sách các column header
                        string[] arrColumnHeader = { "Mã sách", "Tựa sách", "Chuyên mục", "Nhà xuất bản", "Năm XB", "Người mượn", "NV cho mượn", "Ngày mượn", "Hạn trả"};

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        worksheet.Cells[1, 1].Value = "Thống kê mượn sách".ToUpper();
                        ExcelHelper.MergeAndCenter(worksheet, 1, 1, 1, countColHeader, true);

                        worksheet.Cells[2, 1].Value = "Từ ngày " + FromDateBorrow.ToShortDateString() + " đến ngày " + ToDateBorrow.ToShortDateString();
                        ExcelHelper.MergeAndCenter(worksheet, 2, 1, 2, countColHeader, true);

                        int colIndex = 1, rowIndex = 3;

                        //tạo các header từ column header đã tạo từ bên trên
                        foreach (var item in arrColumnHeader)
                        {
                            var cell = worksheet.Cells[rowIndex, colIndex];

                            //set màu thành gray
                            var fill = cell.Style.Fill;
                            fill.PatternType = ExcelFillStyle.Solid;
                            fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                            //căn chỉnh các border
                            var border = cell.Style.Border;
                            border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

                            //gán giá trị
                            cell.Value = item;

                            colIndex++;
                        }

                        // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                        foreach (var item in ListBookBorrow)
                        {
                            colIndex = 1;   // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            rowIndex++;     // rowIndex tương ứng từng dòng dữ liệu

                            //gán giá trị cho từng cell                      
                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.BookId;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Book.Title;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Book.BookCategory.Name;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Book.Publisher.Name;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Book.YearPublish;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.MemberName;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.LibrarianName;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.BorrowDate.ToShortDateString();

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.TermDate.ToShortDateString();
                        }

                        ExcelHelper.SaveExcelPackage(excelPackage, filePath);
                    }

                    if (MyMessageBox.Show("Bạn có muốn mở file excel vừa xuất không ?", "Xuất file Excel thành công !", "Có", "Không", MessageBoxImage.Information))
                    {
                        ExcelHelper.OpenFile(filePath);
                    }
                }

                catch (Exception)
                {
                    MyMessageBox.Show("Có lỗi khi lưu file!", "Thông báo", "OK", "", MessageBoxImage.Error);
                }
            });

            ExportToExcelReturnCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                string filePath = "";
                // tạo SaveFileDialog để lưu file excel
                SaveFileDialog dialog = new SaveFileDialog();

                dialog.Title = "Xuất thống kê trả sách";

                // chỉ lọc ra các file có định dạng Excel
                dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";

                // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
                if (dialog.ShowDialog() == true) { filePath = dialog.FileName; }

                // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
                if (string.IsNullOrEmpty(filePath)) { return; }

                try
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (var excelPackage = new ExcelPackage())
                    {
                        ExcelHelper.SetExcelPackageInfo(excelPackage, "Library Manger", "Thống kê trả sách", new List<string>() { "Return Sheet" });

                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

                        ExcelHelper.SetSheetInfo(worksheet, "Return Sheet");

                        // set column width
                        ExcelHelper.SetColumWidth(worksheet, new double[] { 15, 50, 26, 30, 10, 22, 22, 22, 14, 14 });

                        // Tạo danh sách các column header
                        string[] arrColumnHeader = { "Mã sách", "Tựa sách", "Chuyên mục", "Nhà xuất bản", "Năm XB", "Người mượn", "NV cho mượn", "NV nhận sách","Ngày mượn", "Ngày trả" };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        worksheet.Cells[1, 1].Value = "Thống kê trả sách".ToUpper();
                        ExcelHelper.MergeAndCenter(worksheet, 1, 1, 1, countColHeader, true);

                        worksheet.Cells[2, 1].Value = "Từ ngày " + FromDateReturn.ToShortDateString() + " đến ngày " + ToDateReturn.ToShortDateString();
                        ExcelHelper.MergeAndCenter(worksheet, 2, 1, 2, countColHeader, true);

                        int colIndex = 1, rowIndex = 3;

                        //tạo các header từ column header đã tạo từ bên trên
                        foreach (var item in arrColumnHeader)
                        {
                            var cell = worksheet.Cells[rowIndex, colIndex];

                            //set màu thành gray
                            var fill = cell.Style.Fill;
                            fill.PatternType = ExcelFillStyle.Solid;
                            fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                            //căn chỉnh các border
                            var border = cell.Style.Border;
                            border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;

                            //gán giá trị
                            cell.Value = item;

                            colIndex++;
                        }

                        // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                        foreach (var item in ListBookReturn)
                        {
                            colIndex = 1;   // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            rowIndex++;     // rowIndex tương ứng từng dòng dữ liệu

                            //gán giá trị cho từng cell                      
                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Borrow.BookId;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Borrow.Book.Title;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Borrow.Book.BookCategory.Name;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Borrow.Book.Publisher.Name;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Borrow.Book.YearPublish;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.MemberName;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.LibrarianBorrow;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.LibrarianReturn;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Borrow.BorrowDate.ToShortDateString();

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.ReturnDate.ToShortDateString();
                        }

                        ExcelHelper.SaveExcelPackage(excelPackage, filePath);
                    }

                    if (MyMessageBox.Show("Bạn có muốn mở file excel vừa xuất không ?", "Xuất file Excel thành công !", "Có", "Không", MessageBoxImage.Information))
                    {
                        ExcelHelper.OpenFile(filePath);
                    }
                }

                catch (Exception)
                {
                    MyMessageBox.Show("Có lỗi khi lưu file!", "Thông báo", "OK", "", MessageBoxImage.Error);
                }
            });
        }

        ObservableCollection<BorrowDTO> listBookBorrow;
        ObservableCollection<ReturnDTO> listBookReturn;

        DateTime fromDateBorrow = DateTime.Now.AddDays(-30);
        DateTime fromDateReturn = DateTime.Now.AddDays(-30);
        DateTime fromDateMember = DateTime.Now.AddDays(-30);
        DateTime fromDateLibrarian = DateTime.Now.AddDays(-30);
        DateTime toDateBorrow = DateTime.Now;
        DateTime toDateReturn = DateTime.Now;
        DateTime toDateMember = DateTime.Now;
        DateTime toDateLibrarian = DateTime.Now;
    }
}
