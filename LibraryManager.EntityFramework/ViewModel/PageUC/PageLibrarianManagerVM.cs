using LibraryManager.EntityFramework.Model;
using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.EntityFramework.View.AddWindow;
using LibraryManager.Utility;
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
    public class PageLibrarianManagerVM : BaseViewModel
    {
        public ObservableCollection<LibrarianDTO> ListLibrarian { get => listLibrarian; set { listLibrarian = value; OnPropertyChanged(); } }
        public LibrarianDTO LibrarianSelected { get => librarianSelected; set { librarianSelected = value; OnPropertyChanged(); } }
        public ICommand FilterByStatusCommand { get; set; }
        public ICommand AddLibrarianCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand EmailToCommand { get; set; }
        public ICommand StatusChangeCommand { get; set; }

        public PageLibrarianManagerVM()
        {
            ListLibrarian = LibrarianDAL.Instance.GetListByFillter(true);

            FilterByStatusCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p.ToString() == "Tất cả")
                {
                    ListLibrarian = LibrarianDAL.Instance.GetList();
                }
                else if (p.ToString() == "Đang làm")
                {
                    ListLibrarian = LibrarianDAL.Instance.GetListByFillter(true);
                }
                else
                {
                    ListLibrarian = LibrarianDAL.Instance.GetListByFillter(false);
                }
            });

            AddLibrarianCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var addLibrarianWindow = new AddLibrarianWindow();
                addLibrarianWindow.ShowDialog();

                ListLibrarian = LibrarianDAL.Instance.GetList();
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
