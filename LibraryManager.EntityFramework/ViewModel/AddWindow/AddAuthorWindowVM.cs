using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.Utility;
using LibraryManager.Utility.Interfaces;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace LibraryManager.EntityFramework.ViewModel.AddWindow
{
    public class AddAuthorWindowVM : BaseViewModel, IAddNewObject<AuthorDTO>
    {
        public AuthorDTO Result { get; set; }

        public ICommand OKCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public AddAuthorWindowVM()
        {
            OKCommand = new RelayCommand<Window>((p) => { return !(p == null); }, (p) =>
            {
                var txtNickName = p.FindName("txtNickName") as TextBox;
                var tblNickNameWarning = p.FindName("tblNickNameWarning") as TextBlock;

                if (txtNickName.Text == "")
                {
                    tblNickNameWarning.Visibility = Visibility.Visible;
                    txtNickName.Focus();
                    return;
                }
                else { tblNickNameWarning.Visibility = Visibility.Hidden; }

                var exit = AuthorDAL.Instance.GetList().ToList().FindAll(a => a.NickName == StringHelper.CapitalizeEachWord(txtNickName.Text)).Count();

                if (exit > 0)
                {
                    tblNickNameWarning.Text = "Tác giả đã tồn tại";
                    tblNickNameWarning.Visibility = Visibility.Visible;
                    txtNickName.Focus();
                    return;
                }

                AuthorDTO newAuthor = new AuthorDTO()
                {
                    NickName = StringHelper.CapitalizeEachWord(txtNickName.Text),
                    Status = true
                };

                AuthorDAL.Instance.Add(newAuthor);
                Result = newAuthor;
                p.Close();
            });

            CancelCommand = new RelayCommand<Window>((p) => { return !(p == null); }, (p) => { p.Close(); });
        }
    }
}
