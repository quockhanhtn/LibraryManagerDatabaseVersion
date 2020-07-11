﻿using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.EntityFramework.View.PageUC;
using LibraryManager.EntityFramework.ViewModel.PageUC;
using LibraryManager.MyUserControl.MyBox;
using LibraryManager.Utility;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManager.EntityFramework.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ICommand LoadedWindow { get; set; }
        public ICommand MenuSelectionChangedCommand { get; set; }
        public Grid GridMain { get; set; }
        public UserControl PageLibrarianManager { get; set; }
        public UserControl PageMemberManager { get; set; }
        public UserControl PageBookManager { get; set; }
        public UserControl PagePublisherManager { get; set; }
        public UserControl PageBookCategoryManager { get; set; }
        public UserControl PageAuthorManager { get; set; }
        public UserControl PageStatistic { get; set; }
        public UserControl PageAboutSoftware { get; set; }

        public MainWindowViewModel()
        {
            MenuSelectionChangedCommand = new RelayCommand<Window>((p) => { return (p != null); }, (p) =>
            {
                var gridCursor = p.FindName("gridCursor") as Grid;
                var listViewMenu = p.FindName("ListViewMenu") as ListView;
                var listViewSelectedItem = (listViewMenu).SelectedItem as ListViewItem;

                gridCursor.Margin = new Thickness(0, 60 * listViewMenu.SelectedIndex, 0, 0);

                GridMain.Children.Clear();
                switch (listViewSelectedItem.Name)
                {
                    case "LibrarianManager":
                        GridMain.Children.Add(this.PageLibrarianManager);
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
                    case "Statistic":
                        GridMain.Children.Add(this.PageStatistic);
                        break;
                    case "ChangePassword":
                        var dataContext = new ChangePasswordWindowViewModel("admin");
                        var changePasswordWindow = new ChangePasswordWindow() { DataContext = dataContext };
                        changePasswordWindow.ShowDialog();
                        break;
                    case "AboutSoftware":
                        GridMain.Children.Add(this.PageAboutSoftware);
                        break;
                    case "Logout":
                        var messageboxResult = MyMessageBox.Show("Bạn có muốn đăng xuất khỏi phần mềm ?", "Cảnh báo", "Không", "Có", MessageBoxImage.Warning);
                        if (messageboxResult == true)
                        {
                            listViewMenu.SelectedIndex = 0;
                            MenuSelectionChangedCommand.Execute(p);
                        }
                        else
                        {
                            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                            Application.Current.Shutdown();
                        }
                        break;
                }
            });

            LoadedWindow = new RelayCommand<Window>((p) => { return (p != null); }, (p) =>
            {
                InitPage();
                GridMain = p.FindName("gridMain") as Grid;
                GridMain.Children.Add(this.PageLibrarianManager);
            });
        }

        void InitPage()
        {
            this.PageLibrarianManager = new PageLibrarianManager();// { DataContext = new PageLibrarianManagerVM() };
            this.PageMemberManager = new PageMemberManager() { DataContext = new PageMemberManagerVM() };
            this.PageBookManager = new PageBookManager() { DataContext = new PageBookManagerVM(new LibrarianDTO(LibrarianDAL.Instance.GetLibrarian("LIB000"))) };
            this.PagePublisherManager = new PagePublisherManager() { DataContext = new PagePublisherManagerVM() };
            this.PageBookCategoryManager = new PageBookCategoryManager() { DataContext = new PageBookCategoryManagerVM() };
            this.PageAuthorManager = new PageAuthorManager() { DataContext = new PageAuthorManagerVM() };
            this.PageStatistic = new PageStatistic() { DataContext = new PageStatisticVM() };
            this.PageAboutSoftware = new PageAboutSoftware();
        }
    }
}
