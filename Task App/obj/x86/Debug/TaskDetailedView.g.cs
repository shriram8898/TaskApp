﻿#pragma checksum "E:\Zoho(pt3644)\Task App\TaskDetailedView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "37FAF1477DF8CD648EE0101DBF9D6D05"
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
    partial class TaskDetailedView : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_TextBlock_Text(global::Windows.UI.Xaml.Controls.TextBlock obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private class TaskDetailedView_obj29_Bindings :
            global::Windows.UI.Xaml.IDataTemplateExtension,
            global::Windows.UI.Xaml.Markup.IDataTemplateComponent,
            global::Windows.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            ITaskDetailedView_Bindings
        {
            private global::Task_App.Models.TaskDetails dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private bool removedDataContextHandler = false;

            // Fields for each control that has bindings.
            private global::System.WeakReference obj29;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj29TextDisabled = false;

            public TaskDetailedView_obj29_Bindings()
            {
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 199 && columnNumber == 97)
                {
                    isobj29TextDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 29: // TaskDetailedView.xaml line 199
                        this.obj29 = new global::System.WeakReference((global::Windows.UI.Xaml.Controls.TextBlock)target);
                        break;
                    default:
                        break;
                }
            }

            public void DataContextChangedHandler(global::Windows.UI.Xaml.FrameworkElement sender, global::Windows.UI.Xaml.DataContextChangedEventArgs args)
            {
                 if (this.SetDataRoot(args.NewValue))
                 {
                    this.Update();
                 }
            }

            // IDataTemplateExtension

            public bool ProcessBinding(uint phase)
            {
                throw new global::System.NotImplementedException();
            }

            public int ProcessBindings(global::Windows.UI.Xaml.Controls.ContainerContentChangingEventArgs args)
            {
                int nextPhase = -1;
                ProcessBindings(args.Item, args.ItemIndex, (int)args.Phase, out nextPhase);
                return nextPhase;
            }

            public void ResetTemplate()
            {
                Recycle();
            }

            // IDataTemplateComponent

            public void ProcessBindings(global::System.Object item, int itemIndex, int phase, out int nextPhase)
            {
                nextPhase = -1;
                switch(phase)
                {
                    case 0:
                        nextPhase = -1;
                        this.SetDataRoot(item);
                        if (!removedDataContextHandler)
                        {
                            removedDataContextHandler = true;
                            (this.obj29.Target as global::Windows.UI.Xaml.Controls.TextBlock).DataContextChanged -= this.DataContextChangedHandler;
                        }
                        this.initialized = true;
                        break;
                }
                this.Update_((global::Task_App.Models.TaskDetails) item, 1 << phase);
            }

            public void Recycle()
            {
            }

            // ITaskDetailedView_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                if (newDataRoot != null)
                {
                    this.dataRoot = (global::Task_App.Models.TaskDetails)newDataRoot;
                    return true;
                }
                return false;
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::Task_App.Models.TaskDetails obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_name(obj.name, phase);
                    }
                }
            }
            private void Update_name(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // TaskDetailedView.xaml line 199
                    if (!isobj29TextDisabled)
                    {
                        if ((this.obj29.Target as global::Windows.UI.Xaml.Controls.TextBlock) != null)
                        {
                            XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text((this.obj29.Target as global::Windows.UI.Xaml.Controls.TextBlock), obj, null);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // TaskDetailedView.xaml line 13
                {
                    this.cvs = (global::Windows.UI.Xaml.Data.CollectionViewSource)(target);
                }
                break;
            case 3: // TaskDetailedView.xaml line 14
                {
                    this.cvs1 = (global::Windows.UI.Xaml.Data.CollectionViewSource)(target);
                }
                break;
            case 4: // TaskDetailedView.xaml line 65
                {
                    this.listviewcolumn = (global::Windows.UI.Xaml.Controls.ColumnDefinition)(target);
                }
                break;
            case 5: // TaskDetailedView.xaml line 66
                {
                    this.detailviewcolumn = (global::Windows.UI.Xaml.Controls.ColumnDefinition)(target);
                }
                break;
            case 6: // TaskDetailedView.xaml line 225
                {
                    this.contentDialog2 = (global::Windows.UI.Xaml.Controls.ContentDialog)(target);
                }
                break;
            case 7: // TaskDetailedView.xaml line 275
                {
                    this.contentDialog1 = (global::Windows.UI.Xaml.Controls.ContentDialog)(target);
                }
                break;
            case 9: // TaskDetailedView.xaml line 297
                {
                    this.add = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.add).Click += this.add_Click;
                }
                break;
            case 10: // TaskDetailedView.xaml line 299
                {
                    this.cancel = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.cancel).Click += this.cancel_Click;
                }
                break;
            case 11: // TaskDetailedView.xaml line 301
                {
                    this.taskName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 12: // TaskDetailedView.xaml line 302
                {
                    this.taskDetails = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 13: // TaskDetailedView.xaml line 304
                {
                    this.priority = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 14: // TaskDetailedView.xaml line 306
                {
                    this.Assignto = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 16: // TaskDetailedView.xaml line 250
                {
                    this.save = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.save).Click += this.save_Click;
                }
                break;
            case 17: // TaskDetailedView.xaml line 252
                {
                    this.cancel1 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.cancel1).Click += this.cancel1_Click;
                }
                break;
            case 18: // TaskDetailedView.xaml line 254
                {
                    this.taskName1 = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 19: // TaskDetailedView.xaml line 264
                {
                    this.priority1 = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 20: // TaskDetailedView.xaml line 266
                {
                    this.collective1 = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 21: // TaskDetailedView.xaml line 268
                {
                    this.status = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                }
                break;
            case 22: // TaskDetailedView.xaml line 260
                {
                    this.taskDetails1 = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 23: // TaskDetailedView.xaml line 258
                {
                    this.edit1 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.edit1).Click += this.edit1_Click;
                }
                break;
            case 24: // TaskDetailedView.xaml line 105
                {
                    this.taskname = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 25: // TaskDetailedView.xaml line 106
                {
                    this.detsila = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 26: // TaskDetailedView.xaml line 194
                {
                    this.subtask = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.subtask).Click += this.subtask_Click;
                }
                break;
            case 27: // TaskDetailedView.xaml line 196
                {
                    this.subtaskview = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)this.subtaskview).ItemClick += this.subtaskview_ItemClick;
                }
                break;
            case 31: // TaskDetailedView.xaml line 188
                {
                    this.upload = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.upload).Click += this.upload_Click;
                }
                break;
            case 32: // TaskDetailedView.xaml line 189
                {
                    this.files = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 33: // TaskDetailedView.xaml line 157
                {
                    this.commen = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 34: // TaskDetailedView.xaml line 177
                {
                    this.combox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 35: // TaskDetailedView.xaml line 180
                {
                    global::Windows.UI.Xaml.Controls.Button element35 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element35).Click += this.Button_Click_1;
                }
                break;
            case 36: // TaskDetailedView.xaml line 181
                {
                    global::Windows.UI.Xaml.Controls.Button element36 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element36).Click += this.Button_Click;
                }
                break;
            case 37: // TaskDetailedView.xaml line 159
                {
                    this.commentsSection = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 40: // TaskDetailedView.xaml line 109
                {
                    this.td = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 41: // TaskDetailedView.xaml line 110
                {
                    this.Panel1 = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 42: // TaskDetailedView.xaml line 120
                {
                    this.Panel2 = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 43: // TaskDetailedView.xaml line 130
                {
                    this.Panel3 = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 44: // TaskDetailedView.xaml line 140
                {
                    this.Panel4 = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 45: // TaskDetailedView.xaml line 145
                {
                    this.collective = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 46: // TaskDetailedView.xaml line 147
                {
                    this.trelate = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.trelate).SelectionChanged += this.combo_SelectionChanged1;
                }
                break;
            case 47: // TaskDetailedView.xaml line 143
                {
                    this.tstatus = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.tstatus).SelectionChanged += this.combo_SelectionChanged2;
                }
                break;
            case 48: // TaskDetailedView.xaml line 137
                {
                    this.tprior = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.tprior).SelectionChanged += this.combo_SelectionChanged;
                }
                break;
            case 49: // TaskDetailedView.xaml line 133
                {
                    this.tup = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 50: // TaskDetailedView.xaml line 127
                {
                    this.tcreated = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 51: // TaskDetailedView.xaml line 123
                {
                    this.tto = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 52: // TaskDetailedView.xaml line 117
                {
                    this.tby = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 53: // TaskDetailedView.xaml line 113
                {
                    this.tid = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 54: // TaskDetailedView.xaml line 100
                {
                    this.goBack = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.goBack).Click += this.goBack_Click;
                }
                break;
            case 55: // TaskDetailedView.xaml line 101
                {
                    this.delete = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.delete).Click += this.delete_Click;
                }
                break;
            case 56: // TaskDetailedView.xaml line 102
                {
                    this.edit = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.edit).Click += this.edit_Click;
                }
                break;
            case 57: // TaskDetailedView.xaml line 103
                {
                    this.complete = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.complete).Click += this.complete_Click;
                }
                break;
            case 58: // TaskDetailedView.xaml line 73
                {
                    this.list = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)this.list).ItemClick += this.list_ItemClick;
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
            switch(connectionId)
            {
            case 29: // TaskDetailedView.xaml line 199
                {                    
                    global::Windows.UI.Xaml.Controls.TextBlock element29 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                    TaskDetailedView_obj29_Bindings bindings = new TaskDetailedView_obj29_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(element29.DataContext);
                    element29.DataContextChanged += bindings.DataContextChangedHandler;
                    global::Windows.UI.Xaml.DataTemplate.SetExtensionInstance(element29, bindings);
                    global::Windows.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element29, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

