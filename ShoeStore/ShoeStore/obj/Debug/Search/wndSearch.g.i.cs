﻿#pragma checksum "..\..\..\Search\wndSearch.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6D7072C83B7F36C146B8AFC5FBCDA0F1DE70551B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ShoeStore.Search;
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


namespace ShoeStore.Search {
    
    
    /// <summary>
    /// wndSearch
    /// </summary>
    public partial class wndSearch : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\Search\wndSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgInvoices;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Search\wndSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbbxInvNum;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Search\wndSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbbxInvDate;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Search\wndSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbbxInvTot;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Search\wndSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClear;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Search\wndSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSelect;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Search\wndSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
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
            System.Uri resourceLocater = new System.Uri("/ShoeStore;component/search/wndsearch.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Search\wndSearch.xaml"
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
            
            #line 8 "..\..\..\Search\wndSearch.xaml"
            ((ShoeStore.Search.wndSearch)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.dgInvoices = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 3:
            this.cmbbxInvNum = ((System.Windows.Controls.ComboBox)(target));
            
            #line 15 "..\..\..\Search\wndSearch.xaml"
            this.cmbbxInvNum.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CmbbxInvNum_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cmbbxInvDate = ((System.Windows.Controls.ComboBox)(target));
            
            #line 17 "..\..\..\Search\wndSearch.xaml"
            this.cmbbxInvDate.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CmbbxInvNum_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.cmbbxInvTot = ((System.Windows.Controls.ComboBox)(target));
            
            #line 19 "..\..\..\Search\wndSearch.xaml"
            this.cmbbxInvTot.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CmbbxInvNum_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnClear = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\Search\wndSearch.xaml"
            this.btnClear.Click += new System.Windows.RoutedEventHandler(this.BtnClear_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnSelect = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\Search\wndSearch.xaml"
            this.btnSelect.Click += new System.Windows.RoutedEventHandler(this.BtnSelect_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\Search\wndSearch.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.BtnCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

