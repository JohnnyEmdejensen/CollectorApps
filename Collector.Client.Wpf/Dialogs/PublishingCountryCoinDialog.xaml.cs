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
    /// Interaction logic for PublishingCountryCoinDialog.xaml
    /// </summary>
    public partial class PublishingCountryCoinDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.PublishedCountryCoinLink? _link;
        public PublishingCountryCoinDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += PublishingCountryCoinDialog_ContentRendered;
            Closing += PublishingCountryCoinDialog_Closing;
            KeyDown += PublishingCountryCoinDialog_KeyDown;
            App.EventHelper.PublishedCountryCoinLinksChanged += EventHelper_PublishedCountryCoinLinksChanged;
            dgPubCtryCoin.SizeChanged += DgPubCtryCoin_SizeChanged;
            FillControls();
            _settings.Load();
        }


        private void FillControls()
        {
            FillPublisedCountryCoinLinksControl();
            FillPublishedCountryControl();
            FillCoinControl();
        }
        private void FillCoinControl()
        {
            cboCoin.BeginInit();
            cboCoin.ItemsSource = App.DataShare.CoinList;
            cboCoin.DisplayMemberPath = "Value";
            cboCoin.SelectedValuePath = "Id";
            cboCoin.EndInit();
        }
        private void FillPublishedCountryControl()
        {
            cboPublishCountry.BeginInit();
            cboPublishCountry.ItemsSource = App.DataShare.PublishedCountryList;
            cboPublishCountry.DisplayMemberPath = "Name";
            cboPublishCountry.SelectedValuePath = "Id";
            cboPublishCountry.EndInit();
        }
        private void EventHelper_PublishedCountryCoinLinksChanged(object sender, EventArgs e)
        {
            FillPublisedCountryCoinLinksControl();
        }

        private void FillPublisedCountryCoinLinksControl()
        {
            if (App.DataShare.PublishedCountryCoinLinks == null) return;
            List<ViewModel.PublishedCountryCoinView> views = new List<ViewModel.PublishedCountryCoinView>();
            foreach (var link in App.DataShare.PublishedCountryCoinLinks)
            {
                ViewModel.PublishedCountryCoinView view = new ViewModel.PublishedCountryCoinView();
                view.Id = link.Id;
                view.PublishedCountryId = link.PublishedCountryId;
                view.CoinId = link.CoinId;
                if (App.DataShare.CoinList != null)
                {
                    var coin = App.DataShare.CoinList.FirstOrDefault(x => x.Id == link.CoinId);
                    if (coin != null) view.Coin = coin.Value;
                }
                if (App.DataShare.PublishedCountryList != null) 
                {
                    foreach (var country in App.DataShare.PublishedCountryList)
                    {
                        var ctry = App.DataShare.PublishedCountryList.FirstOrDefault(x => x.Id == link.PublishedCountryId);
                        if (ctry != null) view.PublishedCountry = ctry.Name;
                    }
                }
                views.Add(view);
            }
            dgPubCtryCoin.BeginInit();
            dgPubCtryCoin.ItemsSource = views;
            dgPubCtryCoin.EndInit();
        }
        private void DoSave()
        {
            _link = DataContext as Entity.PublishedCountryCoinLink;
            if (_link != null)
            {
                if (_link.Id == 0)
                {
                    _link = App.AppShare.Repositories.PublishedCountryCoinRepository.Create(_link);
                }
                else
                {
                    _link = App.AppShare.Repositories.PublishedCountryCoinRepository.Create(_link);
                }
                DataContext = _link;
                SetGui();
            }
        }

        private void SetFilterView()
        {
            if (App.DataShare.PublishedCountryCoinLinks == null) return;
            if (txtFilter.Text == "")
            {
                FillPublisedCountryCoinLinksControl();
            }
            else
            {
                List<ViewModel.PublishedCountryCoinView> views = new List<ViewModel.PublishedCountryCoinView>();
                foreach (var link in App.DataShare.PublishedCountryCoinLinks)
                {
                    ViewModel.PublishedCountryCoinView view = new ViewModel.PublishedCountryCoinView();
                    view.Id = link.Id;
                    view.PublishedCountryId = link.PublishedCountryId;
                    view.CoinId = link.CoinId;
                    if (App.DataShare.CoinList != null)
                    {
                        var coin = App.DataShare.CoinList.FirstOrDefault(x => x.Id == link.CoinId);
                        if (coin != null) view.Coin = coin.Value;
                    }
                    if (App.DataShare.PublishedCountryList != null)
                    {
                        var ctry = App.DataShare.PublishedCountryList.FirstOrDefault(x => x.Id == link.PublishedCountryId);
                        if (ctry != null)
                        {
                            view.PublishedCountry = ctry.Name;
                        }
                    }
                    if (view.Coin.ToLower().Contains(txtFilter.Text.ToLower()) || view.PublishedCountry.ToLower().Contains(txtFilter.Text.ToLower()))
                        views.Add(view);
                }
                dgPubCtryCoin.BeginInit();
                dgPubCtryCoin.ItemsSource = views;
                dgPubCtryCoin.EndInit();
            }

        }
        private void SetGui()
        {
            btnAdd.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnDelete.IsEnabled = false;
            if (_link != null)
            {
                if (_link.Id == 0)
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

        private void PublishingCountryCoinDialog_KeyDown(object sender, KeyEventArgs e)
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
                    DoSave();
                }
            }

        }

        private void PublishingCountryCoinDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void PublishingCountryCoinDialog_ContentRendered(object? sender, EventArgs e)
        {
            _link = DataContext as Entity.PublishedCountryCoinLink;
            SetGui();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            _link = new PublishedCountryCoinLink();
            DataContext = _link;
            SetGui();
            cboPublishCountry.Focus();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DoSave();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete the Publishingcountry coinunit?", "Delete Coinunit", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;

            _link = DataContext as Entity.PublishedCountryCoinLink;
            if (_link != null)
            {
                App.AppShare.Repositories.PublishedCountryCoinRepository.Delete(_link.Id);
                _link = null;
                DataContext = _link;
                SetGui();
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetFilterView();
        }

        private void dgPubCtryCoin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var view = dgPubCtryCoin.SelectedItem as ViewModel.PublishedCountryCoinView;
            if (view != null && App.DataShare.PublishedCountryCoinLinks != null)
            {
                _link = App.DataShare.PublishedCountryCoinLinks.FirstOrDefault(x => x.Id == view.Id);
                DataContext = _link;
                SetGui();
            }
        }
        private void DgPubCtryCoin_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var width = dgPubCtryCoin.ActualWidth - 4;
            dgPubCtryCoin.Columns[1].Width = width / 2;
            dgPubCtryCoin.Columns[2].Width = width / 2;
        }
    }
}
