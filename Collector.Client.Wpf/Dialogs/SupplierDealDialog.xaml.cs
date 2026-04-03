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
    /// Interaction logic for SupplierDealDialog.xaml
    /// </summary>
    public partial class SupplierDealDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.Supplierdeal? _supplierdeal;

        public SupplierDealDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += SupplierDealDialog_ContentRendered;
            Closing += SupplierDealDialog_Closing;
            KeyDown += SupplierDealDialog_KeyDown;
            App.EventHelper.SupplierDealListChanged += EventHelper_SupplierDealListChanged;
            FillControls();
            _settings.Load();
        }

        private void FillControls()
        {
            using (new WorkerHandler("FillControls"))
            {
                FillSupplierControl();
                FillSupplierdealsControl();
            }
        }
        private void DoAdd()
        {
            _supplierdeal = new Supplierdeal();
            DataContext = _supplierdeal;
            SetGui();
            txtName.Focus();
        }
        private void DoSave(bool CloseAfter)
        {
            _supplierdeal = DataContext as Supplierdeal;
            if (_supplierdeal != null)
            {
                if (_supplierdeal.Id == 0)
                {
                    _supplierdeal = App.AppShare.Repositories.SupplierDealRepository.Create(_supplierdeal);
                }
                else
                {
                    _supplierdeal = App.AppShare.Repositories.SupplierDealRepository.Update(_supplierdeal);
                }
                DataContext = _supplierdeal;
                SetGui();
            }
            if (CloseAfter)
            {
                Close();
            }
        }
        private void FillSupplierControl()
        {
            using (new WorkerHandler("FillSupplierControl"))
            {
                cboSupplier.BeginInit();
                cboSupplier.ItemsSource = App.DataShare.SupplierList;
                cboSupplier.DisplayMemberPath = "Value";
                cboSupplier.SelectedValuePath = "Id";
                cboSupplier.EndInit();
            }
        }
        private void FillSupplierdealsControl()
        {
            using (new WorkerHandler("FillSupplierdealsControl"))
            {
                dgSupplierdeals.BeginInit();
                dgSupplierdeals.ItemsSource = App.DataShare.SupplierDealList;
                dgSupplierdeals.EndInit();
            }
        }
        private void SetGui()
        {
            btnAdd.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnDelete.IsEnabled = false;
            if (_supplierdeal != null)
            {
                if (_supplierdeal.Id == 0)
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
        private void EventHelper_SupplierDealListChanged(object sender, EventArgs e)
        {
            using (new WorkerHandler("EventHelper_SupplierDealListChanged"))
            {
                FillSupplierdealsControl();
            }
        }

        private void SupplierDealDialog_KeyDown(object sender, KeyEventArgs e)
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

        private void SupplierDealDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void SupplierDealDialog_ContentRendered(object? sender, EventArgs e)
        {
            _supplierdeal = DataContext as Supplierdeal;
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

        private void dgSupplierdeals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _supplierdeal = dgSupplierdeals.SelectedItem as Supplierdeal;
            DataContext = _supplierdeal;
            SetGui();
            txtName.Focus();
        }
    }
}
