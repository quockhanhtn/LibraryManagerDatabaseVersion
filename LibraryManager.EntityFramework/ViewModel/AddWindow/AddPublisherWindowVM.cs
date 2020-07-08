using LibraryManager.EntityFramework.Model.DataAccessLayer;
using LibraryManager.EntityFramework.Model.DataTransferObject;
using LibraryManager.Utility;
using LibraryManager.Utility.Interfaces;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManager.EntityFramework.ViewModel.AddWindow
{
    public class AddPublisherWindowVM : BaseViewModel, IAddNewObject<PublisherDTO>
    {
        public PublisherDTO Result { get; set; }
        public ICommand OKCommand { get; set; }
        public ICommand RetypeCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public AddPublisherWindowVM()
        {
            OKCommand = new RelayCommand<Window>((p) => { return !(p == null); }, (p) =>
            {
                var txtName = p.FindName("txtName") as TextBox; 
                var txtPhone = p.FindName("txtPhone") as TextBox;
                var txtAddress = p.FindName("txtAddress") as TextBox;
                var txtEmail = p.FindName("txtEmail") as TextBox;
                var txtWebsite = p.FindName("txtWebsite") as TextBox;

                var tblNameWarning = p.FindName("tblNameWarning") as TextBlock;
                var tblPhoneWarning = p.FindName("tblPhoneWarning") as TextBlock;

                if (txtName.Text == "")
                {
                    tblNameWarning.Visibility = Visibility.Visible;
                    txtName.Focus();
                    return;
                }
                else { tblNameWarning.Visibility = Visibility.Hidden; }

                if (txtPhone.Text == "")
                {
                    tblPhoneWarning.Visibility = Visibility.Visible;
                    txtPhone.Focus();
                    return;
                }
                else { tblPhoneWarning.Visibility = Visibility.Hidden; }

                var newPublisher = new PublisherDTO()
                {
                    Name = txtName.Text,
                    PhoneNumber = txtPhone.Text,
                    Address = txtAddress.Text,
                    Email = txtEmail.Text,
                    Website = txtWebsite.Text,
                    Status = true
                };

                PublisherDAL.Instance.Add(newPublisher);
                Result = newPublisher;
                p.Close();
            });

            RetypeCommand = new RelayCommand<Window>((p) => { return !(p == null); }, (p) =>
            {
                var txtLastName = p.FindName("txtLastName") as TextBox;
                var txtFirstName = p.FindName("txtFirstName") as TextBox;
                var txtSSN = p.FindName("txtSSN") as TextBox;
                var txtAddress = p.FindName("txtAddress") as TextBox;
                var txtEmail = p.FindName("txtEmail") as TextBox;
                var txtPhone = p.FindName("txtPhone") as TextBox;
                var txtSalary = p.FindName("txtSalary") as TextBox;

                txtLastName.Text = "";
                txtFirstName.Text = "";
                txtSSN.Text = "";
                txtAddress.Text = "";
                txtEmail.Text = "";
                txtPhone.Text = "";
                txtSalary.Text = "";
            });

            CancelCommand = new RelayCommand<Window>((p) => { return !(p == null); }, (p) => { p.Close(); });
        }
    }
}
