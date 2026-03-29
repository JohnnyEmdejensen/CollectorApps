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
            App.EventHelper.ColorListChanged += EventHelper_ColorListChanged;
            App.EventHelper.CoinListChanged += EventHelper_CoinListChanged;
            App.EventHelper.CatalogListChanged += EventHelper_CatalogListChanged;
            App.EventHelper.CategoryListChanged += EventHelper_CategoryListChanged;
            App.EventHelper.PublishedCountryListChanged += EventHelper_PublishedCountryListChanged;
            App.EventHelper.TopicListChanged += EventHelper_TopicListChanged;
            FillControls();
            _settings.Load();
        }


        private void DoSave(bool CloseAfter)
        {
            _stamp = DataContext as Entity.Stamp;
            if (_stamp != null)
            {
                if (_stamp.Id == 0)
                {
                    _stamp = App.AppShare.Repositories.StampRepository.Create(_stamp);
                }
                else
                {
                    _stamp = App.AppShare.Repositories.StampRepository.Update(_stamp);
                }
                DataContext = _stamp;
                SetGui();
            }
            if (CloseAfter)
            {
                Close();
            }
        }
        private void FillControls()
        {
            using (Internals.WorkerHandler worker = new Internals.WorkerHandler("FillControls"))
            {
                FillPublishedCountryControl();
                FillCatalogControl();
                FillCategoryControl();
                FillCoinunits();
                FillColors();
                FillWatermarks();
                FillTopicControl();
            }
        }
        private void FillWatermarks()
        {
            cboWatermark.BeginInit();
            cboWatermark.ItemsSource = App.DataShare.WatermarkList;
            cboWatermark.DisplayMemberPath = "Value";
            cboWatermark.SelectedValuePath = "Id";
            cboWatermark.EndInit();
        }
        private void FillColors()
        {
            using (Internals.WorkerHandler worker = new Internals.WorkerHandler("FillColors"))
            {
                cboColor.BeginInit();
                cboColor.ItemsSource = App.DataShare.ColorList;
                cboColor.DisplayMemberPath = "Value";
                cboColor.SelectedValuePath = "Id";
                cboColor.EndInit();
            }
        }
        private void FillCategoryControl()
        {
            using (Internals.WorkerHandler worker = new Internals.WorkerHandler("FillCategoryControl"))
            {
                cboCategory.BeginInit();
                cboCategory.ItemsSource = App.DataShare.CategoryList;
                cboCategory.DisplayMemberPath = "Value";
                cboCategory.SelectedValuePath = "Id";
                cboCategory.EndInit();
            }
        }
        private void FillCoinunits()
        {
            using (WorkerHandler worker = new WorkerHandler("FillCoinunits"))
            {
                cboCoin.BeginInit();
                cboCoin.ItemsSource = App.DataShare.CoinList;
                cboCoin.DisplayMemberPath = "Value";
                cboCoin.SelectedValuePath = "Id";
                cboCoin.EndInit();
            }
        }
        private void FillCatalogControl()
        {
            using (WorkerHandler worker = new WorkerHandler("FillCatalogControl"))
            {
                cboCatalog.BeginInit();
                cboCatalog.ItemsSource = App.DataShare.CatalogList;
                cboCatalog.DisplayMemberPath = "Value";
                cboCatalog.SelectedValuePath = "Id";
                cboCatalog.EndInit();
            }
        }
        private void FillTopicControl()
        {
            using (new WorkerHandler("FillTopicControl"))
            {
                cboTopic.BeginInit();
                cboTopic.ItemsSource = App.DataShare.TopicList;
                cboTopic.DisplayMemberPath = "Value";
                cboTopic.SelectedValuePath = "Id";
                cboTopic.EndInit();
            }
        }
        private void FillPublishedCountryControl()
        {
            using (WorkerHandler worker = new WorkerHandler("FillPublishedCountryControl"))
            {
                cboPublishedCountry.BeginInit();
                cboPublishedCountry.ItemsSource = App.DataShare.PublishedCountryList;
                cboPublishedCountry.DisplayMemberPath = "Name";
                cboPublishedCountry.SelectedValuePath = "Id";
                cboPublishedCountry.EndInit();
            }
        }
        private void SetGui()
        {
            btnAdd.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnAddStock.IsEnabled = false;
            btnAddImage.IsEnabled = false;
            btnOpenImage.IsEnabled = false;
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
                    btnAddStock.IsEnabled = true;
                    btnAddImage.IsEnabled = true;
                    btnOpenImage.IsEnabled = true;
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
            using (WorkerHandler worker = new WorkerHandler("btnSave_Click"))
            {
                DoSave(false);
            }
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
        private void EventHelper_ColorListChanged(object sender, EventArgs e)
        {
            FillColors();
        }
        private void EventHelper_CoinListChanged(object sender, EventArgs e)
        {
            FillCoinunits();
        }
        private void EventHelper_CatalogListChanged(object sender, EventArgs e)
        {
            FillCatalogControl();
        }
        private void EventHelper_CategoryListChanged(object sender, EventArgs e)
        {
            FillCategoryControl();
        }
        private void EventHelper_PublishedCountryListChanged(object sender, EventArgs e)
        {
            FillPublishedCountryControl();
        }
        private void EventHelper_TopicListChanged(object sender, EventArgs e)
        {
            using (new WorkerHandler("EventHelper_TopicListChanged"))
            {
                FillTopicControl();
            }
        }

        private void lblColor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            App.FormHelper.ShowColorsDialog(new());
        }

        private void btnAddStock_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSaveStock_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOpenImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lblWatermark_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("lblWatermark_MouseDown"))
            {
                App.FormHelper.ShowWatermarkDialog(new());
            }
        }

        private void lblTopic_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (new WorkerHandler("lblTopic_MouseDown"))
            {
                App.FormHelper.ShowTopicsDialog(new());
            }
        }
    }
}
