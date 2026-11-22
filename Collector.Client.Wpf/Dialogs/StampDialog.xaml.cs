using Azure.Identity;
using Collector.Client.Wpf.Internals;
using Collector.Entity;
using System;
using System.Collections.Generic;
using System.IO;
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
        private List<Entity.Stock>? _stocks;
        private Entity.Stampimage? _imageRecord;
        private List<Components.StockItemComponent>? _StockItemList;
        private IEnumerable<TextBox>? textBoxes;

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
            App.EventHelper.PerforationListChanged += EventHelper_PerforationListChanged;
            App.EventHelper.ClasssListChanged += EventHelper_ClasssListChanged;
            App.EventHelper.TypeeListChanged += EventHelper_TypeeListChanged;
            App.AppShare.Repositories.StampImageRepository.StampImageChanged += StampImageRepository_StampImageChanged;
           
            FillControls();
            _settings.Load();
        }

        private void DoAdd()
        {
            _stamp = new Stamp();
            DataContext = _stamp;
            SetGui();
            txtNumber.Focus();
        }
        private void DoSavePrices()
        {
            using (new WorkerHandler("DoSavePrices"))
            {
                foreach (var item in grdPricesContainer.Children)
                {
                    if (item is Components.PriceItemComponent priceComponent)
                    {
                        var priceItem = priceComponent as Components.PriceItemComponent;
                        var priceData = priceItem.DataContext as Entity.StampPrice;
                        if (priceData != null)
                        {
                            if (priceData.Id == 0)
                            {
                                priceData = App.AppShare.Repositories.StampPriceRepository.Create(priceData);
                            }
                            else
                            {
                                priceData = App.AppShare.Repositories.StampPriceRepository.Update(priceData);
                            }
                            priceItem.DataContext = priceData;
                        }
                    }
                }
            }
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
        private void DoSaveStocks()
        {
            if (_stocks == null || _stocks.Count == 0) return;
            using (new WorkerHandler("DoSaveStocks"))
            {
                bool hasUpdated = false;
                bool hasCreated = false;
                App.AppShare.Repositories.StockRepository.SupressEvents = true;
                var news = _stocks.Where(x =>x.Id == 0).ToList();
                if (news.Any())
                {
                    hasCreated = true;
                    foreach (var item in news)
                    {
                        App.AppShare.Repositories.StockRepository.Create(item);
                        item.IsDirty = false;
                    }
                }
                var changed = _stocks.Where(x => x.IsDirty).ToList();
                if (changed.Any())
                {
                    hasUpdated = true;
                    foreach (var item in changed)
                    {
                        App.AppShare.Repositories.StockRepository.Update(item);
                        item.IsDirty = false;
                    }                    
                }
                App.AppShare.Repositories.StockRepository.SupressEvents = false;
                if (hasCreated)
                {
                    GetStocks();
                }
            }
        }
        private void DoAddStockItem(Entity.Stock Stock)
        {
            if (_StockItemList == null) 
                _StockItemList = new List<Components.StockItemComponent>();
            Components.StockItemComponent item = new Components.StockItemComponent(Stock);
            item.Height = 20;
            item.SetGui();
            spContainer.Children.Add(item);
            Label lbl = new Label();
            lbl.Height = 4;
            spContainer.Children.Add(lbl);
            _StockItemList.Add(item);
            item.StockItemChanged += Item_StockItemChanged;
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
                FillStockControl();
                FillPerforationControls();
                FillClasses();
                FillTypees();
            }
        }

        private void FillPerforationControls()
        {
            using (new WorkerHandler("FillPerforationControls"))
            {
                cboPerforationH.BeginInit();
                cboPerforationH.ItemsSource = App.DataShare.PerforationList;
                cboPerforationH.DisplayMemberPath = "Value";
                cboPerforationH.SelectedValuePath = "Id";
                cboPerforationH.EndInit();
                cboPerforationV.BeginInit();
                cboPerforationV.ItemsSource = App.DataShare.PerforationList;
                cboPerforationV.DisplayMemberPath = "Value";
                cboPerforationV.SelectedValuePath = "Id";
                cboPerforationV.EndInit();
            }
        }
        private void FillClasses()
        {
            using (new WorkerHandler("FillClasses"))
            {
                cboClasses.BeginInit();
                cboClasses.ItemsSource = App.DataShare.ClasssList;
                cboClasses.DisplayMemberPath = "Value";
                cboClasses.SelectedValuePath = "Id";
                cboClasses.EndInit();
            }
        }
        private void FillTypees()
        {
            using (new WorkerHandler("FillTypees"))
            {
                cboTypes.BeginInit();
                cboTypes.ItemsSource = App.DataShare.TypeeList;
                cboTypes.DisplayMemberPath = "Value";
                cboTypes.SelectedValuePath = "Id";
                cboTypes.EndInit();
            }
        }

        private void FillStockControl()
        {
            using (new WorkerHandler("FillStockControl"))
            {
                if (_stocks != null && _stocks.Count > 0)
                {
                    spContainer.Children.Clear();   
                    foreach (var stock in _stocks)
                    {
                        DoAddStockItem(stock);
                    }
                }
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
        public void GetStampImages()
        {
            if (_stamp == null || _stamp.Id == 0) return;
            using (new WorkerHandler("GetStampImages"))
            {
                _imageRecord = App.AppShare.Repositories.StampImageRepository.GetByStampId(_stamp.Id);
                if (_imageRecord != null)
                {
                    spImagePreviewContainer.Children.Clear();
                    if (_imageRecord.Hunting != null)
                    {
                        var huntingSource = App.ToolHelper.ConvertByteArrayToBitmapImage(_imageRecord.Hunting);
                        if (huntingSource != null)
                        {
                            Image imageCtrl = new Image();
                            imageCtrl.Margin = new Thickness(0, 0, 8, 0);
                            imageCtrl.Source = huntingSource;
                            imageCtrl.MouseDown += ImageCtrl_MouseDown;
                            spImagePreviewContainer.Children.Add(imageCtrl);
                            imgDialog.BeginInit();
                            imgDialog.Source = huntingSource;
                            imgDialog.EndInit();
                        }
                    }
                    if (_imageRecord.Original != null)
                    {
                        var originalSource = App.ToolHelper.ConvertByteArrayToBitmapImage(_imageRecord.Original);
                        if (originalSource != null)
                        {
                            Image imageCtrl = new Image();
                            imageCtrl.Margin = new Thickness(0, 0, 8, 0);
                            imageCtrl.Source = originalSource;
                            imageCtrl.MouseDown += ImageCtrl_MouseDown;
                            spImagePreviewContainer.Children.Add(imageCtrl);
                        }
                    }
                }
            }
            SetStampImageGui();
        }

        private void ImageCtrl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            var image = sender as Image;
            if (image != null && image.Source != null)
            {
                imgBig.Source = image.Source;
            }
        }

        public void GetStocks()
        {
            _stamp = DataContext as Entity.Stamp;
            if (_stamp == null) return;
            _stocks = App.AppShare.Repositories.StockRepository.GetByStamp(_stamp.Id);
            FillStockControl();
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
        private void SetStockGui()
        {
            btnSaveStock.IsEnabled = false;
            btnAddStock.IsEnabled = false;
            if (_stocks == null) return;
            var news = _stocks.Where(x => x.Id == 0).ToList();
            if (news.Any())
            {
                btnSaveStock.IsEnabled = true;
            }
            else
            {
                btnAddStock.IsEnabled= true;
            }
        }
        private void SetStampImageGui()
        {
            btnAddImage.IsEnabled= true;
            btnOpenImage.IsEnabled = true;
            btnDeleteImage.IsEnabled = false;
            if (_imageRecord != null)
            {
                if (_imageRecord.Id > 0)
                {
                    btnDeleteImage.IsEnabled = true;
                }
            }
        }
        private void StampDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                e.Handled = true;
                Close();
            }
            if (e.Key == Key.F12)
            {
                e.Handled = true;
                tabItemPrices.IsSelected = true;
                if (grdPricesContainer.Children.Count > 0)
                {
                    var firstItem = grdPricesContainer.Children[0] as Components.PriceItemComponent;
                    if (firstItem != null)
                    {
                        firstItem.SetFocus();
                    }
                } 
            }
            if (e.Key == Key.F11)
            {
                e.Handled = true;
                tabItemImages.IsSelected = true;
            }
            if (e.Key == Key.F9)
            {
                e.Handled = true;
                tabItemStock.IsSelected = true;
            }
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                if (e.Key == Key.S)
                {
                    e.Handled = true;
                    btnSave.Focus();
                    DoSave(false);
                }
                if (e.Key == Key.N)
                {
                    e.Handled = true;
                    btnAdd.Focus();
                    
                }
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                {
                    if (e.Key == Key.S)
                    {
                        e.Handled = true;
                        btnSave.Focus();
                        DoSave(true);
                    }
                }
            }
        }

        private void StampDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            App.EventHelper.RaiseStampDialogClosedEvent();
            _settings.Save();
        }

        private void StampDialog_ContentRendered(object? sender, EventArgs e)
        {
            textBoxes = App.ToolHelper.FindVisualChildren<TextBox>(this);
            foreach (var TextBoxControl in textBoxes)
            {
                TextBoxControl.GotFocus += TextBoxControl_GotFocus;
            }
            _stamp = DataContext as Entity.Stamp;
            if (_stamp != null)
            {
                SetupPrices();
            }
            SetGui();
            if (_stamp != null && _stamp.IsCloned)
            {
                txtNumber.Focus();
            }
        }

        private void SetupPrices()
        {
            if (_stamp != null && App.DataShare.ConditionList != null && App.DataShare.StampPriceList != null)
            {
                grdPricesContainer.Children.Clear();
                foreach (var condition in App.DataShare.ConditionList)
                {
                    var priceItem = App.DataShare.StampPriceList.FirstOrDefault(x => x.StampId == _stamp.Id && x.ConditionId == condition.Id);
                    Components.PriceItemComponent priceComponent = new Components.PriceItemComponent(_stamp.Id, condition.Id, priceItem?.Price ?? 0);
                    grdPricesContainer.Children.Add(priceComponent);
                    Label lbl = new Label();
                    lbl.Height = 4;
                    grdPricesContainer.Children.Add(lbl);
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DoAdd();
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
        private void EventHelper_TypeeListChanged(object sender, EventArgs e)
        {
            using (new WorkerHandler("EventHelper_TypeeListChanged"))
            {
                FillTypees();
            }
        }

        private void EventHelper_ClasssListChanged(object sender, EventArgs e)
        {
            using (new WorkerHandler("EventHelper_ClasssListChanged"))
            {
                FillClasses();
            }
        }

        private void EventHelper_TopicListChanged(object sender, EventArgs e)
        {
            using (new WorkerHandler("EventHelper_TopicListChanged"))
            {
                FillTopicControl();
            }
        }
        private void EventHelper_PerforationListChanged(object sender, EventArgs e)
        {
            using (new WorkerHandler("EventHelper_PerforationListChanged"))
            {
                FillPerforationControls();
            }
        }

        private void lblColor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            App.FormHelper.ShowColorsDialog(new());
        }

        private void btnAddStock_Click(object sender, RoutedEventArgs e)
        {
            if (_stamp == null || _stamp.Id == 0) return;
            using (new WorkerHandler("btnAddStock_Click"))
            {
                Entity.Stock stock = new Entity.Stock() { Quantity = 1, ConditionId = -1, LocationId = -1, SupplierDealId = -1, StampId = _stamp.Id };
                if (_stocks == null) _stocks = new List<Entity.Stock>();
                _stocks.Add(stock);
                DoAddStockItem(stock);
                SetStockGui();
            }
        }

        private void btnSaveStock_Click(object sender, RoutedEventArgs e)
        {
            using (new WorkerHandler("btnSaveStock_Click"))
            {
                DoSaveStocks();
            }
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            using (new WorkerHandler("btnAddImage_Click"))
            {
                if (Clipboard.ContainsImage() )
                {
                    if (_stamp != null && _stamp.Id > 0)
                    {
                        BitmapSource bitmapSource = Clipboard.GetImage();
                        PngBitmapEncoder encoder = new PngBitmapEncoder();
                        MemoryStream memoryStream = new MemoryStream();
                        BitmapImage bitmapImage = new BitmapImage();
                        encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                        encoder.Save(memoryStream);
                        memoryStream.Position = 0;
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = memoryStream;
                        bitmapImage.EndInit();
                        var data = App.ToolHelper.ConvertImageToByteArray(bitmapImage);
                        if (data != null)
                        {
                            if (_imageRecord == null)
                            {
                                _imageRecord = new Stampimage
                                {
                                    StampId = _stamp.Id,
                                    Hunting = data,
                                };
                            }
                            else
                            {
                                if (_imageRecord.Hunting == null && _imageRecord.Original == null)
                                {
                                    _imageRecord = new Stampimage
                                    {
                                        StampId = _stamp.Id,
                                        Hunting = data,
                                    };
                                }
                                else
                                {
                                    if (_imageRecord.Original == null && _imageRecord.Hunting != null)
                                    {
                                        var title = Application.Current.Resources["messagebox.title.hashuntingimage"];
                                        var message = Application.Current.Resources["messagebox.message.stampimagehashuntingsetoriginal"];
                                        if (MessageBox.Show(message.ToString(), title.ToString(), MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            _imageRecord.Original = data;
                                        }
                                    }
                                    if (_imageRecord.Hunting == null)
                                    {
                                        _imageRecord = new Stampimage
                                        {
                                            StampId = _stamp.Id,
                                            Hunting = data,
                                        };
                                    }
                                }
                            }
                            if (_imageRecord.Id == 0)
                            {
                                _imageRecord = App.AppShare.Repositories.StampImageRepository.Create(_imageRecord);
                            }
                            else
                            {
                                _imageRecord = App.AppShare.Repositories.StampImageRepository.Update(_imageRecord);
                            }
                        }
                    }
                }
                else
                {
                    var title = Application.Current.Resources["messagebox.title.clipboard"];
                    var message = Application.Current.Resources["messagebox.message.noimageinclipboard"];
                    MessageBox.Show(message.ToString(), title.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
        private void lblPerforations_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (new WorkerHandler("lblPerforations_MouseDown"))
            {
                App.FormHelper.ShowPerforationDialog(new());
            }
        }
        private void Item_StockItemChanged(object sender, Components.StockItemEventArgs e)
        {
            btnSaveStock.IsEnabled = false;
            if (e.Stock.IsDirty)
            {
                btnSaveStock.IsEnabled = true;
            }
        }
        private void StampImageRepository_StampImageChanged(object sender, Repository.StampImageEventArgs e)
        {
            using (new WorkerHandler("StampImageRepository_StampImageChanged"))
            {
                GetStampImages();
            }
        }

        private void lblClass_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (new WorkerHandler("lblClass_MouseDown"))
            {
                App.FormHelper.ShowClasssDialog(new());
            }
        }

        private void lblType_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (new WorkerHandler("lblType_MouseDown"))
            {
                App.FormHelper.ShowTypeeDialog(new());
            }

        }

        private void btnSavePrices_Click(object sender, RoutedEventArgs e)
        {
            DoSavePrices();
        }
        private void TextBoxControl_GotFocus(object sender, RoutedEventArgs e)
        {
            var txt = sender as TextBox;
            if (txt != null)
            {
                txt.SelectAll();
            }
        }
    }
}
