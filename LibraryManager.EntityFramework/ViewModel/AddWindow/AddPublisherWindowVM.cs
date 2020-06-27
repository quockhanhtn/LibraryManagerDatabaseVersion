using LibraryManager.EntityFramework.Model;
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
    public class AddPublisherWindowVM : BaseViewModel
    {
        /// <summary>
        /// if (Result == "") -> Not add new librarian
        /// else Result = new Publisher().FullName
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
        public ICommand RetypeCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public AddPublisherWindowVM()
        {
            OKCommand = new RelayCommand<Window>((p) => { return !(p == null); }, (p) =>
            {
                var tbxName = p.FindName("tbxName") as TextBox; 
                var tbxPhone = p.FindName("tbxPhone") as TextBox;
                var tbxAddress = p.FindName("tbxAddress") as TextBox;
                var tbxEmail = p.FindName("tbxEmail") as TextBox;
                var tbxWebsite = p.FindName("tbxWebsite") as TextBox;

                var tblNameWarning = p.FindName("tblNameWarning") as TextBlock;
                var tblPhoneWarning = p.FindName("tblPhoneWarning") as TextBlock;

                if (tbxName.Text == "")
                {
                    tblNameWarning.Visibility = Visibility.Visible;
                    tbxName.Focus();
                    return;
                }
                else { tblNameWarning.Visibility = Visibility.Hidden; }

                if (tbxPhone.Text == "")
                {
                    tblPhoneWarning.Visibility = Visibility.Visible;
                    tbxPhone.Focus();
                    return;
                }
                else { tblPhoneWarning.Visibility = Visibility.Hidden; }

                var newPublisher = new PublisherDTO()
                {
                    Name = tbxName.Text,
                    PhoneNumber = tbxPhone.Text,
                    Address = tbxAddress.Text,
                    Email = tbxEmail.Text,
                    Website = tbxWebsite.Text,
                    Status = true
                };

                PublisherDAL.Instance.Add(newPublisher);
                Result = newPublisher.Name;
                p.Close();
            });

            RetypeCommand = new RelayCommand<Window>((p) => { return !(p == null); }, (p) =>
            {
                var tbxLastName = p.FindName("tbxLastName") as TextBox;
                var tbxFirstName = p.FindName("tbxFirstName") as TextBox;
                var tbxSSN = p.FindName("tbxSSN") as TextBox;
                var tbxAddress = p.FindName("tbxAddress") as TextBox;
                var tbxEmail = p.FindName("tbxEmail") as TextBox;
                var tbxPhone = p.FindName("tbxPhone") as TextBox;
                var tbxSalary = p.FindName("tbxSalary") as TextBox;

                tbxLastName.Text = "";
                tbxFirstName.Text = "";
                tbxSSN.Text = "";
                tbxAddress.Text = "";
                tbxEmail.Text = "";
                tbxPhone.Text = "";
                tbxSalary.Text = "";
            });

            CancelCommand = new RelayCommand<Window>((p) => { return !(p == null); }, (p) => { p.Close(); });
        }

        private string result = "";
    }
}
