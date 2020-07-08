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
                var txtLastName = p.FindName("txtLastName") as TextBox;
                var txtFirstName = p.FindName("txtFirstName") as TextBox;
                var cmbSex = p.FindName("cmbSex") as ComboBox;
                var dtpkBirthday = p.FindName("dtpkBirthday") as DatePicker;
                var txtSSN = p.FindName("txtSSN") as TextBox;
                var txtAddress = p.FindName("txtAddress") as TextBox;
                var txtEmail = p.FindName("txtEmail") as TextBox;
                var txtPhone = p.FindName("txtPhone") as TextBox;
                var txtSalary = p.FindName("txtSalary") as TextBox;

                var tblLastNameWarning = p.FindName("tblLastNameWarning") as TextBlock;
                var tblFirstNameWarning = p.FindName("tblFirstNameWarning") as TextBlock;
                var tblSexWarning = p.FindName("tblSexWarning") as TextBlock;
                var tblBirthdayWarning = p.FindName("tblBirthdayWarning") as TextBlock;
                var tblSSNWarning = p.FindName("tblSSNWarning") as TextBlock;
                var tblAddressWarning = p.FindName("tblAddressWarning") as TextBlock;
                var tblEmailWarning = p.FindName("tblEmailWarning") as TextBlock;
                var tblPhoneWarning = p.FindName("tblPhoneWarning") as TextBlock;
                var tblSalaryWarning = p.FindName("tblSalaryWarning") as TextBlock;

                if (txtLastName.Text == "")
                {
                    tblLastNameWarning.Visibility = Visibility.Visible;
                    txtLastName.Focus();
                    return;
                }
                else { tblLastNameWarning.Visibility = Visibility.Hidden; }

                if (txtFirstName.Text == "")
                {
                    tblFirstNameWarning.Visibility = Visibility.Visible;
                    txtFirstName.Focus();
                    return;
                }
                else { tblFirstNameWarning.Visibility = Visibility.Hidden; }

                if (cmbSex.SelectedItem == null)
                {
                    tblSexWarning.Visibility = Visibility.Visible;
                    txtLastName.Focus();
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

                if (txtSSN.Text == "")
                {
                    tblSSNWarning.Visibility = Visibility.Visible;
                    txtSSN.Focus();
                    return;
                }
                else { tblSSNWarning.Visibility = Visibility.Hidden; }

                if (txtAddress.Text == "")
                {
                    tblAddressWarning.Visibility = Visibility.Visible;
                    txtAddress.Focus();
                    return;
                }
                else { tblAddressWarning.Visibility = Visibility.Hidden; }

                if (txtEmail.Text == "")
                {
                    tblEmailWarning.Visibility = Visibility.Visible;
                    txtEmail.Focus();
                    return;
                }
                else { tblEmailWarning.Visibility = Visibility.Hidden; }

                if (txtPhone.Text == "")
                {
                    tblPhoneWarning.Visibility = Visibility.Visible;
                    txtPhone.Focus();
                    return;
                }
                else { tblPhoneWarning.Visibility = Visibility.Hidden; }

                if (StringHelper.ToDecimal(txtSalary.Text) == 0)
                {
                    tblSalaryWarning.Visibility = Visibility.Visible;
                    txtSalary.Focus();
                    return;
                }
                else { tblSalaryWarning.Visibility = Visibility.Hidden; }


                var newLibrarian = new LibrarianDTO()
                {
                    Id = "",
                    LastName = StringHelper.CapitalizeEachWord(txtLastName.Text),
                    FirstName = StringHelper.CapitalizeEachWord(txtFirstName.Text),
                    Sex = cmbSex.SelectedValue.ToString(),
                    Birthday = dtpkBirthday.SelectedDate,
                    SSN = txtSSN.Text,
                    Address = txtAddress.Text,
                    Email = txtEmail.Text,
                    PhoneNumber = txtPhone.Text,
                    Salary = StringHelper.ToDecimal(txtSalary.Text),
                    StartDate = DateTime.Now,
                    Status = true
                };

                LibrarianDAL.Instance.Add(newLibrarian);
                Result = newLibrarian;
                p.Close();
            });

            RetypeCommand = new RelayCommand<Window>((p) => { return !(p == null); }, (p) =>
            {
                var txtLastName = p.FindName("txtLastName") as TextBox;
                var txtFirstName = p.FindName("txtFirstName") as TextBox;
                var txtSSN = p.FindName("txtSSN") as TextBox;
                var txtAddress = p.FindName("txtAddress") as TextBox;
                var txtEmail = p.FindName("txtEmail") as TextBox;
                var txtPhone = p.FindName("txtPhone") as TextBox;
                var txtSalary = p.FindName("txtSalary") as TextBox;

                txtLastName.Text = "";
                txtFirstName.Text = "";
                txtSSN.Text = "";
                txtAddress.Text = "";
                txtEmail.Text = "";
                txtPhone.Text = "";
                txtSalary.Text = "";
            });

            CancelCommand = new RelayCommand<Window>((p) => { return !(p == null); }, (p) => { p.Close(); });
        }
    }
}
