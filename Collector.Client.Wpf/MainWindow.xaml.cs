using Collector.Client.Wpf.Internals;
using System.IO;
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
        public MainWindow()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += MainWindow_ContentRendered;
            Closing += MainWindow_Closing;
            dgStamps.SizeChanged += DgStamps_SizeChanged;
            App.EventHelper.TranslationSourceChanged += EventHelper_TranslationSourceChanged;
            App.EventHelper.StampModelListChanged += EventHelper_StampModelListChanged;
            _settings.Load();
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

        }
    }
}