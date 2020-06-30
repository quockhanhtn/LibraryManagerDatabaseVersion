using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.EntityFramework.View.PageUC;
using LibraryManager.MyUserControl.MyBox;
using LibraryManager.Utility;
using LibraryManager.Utility.Enums;
using LibraryManager.Utility.Interfaces;
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
    public class PageBookManagerVM : BaseViewModel, IObjectManager
    {
        public ObservableCollection<BookDTO> ListBook { get => listBook; set { listBook = value; OnPropertyChanged(); } }
        public ObservableCollection<BookCategoryDTO> ListBookCategory { get => listBookCategory; set { listBookCategory = value; OnPropertyChanged(); } }
        public ObservableCollection<PublisherDTO> ListPublisher { get => listPublisher; set { listPublisher = value; OnPropertyChanged(); } }
        public BookDTO BookSelected { get => bookSelected; set { bookSelected = value; OnPropertyChanged(); } }
        public BookCategoryDTO BookCategorySelected { get => bookCategorySelected; set { bookCategorySelected = value; OnPropertyChanged(); ReloadList(); } }
        public PublisherDTO PublisherSelected { get => publisherSelected; set { publisherSelected = value; OnPropertyChanged(); ReloadList();} }

        public ICommand BorrowBookCommand { get; set; }
        public ICommand ReturnBookCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand ChangeBookCategoryCommand { get; set; }
        public ICommand ChangePublisherCommand { get; set; }
        public ICommand BookSelectedChanged { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand StatisticCommand { get; set; }
        public ICommand ExportToExcelCommand { get; set; }

        public ICommand ObjectSelectedChangedCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ICommand StatusChangeCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ICommand DeleteCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public PageBookManagerVM(LibrarianDTO librarian)
        {
            ListBookCategory = BookCategoryDAL.Instance.GetList(StatusFillter.Active);
            ListBookCategory.Add(new BookCategoryDTO() { Id = 0, Name = "Tất cả chuyên mục" });
            ListPublisher = PublisherDAL.Instance.GetList(StatusFillter.Active);
            ListPublisher.Add(new PublisherDTO() { Id = 0, Name = "Tất cả NXB" });
            ReloadList();

            BorrowBookCommand = new RelayCommand<UserControl>((p) => { return p != null; }, (p) =>
            {
                var findMemberVM = new FindMemberWindowViewModel()
                {
                    MemberAction = FindMemberWindowViewModel.BookAction.Borrow,
                    Title = "Nhập thông tin người mượn sách"
                };
                var findMemberWindow = new FindMemberWindow() { DataContext = findMemberVM };
                findMemberWindow.ShowDialog();

                var memberFound = findMemberVM.MemberSelected;
                if(memberFound != null)
                {
                    var borrowVM = new PageBorrowBookVM(memberFound, librarian);
                    var borrowPage = new PageBorrowBook() { DataContext = borrowVM };
                    try
                    {
                        var w = FrameworkElementExtend.GetWindowParent(p) as Window;
                        var gridMain = w.FindName("gridMain") as Grid;
                        //gridMain.Children.Clear();
                        gridMain.Children.Add(borrowPage);
                    }
                    catch (Exception) { }
                }
            });

            ReturnBookCommand = new RelayCommand<UserControl>((p) => { return p != null; }, (p) =>
            {
                var findMemberVM = new FindMemberWindowViewModel()
                {
                    MemberAction = FindMemberWindowViewModel.BookAction.Return,
                    Title = "Nhập thông tin người trả sách"
                };
                var findMemberWindow = new FindMemberWindow() { DataContext = findMemberVM };
                findMemberWindow.ShowDialog();

                var memberFound = findMemberVM.MemberSelected;
                if (memberFound != null)
                {
                    var returnVM = new PageReturnBookVM(memberFound, librarian);
                    var returnPage = new PageReturnBook() { DataContext = returnVM };
                    try
                    {
                        var w = FrameworkElementExtend.GetWindowParent(p) as Window;
                        var gridMain = w.FindName("gridMain") as Grid;
                        //gridMain.Children.Clear();
                        gridMain.Children.Add(returnPage);
                    }
                    catch (Exception) { }
                }
            });

            SearchCommand = new RelayCommand<TextBox>((p) => { return p != null; }, (p) =>
            {
                if (p.Text == "" || p.Text == " ")
                {
                    p.Text = "";
                    ReloadList();
                    return;
                }

                var searchKeyWord = StringHelper.StringConvertToUnSign(p.Text).ToLower();

                ReloadList();
                ListBook = new ObservableCollection<BookDTO>(ListBook.Where(
                    x => StringHelper.StringConvertToUnSign(x.Title).ToLower().Contains(searchKeyWord)
                    || StringHelper.StringConvertToUnSign(x.AuthorNames).ToLower().Contains(searchKeyWord)));
            });

            BookSelectedChanged = new RelayCommand<UserControl>((p) => { return p != null && BookSelected != null; }, (p) =>
            {
                //var btnStatusChange = p.FindName("btnStatusChange") as Button;
                ////var tblStatusChange = p.FindName("tblStatusChange") as TextBlock;
                ////var icoStatusChange = p.FindName("icoStatusChange") as PackIcon;
                //if (BookSelected.Status == true)
                //{
                //    //tblStatusChange.Text = "THÔI VIỆC";
                //    //icoStatusChange.Kind = PackIconKind.BlockHelper;
                //    btnStatusChange.Content = "THÔI VIỆC";
                //    btnStatusChange.ToolTip = "Nhân viên " + BookSelected.FullName + " nghỉ việc";
                //}
                //else
                //{
                //    //tblStatusChange.Text = "ĐI LÀM LẠI";
                //    //icoStatusChange.Kind = PackIconKind.Restore;
                //    btnStatusChange.Content = "ĐI LÀM LẠI";
                //    btnStatusChange.ToolTip = "Nhân viên " + BookSelected.FullName + " làm viêc lại";
                //}
            });

            AddCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                //var addBookWindow = new AddBookWindow();
                //addBookWindow.ShowDialog();

                //var mySnackbar = p.FindName("mySnackbar") as Snackbar;
                //var newBookName = (addBookWindow.DataContext as AddBookWindowVM).Result;
                //if (newBookName != "")
                //{
                //    mySnackbar.MessageQueue.Enqueue("Thêm nhân viên \"" + newBookName + "\" thành công");
                //    ReloadList();
                //}
                //else { mySnackbar.MessageQueue.Enqueue("Không có thay đổi"); }
            });

            UpdateCommand = new RelayCommand<object>((p) => { return BookSelected != null; }, (p) => { });

            RemoveCommand = new RelayCommand<object>((p) => { return BookSelected != null; }, (p) => { });

            StatisticCommand = new RelayCommand<object>((p) => { return BookSelected != null && BookSelected.BookItem.Number > BookSelected.BookItem.Count; }, (p) => { });

            ExportToExcelCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                string filePath = "";
                // tạo SaveFileDialog để lưu file excel
                SaveFileDialog dialog = new SaveFileDialog();

                dialog.Title = "Xuất danh sách sách thư viện";

                // chỉ lọc ra các file có định dạng Excel
                dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";

                // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
                if (dialog.ShowDialog() == true) { filePath = dialog.FileName; }

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
                        ExcelHelper.SetExcelPackageInfo(excelPackage, "Library Manger", "Danh sách sách thư viện", new List<string>() { "List Book Sheet" });

                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

                        ExcelHelper.SetSheetInfo(worksheet, "List Book Sheet");

                        // set column width
                        ExcelHelper.SetColumWidth(worksheet, new int[] { 14, 50, 25, 25, 7, 25, 10, 13, 9, 15, 15 });

                        // Tạo danh sách các column header
                        string[] arrColumnHeader = { "Mã sách", "Tựa sách", "Chuyên mục", "Nhà xuất bản", "Năm", "Tác giả", "Số trang", "Kích thước", "Giá tiền", "Tổng số sách", "Còn lại" };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        worksheet.Cells[1, 1].Value = "Thống kê danh sách sách thư viện".ToUpper();
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
                        foreach (var item in ListBook)
                        {
                            colIndex = 1;   // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            rowIndex++;     // rowIndex tương ứng từng dòng dữ liệu

                            //gán giá trị cho từng cell                      
                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Id;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Title;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.BookCategory.Name;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Publisher.Name;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.YearPublish;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.AuthorNames;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.PageNumber;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Size;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.Price;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.BookItem.Count;

                            ExcelHelper.FormatCellBorder(worksheet, rowIndex, colIndex);
                            worksheet.Cells[rowIndex, colIndex++].Value = item.BookItem.Count;
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

        public void ReloadList()
        {
            if (BookCategorySelected == null && PublisherSelected == null)
            {
                ListBook = BookDAL.Instance.GetList();
            }
            else if (BookCategorySelected == null)
            {
                ListBook = BookDAL.Instance.GetList(0, PublisherSelected.Id);
            }
            else if (PublisherSelected == null)
            {
                ListBook = BookDAL.Instance.GetList(BookCategorySelected.Id, 0);
            }
            else { ListBook = BookDAL.Instance.GetList(BookCategorySelected.Id, PublisherSelected.Id); }

            //ListBookCategory = BookCategoryDAL.Instance.GetList(true);
            //ListBookCategory.Add(new BookCategoryDTO() { Id = 0, Name = "Tất cả chuyên mục" });
            //ListPublisher = PublisherDAL.Instance.GetList(true);
            //ListPublisher.Add(new PublisherDTO() { Id = 0, Name = "Tất cả NXB" });
        }

        ObservableCollection<BookDTO> listBook;
        ObservableCollection<BookCategoryDTO> listBookCategory;
        ObservableCollection<PublisherDTO> listPublisher;
        BookDTO bookSelected;
        BookCategoryDTO bookCategorySelected;
        PublisherDTO publisherSelected;
    }
}
