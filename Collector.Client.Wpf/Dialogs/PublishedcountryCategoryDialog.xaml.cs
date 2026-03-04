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
    /// Interaction logic for PublishedcountryCategoryDialog.xaml
    /// </summary>
    public partial class PublishedcountryCategoryDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.PublishedCountryCategoryLink? _publishedCountryCategoryLink;
        private List<ViewModel.PublishedCountryCategoryModel> _list = new List<ViewModel.PublishedCountryCategoryModel>();
        public PublishedcountryCategoryDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += PublishedcountryCategoryDialog_ContentRendered;
            Closing += PublishedcountryCategoryDialog_Closing;
            KeyDown += PublishedcountryCategoryDialog_KeyDown;
            App.EventHelper.PublishedCountryCategoryListChanged += EventHelper_PublishedCountryCategoryListChanged;
            App.EventHelper.PublishedCountryListChanged += EventHelper_PublishedCountryListChanged;
            App.EventHelper.CategoryListChanged += EventHelper_CategoryListChanged;
            dgPublishedcountryCategories.SizeChanged += DgPublishedcountryCategories_SizeChanged;
            FillControls();
            _settings.Load();
        }


        private void DoSave()
        {
            _publishedCountryCategoryLink = DataContext as Entity.PublishedCountryCategoryLink;
            if (_publishedCountryCategoryLink != null)
            {
                if (_publishedCountryCategoryLink.Id == 0)
                {
                    _publishedCountryCategoryLink = App.AppShare.Repositories.PublishedCountryCategortRepository.Create(_publishedCountryCategoryLink);
                }
                else
                {
                    _publishedCountryCategoryLink = App.AppShare.Repositories.PublishedCountryCategortRepository.Update(_publishedCountryCategoryLink);
                }
                DataContext = _publishedCountryCategoryLink;
                SetGui();
            }
        }
        private void FillControls()
        {
            FillPublishedCountryControl();
            FillCategoryControl();
            FillPublichedCountryCategoryControl();
        }
        private void FillPublishedCountryControl()
        {
            cboPublishedCountry.BeginInit();
            cboPublishedCountry.ItemsSource = App.DataShare.PublishedCountryList;
            cboPublishedCountry.DisplayMemberPath = "Name";
            cboPublishedCountry.SelectedValuePath = "Id";
            cboPublishedCountry.EndInit();
        }
        public void FillCategoryControl()
        {
            cboCategory.BeginInit();
            cboCategory.ItemsSource = App.DataShare.CategoryList;
            cboCategory.DisplayMemberPath = "Value";
            cboCategory.SelectedValuePath = "Id";
            cboCategory.EndInit();
        }
        private void FillPublichedCountryCategoryControl()
        {
            if (App.DataShare.PublishedCountryCategoryList == null) return;
            _list.Clear();
            foreach (var item in App.DataShare.PublishedCountryCategoryList)
            {
                ViewModel.PublishedCountryCategoryModel c = new ViewModel.PublishedCountryCategoryModel();
                c.Id = item.Id;
                c.PublishedCountryId = item.PublishedCountryId;
                c.CategoryId = item.CategoryId;
                if (App.DataShare.PublishedCountryList != null)
                {
                    var f = App.DataShare.PublishedCountryList.FirstOrDefault(x => x.Id == item.PublishedCountryId);
                    if (f != null) c.PublishedCountryName = f.Name;
                }
                if (App.DataShare.CategoryList != null)
                {
                    var cl = App.DataShare.CategoryList.FirstOrDefault(x => x.Id == item.CategoryId);
                    if (cl != null) c.CategoryName = cl.Value;
                }
                _list.Add(c);
            }
            dgPublishedcountryCategories.BeginInit();
            dgPublishedcountryCategories.ItemsSource = _list;
            dgPublishedcountryCategories.EndInit();
        }

        private void SetFilterView()
        {
            if (App.DataShare.PublishedCountryCategoryList == null) return;
            if (txtFilter.Text == "")
            {
                FillPublichedCountryCategoryControl();
            }
            else
            {
                List<ViewModel.PublishedCountryCategoryModel> nList = new List<ViewModel.PublishedCountryCategoryModel>();
                foreach (var item in App.DataShare.PublishedCountryCategoryList)
                {
                    var p = new ViewModel.PublishedCountryCategoryModel();
                    p.CategoryId = item.CategoryId;
                    p.PublishedCountryId = item.PublishedCountryId;
                    if (App.DataShare.PublishedCountryList != null)
                    {
                        var pc = App.DataShare.PublishedCountryList.FirstOrDefault(x => x.Id == item.PublishedCountryId);
                        if (pc != null) p.PublishedCountryName = pc.Name;
                    }
                    if (App.DataShare.CategoryList != null)
                    {
                        var c = App.DataShare.CategoryList.FirstOrDefault(x => x.Id == item.CategoryId);
                        if (c != null) p.CategoryName = c.Value;
                    }
                    if (p.PublishedCountryName.ToLower().Contains(txtFilter.Text.ToLower()) || p.CategoryName.ToLower().Contains(txtFilter.Text.ToLower()))
                    {
                        nList.Add(p);
                    }
                }
                dgPublishedcountryCategories.BeginInit();
                dgPublishedcountryCategories.ItemsSource = nList;
                dgPublishedcountryCategories.EndInit();
            }
        }
        private void SetGui()
        {
            btnAdd.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnDelete.IsEnabled = false;
            if (_publishedCountryCategoryLink != null)
            {
                if (_publishedCountryCategoryLink.Id == 0)
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
        private void PublishedcountryCategoryDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                e.Handled = true;
                Close();
            }
        }

        private void PublishedcountryCategoryDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void PublishedcountryCategoryDialog_ContentRendered(object? sender, EventArgs e)
        {
            _publishedCountryCategoryLink = DataContext as Entity.PublishedCountryCategoryLink;
            SetGui();
        }

        private void dgPublishedcountryCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = dgPublishedcountryCategories.SelectedItem as ViewModel.PublishedCountryCategoryModel;
            if (item != null && App.DataShare.PublishedCountryCategoryList != null)
            {
                _publishedCountryCategoryLink = App.DataShare.PublishedCountryCategoryList.FirstOrDefault(x => x.Id == item.Id);
                DataContext = _publishedCountryCategoryLink;
                SetGui();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            _publishedCountryCategoryLink = new PublishedCountryCategoryLink();
            DataContext = _publishedCountryCategoryLink;
            SetGui();
            cboPublishedCountry.Focus();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DoSave();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete the Publisedcountry category?", "Delete Publiscountry category", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;
            _publishedCountryCategoryLink = DataContext as Entity.PublishedCountryCategoryLink;
            if (_publishedCountryCategoryLink != null)
            {
                App.AppShare.Repositories.PublishedCountryCategortRepository.Delete(_publishedCountryCategoryLink.Id);
                _publishedCountryCategoryLink = null;
                DataContext = _publishedCountryCategoryLink;
                SetGui();
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetFilterView();
        }
        private void EventHelper_CategoryListChanged(object sender, EventArgs e)
        {
            FillCategoryControl();
        }

        private void EventHelper_PublishedCountryListChanged(object sender, EventArgs e)
        {
            FillPublishedCountryControl();
        }

        private void EventHelper_PublishedCountryCategoryListChanged(object sender, EventArgs e)
        {
            FillPublichedCountryCategoryControl();
        }
        private void DgPublishedcountryCategories_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var width = dgPublishedcountryCategories.ActualWidth - 2;
            dgPublishedcountryCategories.Columns[1].Width = width / 2;
            dgPublishedcountryCategories.Columns[2].Width = width / 2;
        }
    }
}
