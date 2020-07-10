using LibraryManager.EntityFramework.Model;
using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.Utility;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManager.EntityFramework.ViewModel.PageUC
{
    public class PageLibrarianInforVM : BaseViewModel
    {
        public Account AccountLogin { get => accountLogin; set { accountLogin = value; OnPropertyChanged(); } }
        public LibrarianDTO LibrarianLogin { get => memberLogin; set { memberLogin = value; OnPropertyChanged(); } }
        public ICommand LoadedCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand CancelUpdateCommand { get; set; }
        public ICommand UpdateLoginInfoCommand { get; set; }
        public ICommand CancelLoginInfoCommand { get; set; }
        
        public PageLibrarianInforVM(LibrarianDTO member, Account account)
        {
            LibrarianLogin = member;
            AccountLogin = account;

            LoadedCommand = new RelayCommand<UserControl>((p) => { return p != null; }, (p) =>
            {
                //var icoAccount = p.FindName("icoAccount") as PackIcon;
                //int firstChar = char.ToUpper(member.FirstName[0]);
                //icoAccount.Kind = (PackIconKind)(158 + 5 * (firstChar - (int)'A' + 2));
            });

            CancelUpdateCommand = new RelayCommand<UserControl>((p) => { return p != null; }, (p) =>
            {
                var txtLastName = p.FindName("txtLastName") as TextBox;
                var txtFirstName = p.FindName("txtFirstName") as TextBox;
                var cmbSex = p.FindName("cmbSex") as ComboBox;
                var dtpkBirthday = p.FindName("dtpkBirthday") as DatePicker;
                var txtSSN = p.FindName("txtSSN") as TextBox;
                var txtAddress = p.FindName("txtAddress") as TextBox;
                var txtEmail = p.FindName("txtEmail") as TextBox;
                var txtPhone = p.FindName("txtPhone") as TextBox;

                txtLastName.Text = LibrarianLogin.LastName;
                txtFirstName.Text = LibrarianLogin.FirstName;
                cmbSex.SelectedValue = LibrarianLogin.Sex;
                dtpkBirthday.SelectedDate = LibrarianLogin.Birthday;
                txtSSN.Text = LibrarianLogin.SSN;
                txtAddress.Text = LibrarianLogin.Address;
                txtEmail.Text = LibrarianLogin.Email;
                txtPhone.Text = LibrarianLogin.PhoneNumber;
            });

            UpdateCommand = new RelayCommand<UserControl>((p) => { return p != null; }, (p) =>
            {
                var txtLastName = p.FindName("txtLastName") as TextBox;
                var txtFirstName = p.FindName("txtFirstName") as TextBox;
                var cmbSex = p.FindName("cmbSex") as ComboBox;
                var dtpkBirthday = p.FindName("dtpkBirthday") as DatePicker;
                var txtSSN = p.FindName("txtSSN") as TextBox;
                var txtAddress = p.FindName("txtAddress") as TextBox;
                var txtEmail = p.FindName("txtEmail") as TextBox;
                var txtPhone = p.FindName("txtPhone") as TextBox;

                var tblLastNameWarning = p.FindName("tblLastNameWarning") as TextBlock;
                var tblFirstNameWarning = p.FindName("tblFirstNameWarning") as TextBlock;
                var tblSexWarning = p.FindName("tblSexWarning") as TextBlock;
                var tblBirthdayWarning = p.FindName("tblBirthdayWarning") as TextBlock;
                var tblSSNWarning = p.FindName("tblSSNWarning") as TextBlock;
                var tblAddressWarning = p.FindName("tblAddressWarning") as TextBlock;
                var tblEmailWarning = p.FindName("tblEmailWarning") as TextBlock;
                var tblPhoneWarning = p.FindName("tblPhoneWarning") as TextBlock;

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

                LibrarianLogin.LastName = StringHelper.CapitalizeEachWord(txtLastName.Text);
                LibrarianLogin.FirstName = StringHelper.CapitalizeEachWord(txtFirstName.Text);
                LibrarianLogin.Sex = cmbSex.SelectedValue.ToString();
                LibrarianLogin.Birthday = dtpkBirthday.SelectedDate;
                LibrarianLogin.SSN = txtSSN.Text;
                LibrarianLogin.Address = txtAddress.Text;
                LibrarianLogin.Email = txtEmail.Text;
                LibrarianLogin.PhoneNumber = txtPhone.Text;

                LibrarianDAL.Instance.Update(LibrarianLogin);
                var mySnackbar = p.FindName("mySnackbar") as Snackbar;
                mySnackbar.MessageQueue.Enqueue("Cập nhật thông tin thành viên thành công");
                OnPropertyChanged();
            });

            UpdateLoginInfoCommand = new RelayCommand<System.Windows.Controls.UserControl>((p) => { return p != null; }, (p) =>
            {
                var txtUsername = p.FindName("txtUsername") as System.Windows.Controls.TextBox;
                var pwxPassword = p.FindName("pwxPassword") as PasswordBox;
                var pwxPasswordNew = p.FindName("pwxPasswordNew") as PasswordBox;
                var pwxRetypePasswordNew = p.FindName("pwxRetypePasswordNew") as PasswordBox;

                var tblUsernameWarning = p.FindName("tblUsernameWarning") as TextBlock;
                var tblPasswordWarning = p.FindName("tblPasswordWarning") as TextBlock;
                var tblPasswordNewWarning = p.FindName("tblPasswordNewWarning") as TextBlock;
                var tblRetypePasswordNewWarning = p.FindName("tblRetypePasswordNewWarning") as TextBlock;

                if (txtUsername.Text.Length < 6)
                {
                    tblUsernameWarning.Text = "Tên đăng nhập tối thiểu 6 kí tự";
                    tblUsernameWarning.Visibility = Visibility.Visible;
                    txtUsername.Focus();
                    return;
                }
                else { tblUsernameWarning.Visibility = Visibility.Hidden; }

                if (AccountDAL.Instance.CheckPassword(account, pwxPassword.Password) == false)
                {
                    tblPasswordWarning.Visibility = Visibility.Visible;
                    pwxPassword.Focus();
                    return;
                }
                else { tblPasswordWarning.Visibility = Visibility.Hidden; }

                if (pwxPasswordNew.Password.Length < 6 && pwxPasswordNew.IsEnabled)
                {
                    tblPasswordNewWarning.Visibility = Visibility.Visible;
                    pwxPasswordNew.Focus();
                    return;
                }
                else { tblPasswordNewWarning.Visibility = Visibility.Hidden; }

                if (pwxPasswordNew.Password != pwxRetypePasswordNew.Password && pwxPasswordNew.IsEnabled)
                {
                    tblRetypePasswordNewWarning.Visibility = Visibility.Visible;
                    pwxRetypePasswordNew.Focus();
                    return;
                }
                else { tblRetypePasswordNewWarning.Visibility = Visibility.Hidden; }

                var updateResult = AccountDAL.Instance.Update(AccountLogin.PersonId, txtUsername.Text, pwxPasswordNew.IsEnabled ? pwxPasswordNew.Password : pwxPassword.Password);

                if (updateResult != null)
                {
                    var mySnackbar = p.FindName("mySnackbar") as Snackbar;
                    mySnackbar.MessageQueue.Enqueue("Cập nhật thông tin đăng nhập thành công");
                    tblUsernameWarning.Visibility = Visibility.Hidden;
                    AccountLogin = updateResult;
                }
                else
                {
                    tblUsernameWarning.Text = "Tên đăng nhập đã tồn tại";
                    tblUsernameWarning.Visibility = Visibility.Visible;
                    txtUsername.Focus();
                }
            });

            CancelLoginInfoCommand = new RelayCommand<UserControl>((p) => { return p != null; }, (p) =>
            {
            });
        }

        LibrarianDTO memberLogin;
        Account accountLogin;
    }
}
