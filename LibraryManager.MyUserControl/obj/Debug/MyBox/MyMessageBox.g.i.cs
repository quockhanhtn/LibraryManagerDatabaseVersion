﻿#pragma checksum "..\..\..\MyBox\MyMessageBox.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "891620505A2A77CC1B8A549CC50004754CC5E4382E54453A929695A8EC90F6EF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using LibraryManager.MyUserControl.MyBox;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace LibraryManager.MyUserControl.MyBox {
    
    
    /// <summary>
    /// MyMessageBox
    /// </summary>
    public partial class MyMessageBox : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\MyBox\MyMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LibraryManager.MyUserControl.MyBox.MyMessageBox myMessageBox;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\MyBox\MyMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tblTitle;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\MyBox\MyMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridContent;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\MyBox\MyMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.PackIcon icoBox;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\MyBox\MyMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tblContent;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\..\MyBox\MyMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn1;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\MyBox\MyMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tblButton1;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\..\MyBox\MyMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn2;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\MyBox\MyMessageBox.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tblButton2;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/LibraryManager.MyUserControl;component/mybox/mymessagebox.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MyBox\MyMessageBox.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.myMessageBox = ((LibraryManager.MyUserControl.MyBox.MyMessageBox)(target));
            return;
            case 2:
            
            #line 44 "..\..\..\MyBox\MyMessageBox.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Grid_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tblTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            
            #line 46 "..\..\..\MyBox\MyMessageBox.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn2_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.gridContent = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.icoBox = ((MaterialDesignThemes.Wpf.PackIcon)(target));
            return;
            case 7:
            this.tblContent = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.btn1 = ((System.Windows.Controls.Button)(target));
            
            #line 109 "..\..\..\MyBox\MyMessageBox.xaml"
            this.btn1.Click += new System.Windows.RoutedEventHandler(this.btn1_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.tblButton1 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.btn2 = ((System.Windows.Controls.Button)(target));
            
            #line 115 "..\..\..\MyBox\MyMessageBox.xaml"
            this.btn2.Click += new System.Windows.RoutedEventHandler(this.btn2_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.tblButton2 = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
