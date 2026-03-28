using Collector.Client.Wpf.Internals;
using Collector.Entity;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Collector.Client.Wpf.Dialogs
{
    /// <summary>
    /// Interaction logic for WatermarkDialog.xaml
    /// </summary>
    public partial class WatermarkDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.Watermark? _watermark;
        public WatermarkDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += WatermarkDialog_ContentRendered;
            Closing += WatermarkDialog_Closing;
            KeyDown += WatermarkDialog_KeyDown;
            App.EventHelper.WatermarkListChanged += EventHelper_WatermarkListChanged;
            FillControls();
            _settings.Load();
        }
        private void FillControls()
        {
            using (WorkerHandler worker = new WorkerHandler("FillControls"))
            {
                FillWatermarkControl();
            }
        }
        private void FillWatermarkControl()
        {
            using (WorkerHandler worker = new WorkerHandler("FillWatermarkControl"))
            {
                dgCatalogs.BeginInit();
                dgCatalogs.ItemsSource = App.DataShare.WatermarkList;
                dgCatalogs.EndInit();
            }
        }

        private void DoAdd()
        {
            using (WorkerHandler worker = new WorkerHandler("DoAdd"))
            {
                _watermark = new Entity.Watermark();
                DataContext = _watermark;
                SetGui();
                txtName.Focus();
            }
        }
        public void DoSave(bool CloseAfter)
        {
            using (WorkerHandler worker = new WorkerHandler("DoSave"))
            {
                gsMain.Focus();
                _watermark = DataContext as Entity.Watermark;
                if (_watermark != null)
                {
                    if (_watermark.Id == 0)
                    {
                        _watermark = App.AppShare.Repositories.WatermarkRepository.Create(_watermark);
                    }
                    else
                    {
                        _watermark = App.AppShare.Repositories.WatermarkRepository.Update(_watermark);
                    }
                    DataContext = _watermark;
                    SetGui();
                }
            }
            if (CloseAfter)
            {
                Close();
            }
        }

        private void SetGui()
        {
            using (WorkerHandler worker = new WorkerHandler("SetGui"))
            {
                btnAdd.IsEnabled = false;
                btnSave.IsEnabled = false;
                btnDelete.IsEnabled = false;
                if (_watermark != null)
                {
                    if (_watermark.Id == 0)
                    {
                        btnSave.IsEnabled = true;
                        txtName.Focus();
                    }
                    else
                    {
                        btnAdd.IsEnabled = true;
                        btnSave.IsEnabled = true;
                        btnDelete.IsEnabled = true;
                    }
                }
                else
                {
                    btnAdd.IsEnabled = true;
                }

            }
        }
        private void WatermarkDialog_KeyDown(object sender, KeyEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("WatermarkDialog_KeyDown"))
            {
                if (e.Key == Key.Escape)
                {
                    Close();
                }
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                {
                    if (e.Key == Key.N)
                    {
                        DoAdd();
                    }
                }
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                {
                    if (e.Key == Key.S)
                    {
                        DoSave(false);
                    }
                }
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                {
                    if (e.Key == Key.S)
                    {
                        DoSave(true);
                    }
                }
            }
        }

        private void WatermarkDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("WatermarkDialog_Closing"))
            {
                _settings.Save();
            }        
        }

        private void WatermarkDialog_ContentRendered(object? sender, EventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler(""))
            {
                _watermark = DataContext as Entity.Watermark;
                SetGui();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("btnAdd_Click"))
            {
                DoAdd();
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("btnSave_Click"))
            {
                DoSave(false);
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("btnDelete_Click"))
            {

            }

        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("txtFilter_TextChanged"))
            {

            }

        }

        private void dgCatalogs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("dgCatalogs_SelectionChanged"))
            {
                _watermark = dgCatalogs.SelectedItem as Entity.Watermark;
                DataContext = _watermark;
                SetGui();
                txtName.Focus();
                txtName.SelectAll();
            }
        }
        private void EventHelper_WatermarkListChanged(object sender, EventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("EventHelper_WatermarkListChanged"))
            {
                FillWatermarkControl();
            }
        }
    }
}
