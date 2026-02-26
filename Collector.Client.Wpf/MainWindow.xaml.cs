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
            _settings.Load();
        }


        private void MainWindow_ContentRendered(object? sender, EventArgs e)
        {
            App.DataShare.Initialize();
        }
        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _settings.Save();
        }

        private void DgStamps_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            dgStamps.Columns[7].Width = dgStamps.ActualWidth - stampsViewFixedWidth;
        }

        private void tglStampview_Click(object sender, RoutedEventArgs e)
        {
            tglStampview.IsChecked = true;
            tglStockview.IsChecked = false;
            grdStampView.Visibility = Visibility.Visible;
            grdStockView.Visibility = Visibility.Collapsed;
        }

        private void tglStockview_Click(object sender, RoutedEventArgs e)
        {
            tglStampview.IsChecked = false;
            tglStockview.IsChecked = true;
            grdStampView.Visibility = Visibility.Collapsed;
            grdStockView.Visibility = Visibility.Visible;
        }
        private void EventHelper_TranslationSourceChanged(object sender, EventArgs e)
        {
            ResourceDictionary disc = Application.Current.Resources.MergedDictionaries[1];
            foreach (var columns in dgStamps.Columns)
            {
                var t = columns.Header.ToString();
            }
        }

        private void btnLanUK_Click(object sender, RoutedEventArgs e)
        {
            btnLanUK.IsChecked = true;
            btnLanDK.IsChecked = false;
            ResourceDictionary dict_enUK = new ResourceDictionary();
            ResourceDictionary dict_daDK = new ResourceDictionary();
            ResourceDictionary dict_Color = new ResourceDictionary();
            dict_enUK.Source = new Uri("Dictionary_ENUS.xaml", UriKind.Relative);
            dict_daDK.Source = new Uri("Dictionary_DADK.xaml", UriKind.Relative);
            dict_Color.Source = new Uri("Dictionary_COLOR.xaml", UriKind.Relative);
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dict_Color);
            Application.Current.Resources.MergedDictionaries.Add(dict_enUK);
            App.EventHelper.RaiseTranslationSourceChangedEvent();
        }

        private void btnLanDK_Click(object sender, RoutedEventArgs e)
        {
            btnLanUK.IsChecked = false;
            btnLanDK.IsChecked = true;
            ResourceDictionary dict_enUK = new ResourceDictionary();
            ResourceDictionary dict_daDK = new ResourceDictionary();
            ResourceDictionary dict_Color = new ResourceDictionary();
            dict_enUK.Source = new Uri("Dictionary_ENUS.xaml", UriKind.Relative);
            dict_daDK.Source = new Uri("Dictionary_DADK.xaml", UriKind.Relative);
            dict_Color.Source = new Uri("Dictionary_COLOR.xaml", UriKind.Relative);
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dict_Color);
            Application.Current.Resources.MergedDictionaries.Add(dict_daDK);
            App.EventHelper.RaiseTranslationSourceChangedEvent();
        }

        private void btnCountries_Click(object sender, RoutedEventArgs e)
        {
            App.FormHelper.ShowCountries(null);
        }
        private void btnCatalogs_Click(object sender, RoutedEventArgs e)
        {
            App.FormHelper.ShowCatalogs(null);
        }

        private void btnCategories_Click(object sender, RoutedEventArgs e)
        {
            App.FormHelper.ShowCategories(null);
        }
    }
}