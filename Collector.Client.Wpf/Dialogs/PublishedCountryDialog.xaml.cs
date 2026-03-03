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
    /// Interaction logic for PublishedCountryDialog.xaml
    /// </summary>
    public partial class PublishedCountryDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.PublishedCountry? _publishedcountry;
        private List<ViewModel.PublishedCountryModel>? _publishedcountries;
        public PublishedCountryDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += PublishedCountryDialog_ContentRendered;
            Closing += PublishedCountryDialog_Closing;
            KeyDown += PublishedCountryDialog_KeyDown;
            App.EventHelper.PublishedCountryListChanged += EventHelper_PublishedCountryListChanged;
            FillCountryControl();
            FillPublishedCountryCotrol();
            _settings.Load();
        }

        public void DoSave()
        {
            _publishedcountry = DataContext as Entity.PublishedCountry;
            if (_publishedcountry != null)
            {
                if (_publishedcountry.Id == 0)
                {
                    _publishedcountry = App.AppShare.Repositories.PublishedcountryRepository.Create(_publishedcountry);
                }
                else
                {
                    _publishedcountry = App.AppShare.Repositories.PublishedcountryRepository.Update(_publishedcountry);
                }
                DataContext = _publishedcountry;
                SetGui();
            }
        }
        public void FillCountryControl()
        {
            cboCountry.BeginInit();
            cboCountry.ItemsSource = App.DataShare.CountryList;
            cboCountry.DisplayMemberPath = "Name";
            cboCountry.SelectedValuePath = "Id";
            cboCountry.EndInit();
        }

        private void FillPublishedCountryCotrol()
        {
            if (App.DataShare.PublishedCountryList == null || App.DataShare.CountryList == null) return;
            _publishedcountries = new List<ViewModel.PublishedCountryModel>();
            foreach (var ctry in App.DataShare.PublishedCountryList)
            {
                ViewModel.PublishedCountryModel model = new ViewModel.PublishedCountryModel();
                model.Name = ctry.Name;
                var c = App.DataShare.CountryList.FirstOrDefault(x => x.Id == ctry.Id);
                if (c != null)
                {
                    model.CountryId = ctry.Id;
                    model.CountryName = c.Name;
                }
                _publishedcountries.Add(model);
            }
            dgPublishedCountries.BeginInit();
            dgPublishedCountries.ItemsSource = _publishedcountries;
            dgPublishedCountries.EndInit();
        }
        private void SetFillterView()
        {
            if (App.DataShare.PublishedCountryList == null || App.DataShare.CountryList == null) return;
            if (txtFilter.Text == "")
            {
                FillPublishedCountryCotrol();
            }
            else
            {
                var list = App.DataShare.PublishedCountryList.Where(x => x.Name.ToLower().Contains(txtFilter.Text.ToLower())).ToList();
                if (list != null)
                {
                    _publishedcountries = new List<ViewModel.PublishedCountryModel>();
                    foreach (var item in list)
                    {
                        var model = new ViewModel.PublishedCountryModel();
                        model.Name = item.Name;
                        var ctry = App.DataShare.CountryList.FirstOrDefault(x =>x.Id == item.ContryId);
                        if (ctry != null)
                        {
                            model.CountryName = ctry.Name;
                            model.CountryId = ctry.Id;
                        }
                        _publishedcountries.Add(model);
                    }
                    dgPublishedCountries.BeginInit();
                    dgPublishedCountries.ItemsSource = _publishedcountries;
                    dgPublishedCountries.EndInit();
                }
            }
        }
        private void SetGui()
        {
            btnAdd.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnDelete.IsEnabled = false;
            if (_publishedcountry != null)
            {
                if (_publishedcountry.Id == 0)
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
        private void PublishedCountryDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                e.Handled = true;
                Close();
            }
        }

        private void PublishedCountryDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void PublishedCountryDialog_ContentRendered(object? sender, EventArgs e)
        {
            _publishedcountry = DataContext as Entity.PublishedCountry;
            SetGui();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            _publishedcountry = new PublishedCountry();
            DataContext = _publishedcountry;
            SetGui();
            txtName.Focus();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DoSave();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete the Publichedcountry?", "Delete Publishedcountry", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;
            _publishedcountry = DataContext as Entity.PublishedCountry;
            if (_publishedcountry != null)
            {
                App.AppShare.Repositories.PublishedcountryRepository.Delete(_publishedcountry.Id);
                _publishedcountry = null;
                DataContext = _publishedcountry;
                SetGui();
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetFillterView();
        }

        private void dgPublishedCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = dgPublishedCountries.SelectedItem as ViewModel.PublishedCountryModel;
            if (item != null && App.DataShare.PublishedCountryList != null)
            {
                _publishedcountry = App.DataShare.PublishedCountryList.FirstOrDefault(x => x.Id == item.Id);
                DataContext = _publishedcountry;
                SetGui();
            }
        }
        private void EventHelper_PublishedCountryListChanged(object sender, EventArgs e)
        {
            FillPublishedCountryCotrol();
        }
    }
}
