﻿#pragma checksum "..\..\ChangePasswordWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "84D1BBE360B7A4FB338AB74B178F41B327E6E36C0EC90A9193FB032D5FE7F09A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using LibraryManager.EntityFramework;
using LibraryManager.MyUserControl;
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


namespace LibraryManager.EntityFramework {
    
    
    /// <summary>
    /// ChangePasswordWindow
    /// </summary>
    public partial class ChangePasswordWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\ChangePasswordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LibraryManager.EntityFramework.ChangePasswordWindow changePasswordWindow;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\ChangePasswordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox pwxPassword;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\ChangePasswordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tblPasswordWarning;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\ChangePasswordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox pwxPasswordNew;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\ChangePasswordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tblPasswordNewWarning;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\ChangePasswordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox pwxRetypePasswordNew;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\ChangePasswordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tblRetypePasswordNewWarning;
        
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
            System.Uri resourceLocater = new System.Uri("/LibraryManager.EntityFramework;component/changepasswordwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ChangePasswordWindow.xaml"
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
            this.changePasswordWindow = ((LibraryManager.EntityFramework.ChangePasswordWindow)(target));
            return;
            case 2:
            this.pwxPassword = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 3:
            this.tblPasswordWarning = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.pwxPasswordNew = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 5:
            this.tblPasswordNewWarning = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.pwxRetypePasswordNew = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 7:
            this.tblRetypePasswordNewWarning = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

