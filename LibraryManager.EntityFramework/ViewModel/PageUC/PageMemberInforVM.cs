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
        public ICommand UpdateCommand { get; set; }
        public ICommand CancelUpdateCommand { get; set; }

        public PageMemberInforVM(MemberDTO member)
        {
            MemberLogin = member;

            CancelUpdateCommand = new RelayCommand<UserControl>((p) => { return p != null; }, (p) =>
            {
                var tbxLastName = p.FindName("tbxLastName") as TextBox;
                var tbxFirstName = p.FindName("tbxFirstName") as TextBox;
                var cmbSex = p.FindName("cmbSex") as ComboBox;
                var dtpkBirthday = p.FindName("dtpkBirthday") as DatePicker;
                var tbxSSN = p.FindName("tbxSSN") as TextBox;
                var tbxAddress = p.FindName("tbxAddress") as TextBox;
                var tbxEmail = p.FindName("tbxEmail") as TextBox;
                var tbxPhone = p.FindName("tbxPhone") as TextBox;

                tbxLastName.Text = MemberLogin.LastName;
                tbxFirstName.Text = MemberLogin.FirstName;
                cmbSex.SelectedValue = MemberLogin.Sex;
                dtpkBirthday.SelectedDate = MemberLogin.Birthday;
                tbxSSN.Text = MemberLogin.SSN;
                tbxAddress.Text = MemberLogin.Address;
                tbxEmail.Text = MemberLogin.Email;
                tbxPhone.Text = MemberLogin.PhoneNumber;
            });

            UpdateCommand = new RelayCommand<UserControl>((p) => { return p != null; }, (p) =>
            {
                var tbxLastName = p.FindName("tbxLastName") as TextBox;
                var tbxFirstName = p.FindName("tbxFirstName") as TextBox;
                var cmbSex = p.FindName("cmbSex") as ComboBox;
                var dtpkBirthday = p.FindName("dtpkBirthday") as DatePicker;
                var tbxSSN = p.FindName("tbxSSN") as TextBox;
                var tbxAddress = p.FindName("tbxAddress") as TextBox;
                var tbxEmail = p.FindName("tbxEmail") as TextBox;
                var tbxPhone = p.FindName("tbxPhone") as TextBox;

                var tblLastNameWarning = p.FindName("tblLastNameWarning") as TextBlock;
                var tblFirstNameWarning = p.FindName("tblFirstNameWarning") as TextBlock;
                var tblSexWarning = p.FindName("tblSexWarning") as TextBlock;
                var tblBirthdayWarning = p.FindName("tblBirthdayWarning") as TextBlock;
                var tblSSNWarning = p.FindName("tblSSNWarning") as TextBlock;
                var tblAddressWarning = p.FindName("tblAddressWarning") as TextBlock;
                var tblEmailWarning = p.FindName("tblEmailWarning") as TextBlock;
                var tblPhoneWarning = p.FindName("tblPhoneWarning") as TextBlock;

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

                MemberLogin.LastName = StringHelper.CapitalizeEachWord(tbxLastName.Text);
                MemberLogin.FirstName = StringHelper.CapitalizeEachWord(tbxFirstName.Text);
                MemberLogin.Sex = cmbSex.SelectedValue.ToString();
                MemberLogin.Birthday = dtpkBirthday.SelectedDate;
                MemberLogin.SSN = tbxSSN.Text;
                MemberLogin.Address = tbxAddress.Text;
                MemberLogin.Email = tbxEmail.Text;
                MemberLogin.PhoneNumber = tbxPhone.Text;

                MemberDAL.Instance.Update(MemberLogin);
                var mySnackbar = p.FindName("mySnackbar") as Snackbar;
                mySnackbar.MessageQueue.Enqueue("Cập nhật thông tin thành viên thành công");
                OnPropertyChanged();
            });
        }

        MemberDTO memberLogin;
    }
}
