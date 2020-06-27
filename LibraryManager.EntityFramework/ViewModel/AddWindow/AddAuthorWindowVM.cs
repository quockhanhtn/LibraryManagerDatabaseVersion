using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace LibraryManager.EntityFramework.ViewModel.AddWindow
{
    public class AddAuthorWindowVM : BaseViewModel
    {
        /// <summary>
        /// if (Result == "") -> Not add new librarian
        /// else Result = new Author().FullName
        /// </summary>
        public string Result
        {
            get
            {
                var resultCopy = result;
                result = "";
                return resultCopy;
            }
            set => result = value;
        }

        public ICommand OKCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public AddAuthorWindowVM()
        {
            OKCommand = new RelayCommand<Window>((p) => { return !(p == null); }, (p) =>
            {
                var tbxNickName = p.FindName("tbxNickName") as TextBox;
                var tblNickNameWarning = p.FindName("tblNickNameWarning") as TextBlock;

                if (tbxNickName.Text == "")
                {
                    tblNickNameWarning.Visibility = Visibility.Visible;
                    tbxNickName.Focus();
                    return;
                }
                else { tblNickNameWarning.Visibility = Visibility.Hidden; }

                var exit = AuthorDAL.Instance.GetList().ToList().FindAll(a => a.NickName == StringHelper.CapitalizeEachWord(tbxNickName.Text)).Count();

                if (exit > 0)
                {
                    tblNickNameWarning.Text = "Tác giả đã tồn tại";
                    tblNickNameWarning.Visibility = Visibility.Visible;
                    tbxNickName.Focus();
                    return;
                }

                AuthorDTO newAuthor = new AuthorDTO()
                {
                    NickName = StringHelper.CapitalizeEachWord(tbxNickName.Text),
                    Status = true
                };

                AuthorDAL.Instance.Add(newAuthor);
                Result = newAuthor.NickName;
                p.Close();
            });

            CancelCommand = new RelayCommand<Window>((p) => { return !(p == null); }, (p) => { p.Close(); });
        }

        private string result = "";
    }
}
