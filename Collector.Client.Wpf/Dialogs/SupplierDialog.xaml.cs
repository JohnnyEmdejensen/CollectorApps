using Collector.Client.Wpf.Internals;
using Collector.Entity;
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
using System.Windows.Shapes;

namespace Collector.Client.Wpf.Dialogs
{
    /// <summary>
    /// Interaction logic for SupplierDialog.xaml
    /// </summary>
    public partial class SupplierDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.Supplier? _supplier;
        public SupplierDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += SupplierDialog_ContentRendered;
            Closing += SupplierDialog_Closing;
            KeyDown += SupplierDialog_KeyDown;
            App.EventHelper.SupplierListChanged += EventHelper_SupplierListChanged;
            FillControls();
            _settings.Load();
        }


        private void FillControls()
        {
            using (new WorkerHandler("FillControls"))
            {
                FillSupplierControl();
            }
        }
        private void FillSupplierControl()
        {
            using (new WorkerHandler("FillSupplierControl"))
            {
                dgSuppliers.BeginInit();
                dgSuppliers.ItemsSource = App.DataShare.SupplierList;
                dgSuppliers.EndInit();
            }
        }
        private void DoAdd()
        {
            _supplier = new Entity.Supplier();
            DataContext = _supplier;
            SetGui();
            txtName.Focus();
        }

        private void DoSave(bool CloseAfter)
        {
            gsMain.Focus();
            _supplier = DataContext as Entity.Supplier;
            if (_supplier != null)
            {
                if (_supplier.Id == 0)
                {
                    _supplier = App.AppShare.Repositories.SupplierRepository.Create(_supplier);
                }
                else
                {
                    _supplier = App.AppShare.Repositories.SupplierRepository.Update(_supplier);
                }
                DataContext = _supplier;
                SetGui();
            }
            if (CloseAfter)
            {
                Close();
            }
        }

        private void SetGui()
        {
            btnAdd.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnDelete.IsEnabled = false;
            if (_supplier != null)
            {
                if (_supplier.Id == 0)
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
        private void SupplierDialog_KeyDown(object sender, KeyEventArgs e)
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

        private void SupplierDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void SupplierDialog_ContentRendered(object? sender, EventArgs e)
        {
            _supplier = DataContext as Entity.Supplier;
            DataContext = _supplier;
            SetGui();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DoAdd();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DoSave(false);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dgSuppliers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _supplier = dgSuppliers.SelectedItem as Entity.Supplier;
            DataContext = _supplier;
            SetGui();
            txtName.Focus();
        }
        private void EventHelper_SupplierListChanged(object sender, EventArgs e)
        {
            using (new WorkerHandler("EventHelper_SupplierListChanged"))
            {
                FillSupplierControl();
            }
        }
    }
}
