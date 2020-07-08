using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.MyUserControl;
using LibraryManager.Utility;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManager.EntityFramework.ViewModel
{
    public class FindMemberWindowViewModel : BaseViewModel
    {
        public BookAction MemberAction { get; set; } = BookAction.Borrow;
        public Visibility WarningVisibility { get; set; } = Visibility.Collapsed;
        public ObservableCollection<MemberDTO> ListMember { get => listMember; set { listMember = value; OnPropertyChanged(); } }
        public MemberDTO MemberSelected { get => memberSelected; set { memberSelected = value; OnPropertyChanged(); } }

        public ICommand LoadedCommand { get; set; }
        public ICommand ChangeMemberSelectedCommand { get; set; }
        public ICommand OKCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        

        public FindMemberWindowViewModel()
        {
            ListMember = MemberDAL.Instance.GetList();

            LoadedCommand = new RelayCommand<Window>((p) => { return p != null; }, (p) =>
            {
                var titleBar = p.FindName("titleBar") as TitleBar;
                switch (MemberAction)
                {
                    case BookAction.Borrow:
                        titleBar.Tag = "Thông tin người mượn";
                        break;
                    case BookAction.Return:
                        titleBar.Tag = "Thông tin người trả";
                        break;
                }
            });

            ChangeMemberSelectedCommand = new RelayCommand<Window>((p) => { return (p != null && MemberSelected != null); }, (p) =>
            {
                var tblWarning = p.FindName("tblWarning") as TextBlock;
                if (MemberAction == BookAction.Borrow && MemberSelected.Status != true)
                {
                    tblWarning.Visibility = Visibility.Visible;
                    return;
                }
                else { tblWarning.Visibility = Visibility.Collapsed; }
            });

            OKCommand = new RelayCommand<Window>((p) => {
                return (MemberSelected == null) ? false : (MemberAction == BookAction.Return || MemberSelected.Status == true) ? true : false;
            }, (p) => { p.Close(); });

            CancelCommand=new RelayCommand<Window>((p) => { return p != null; }, (p) => { MemberSelected = null; p.Close(); });
        }

        ObservableCollection<MemberDTO> listMember;
        MemberDTO memberSelected;

        public enum BookAction { Borrow, Return };
    }
}
