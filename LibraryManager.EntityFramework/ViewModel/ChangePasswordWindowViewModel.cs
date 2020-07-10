using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.MyUserControl.MyBox;
using LibraryManager.Utility;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManager.EntityFramework.ViewModel
{
    public class ChangePasswordWindowViewModel : BaseViewModel
    {
        /// <summary>
        /// Result = True => change password thành công,
        /// Result != True => change password thất bại
        /// </summary>
        public bool Result { get; set; } = false;
        public ICommand OKCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ChangePasswordWindowViewModel(string personId)
        {
            OKCommand = new RelayCommand<Window>((p) => { return p != null; }, (p) =>
            {
                var pwxPassword = p.FindName("pwxPassword") as PasswordBox;
                var pwxPasswordNew = p.FindName("pwxPasswordNew") as PasswordBox;
                var pwxRetypePasswordNew = p.FindName("pwxRetypePasswordNew") as PasswordBox;

                var tblPasswordWarning = p.FindName("tblPasswordWarning") as TextBlock;
                var tblPasswordNewWarning = p.FindName("tblPasswordNewWarning") as TextBlock;
                var tblRetypePasswordNewWarning = p.FindName("tblRetypePasswordNewWarning") as TextBlock;

                if (AccountDAL.Instance.CheckPassword(personId, pwxPassword.Password) != true)
                {
                    tblPasswordWarning.Visibility = Visibility.Visible;
                    pwxPassword.Focus();
                    return;
                }
                else { tblPasswordWarning.Visibility = Visibility.Hidden; }

                if (pwxPasswordNew.Password.Length < 6)
                {
                    tblPasswordNewWarning.Visibility = Visibility.Visible;
                    pwxPasswordNew.Focus();
                    return;
                }
                else { tblPasswordNewWarning.Visibility = Visibility.Hidden; }

                if (pwxRetypePasswordNew.Password != pwxPasswordNew.Password)
                {
                    tblRetypePasswordNewWarning.Visibility = Visibility.Visible;
                    pwxRetypePasswordNew.Focus();
                    return;
                }
                else { tblRetypePasswordNewWarning.Visibility = Visibility.Hidden; }

                AccountDAL.Instance.ChangePassword(personId, pwxPasswordNew.Password);

                p.Hide();
                MyMessageBox.Show("Đổi mật khẩu thành công !", "Thông báo", "OK", "", MessageBoxImage.Information);
                p.Close();
            });

            CancelCommand = new RelayCommand<Window>((p) => { return p != null; }, (p) => { p.Close(); });
        }
    }
}
