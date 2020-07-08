using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.EntityFramework.View.AddWindow;
using LibraryManager.EntityFramework.ViewModel.AddWindow;
using LibraryManager.MyUserControl.MyBox;
using LibraryManager.Utility;
using LibraryManager.Utility.Interfaces;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManager.EntityFramework.ViewModel.PageUC
{
    public class PageAuthorManagerVM : BaseViewModel, IObjectManager, ICopyInfo
    {
        public bool IsShowHiddenAuthor { get => isShowHiddenAuthor; set { isShowHiddenAuthor = value; ReloadList(); } }

        public ObservableCollection<AuthorDTO> ListAuthor { get => listAuthor; set { listAuthor = value; OnPropertyChanged(); } }
        public AuthorDTO AuthorSelected { get => publisherSelected; set { publisherSelected = value; OnPropertyChanged(); } }

        public ICommand SearchCommand { get; set; }
        public ICommand ObjectSelectedChangedCommand { get; set; }
        public ICommand ExportToExcelCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand StatusChangeCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand CopyIdCommand { get ; set; }
        public ICommand CopyNameCommand { get; set; }

        public PageAuthorManagerVM()
        {
            ReloadList();

            SearchCommand = new RelayCommand<TextBox>((p) => { return p != null; }, (p) =>
        {
            if (p.Text == "" || p.Text == " ")
            {
                p.Text = "";
                ReloadList();
                return;
            }

            var searchKeyWord = StringHelper.StringConvertToUnSign(p.Text).ToLower();

            ListAuthor = new ObservableCollection<AuthorDTO>(AuthorDAL.Instance.GetList().Where(
                x => StringHelper.StringConvertToUnSign(x.NickName).ToLower().Contains(searchKeyWord)));
        });

            ObjectSelectedChangedCommand = new RelayCommand<UserControl>((p) => { return p != null && AuthorSelected != null; }, (p) =>
            {
                var btnStatusChange = p.FindName("btnStatusChange") as Button;
                var mnuStatusChange = p.FindName("mnuStatusChange") as MenuItem;
                //var tblStatusChange = p.FindName("tblStatusChange") as TextBlock;
                //var icoStatusChange = p.FindName("icoStatusChange") as PackIcon;
                if (AuthorSelected.Status == true)
                {
                    //tblStatusChange.Text = "THÔI VIỆC";
                    //icoStatusChange.Kind = PackIconKind.BlockHelper;
                    btnStatusChange.Content = "ẨN";
                    btnStatusChange.ToolTip = "Ẩn tác giả \"" + AuthorSelected.NickName + "\"";
                    mnuStatusChange.Header = "Ẩn tác giả";
                }
                else
                {
                    //tblStatusChange.Text = "ĐI LÀM LẠI";
                    //icoStatusChange.Kind = PackIconKind.Restore;
                    btnStatusChange.Content = "HIỂN THỊ";
                    btnStatusChange.ToolTip = "Hiển thị tác giả \"" + AuthorSelected.NickName + "\"";
                    mnuStatusChange.Header = "Hiển thị tác giả";
                }
            });

            ExportToExcelCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                string filePath = "";
                // tạo SaveFileDialog để lưu file excel
                SaveFileDialog dialog = new SaveFileDialog();

                dialog.Title = "Xuất danh sách tác giả";

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

                        ExcelHelper.SetExcelPackageInfo(excelPackage, "Library Manger", "Danh sách tác giả", new List<string>() { "List Author Sheet" });

                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

                        ExcelHelper.SetSheetInfo(worksheet, "List Author Sheet");

                        // set column width
                        ExcelHelper.SetColumWidth(worksheet, new double[] { 13, 25, 17, 10 });

                        // Tạo danh sách các column header
                        string[] arrColumnHeader = { "Mã tác giả", "Tên tác giả", "Số lượng sách", "Ghi chú" };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        worksheet.Cells[1, 1].Value = "Thống kê Tác giả".ToUpper();
                        ExcelHelper.MergeAndCenter(worksheet, 1, 1, 1, countColHeader, true);

                        worksheet.Cells[2, 1].Value = "Ngày " + DateTime.Now.ToString();
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
                        foreach (var item in ListAuthor)
                        {
                            colIndex = 1;   // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            rowIndex++;     // rowIndex tương ứng từng dòng dữ liệu

                            //gán giá trị cho từng cell                      
                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Id;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.NickName;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.NumberOfBook;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Note;
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

            AddCommand = new RelayCommand<UserControl>((p) => { return p != null; }, (p) =>
            {
                var addDataContext = new AddAuthorWindowVM();
                var addAuthorWindow = new AddAuthorWindow() { DataContext = addDataContext };
                addAuthorWindow.ShowDialog();

                var mySnackbar = p.FindName("mySnackbar") as Snackbar;
                if (addDataContext.Result != null)
                {
                    mySnackbar.MessageQueue.Enqueue("Thêm tác giả \"" + addDataContext.Result.NickName + "\" thành công");
                    ReloadList();
                }
                else { mySnackbar.MessageQueue.Enqueue("Không có thay đổi"); }
            });

            UpdateCommand = new RelayCommand<UserControl>((p) => { return p != null && AuthorSelected != null; }, (p) =>
            {
                var txtNickName = p.FindName("txtNickName") as TextBox;
                var tblNickNameWarning = p.FindName("tblNickNameWarning") as TextBlock;

                if (txtNickName.Text == "")
                {
                    tblNickNameWarning.Visibility = Visibility.Visible;
                    txtNickName.Focus();
                    return;
                }
                else { tblNickNameWarning.Visibility = Visibility.Hidden; }

                AuthorSelected.NickName = txtNickName.Text;

                AuthorDAL.Instance.Update(AuthorSelected);
                var mySnackbar = p.FindName("mySnackbar") as Snackbar;
                mySnackbar.MessageQueue.Enqueue("Cập nhật tác giả \"" + AuthorSelected.NickName + "\" thành công");
                ReloadList();
            });

            StatusChangeCommand = new RelayCommand<object>((p) => { return AuthorSelected != null; }, (p) =>
            {
                AuthorDAL.Instance.ChangeStatus(AuthorSelected.Id);
                ReloadList();
            });

            DeleteCommand = new RelayCommand<UserControl>((p) => { return AuthorSelected != null && AuthorSelected.NumberOfBook == 0; }, (p) =>
            {
                AuthorDAL.Instance.Delete(AuthorSelected.Id);
                ReloadList();
                var mySnackbar = p.FindName("mySnackbar") as Snackbar;
                mySnackbar.MessageQueue.Enqueue("Xóa tác giả thành công !");
            });

            CopyIdCommand = new RelayCommand<object>((p) => { return AuthorSelected != null; }, (p) => { Clipboard.SetText(AuthorSelected.Id.ToString()); });

            CopyNameCommand = new RelayCommand<object>((p) => { return AuthorSelected != null; }, (p) => { Clipboard.SetText(AuthorSelected.NickName); });
        }

        void ReloadList()
        {
            if (isShowHiddenAuthor) { ListAuthor = AuthorDAL.Instance.GetList(); }
            else { ListAuthor = AuthorDAL.Instance.GetList(Utility.Enums.StatusFillter.Active); }
        }

        ObservableCollection<AuthorDTO> listAuthor;
        AuthorDTO publisherSelected;

        bool isShowHiddenAuthor = false;
    }
}
