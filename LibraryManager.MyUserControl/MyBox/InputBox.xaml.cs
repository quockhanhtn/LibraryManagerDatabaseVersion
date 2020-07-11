using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManager.MyUserControl.MyBox
{
    /// <summary>
    /// Interaction logic for InputBox.xaml
    /// </summary>
    public partial class InputBox : Window
    {
        /// <summary>
        /// Result != "" , button 1 được nhấn
        /// </summary>
        public string Result { get; set; }
        public InputBox(string inputTextExam, string caption, string btn1_Text, string btn2_Text)
        {
            InitializeComponent();
            Result = "";
            txtInput.Text = inputTextExam;
            tblTitle.Text = caption.ToUpper();
            tblButton1.Text = btn1_Text;
            tblButton2.Text = btn2_Text;

            txtInput.Focus();
            txtInput.CaretIndex = txtInput.Text.Length;

            if (btn2_Text == "")
            {
                Grid.SetColumnSpan(btn1, 2);
                this.btn2.Visibility = Visibility.Hidden;
            }
        }

        public static string Show(string inputTextExam, string caption, string btn1_Text, string btn2_Text)
        {
            InputBox kInputBox = new InputBox(inputTextExam, caption, btn1_Text, btn2_Text);
            kInputBox.ShowDialog();
            return kInputBox.Result;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            Result = txtInput.Text.Trim();
            this.Close();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            Result = "";
            this.Close();
        }
    }
}
