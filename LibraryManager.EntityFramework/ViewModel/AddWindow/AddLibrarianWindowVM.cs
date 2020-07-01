using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.Utility;
using LibraryManager.Utility.Interfaces;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManager.EntityFramework.ViewModel.AddWindow
{
    public class AddLibrarianWindowVM : BaseViewModel, IAddNewObject<LibrarianDTO>
    {
        public ICommand OKCommand { get; set; }
        public ICommand RetypeCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public LibrarianDTO Result { get; set; }

        public AddLibrarianWindowVM()
        {
            OKCommand = new RelayCommand<Window>((p) => { return !(p == null); }, (p) =>
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


                var newLibrarian = new LibrarianDTO()
                {
                    Id = "",
                    LastName = StringHelper.CapitalizeEachWord(tbxLastName.Text),
                    FirstName = StringHelper.CapitalizeEachWord(tbxFirstName.Text),
                    Sex = cmbSex.SelectedValue.ToString(),
                    Birthday = dtpkBirthday.SelectedDate,
                    SSN = tbxSSN.Text,
                    Address = tbxAddress.Text,
                    Email = tbxEmail.Text,
                    PhoneNumber = tbxPhone.Text,
                    Salary = StringHelper.ToDecimal(tbxSalary.Text),
                    StartDate = DateTime.Now,
                    Status = true
                };

                LibrarianDAL.Instance.Add(newLibrarian);
                Result = newLibrarian;
                p.Close();
            });

            RetypeCommand = new RelayCommand<Window>((p) => { return !(p == null); }, (p) =>
            {
                var tbxLastName = p.FindName("tbxLastName") as TextBox;
                var tbxFirstName = p.FindName("tbxFirstName") as TextBox;
                var tbxSSN = p.FindName("tbxSSN") as TextBox;
                var tbxAddress = p.FindName("tbxAddress") as TextBox;
                var tbxEmail = p.FindName("tbxEmail") as TextBox;
                var tbxPhone = p.FindName("tbxPhone") as TextBox;
                var tbxSalary = p.FindName("tbxSalary") as TextBox;

                tbxLastName.Text = "";
                tbxFirstName.Text = "";
                tbxSSN.Text = "";
                tbxAddress.Text = "";
                tbxEmail.Text = "";
                tbxPhone.Text = "";
                tbxSalary.Text = "";
            });

            CancelCommand = new RelayCommand<Window>((p) => { return !(p == null); }, (p) => { p.Close(); });
        }
    }
}
