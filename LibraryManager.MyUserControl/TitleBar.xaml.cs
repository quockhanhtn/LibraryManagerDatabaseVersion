﻿using LibraryManager.Utility;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryManager.MyUserControl
{
    /// <summary>
    /// Interaction logic for TitleBar.xaml
    /// </summary>
    public partial class TitleBar : UserControl
    {
        public TitleBar()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.TitleBarViewModel();
        }

        public bool WindowMaximizeButton 
        { 
            get => btnWindowMaximize.IsEnabled;
            set => btnWindowMaximize.IsEnabled = value;
        }
    }
}
