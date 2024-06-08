﻿#pragma checksum "..\..\..\EditLockWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9EB9637A585A9EA2229B9493C42831118F03A869"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Converters;
using Wpf.Ui.Markup;


namespace Vault {
    
    
    /// <summary>
    /// EditLockWindow
    /// </summary>
    public partial class EditLockWindow : Wpf.Ui.Controls.FluentWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\EditLockWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.Ui.Controls.PasswordBox PasswordBox;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\EditLockWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.Ui.Controls.PasswordBox ConfirmPasswordBox;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\EditLockWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.Ui.Controls.PasswordBox OldPasswordBox;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\EditLockWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.Ui.Controls.PasswordBox NewPasswordBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Vault;component/editlockwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\EditLockWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.PasswordBox = ((Wpf.Ui.Controls.PasswordBox)(target));
            return;
            case 2:
            this.ConfirmPasswordBox = ((Wpf.Ui.Controls.PasswordBox)(target));
            
            #line 17 "..\..\..\EditLockWindow.xaml"
            this.ConfirmPasswordBox.KeyDown += new System.Windows.Input.KeyEventHandler(this.OnConfirmPasswordBoxKeyDown);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 18 "..\..\..\EditLockWindow.xaml"
            ((Wpf.Ui.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnRemoveButtonClicked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.OldPasswordBox = ((Wpf.Ui.Controls.PasswordBox)(target));
            return;
            case 5:
            this.NewPasswordBox = ((Wpf.Ui.Controls.PasswordBox)(target));
            
            #line 22 "..\..\..\EditLockWindow.xaml"
            this.NewPasswordBox.KeyDown += new System.Windows.Input.KeyEventHandler(this.OnNewPasswordBoxKeyDown);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 23 "..\..\..\EditLockWindow.xaml"
            ((Wpf.Ui.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnChangeButtonClicked);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
