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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace Collector.Client.Wpf.Dialogs
{
    /// <summary>
    /// Interaction logic for StampDialog.xaml
    /// </summary>
    public partial class StampDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.Stamp? _stamp;
        public StampDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += StampDialog_ContentRendered;
            Closing += StampDialog_Closing;
            KeyDown += StampDialog_KeyDown;
            FillControls();
            _settings.Load();
        }

        private void FillControls()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            FillPublishedCountryControl();
            FillCatalogControl();
            FillCategoryControl();
            FillCoinunits();
            Mouse.OverrideCursor = c;
        }
        private void FillCategoryControl()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            cboCategory.BeginInit();
            cboCategory.ItemsSource = App.DataShare.CategoryList;
            cboCategory.DisplayMemberPath = "Value";
            cboCategory.SelectedValuePath = "Id";
            cboCategory.EndInit();
            Mouse.OverrideCursor = c;
        }
        private void FillCoinunits()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            cboCoin.BeginInit();
            cboCoin.ItemsSource = App.DataShare.CoinList;
            cboCoin.DisplayMemberPath = "Value";
            cboCoin.SelectedValuePath = "Id";
            cboCoin.EndInit();
            Mouse.OverrideCursor = c;
        }
        private void FillCatalogControl()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            cboCatalog.BeginInit();
            cboCatalog.ItemsSource = App.DataShare.CatalogList;
            cboCatalog.DisplayMemberPath = "Value";
            cboCatalog.SelectedValuePath = "Id";
            cboCatalog.EndInit();
            Mouse.OverrideCursor = c;
        }
        private void FillPublishedCountryControl()
        {
            var c = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
            cboPublishedCountry.BeginInit();
            cboPublishedCountry.ItemsSource = App.DataShare.PublishedCountryList;
            cboPublishedCountry.DisplayMemberPath = "Name";
            cboPublishedCountry.SelectedValuePath = "Id";
            cboPublishedCountry.EndInit();
            Mouse.OverrideCursor = c;
        }
        private void SetGui()
        {
            btnAdd.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnDelete.IsEnabled = false;
            if (_stamp != null)
            {
                if (_stamp.Id == 0)
                {
                    btnSave.IsEnabled = true;
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
        private void StampDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        private void StampDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void StampDialog_ContentRendered(object? sender, EventArgs e)
        {
            _stamp = DataContext as Entity.Stamp;
            SetGui();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            _stamp = new Stamp();
            DataContext = _stamp;
            SetGui();

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lblPublishedCountry_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            App.FormHelper.ShowPublishedCountries(new());
        }

        private void lblCatalog_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            App.FormHelper.ShowCatalogs(new());
        }

        private void lblCategory_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            App.FormHelper.ShowCategories(new());
        }

        private void lblCoinUnit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            App.FormHelper.ShowCoinunits(new());
        }
    }
}
