using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.Utility;
using LibraryManager.Utility.Interfaces;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManager.EntityFramework.ViewModel.AddWindow
{
    public class AddBookCategoryWindowVM : BaseViewModel, IAddNewObject<BookCategoryDTO>
    {
        public BookCategoryDTO Result { get; set; }

        public ICommand OKCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public AddBookCategoryWindowVM()
        {
            OKCommand = new RelayCommand<Window>((p) => { return !(p == null); }, (p) =>
            {
                var tbxName = p.FindName("tbxName") as TextBox;
                var tbxLimitDays = p.FindName("tbxLimitDays") as TextBox;
                var tblNameWarning = p.FindName("tblNameWarning") as TextBlock;
                var tblLimitDaysWarning = p.FindName("tblLimitDaysWarning") as TextBlock;

                if (tbxName.Text == "")
                {
                    tblNameWarning.Visibility = Visibility.Visible;
                    tbxName.Focus();
                    return;
                }
                else { tblNameWarning.Visibility = Visibility.Hidden; }

                if (StringHelper.ToInt(tbxLimitDays.Text) == 0)
                {
                    tblLimitDaysWarning.Visibility = Visibility.Visible;
                    tbxLimitDays.Focus();
                    return;
                }
                else { tblLimitDaysWarning.Visibility = Visibility.Hidden; }

                BookCategoryDTO newBookCategory = new BookCategoryDTO()
                {
                    Name = tbxName.Text,
                    LimitDays = StringHelper.ToInt(tbxLimitDays.Text),
                    Status = true
                };

                BookCategoryDAL.Instance.Add(newBookCategory);
                Result = newBookCategory;
                p.Close();
            });

            CancelCommand = new RelayCommand<Window>((p) => { return !(p == null); }, (p) => { p.Close(); });
        }
    }
}
