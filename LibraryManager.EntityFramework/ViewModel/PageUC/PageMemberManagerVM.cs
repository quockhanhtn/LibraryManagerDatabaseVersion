using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.EntityFramework.View.AddWindow;
using LibraryManager.EntityFramework.ViewModel.AddWindow;
using LibraryManager.MyUserControl.MyBox;
using LibraryManager.Utility;
using LibraryManager.Utility.Enums;
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
    public class PageMemberManagerVM : BaseViewModel, IObjectManager, ICopyInfoAndContact
    {
        public ObservableCollection<MemberDTO> ListMember { get => listMember; set { listMember = value; OnPropertyChanged(); } }
        public MemberDTO MemberSelected { get => memberSelected; set { memberSelected = value; OnPropertyChanged(); } }
        public ICommand SearchCommand { get; set; }
        public ICommand ObjectSelectedChangedCommand { get; set; }
        public ICommand ExportToExcelCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand SendEmailCommand { get; set; }
        public ICommand StatusChangeCommand { get; set; }
        public int StatusFillter { get => (int)statusFillter; set { statusFillter = (StatusFillter)value; ReloadList(); OnPropertyChanged(); } }
        public ICommand DeleteCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ICommand CopyIdCommand { get; set; }
        public ICommand CopyNameCommand { get; set; }
        public ICommand CopyPhoneNumberCommand { get; set; }
        public ICommand CopyAddressCommand { get; set; }

        public PageMemberManagerVM()
        {
            ReloadList();

            SearchCommand = new RelayCommand<TextBox>((p) => { return p != null; }, (p) =>
            {
                if (p.Text == "" || p.Text == " ")
                {
                    p.Text = "";
                    ListMember = MemberDAL.Instance.GetList();
                    return;
                }

                var searchKeyWord = StringHelper.StringConvertToUnSign(p.Text).ToLower();

                // Tìm kiếm theo email
                if (p.Text.Contains("@"))
                {
                    ListMember = new ObservableCollection<MemberDTO>(MemberDAL.Instance.GetList().Where(
                        x => x.Email.ToLower().Contains(searchKeyWord)));
                }
                // Tìm kiếm theo số điện thoại
                else if (p.Text[0] >= '0' && p.Text[0] <= '9')
                {
                    ListMember = new ObservableCollection<MemberDTO>(MemberDAL.Instance.GetList().Where(
                        x => x.PhoneNumber.ToLower().Contains(searchKeyWord)));
                }
                // Tìm theo họ tên
                else
                {
                    ListMember = new ObservableCollection<MemberDTO>(MemberDAL.Instance.GetList().Where(
                        x => StringHelper.StringConvertToUnSign(x.FullName).ToLower().Contains(searchKeyWord)));
                }
            });

            ObjectSelectedChangedCommand = new RelayCommand<UserControl>((p) => { return p != null && MemberSelected != null; }, (p) =>
            {
                var btnStatusChange = p.FindName("btnStatusChange") as Button;
                var mnuStatusChange = p.FindName("mnuStatusChange") as MenuItem;
                if (MemberSelected.Status == true)
                {
                    btnStatusChange.Content = "KHÓA";
                    btnStatusChange.ToolTip = "Khóa thành viên \"" + MemberSelected.FullName + "\"";
                    mnuStatusChange.Header = "Khóa";
                }
                else
                {
                    btnStatusChange.Content = "MỞ KHÓA";
                    btnStatusChange.ToolTip = "Mở khóa thành viên \"" + MemberSelected.FullName + "\"";
                    mnuStatusChange.Header = "Mở khóa";
                }
            });

            ExportToExcelCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                string filePath = "";
                // tạo SaveFileDialog để lưu file excel
                SaveFileDialog dialog = new SaveFileDialog();

                dialog.Title = "Xuất danh sách thành viên thư viện";

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

                        ExcelHelper.SetExcelPackageInfo(excelPackage, "Library Manger", "Danh sách thành viên thư viện", new List<string>() { "List Member Sheet" });

                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

                        ExcelHelper.SetSheetInfo(worksheet, "List Member Sheet");

                        // set column width
                        ExcelHelper.SetColumWidth(worksheet, new double[] { 10, 20, 10, 15, 10, 18, 45, 35, 18, 18, 13 });

                        // Tạo danh sách các column header
                        string[] arrColumnHeader = { "Mã số", "Họ", "Tên", "Ngày sinh", "Giới tính", "CCCD", "Địa chỉ", "Email", "Số điện thoại", "Ngày đăng ký", "Ghi chú" };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        worksheet.Cells[1, 1].Value = "Thống kê danh sách thành viên thư viện".ToUpper();
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
                        foreach (var item in ListMember)
                        {
                            colIndex = 1;   // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            rowIndex++;     // rowIndex tương ứng từng dòng dữ liệu

                            //gán giá trị cho từng cell                      
                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Id;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.LastName;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.FirstName;

                            // lưu ý phải .ToShortDateString để dữ liệu khi in ra Excel là ngày như ta vẫn thấy.Nếu không sẽ ra tổng số :v
                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Birthday.Value.Date.ToShortDateString();

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Sex;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.SSN;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Address;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Email;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.PhoneNumber;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.RegisterDate.Value.Date.ToShortDateString();

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
                var addDataContext = new AddMemberWindowVM();
                var addMemberWindow = new AddMemberWindow() { DataContext = addDataContext };
                addMemberWindow.ShowDialog();

                var mySnackbar = p.FindName("mySnackbar") as Snackbar;
                if (addDataContext.Result != null)
                {
                    mySnackbar.MessageQueue.Enqueue("Thêm thành viên \"" + addDataContext.Result.FullName + "\" thành công");
                    ReloadList();
                }
                else { mySnackbar.MessageQueue.Enqueue("Không có thay đổi"); }
            });

            UpdateCommand = new RelayCommand<UserControl>((p) => { return p != null && MemberSelected != null; }, (p) =>
            {
                var tbxLastName = p.FindName("tbxLastName") as TextBox;
                var tbxFirstName = p.FindName("tbxFirstName") as TextBox;
                var cmbSex = p.FindName("cmbSex") as ComboBox;
                var dtpkBirthday = p.FindName("dtpkBirthday") as DatePicker;
                var tbxSSN = p.FindName("tbxSSN") as TextBox;
                var tbxAddress = p.FindName("tbxAddress") as TextBox;
                var tbxEmail = p.FindName("tbxEmail") as TextBox;
                var tbxPhone = p.FindName("tbxPhone") as TextBox;
                var dtpkRegisterDate = p.FindName("dtpkRegisterDate") as DatePicker;

                var tblLastNameWarning = p.FindName("tblLastNameWarning") as TextBlock;
                var tblFirstNameWarning = p.FindName("tblFirstNameWarning") as TextBlock;
                var tblSexWarning = p.FindName("tblSexWarning") as TextBlock;
                var tblBirthdayWarning = p.FindName("tblBirthdayWarning") as TextBlock;
                var tblSSNWarning = p.FindName("tblSSNWarning") as TextBlock;
                var tblAddressWarning = p.FindName("tblAddressWarning") as TextBlock;
                var tblEmailWarning = p.FindName("tblEmailWarning") as TextBlock;
                var tblPhoneWarning = p.FindName("tblPhoneWarning") as TextBlock;
                var tblRegisterDateWarning = p.FindName("tblRegisterDateWarning") as TextBlock;

                if (tbxLastName.Text == "")
                {
                    tblLastNameWarning.Visibility = Visibility.Visible;
                    tbxLastName.Focus();
                    return;
                }
                else { tblLastNameWarning.Visibility = Visibility.Hidden; }

                if (tbxFirstName.Text == "")
                {
                    tblFirstNameWarning.Visibility = Visibility.Visible;
                    tbxFirstName.Focus();
                    return;
                }
                else { tblFirstNameWarning.Visibility = Visibility.Hidden; }

                if (cmbSex.SelectedItem == null)
                {
                    tblSexWarning.Visibility = Visibility.Visible;
                    tbxLastName.Focus();
                    return;
                }
                else { tblSexWarning.Visibility = Visibility.Hidden; }

                if (dtpkBirthday.SelectedDate == null)
                {
                    tblBirthdayWarning.Visibility = Visibility.Visible;
                    dtpkBirthday.Focus();
                    return;
                }
                else { tblBirthdayWarning.Visibility = Visibility.Hidden; }

                if (tbxSSN.Text == "")
                {
                    tblSSNWarning.Visibility = Visibility.Visible;
                    tbxSSN.Focus();
                    return;
                }
                else { tblSSNWarning.Visibility = Visibility.Hidden; }

                if (tbxAddress.Text == "")
                {
                    tblAddressWarning.Visibility = Visibility.Visible;
                    tbxAddress.Focus();
                    return;
                }
                else { tblAddressWarning.Visibility = Visibility.Hidden; }

                if (tbxEmail.Text == "")
                {
                    tblEmailWarning.Visibility = Visibility.Visible;
                    tbxEmail.Focus();
                    return;
                }
                else { tblEmailWarning.Visibility = Visibility.Hidden; }

                if (tbxPhone.Text == "")
                {
                    tblPhoneWarning.Visibility = Visibility.Visible;
                    tbxPhone.Focus();
                    return;
                }
                else { tblPhoneWarning.Visibility = Visibility.Hidden; }

                if (dtpkRegisterDate.SelectedDate == null)
                {
                    tblRegisterDateWarning.Visibility = Visibility.Visible;
                    dtpkRegisterDate.Focus();
                    return;
                }
                else { tblRegisterDateWarning.Visibility = Visibility.Hidden; }

                MemberSelected.LastName = StringHelper.CapitalizeEachWord(tbxLastName.Text);
                MemberSelected.FirstName = StringHelper.CapitalizeEachWord(tbxFirstName.Text);
                MemberSelected.Sex = cmbSex.SelectedValue.ToString();
                MemberSelected.Birthday = dtpkBirthday.SelectedDate;
                MemberSelected.SSN = tbxSSN.Text;
                MemberSelected.Address = tbxAddress.Text;
                MemberSelected.Email = tbxEmail.Text;
                MemberSelected.PhoneNumber = tbxPhone.Text;
                MemberSelected.RegisterDate = dtpkRegisterDate.SelectedDate;

                MemberDAL.Instance.Update(MemberSelected);
                var mySnackbar = p.FindName("mySnackbar") as Snackbar;
                mySnackbar.MessageQueue.Enqueue("Cập nhật thông tin thành viên \"" + MemberSelected.FullName + "\" thành công");
                ReloadList();
            });

            SendEmailCommand = new RelayCommand<object>((p) => { return MemberSelected != null && MemberSelected.Email != null; }, (p) =>
            {
                WebHelper.SendEmail(MemberSelected.Email);
            });

            StatusChangeCommand = new RelayCommand<object>((p) => { return MemberSelected != null; }, (p) =>
            {
                MemberDAL.Instance.ChangeStatus(MemberSelected.Id);
                ReloadList();
            });

            CopyIdCommand = new RelayCommand<object>((p) => { return MemberSelected != null; }, (p) => { Clipboard.SetText(MemberSelected.Id); });

            CopyNameCommand = new RelayCommand<object>((p) => { return MemberSelected != null; }, (p) => { Clipboard.SetText(MemberSelected.FullName); });

            CopyPhoneNumberCommand = new RelayCommand<object>((p) => { return MemberSelected != null; }, (p) => { Clipboard.SetText(MemberSelected.PhoneNumber); });

            CopyAddressCommand = new RelayCommand<object>((p) => { return MemberSelected != null; }, (p) => { Clipboard.SetText(MemberSelected.Address); });
        }

        private void ReloadList()
        {
            ListMember = MemberDAL.Instance.GetList(statusFillter);
        }

        private ObservableCollection<MemberDTO> listMember;
        private MemberDTO memberSelected;
        StatusFillter statusFillter = Utility.Enums.StatusFillter.Active;
    }
}
