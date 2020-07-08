using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.Utility;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManager.EntityFramework.ViewModel.PageUC
{
    public class PageMemberInforVM : BaseViewModel
    {
        public MemberDTO MemberLogin { get => memberLogin; set { memberLogin = value; OnPropertyChanged(); } }
        public ICommand LoadedCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand CancelUpdateCommand { get; set; }
        public ICommand UpdatePasswordCommand { get; set; }
        public ICommand CancelPasswordCommand { get; set; }

        public PageMemberInforVM(MemberDTO member)
        {
            MemberLogin = member;

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

                txtLastName.Text = MemberLogin.LastName;
                txtFirstName.Text = MemberLogin.FirstName;
                cmbSex.SelectedValue = MemberLogin.Sex;
                dtpkBirthday.SelectedDate = MemberLogin.Birthday;
                txtSSN.Text = MemberLogin.SSN;
                txtAddress.Text = MemberLogin.Address;
                txtEmail.Text = MemberLogin.Email;
                txtPhone.Text = MemberLogin.PhoneNumber;
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

                MemberLogin.LastName = StringHelper.CapitalizeEachWord(txtLastName.Text);
                MemberLogin.FirstName = StringHelper.CapitalizeEachWord(txtFirstName.Text);
                MemberLogin.Sex = cmbSex.SelectedValue.ToString();
                MemberLogin.Birthday = dtpkBirthday.SelectedDate;
                MemberLogin.SSN = txtSSN.Text;
                MemberLogin.Address = txtAddress.Text;
                MemberLogin.Email = txtEmail.Text;
                MemberLogin.PhoneNumber = txtPhone.Text;

                MemberDAL.Instance.Update(MemberLogin);
                var mySnackbar = p.FindName("mySnackbar") as Snackbar;
                mySnackbar.MessageQueue.Enqueue("Cập nhật thông tin thành viên thành công");
                OnPropertyChanged();
            });

            UpdatePasswordCommand = new RelayCommand<UserControl>((p) => { return p != null; }, (p) =>
            {
            });

            CancelPasswordCommand = new RelayCommand<UserControl>((p) => { return p != null; }, (p) =>
            {
            });
        }

        MemberDTO memberLogin;
    }
}
