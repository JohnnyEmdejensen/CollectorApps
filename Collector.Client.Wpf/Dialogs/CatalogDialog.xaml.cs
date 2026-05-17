using Collector.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
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
    /// Interaction logic for CatalogDialog.xaml
    /// </summary>
    public partial class CatalogDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.Catalog? _catalog;
        public CatalogDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += CatalogDialog_ContentRendered;
            Closing += CatalogDialog_Closing;
            KeyDown += CatalogDialog_KeyDown;
            App.EventHelper.CatalogListChanged += EventHelper_CatalogListChanged;
            FillCatalogControl();
            _settings.Load();
        }


        private void FillCatalogControl()
        {
            dgCatalogs.BeginInit();
            dgCatalogs.ItemsSource = App.DataShare.CatalogList;
            dgCatalogs.EndInit();
        }
        private void DoSave(bool CloseAfter)
        {
            _catalog = DataContext as Entity.Catalog;
            if (_catalog != null)
            {
                if (_catalog.Id == 0)
                {
                    _catalog = App.AppShare.Repositories.CatalogRepository.Create(_catalog);
                }
                else
                {
                    _catalog = App.AppShare.Repositories.CatalogRepository.Update(_catalog);
                }
                DataContext = _catalog;
                SetGui();
            }
            if (CloseAfter)
            {
                Close();
            }
        }
        private void SetFilterView()
        {
            if (App.DataShare.CatalogList == null) return;
            if (txtFilter.Text == "")
            {
                FillCatalogControl();
            }
            else
            {
                dgCatalogs.BeginInit();
                dgCatalogs.ItemsSource = App.DataShare.CatalogList.Where(x => x.Value.ToLower().Contains(txtFilter.Text.ToLower())).ToList();
                dgCatalogs.EndInit();
            }
        } 
        private void SetGui()
        {
            btnAdd.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnDelete.IsEnabled = false;
            if (_catalog != null)
            {
                if (_catalog.Id == 0)
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
            txtFilter.Focus();
        }

        private void CatalogDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                e.Handled = true;
                Close();
            }
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                if (e.Key == Key.S)
                {
                    e.Handled = true;
                    DoSave(false);
                }
            }
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
            {
                if (e.Key == Key.S)
                {
                    e.Handled = true;
                    DoSave(true);
                }
            }
        }

        private void CatalogDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void CatalogDialog_ContentRendered(object? sender, EventArgs e)
        {
            _catalog = DataContext as Entity.Catalog;
            SetGui();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            txtName.Focus();
            _catalog = new Catalog();
            DataContext = _catalog;
            SetGui();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DoSave(false);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete the Catalog?", "Delete Catalog", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;
            
            _catalog = DataContext as Entity.Catalog;
            if (_catalog != null)
            {
                App.AppShare.Repositories.CatalogRepository.Delete(_catalog.Id);
                _catalog = null;
                DataContext = _catalog;
                SetGui();
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetFilterView();
        }

        private void dgCatalogs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _catalog = dgCatalogs.SelectedItem as Entity.Catalog;
            DataContext = _catalog;
            SetGui();
        }
        private void EventHelper_CatalogListChanged(object sender, EventArgs e)
        {
            FillCatalogControl();
        }
    }
}
