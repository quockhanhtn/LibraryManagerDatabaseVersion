using LibraryManager.EntityFramework.Model;
using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.EntityFramework.View.AddWindow;
using LibraryManager.Utility;
using LibraryManager.Utility.Enums;
using LibraryManager.Utility.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManager.EntityFramework.ViewModel.AddWindow
{
    public class AddBookWindowVM : BaseViewModel, IAddNewObject<BookDTO>
    {
        public ObservableCollection<BookCategoryDTO> ListBookCategory { get => listBookCategory; set { listBookCategory = value; OnPropertyChanged(); } }
        public ObservableCollection<PublisherDTO> ListPublisher { get => listPublisher; set { listPublisher = value; OnPropertyChanged(); } }
        public ObservableCollection<Author> ListAuthor { get => listAuthor; set { listAuthor = value; OnPropertyChanged(); } }
        public BookCategoryDTO BookCategorySelected { get => bookCategorySelected; set { bookCategorySelected = value; OnPropertyChanged(); } }
        public PublisherDTO PublisherSelected { get => publisherSelected; set { publisherSelected = value; OnPropertyChanged(); } }
        public Author AuthorSelected1 { get => authorSelected1; set { authorSelected1 = value; OnPropertyChanged(); } }
        public Author AuthorSelected2 { get => authorSelected2; set { authorSelected2 = value; OnPropertyChanged(); } }
        public Author AuthorSelected3 { get => authorSelected3; set { authorSelected3 = value; OnPropertyChanged(); } }

        public ICommand LoadedCommand { get; set; }
        public ICommand AddBookCategoryCommand { get; set; }
        public ICommand AddPublisherCommand { get; set; }
        public ICommand AddAuthorCommand { get; set; }
        public ICommand OKCommand { get; set; }
        public ICommand RetypeCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public BookDTO Result { get; set; }

        public AddBookWindowVM()
        {
            ListBookCategory = BookCategoryDAL.Instance.GetList(StatusFillter.Active);
            ListPublisher = PublisherDAL.Instance.GetList(StatusFillter.Active);
            ListAuthor = AuthorDAL.Instance.GetRawList();

            LoadedCommand = new RelayCommand<Window>((p) => { return p != null; }, (p) =>
             {
                 var tbxTitle = p.FindName("tbxTitle") as TextBox;
                 tbxTitle.Focus();
             });

            AddBookCategoryCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
             {
                 var addDataContext = new AddBookCategoryWindowVM();
                 var addBookCategoryWindow = new AddBookCategoryWindow() { DataContext = addDataContext };
                 addBookCategoryWindow.ShowDialog();

                 if (addDataContext.Result != null)
                 {
                     ListBookCategory = BookCategoryDAL.Instance.GetList(StatusFillter.Active);
                 }
             });
            
            AddPublisherCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
             {
                 var addDataContext = new AddPublisherWindowVM();
                 var addPublisherWindow = new AddPublisherWindow() { DataContext = addDataContext };
                 addPublisherWindow.ShowDialog();

                 if (addDataContext.Result != null)
                 {
                     ListPublisher = PublisherDAL.Instance.GetList(StatusFillter.Active);
                 }
             });

            AddAuthorCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
             {
                 var addDataContext = new AddAuthorWindowVM();
                 var addAuthorWindow = new AddAuthorWindow() { DataContext = addDataContext };
                 addAuthorWindow.ShowDialog();

                 if (addDataContext.Result != null)
                 {
                     ListAuthor = AuthorDAL.Instance.GetRawList();
                 }
             });
            
            OKCommand = new RelayCommand<Window>((p) => { return p != null; }, (p) =>
             {
                 var tbxTitle = p.FindName("tbxTitle") as TextBox;
                 var cmbYearPublish = p.FindName("cmbYearPublish") as ComboBox;
                 var tbxPageNumber = p.FindName("tbxPageNumber") as TextBox;
                 var tbxSize = p.FindName("tbxSize") as TextBox;
                 var tbxPrice = p.FindName("tbxPrice") as TextBox;
                 var tbxNumber = p.FindName("tbxNumber") as TextBox;

                 var tblTitleWarning = p.FindName("tblTitleWarning") as TextBlock;
                 var tblBookCategoryWarning = p.FindName("tblBookCategoryWarning") as TextBlock;
                 var tblPublisherWarning = p.FindName("tblPublisherWarning") as TextBlock;
                 var tblYearPublishWarning = p.FindName("tblYearPublishWarning") as TextBlock;
                 var tblAuthorWaning = p.FindName("tblAuthorWaning") as TextBlock;
                 var tblPageNumberWarning = p.FindName("tblPageNumberWarning") as TextBlock;
                 var tblPriceWarning = p.FindName("tblPriceWarning") as TextBlock;
                 var tblNumberWarning = p.FindName("tblNumberWarning") as TextBlock;

                 if (tbxTitle.Text == "")
                 {
                     tblTitleWarning.Visibility = Visibility.Visible;
                     tbxTitle.Focus();
                     return;
                 }
                 else { tblTitleWarning.Visibility = Visibility.Hidden; }

                 if (BookCategorySelected == null)
                 {
                     tblBookCategoryWarning.Visibility = Visibility.Visible;
                     return;
                 }
                 else { tblBookCategoryWarning.Visibility = Visibility.Hidden; }

                 if (PublisherSelected == null)
                 {
                     tblPublisherWarning.Visibility = Visibility.Visible;
                     return;
                 }
                 else { tblPublisherWarning.Visibility = Visibility.Hidden; }

                 if (cmbYearPublish.SelectedValue == null)
                 {
                     tblYearPublishWarning.Visibility = Visibility.Visible;
                     cmbYearPublish.Focus();
                     return;
                 }
                 else { tblYearPublishWarning.Visibility = Visibility.Hidden; }

                 if (AuthorSelected1 == null)
                 {
                     tblAuthorWaning.Visibility = Visibility.Visible;
                     return;
                 }
                 else { tblAuthorWaning.Visibility = Visibility.Hidden; }

                 if (StringHelper.ToInt(tbxPageNumber.Text) == 0)
                 {
                     tblPageNumberWarning.Visibility = Visibility.Visible;
                     tbxPageNumber.Focus();
                     return;
                 }
                 else { tblPageNumberWarning.Visibility = Visibility.Hidden; }

                 if (StringHelper.ToDecimal(tbxPrice.Text) == 0)
                 {
                     tblPriceWarning.Visibility = Visibility.Visible;
                     tbxPrice.Focus();
                     return;
                 }
                 else { tblPriceWarning.Visibility = Visibility.Hidden; }

                 if (StringHelper.ToInt(tbxNumber.Text) == 0)
                 {
                     tblNumberWarning.Visibility = Visibility.Visible;
                     tbxNumber.Focus();
                     return;
                 }
                 else { tblNumberWarning.Visibility = Visibility.Hidden; }

                 var listAuthor = new List<Author>();
                 listAuthor.Add(authorSelected1);
                 if (AuthorSelected2 != null) { listAuthor.Add(authorSelected2); }
                 if (AuthorSelected3 != null) { listAuthor.Add(authorSelected2); }

                 BookDTO newBook = new BookDTO()
                 {
                     Title = StringHelper.CapitalizeEachWord(tbxTitle.Text),
                     BookCategoryId = BookCategorySelected.Id,
                     PublisherId = PublisherSelected.Id,
                     YearPublish = StringHelper.ToInt(cmbYearPublish.SelectedValue.ToString()),
                     PageNumber = StringHelper.ToInt(tbxPageNumber.Text),
                     Size = tbxSize.Text,
                     Price = StringHelper.ToDecimal(tbxPrice.Text),
                     Authors = listAuthor
                 };

                 BookDAL.Instance.Add(newBook, StringHelper.ToInt(tbxNumber.Text));
                 Result = newBook;
                 p.Close();
             });
            
            RetypeCommand = new RelayCommand<Window>((p) => { return p != null; }, (p) =>
             {
             });
            
            CancelCommand = new RelayCommand<Window>((p) => { return p != null; }, (p) =>
             {
             });
        }


        ObservableCollection<BookCategoryDTO> listBookCategory;
        ObservableCollection<PublisherDTO> listPublisher;
        ObservableCollection<Author> listAuthor;
        BookCategoryDTO bookCategorySelected;
        PublisherDTO publisherSelected;
        Author authorSelected1;
        Author authorSelected2;
        Author authorSelected3;
    }
}
