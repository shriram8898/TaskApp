﻿#pragma checksum "E:\Visual Studio Projects\Task App\taskList.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9290E81C7CDCD15A9ABB605D5620B71A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Task_App
{
    partial class taskList : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // taskList.xaml line 11
                {
                    this.cvs = (global::Windows.UI.Xaml.Data.CollectionViewSource)(target);
                }
                break;
            case 3: // taskList.xaml line 16
                {
                    this.listView = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 4: // taskList.xaml line 58
                {
                    this.contentDialog1 = (global::Windows.UI.Xaml.Controls.ContentDialog)(target);
                }
                break;
            case 6: // taskList.xaml line 81
                {
                    this.add = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.add).Click += this.add_Click;
                }
                break;
            case 7: // taskList.xaml line 83
                {
                    this.cancel = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.cancel).Click += this.cancel_Click;
                }
                break;
            case 8: // taskList.xaml line 85
                {
                    this.taskName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 9: // taskList.xaml line 86
                {
                    this.taskDetails = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 10: // taskList.xaml line 88
                {
                    this.priority = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 11: // taskList.xaml line 90
                {
                    this.Assignto = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 12: // taskList.xaml line 92
                {
                    this.collective = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 13: // taskList.xaml line 31
                {
                    this.stark1 = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 14: // taskList.xaml line 39
                {
                    this.star2 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 15: // taskList.xaml line 40
                {
                    this.tasks = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)this.tasks).ItemClick += this.tasks_ItemClick;
                }
                break;
            case 19: // taskList.xaml line 32
                {
                    this.two = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 20: // taskList.xaml line 33
                {
                    this.three = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 21: // taskList.xaml line 34
                {
                    this.four = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 22: // taskList.xaml line 35
                {
                    this.five = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 23: // taskList.xaml line 36
                {
                    this.six = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 24: // taskList.xaml line 37
                {
                    this.seven = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 25: // taskList.xaml line 28
                {
                    this.creat = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.creat).Click += this.creat_Click;
                }
                break;
            case 26: // taskList.xaml line 25
                {
                    this.select = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.select).SelectionChanged += this.select_SelectionChanged;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

