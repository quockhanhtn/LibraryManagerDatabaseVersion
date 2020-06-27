using LibraryManager.EntityFramework.Model;
using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.View.PageUC;
using LibraryManager.EntityFramework.ViewModel.PageUC;
using LibraryManager.Utility;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LibraryManager.EntityFramework.ViewModel
{
    public class LibrarianWindowViewModel : BaseViewModel
    {
        public ICommand LoadedWindow { get; set; }
        public ICommand MenuSelectionChangedCommand { get; set; }
        public Librarian LibrarianLogin { get; set; }
        public Grid GridMain { get; set; }
        public UserControl PageAccountInfor { get; set; }
        public UserControl PageMemberManager { get; set; }
        public UserControl PageBookManager { get; set; }
        public UserControl PagePublisherManager { get; set; }
        public UserControl PageBookCategoryManager { get; set; }
        public UserControl PageAuthorManager { get; set; }

        public LibrarianWindowViewModel(string idLibrarian)
        {
            LibrarianLogin = LibrarianDAL.Instance.GetLibrarian(idLibrarian);

            MenuSelectionChangedCommand = new RelayCommand<Window>((p) => { return (p != null); }, (p) =>
            {
                var gridCursor = p.FindName("gridCursor") as Grid;
                var listViewMenu = p.FindName("ListViewMenu") as ListView;
                var listViewSelectedItem = (listViewMenu).SelectedItem as ListViewItem;

                gridCursor.Margin = new Thickness(0, 60 * listViewMenu.SelectedIndex, 0, 0);

                GridMain.Children.Clear();
                switch (listViewSelectedItem.Name)
                {
                    case "AccountInfo":
                        GridMain.Children.Add(this.PageAccountInfor);
                        break;
                    case "MemberManager":
                        GridMain.Children.Add(this.PageMemberManager);
                        break;
                    case "BookManager":
                        GridMain.Children.Add(this.PageBookManager);
                        break;
                    case "PublisherManager":
                        GridMain.Children.Add(this.PagePublisherManager);
                        break;
                    case "BookCategoryManager":
                        GridMain.Children.Add(this.PageBookCategoryManager);
                        break;
                    case "AuthorManager":
                        GridMain.Children.Add(this.PageAuthorManager);
                        break;
                }
            });

            LoadedWindow = new RelayCommand<Window>((p) => { return (p != null); }, (p) =>
            {
                InitPage();
                GridMain = p.FindName("gridMain") as Grid;
                GridMain.Children.Add(this.PageBookManager);

                var icoAccount = p.FindName("icoAccount") as PackIcon;
                int firstChar = char.ToUpper(LibrarianLogin.FirstName[0]);
                icoAccount.Kind = (PackIconKind)(158 + 5 * (firstChar - (int)'A' + 2));

            });
        }

        void InitPage()
        {
            this.PageAccountInfor = new PageAccountInfor();// { DataContext = new PageLibrarianManagerVM() };
            this.PageMemberManager = new PageMemberManager() { DataContext = new PageMemberManagerVM() };
            this.PageBookManager = new PageBookManager() { DataContext = new PageBookManagerVM() };
            this.PagePublisherManager = new PagePublisherManager() { DataContext = new PagePublisherManagerVM() };
            this.PageBookCategoryManager = new PageBookCategoryManager() { DataContext = new PageBookCategoryManagerVM() };
            this.PageAuthorManager = new PageAuthorManager() { DataContext = new PageAuthorManagerVM() };
        }
    }
}
