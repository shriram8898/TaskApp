﻿#pragma checksum "E:\Zoho(pt3644)\UWP\EditTask.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D5376D6D08629EA4FA02B900426A39A8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UWP
{
    partial class EditTask : 
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
            case 2: // EditTask.xaml line 26
                {
                    this.add = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.add).Click += this.add_Click;
                }
                break;
            case 3: // EditTask.xaml line 28
                {
                    this.taskName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 4: // EditTask.xaml line 38
                {
                    this.priority = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 5: // EditTask.xaml line 40
                {
                    this.collective = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 6: // EditTask.xaml line 42
                {
                    this.status = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 7: // EditTask.xaml line 34
                {
                    this.taskDetails = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 8: // EditTask.xaml line 32
                {
                    this.edit = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.edit).Click += this.edit_Click;
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
