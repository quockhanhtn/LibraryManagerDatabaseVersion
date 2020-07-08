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
    public class PagePublisherManagerVM : BaseViewModel, IObjectManager, ICopyInfoAndContact
    {
        public bool IsShowHiddenPublisher { get => isShowHiddenPublisher; set { isShowHiddenPublisher = value; ReloadList(); } }

        public ObservableCollection<PublisherDTO> ListPublisher { get => listPublisher; set { listPublisher = value; OnPropertyChanged(); } }
        public PublisherDTO PublisherSelected { get => publisherSelected; set { publisherSelected = value; OnPropertyChanged(); } }
        public ICommand SearchCommand { get; set; }
        public ICommand ObjectSelectedChangedCommand { get; set; }
        public ICommand ExportToExcelCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand StatusChangeCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SendEmailCommand { get; set; }
        public ICommand OpenWebCommand { get; set; }

        public ICommand CopyIdCommand { get; set; }
        public ICommand CopyNameCommand { get; set; }
        public ICommand CopyPhoneNumberCommand { get; set; }
        public ICommand CopyAddressCommand { get; set; }

        public PagePublisherManagerVM()
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

                ListPublisher = new ObservableCollection<PublisherDTO>(PublisherDAL.Instance.GetList().Where(
                    x => StringHelper.StringConvertToUnSign(x.Name).ToLower().Contains(searchKeyWord)));
            });

            ObjectSelectedChangedCommand = new RelayCommand<UserControl>((p) => { return p != null && PublisherSelected != null; }, (p) =>
            {
                var btnStatusChange = p.FindName("btnStatusChange") as Button;
                var mnuStatusChange = p.FindName("mnuStatusChange") as MenuItem;
                //var tblStatusChange = p.FindName("tblStatusChange") as TextBlock;
                //var icoStatusChange = p.FindName("icoStatusChange") as PackIcon;
                if (PublisherSelected.Status == true)
                {
                    //tblStatusChange.Text = "THÔI VIỆC";
                    //icoStatusChange.Kind = PackIconKind.BlockHelper;
                    btnStatusChange.Content = "ẨN";
                    btnStatusChange.ToolTip = "Ẩn nhà xuất bản \"" + PublisherSelected.Name + "\"";
                    mnuStatusChange.Header = "Ẩn nhà xuất bản";
                }
                else
                {
                    //tblStatusChange.Text = "ĐI LÀM LẠI";
                    //icoStatusChange.Kind = PackIconKind.Restore;
                    btnStatusChange.Content = "HIỂN THỊ";
                    btnStatusChange.ToolTip = "Hiển thị nhà xuất bản \"" + PublisherSelected.Name + "\"";
                    mnuStatusChange.Header = "Hiển thị nhà xuất bản";
                }
            });

            ExportToExcelCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                string filePath = "";
                // tạo SaveFileDialog để lưu file excel
                SaveFileDialog dialog = new SaveFileDialog();

                dialog.Title = "Xuất danh sách NXB";

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

                        ExcelHelper.SetExcelPackageInfo(excelPackage, "Library Manger", "Danh sách NXB", new List<string>() { "List Publisher Sheet" });

                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

                        ExcelHelper.SetSheetInfo(worksheet, "List Publisher Sheet");

                        // set column width
                        ExcelHelper.SetColumWidth(worksheet, new double[] { 10, 30, 16, 50, 45, 45, 17, 12 });

                        // Tạo danh sách các column header
                        string[] arrColumnHeader = { "Mã NXB", "Tên nhà xuất bản", "Số điện thoại", "Địa chỉ", "Email", "Trang web", "Số lượng sách", "Ghi chú" };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        worksheet.Cells[1, 1].Value = "Thống kê nhà xuất bản".ToUpper();
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
                        foreach (var item in ListPublisher)
                        {
                            colIndex = 1;   // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            rowIndex++;     // rowIndex tương ứng từng dòng dữ liệu

                            //gán giá trị cho từng cell                      
                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Id;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Name;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.PhoneNumber;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Address;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Email;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Website;

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
                var addDataContext = new AddPublisherWindowVM();
                var addPublisherWindow = new AddPublisherWindow() { DataContext = addDataContext };
                addPublisherWindow.ShowDialog();

                var mySnackbar = p.FindName("mySnackbar") as Snackbar;
                if (addDataContext.Result != null)
                {
                    mySnackbar.MessageQueue.Enqueue("Thêm nhà xuất bản \"" + addDataContext.Result.Name + "\" thành công");
                    ReloadList();
                }
                else { mySnackbar.MessageQueue.Enqueue("Không có thay đổi"); }
            });

            UpdateCommand = new RelayCommand<UserControl>((p) => { return p != null && PublisherSelected != null; }, (p) =>
            {
                var txtName = p.FindName("txtName") as TextBox;
                var txtPhone = p.FindName("txtPhone") as TextBox;
                var txtAddress = p.FindName("txtAddress") as TextBox;
                var txtEmail = p.FindName("txtEmail") as TextBox;
                var txtWebsite = p.FindName("txtWebsite") as TextBox;

                var tblNameWarning = p.FindName("tblNameWarning") as TextBlock;
                var tblPhoneWarning = p.FindName("tblPhoneWarning") as TextBlock;

                if (txtName.Text == "")
                {
                    tblNameWarning.Visibility = Visibility.Visible;
                    txtName.Focus();
                    return;
                }
                else { tblNameWarning.Visibility = Visibility.Hidden; }

                if (txtPhone.Text == "")
                {
                    tblPhoneWarning.Visibility = Visibility.Visible;
                    txtPhone.Focus();
                    return;
                }
                else { tblPhoneWarning.Visibility = Visibility.Hidden; }

                PublisherSelected.Name = txtName.Text;
                PublisherSelected.PhoneNumber = txtPhone.Text;
                PublisherSelected.Address = txtAddress.Text;
                PublisherSelected.Email = txtEmail.Text;
                PublisherSelected.Website = txtWebsite.Text;

                PublisherDAL.Instance.Update(PublisherSelected);
                var mySnackbar = p.FindName("mySnackbar") as Snackbar;
                mySnackbar.MessageQueue.Enqueue("Cập nhật nhà xuất bản \"" + PublisherSelected.Name + "\" thành công");
                ReloadList();
            });

            StatusChangeCommand = new RelayCommand<object>((p) => { return PublisherSelected != null; }, (p) =>
            {
                PublisherDAL.Instance.ChangeStatus(PublisherSelected.Id);
                ReloadList();
            });

            SendEmailCommand = new RelayCommand<object>((p) => { return PublisherSelected != null && PublisherSelected.Email != null; }, (p) =>
            {
                WebHelper.SendEmail(PublisherSelected.Email);
            });

            OpenWebCommand = new RelayCommand<object>((p) => { return PublisherSelected != null && PublisherSelected.Website != null; }, (p) =>
            {
                WebHelper.OpenLink(PublisherSelected.Website);
            });

            DeleteCommand = new RelayCommand<UserControl>((p) => { return PublisherSelected != null && PublisherSelected.NumberOfBook == 0; }, (p) =>
            {
                PublisherDAL.Instance.Delete(PublisherSelected.Id);
                ReloadList();
                if (p != null)
                {
                    var mySnackbar = p.FindName("mySnackbar") as Snackbar;
                    mySnackbar.MessageQueue.Enqueue("Xóa nhà xuất bản thành công !");
                }
            });

            CopyIdCommand = new RelayCommand<object>((p) => { return PublisherSelected != null; }, (p) => { Clipboard.SetText(PublisherSelected.Id.ToString()); });

            CopyNameCommand = new RelayCommand<object>((p) => { return PublisherSelected != null; }, (p) => { Clipboard.SetText(PublisherSelected.Name); });

            CopyPhoneNumberCommand = new RelayCommand<object>((p) => { return PublisherSelected != null; }, (p) => { Clipboard.SetText(PublisherSelected.PhoneNumber); });

            CopyAddressCommand = new RelayCommand<object>((p) => { return PublisherSelected != null; }, (p) => { Clipboard.SetText(PublisherSelected.Address); });
        }

        private void ReloadList()
        {
            if (isShowHiddenPublisher) { ListPublisher = PublisherDAL.Instance.GetList(); }
            else { ListPublisher = PublisherDAL.Instance.GetList(Utility.Enums.StatusFillter.Active); }
        }

        private ObservableCollection<PublisherDTO> listPublisher;
        private PublisherDTO publisherSelected;

        bool isShowHiddenPublisher = false;
    }
}
