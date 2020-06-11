using MaterialDesignThemes.Wpf;
using System;
using System.Windows.Forms;

namespace LibraryManager.MyUserControl
{
    public class MySnackbar : Snackbar
    {
        public bool ShowAlways { get; set; }
        /// <summary>
        /// Time to show snackbar on screen if ShowAlways = false
        /// </summary>
        public int ShowTime { get; set; }

        public MySnackbar() : base()
        {
            this.Loaded += MySnackbar_Loaded;
        }

        private void MySnackbar_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ShowAlways == false)
            {
                Timer timer = new Timer();
                timer.Interval = 1000;
                timer.Start();
                timer.Tick += Timer_Tick;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            counter += 1;
            if (counter == ShowTime)
            {
                this.IsActive = false;
            }
        }

        int counter = -1;
    }
}
