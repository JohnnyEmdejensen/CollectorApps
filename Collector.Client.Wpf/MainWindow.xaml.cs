using Collector.Client.Wpf.Internals;
using System.CodeDom;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Collector.Client.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Internals.FormSettingsClass _settings;
        private double stampsViewFixedWidth = 688;
        private Internals.FilterContainer _filterContainer;
        public MainWindow()
        {
            InitializeComponent();
            _filterContainer = new Internals.FilterContainer();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += MainWindow_ContentRendered;
            Closing += MainWindow_Closing;
            dgStamps.SizeChanged += DgStamps_SizeChanged;
            App.EventHelper.CountryListChanged += EventHelper_CountryListChanged;
            App.EventHelper.ContinentListChanged += EventHelper_ContinentListChanged;
            App.EventHelper.PublishedCountryListChanged += EventHelper_PublishedCountryListChanged;
            App.EventHelper.TranslationSourceChanged += EventHelper_TranslationSourceChanged;
            App.EventHelper.StampModelListChanged += EventHelper_StampModelListChanged;
            App.EventHelper.SelectedStampChanged += EventHelper_SelectedStampChanged;
            _filterContainer.SelectedContinentChanged += _filterContainer_SelectedContinentChanged;
            _filterContainer.SelectedCountryChanged += _filterContainer_SelectedCountryChanged;
            _filterContainer.SelectedPublishedCountryChanged += _filterContainer_SelectedPublishedCountryChanged;
            _settings.Load();
        }

            
        private void DisplayCountries()
        {
            if (App.DataShare.ContinentList == null || App.DataShare.PublishedCountryList == null || App.DataShare.CountryList == null) return;
            using (WorkerHandler worker = new WorkerHandler("DisplayCountries"))
            {
                tvwContinents.Items.Clear();
                foreach (var continent in App.DataShare.ContinentList)
                {
                    ImageSource imgFolder = new BitmapImage(new Uri("Images/folder_16.png", UriKind.Relative));
                    StackPanel conSP = new StackPanel();
                    conSP.Orientation = Orientation.Horizontal;
                    Image conImage = new Image();
                    conImage.Source = imgFolder;
                    Label conLBL = new Label();
                    conLBL.Padding = new Thickness(4, 0, 0, 0);
                    conLBL.Content = continent.Value;
                    conSP.Children.Add(conImage);
                    conSP.Children.Add(conLBL);
                    TreeViewItem item = new TreeViewItem();
                    item.Tag = continent;
                    item.Header = conSP;
                    tvwContinents.Items.Add(item);
                    item.MouseUp += Continent_MouseUp;

                    foreach (var country in App.DataShare.CountryList.Where(x => x.ContinentId == continent.Id).OrderBy(x => x.Name))
                    {
                        StackPanel ctrySP = new StackPanel();
                        ctrySP.Orientation = Orientation.Horizontal;
                        Image ctryImage = new Image();
                        ctryImage.Source = imgFolder;
                        Label ctryLabel = new Label();
                        ctryLabel.Padding = new Thickness(4, 0, 0, 0);
                        ctryLabel.Content = country.Name;
                        ctrySP.Children.Add(ctryImage);
                        ctrySP.Children.Add(ctryLabel);
                        TreeViewItem ctryItem = new TreeViewItem();
                        ctryItem.Tag = country;
                        ctryItem.Header = ctrySP;
                        item.Items.Add(ctryItem);
                        ctryItem.MouseUp += Continent_MouseUp;

                        foreach (var popCtry in App.DataShare.PublishedCountryList.Where(x => x.ContryId == country.Id).OrderBy(x => x.Name))
                        {
                            StackPanel popSP = new StackPanel();
                            popSP.Orientation = Orientation.Horizontal;
                            Image popImage = new Image();
                            popImage.Source = imgFolder;                            
                            Label popLabel = new Label();
                            popLabel.Padding = new Thickness(4, 0, 0, 0);
                            popLabel.Content = popCtry.Name;
                            popSP.Children.Add(popImage);
                            popSP.Children.Add(popLabel);
                            TreeViewItem popItem = new TreeViewItem();
                            popItem.Tag = popCtry;
                            popItem.Header = popSP;
                            ctryItem.Items.Add(popItem);
                            popItem.MouseUp += Continent_MouseUp;
                        }
                    }
                }
            }
        }

        public void SetFilterView()
        {
            using (new WorkerHandler("SetFilterView"))
            {
                if (_filterContainer.SelectedContinent != null)
                {
                    cboFilterContinent.SelectedValue = _filterContainer.SelectedContinent.Id;
                }
                else
                {
                    cboFilterContinent.SelectedValue = null;
                }
                if (_filterContainer.SelectedCountry != null)
                {
                    cboFilterCountry.SelectedValue = _filterContainer.SelectedCountry.Id;
                }
                else
                {
                    cboFilterCountry.SelectedValue = null;
                }
                if (_filterContainer.SelectedPublishedCountry != null)
                {
                    cboFilterPublishedCountry.SelectedValue = _filterContainer.SelectedPublishedCountry.Id;
                }
                else
                {
                    cboFilterPublishedCountry.SelectedValue = null;
                }
            }
        }

        private void MainWindow_ContentRendered(object? sender, EventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("MainWindow_ContentRendered"))
            {
                App.DataShare.Initialize();
            }
        }
        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("MainWindow_Closing"))
            {
                _settings.Save();
            }
        }

        private void DgStamps_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            dgStamps.Columns[7].Width = dgStamps.ActualWidth - stampsViewFixedWidth;
        }

        private void tglStampview_Click(object sender, RoutedEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("tglStampview_Click"))
            {
                tglStampview.IsChecked = true;
                tglStockview.IsChecked = false;
                grdStampView.Visibility = Visibility.Visible;
                grdStockView.Visibility = Visibility.Collapsed;
            }
        }

        private void tglStockview_Click(object sender, RoutedEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("tglStockview_Click"))
            {
                tglStampview.IsChecked = false;
                tglStockview.IsChecked = true;
                grdStampView.Visibility = Visibility.Collapsed;
                grdStockView.Visibility = Visibility.Visible;
            }
        }
        private void EventHelper_TranslationSourceChanged(object sender, EventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("EventHelper_TranslationSourceChanged"))
            {
                ResourceDictionary disc = Application.Current.Resources.MergedDictionaries[1];
                foreach (var columns in dgStamps.Columns)
                {
                    var t = columns.Header.ToString();
                }
            }
        }

        private void btnLanUK_Click(object sender, RoutedEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("btnLanUK_Click"))
            {
                btnLanUK.IsChecked = true;
                btnLanDK.IsChecked = false;
                ResourceDictionary dict_enUK = new ResourceDictionary();
                ResourceDictionary dict_daDK = new ResourceDictionary();
                ResourceDictionary dict_Color = new ResourceDictionary();
                ResourceDictionary dict_Style = new ResourceDictionary();
                dict_enUK.Source = new Uri("Dictionary_ENUS.xaml", UriKind.Relative);
                dict_daDK.Source = new Uri("Dictionary_DADK.xaml", UriKind.Relative);
                dict_Color.Source = new Uri("Dictionary_COLOR.xaml", UriKind.Relative);
                dict_Style.Source = new Uri("Dictionary_STYLE.xaml", UriKind.Relative);
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(dict_Color);
                Application.Current.Resources.MergedDictionaries.Add(dict_Style);
                Application.Current.Resources.MergedDictionaries.Add(dict_enUK);
                App.EventHelper.RaiseTranslationSourceChangedEvent();
            }
        }

        private void btnLanDK_Click(object sender, RoutedEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("btnLanDK_Click"))
            {
                btnLanUK.IsChecked = false;
                btnLanDK.IsChecked = true;
                ResourceDictionary dict_enUK = new ResourceDictionary();
                ResourceDictionary dict_daDK = new ResourceDictionary();
                ResourceDictionary dict_Color = new ResourceDictionary();
                ResourceDictionary dict_Style = new ResourceDictionary();
                dict_enUK.Source = new Uri("Dictionary_ENUS.xaml", UriKind.Relative);
                dict_daDK.Source = new Uri("Dictionary_DADK.xaml", UriKind.Relative);
                dict_Color.Source = new Uri("Dictionary_COLOR.xaml", UriKind.Relative);
                dict_Style.Source = new Uri("Dictionary_STYLE.xaml", UriKind.Relative);
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(dict_Color);
                Application.Current.Resources.MergedDictionaries.Add(dict_Style);
                Application.Current.Resources.MergedDictionaries.Add(dict_daDK);
                App.EventHelper.RaiseTranslationSourceChangedEvent();
            }
        }

        private void btnCountries_Click(object sender, RoutedEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("btnCountries_Click"))
            {
                App.FormHelper.ShowCountries(null);
            }
        }
        private void btnCatalogs_Click(object sender, RoutedEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("btnCatalogs_Click"))
            {
                App.FormHelper.ShowCatalogs(null);
            }
        }

        private void btnCategories_Click(object sender, RoutedEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("btnCategories_Click"))
            {
                App.FormHelper.ShowCategories(null);
            }
        }

        private void btnIssuedCountries_Click(object sender, RoutedEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("btnIssuedCountries_Click"))
            {
                App.FormHelper.ShowPublishedCountries(null);
            }
        }

        private void btnPublishedCountries_Click(object sender, RoutedEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("btnPublishedCountries_Click"))
            {
                App.FormHelper.ShowPublishedCountries(null);
            }
        }

        private void btnPublishedCountryCategories_Click(object sender, RoutedEventArgs e)
        {
            App.FormHelper.ShowPublishedCountryCategories(null);
        }

        private void btnCreateStamp_Click(object sender, RoutedEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("btnCreateStamp_Click"))
            {
                App.FormHelper.ShowStampDialog(new());
            }
        }

        private void btnCoins_Click(object sender, RoutedEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("btnCoins_Click"))
            {
                App.FormHelper.ShowCoinunits(null);
            }
        }

        private void btnPubCountryCoins_Click(object sender, RoutedEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("btnPubCountryCoins_Click"))
            {
                App.FormHelper.ShowPublishingCountryCoinDialog(null);
            }
        }

        private void btnColors_Click(object sender, RoutedEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("btnColors_Click"))
            {
                App.FormHelper.ShowColorsDialog(null);
            }
        }

        private void btnDumbWorker_Click(object sender, RoutedEventArgs e)
        {
            if (App.DataShare.WorkerItems.Count > 0)
            {
                string indent = "";
                StringBuilder sb = new StringBuilder();
                foreach (var item in App.DataShare.WorkerItems)
                {
                    if (item.Status == WorkerItemStatus.Ended)
                    {
                        if (!string.IsNullOrEmpty(indent))
                        {
                            indent = indent.Substring(0, indent.Length - 2);
                        }
                    }
                    sb.AppendLine(item.Timer.ToString() + " " + indent + item.Name );
                    if (item.Status == WorkerItemStatus.Startet)
                    {
                        indent += "  ";
                    }
                }
                using (StreamWriter sw = new StreamWriter(@"C:\Temp\WorkerItems.txt"))
                {
                    sw.Write(sb.ToString());
                    sw.Flush();
                    sw.Close();
                }
            }
        }
        private void EventHelper_StampModelListChanged(object sender, EventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("EventHelper_StampModelListChanged"))
            {
                dgStamps.BeginInit();
                dgStamps.ItemsSource = App.DataShare.StampModelList;
                dgStamps.EndInit();
            }
        }

        private void btnWatermarks_Click(object sender, RoutedEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("btnWatermarks_Click"))
            {
                App.FormHelper.ShowWatermarkDialog(null);
            }
        }

        private void btnTopocs_Click(object sender, RoutedEventArgs e)
        {
            using (new WorkerHandler("btnTopocs_Click"))
            {
                App.FormHelper.ShowTopicsDialog(null);
            }
        }

        private void btnConditions_Click(object sender, RoutedEventArgs e)
        {
            using (new WorkerHandler("btnConditions_Click"))
            {
                App.FormHelper.ShowConditions(null);
            }
        }

        private void btnLocations_Click(object sender, RoutedEventArgs e)
        {
            using (new WorkerHandler("btnLocations_Click"))
            {
                App.FormHelper.ShowLocationsDialog(null);
            }
        }

        private void btnSuppliers_Click(object sender, RoutedEventArgs e)
        {
            using (new WorkerHandler("btnSuppliers_Click"))
            {
                App.FormHelper.ShowSuppliersDialog(null);
            }
        }

        private void btnSupplierDeals_Click(object sender, RoutedEventArgs e)
        {
            using (new WorkerHandler("btnSupplierDeals_Click"))
            {
                App.FormHelper.ShowSupplierdealsDialog(null);
            }
        }

        private void dgStamps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (new WorkerHandler("dgStamps_SelectionChanged"))
            {
                var stampModel = dgStamps.SelectedItem as ViewModel.StampModel;
                if (stampModel != null && App.DataShare.StampList != null)
                {
                    App.AppShare.SelectedStamp = App.DataShare.StampList.FirstOrDefault(x => x.Id == stampModel.Id);
                    App.EventHelper.RaiseSelectedStampChangedEvent();
                }
            }
        }
        private void dgStamps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            using (WorkerHandler worker = new WorkerHandler("dgStamps_MouseDoubleClick"))
            {
                var stampModel = dgStamps.SelectedItem as ViewModel.StampModel;
                if (stampModel != null)
                {
                    if (App.DataShare.StampList != null)
                    {
                        var stamp = App.DataShare.StampList.FirstOrDefault(x => x.Id == stampModel.Id);
                        App.FormHelper.ShowStampDialog(stamp);
                    }
                }
            }
        }
        private void EventHelper_SelectedStampChanged(object sender, EventArgs e)
        {
            using (new WorkerHandler("EventHelper_SelectedStampChanged"))
            {
                if (App.AppShare.SelectedStamp != null)
                {
                    var image = App.AppShare.Repositories.StampImageRepository.GetByStampId(App.AppShare.SelectedStamp.Id);
                    if (image != null)
                    {
                        if (image.Original != null)
                        {
                            selectedStampImage.Source = App.ToolHelper.ConvertByteArrayToBitmapImage(image.Original);
                        }
                        else
                        {
                            if (image.Hunting != null)
                            {
                                selectedStampImage.Source = App.ToolHelper.ConvertByteArrayToBitmapImage(image.Hunting);
                            }
                        }
                    }
                }
            }
        }

        private void tglStampDetailPanel_Click(object sender, RoutedEventArgs e)
        {
            if (tglStampDetailPanel.IsChecked.HasValue)
            {
                if (tglStampDetailPanel.IsChecked.Value)
                {
                    grdStampDetails.Visibility = Visibility.Visible;
                }
                else
                {
                    grdStampDetails.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void EventHelper_PublishedCountryListChanged(object sender, EventArgs e)
        {
            using (new WorkerHandler("EventHelper_PublishedCountryListChanged"))
            {
                cboFilterPublishedCountry.BeginInit();
                cboFilterPublishedCountry.ItemsSource = App.DataShare.PublishedCountryList;
                cboFilterPublishedCountry.DisplayMemberPath = "Name";
                cboFilterPublishedCountry.SelectedValuePath = "Id";
                cboFilterPublishedCountry.EndInit();
                DisplayCountries();
            }
        }

        private void EventHelper_ContinentListChanged(object sender, EventArgs e)
        {
            using (new WorkerHandler("EventHelper_ContinentListChanged"))
            {
                cboFilterContinent.BeginInit();
                cboFilterContinent.ItemsSource = App.DataShare.ContinentList;
                cboFilterContinent.DisplayMemberPath = "Value";
                cboFilterContinent.SelectedValuePath = "Id";
                cboFilterContinent.EndInit();
                DisplayCountries();
            }
        }

        private void EventHelper_CountryListChanged(object sender, EventArgs e)
        {
            using (new WorkerHandler("EventHelper_CountryListChanged"))
            {
                cboFilterCountry.BeginInit();
                cboFilterCountry.ItemsSource = App.DataShare.CountryList;
                cboFilterCountry.DisplayMemberPath = "Name";
                cboFilterCountry.SelectedValuePath = "Id";
                cboFilterCountry.EndInit();
                DisplayCountries();
            }
        }

        private void Continent_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var item = sender as TreeViewItem;
            if (item != null && item.Tag != null)
            {
                var continent = item.Tag as Entity.Continent;
                var country = item.Tag as Entity.Country;
                var pubCountry = item.Tag as Entity.PublishedCountry;
                if (continent != null)
                {
                    _filterContainer.SelectedContinent = continent;
                }
                if (country != null)
                {
                    _filterContainer.SelectedCountry = country;
                }
                if (pubCountry != null)
                {
                    _filterContainer.SelectedPublishedCountry = pubCountry;
                }
            }
        }
        private void _filterContainer_SelectedPublishedCountryChanged(object sender, XPublishedCountryEventArgs e)
        {
            using (new WorkerHandler("filterContainer_SelectedPublishedCountryChanged"))
            {
                SetFilterView();
            }
        }

        private void _filterContainer_SelectedCountryChanged(object sender, CountryEventArgs e)
        {
            using (new WorkerHandler("filterContainer_SelectedCountryChanged"))
            {
                SetFilterView();
            }
        }

        private void _filterContainer_SelectedContinentChanged(object sender, ContinentEventArgs e)
        {
            using (new WorkerHandler("filterContainer_SelectedContinentChanged"))
            {
                SetFilterView();
            }
        }
    }
}