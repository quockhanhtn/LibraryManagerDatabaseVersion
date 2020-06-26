using LibraryManager.EntityFramework.Model;
using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.EntityFramework.View.AddWindow;
using LibraryManager.EntityFramework.ViewModel.AddWindow;
using LibraryManager.MyUserControl.MyBox;
using LibraryManager.Utility;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManager.EntityFramework.ViewModel.PageUC
{
    public class PageLibrarianManagerVM : BaseViewModel
    {
        public ObservableCollection<LibrarianDTO> ListLibrarian { get => listLibrarian; set { listLibrarian = value; OnPropertyChanged(); } }
        public LibrarianDTO LibrarianSelected { get => librarianSelected; set { librarianSelected = value; OnPropertyChanged(); } }
        public ICommand SearchCommand { get; set; }
        public ICommand FilterByStatusCommand { get; set; }
        public ICommand ExportToExcelCommand { get; set; }
        public ICommand AddLibrarianCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand EmailToCommand { get; set; }
        public ICommand StatusChangeCommand { get; set; }

        public PageLibrarianManagerVM()
        {
            ListLibrarian = LibrarianDAL.Instance.GetListByFillter(true);

            SearchCommand = new RelayCommand<TextBox>((p) => { return !(p == null); }, (p) =>
            {
                if (p.Text == "" || p.Text == " ")
                {
                    p.Text = "";
                    ListLibrarian = LibrarianDAL.Instance.GetList();
                    return;
                }

                var searchKeyWord = StringHelper.StringConvertToUnSign(p.Text).ToLower();

                // Tìm kiếm theo email
                if (p.Text.Contains("@"))
                {
                    ListLibrarian = new ObservableCollection<LibrarianDTO>(LibrarianDAL.Instance.GetList().Where(
                        x => x.Email.ToLower().Contains(searchKeyWord)));
                }
                // Tìm kiếm theo số điện thoại
                else if (p.Text[0] >= '0' && p.Text[0] <= '9')
                {
                    ListLibrarian = new ObservableCollection<LibrarianDTO>(LibrarianDAL.Instance.GetList().Where(
                        x => x.PhoneNumber.ToLower().Contains(searchKeyWord)));
                }
                // Tìm theo họ tên
                else
                {
                    ListLibrarian = new ObservableCollection<LibrarianDTO>(LibrarianDAL.Instance.GetList().Where(
                        x => StringHelper.StringConvertToUnSign(x.FullName).ToLower().Contains(searchKeyWord)));
                }
            });

            FilterByStatusCommand = new RelayCommand<ComboBoxItem>((p) => { return true; }, (p) =>
            {
                if (p.Content.ToString() == "Tất cả")
                {
                    ListLibrarian = LibrarianDAL.Instance.GetList();
                }
                else if (p.Content.ToString() == "Đang làm")
                {
                    ListLibrarian = LibrarianDAL.Instance.GetListByFillter(true);
                }
                else
                {
                    ListLibrarian = LibrarianDAL.Instance.GetListByFillter(false);
                }
            });

            ExportToExcelCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                string filePath = "";
                // tạo SaveFileDialog để lưu file excel
                SaveFileDialog dialog = new SaveFileDialog();

                dialog.Title = "Xuất danh sách nhân viên thư viện";

                // chỉ lọc ra các file có định dạng Excel
                dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";

                // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
                if (dialog.ShowDialog() == true)  { filePath = dialog.FileName; }

                // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
                if (string.IsNullOrEmpty(filePath))
                {
                    MyMessageBox.Show("Đường dẫn báo cáo không hợp lệ!", "Lỗi", "OK", "", MessageBoxImage.Error);
                    return;
                }

                try
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (var excelPackage = new ExcelPackage())
                    {
                        
                        ExcelHelper.SetExcelPackageInfo(excelPackage, "Library Manger", "Danh sách nhân viên thư viện", new List<string>() { "List Librarian Sheet" });

                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

                        ExcelHelper.SetSheetInfo(worksheet, "List Librarian Sheet");

                        // set column width
                        ExcelHelper.SetColumWidth(worksheet, new int[] { 10, 20, 10, 15, 10, 18, 45, 35, 18, 18, 12 });

                        // Tạo danh sách các column header
                        string[] arrColumnHeader = { "Mã số", "Họ", "Tên", "Ngày sinh", "Giới tính", "CCCD", "Địa chỉ", "Email", "Số điện thoại", "Ngày làm việc", "Ghi chú" };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        worksheet.Cells[1, 1].Value = "Thống kê danh sách nhân viên thư viện".ToUpper();
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
                        foreach (var item in ListLibrarian)
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
                            worksheet.Cells[rowIndex, colIndex++].Value = item.StartDate.Value.Date.ToShortDateString();

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

            AddLibrarianCommand = new RelayCommand<UserControl>((p) => { return !(p == null); }, (p) =>
            {
                var addLibrarianWindow = new AddLibrarianWindow();
                addLibrarianWindow.ShowDialog();

                var mySnackbar = p.FindName("mySnackbar") as Snackbar;
                var newLibrarianName = (addLibrarianWindow.DataContext as AddLibrarianWindowVM).Result;
                if (newLibrarianName != "")
                {
                    mySnackbar.MessageQueue.Enqueue("Thêm nhân viên \"" + newLibrarianName + "\" thành công");
                    ListLibrarian = LibrarianDAL.Instance.GetList();
                }
                else
                {
                    mySnackbar.MessageQueue.Enqueue("Không có nhân viên được thêm");
                }
            });

            UpdateCommand = new RelayCommand<UserControl>((p) => { return !(p == null); }, (p) =>
            {
                var tbxLastName = p.FindName("tbxLastName") as TextBox;
                var tbxFirstName = p.FindName("tbxFirstName") as TextBox;
                var cmbSex = p.FindName("cmbSex") as ComboBox;
                var dtpkBirthday = p.FindName("dtpkBirthday") as DatePicker;
                var tbxSSN = p.FindName("tbxSSN") as TextBox;
                var tbxAddress = p.FindName("tbxAddress") as TextBox;
                var tbxEmail = p.FindName("tbxEmail") as TextBox;
                var tbxPhone = p.FindName("tbxPhone") as TextBox;
                var tbxSalary = p.FindName("tbxSalary") as TextBox;

                var tblLastNameWarning = p.FindName("tblLastNameWarning") as TextBlock;
                var tblFirstNameWarning = p.FindName("tblFirstNameWarning") as TextBlock;
                var tblSexWarning = p.FindName("tblSexWarning") as TextBlock;
                var tblBirthdayWarning = p.FindName("tblBirthdayWarning") as TextBlock;
                var tblSSNWarning = p.FindName("tblSSNWarning") as TextBlock;
                var tblAddressWarning = p.FindName("tblAddressWarning") as TextBlock;
                var tblEmailWarning = p.FindName("tblEmailWarning") as TextBlock;
                var tblPhoneWarning = p.FindName("tblPhoneWarning") as TextBlock;
                var tblSalaryWarning = p.FindName("tblSalaryWarning") as TextBlock;

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

                if (StringHelper.ToDecimal(tbxSalary.Text) == 0)
                {
                    tblSalaryWarning.Visibility = Visibility.Visible;
                    tbxSalary.Focus();
                    return;
                }
                else { tblSalaryWarning.Visibility = Visibility.Hidden; }


                LibrarianSelected.LastName = StringHelper.CapitalizeEachWord(tbxLastName.Text);
                LibrarianSelected.FirstName = StringHelper.CapitalizeEachWord(tbxFirstName.Text);
                LibrarianSelected.Sex = cmbSex.SelectedValue.ToString();
                LibrarianSelected.Birthday = dtpkBirthday.SelectedDate;
                LibrarianSelected.SSN = tbxSSN.Text;
                LibrarianSelected.Address = tbxAddress.Text;
                LibrarianSelected.Email = tbxEmail.Text;
                LibrarianSelected.PhoneNumber = tbxPhone.Text;
                LibrarianSelected.Salary = StringHelper.ToDecimal(tbxSalary.Text);

                LibrarianDAL.Instance.Update(LibrarianSelected);
                OnPropertyChanged();
            });

            EmailToCommand = new RelayCommand<object>((p) => { return true; }, (p) => { });

            StatusChangeCommand = new RelayCommand<string>((p) =>
            {
                bool canExcute = false;
                if (p == "True" && LibrarianSelected.Status == true) { canExcute = true; }
                else if (p == "False" && LibrarianSelected.Status == false) { canExcute = true; }
                return canExcute;
            }, (p) =>
            {
                LibrarianSelected.Status = !LibrarianSelected.Status;
            });
        }

        private ObservableCollection<LibrarianDTO> listLibrarian;
        private LibrarianDTO librarianSelected;
    }
}
