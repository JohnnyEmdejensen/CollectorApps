using Collector.Client.Wpf.Internals;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Collector.Client.Wpf.Components
{
    /// <summary>
    /// Interaction logic for StockItemComponent.xaml
    /// </summary>
    public partial class StockItemComponent : UserControl
    {
        public delegate void StockItemHandler(object sender, StockItemEventArgs e);
        public event StockItemHandler? StockItemChanged;
        private Entity.Stock _stock;
        private Entity.Stock _backup;
        public StockItemComponent(Entity.Stock Stock)
        {
            _stock = Stock;
            _backup = new Entity.Stock();
            _backup.ConditionId = Stock.ConditionId;
            _backup.Id = Stock.Id;
            _backup.IsDeleted = Stock.IsDeleted;
            _backup.IsDirty = Stock.IsDirty;
            _backup.LocationId = Stock.LocationId;
            _backup.Quantity = Stock.Quantity;
            _backup.StampId = Stock.StampId;
            _backup.SupplierDealId = Stock.SupplierDealId;           
            InitializeComponent();
        }

        public void SetGui()
        {
            FillControls();
            DataContext = _stock;
        }

        private void FillControls()
        {
            using (new WorkerHandler("FillControls"))
            {
                FillConditionControl();
                FillLocationControl();
                FillDealControl();
            }
        }
        private void FillConditionControl()
        {
            using (new WorkerHandler("FillConditionControl"))
            {
                cboCondition.BeginInit();
                cboCondition.ItemsSource = App.DataShare.ConditionList;
                cboCondition.DisplayMemberPath = "Value";
                cboCondition.SelectedValuePath = "Id";
                cboCondition.EndInit();
            }
        }
        private void FillLocationControl()
        {
            using (new WorkerHandler("FillLocationControl"))
            {
                cboLocation.BeginInit();
                cboLocation.ItemsSource = App.DataShare.LocationList;
                cboLocation.DisplayMemberPath = "Value";
                cboLocation.SelectedValuePath = "Id";
                cboLocation.EndInit();
            }
        }
        private void FillDealControl()
        {
            using (new WorkerHandler("FillDealControl"))
            {
                cboDeal.BeginInit();
                cboDeal.ItemsSource = App.DataShare.SupplierDealList;
                cboDeal.DisplayMemberPath = "Value";
                cboDeal.SelectedValuePath = "Id";
                cboDeal.EndInit();
            }
        }
        private void btnAddQuantity_Click(object sender, RoutedEventArgs e)
        {
            int s = 1;
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                s = 10;
            }
            int q = 1;
            int.TryParse(txtQuantitiy.Text, out q);
            q += s;
            txtQuantitiy.Text = q.ToString();
            _stock.IsDirty = true;
            _stock.Quantity = q;
            StockItemChanged?.Invoke(this, new StockItemEventArgs(_stock));
        }

        private void btnSubQuantity_Click(object sender, RoutedEventArgs e)
        {
            int s = 1;
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                s = 10;
            }
            int q = 1;
            int.TryParse(txtQuantitiy.Text, out q);
            q -= s;
            if (q == 0) q = 1;
            txtQuantitiy.Text = q.ToString();
            _stock.IsDirty = true;
            _stock.Quantity = q;
            StockItemChanged?.Invoke(this, new StockItemEventArgs(_stock));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtQuantitiy_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_stock.Quantity != _backup.Quantity)
            {
                _stock.IsDirty = true;
                StockItemChanged?.Invoke(this, new StockItemEventArgs(_stock));
            }
        }

        private void cboCondition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_stock.ConditionId != _backup.ConditionId)
            {
                _stock.IsDirty = true;
                StockItemChanged?.Invoke(this, new StockItemEventArgs(_stock));
            }
        }

        private void cboDeal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_stock.SupplierDealId != _backup.SupplierDealId)
            {
                _stock.IsDirty = true;
                StockItemChanged?.Invoke(this, new StockItemEventArgs(_stock));
            }
        }

        private void cboLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_stock.LocationId != _backup.LocationId)
            {
                _stock.IsDirty = true;
                StockItemChanged?.Invoke(this, new StockItemEventArgs(_stock));
            }
        }
    }
    public class StockItemEventArgs : EventArgs
    {
        public Entity.Stock Stock { get; set; }
        public StockItemEventArgs(Entity.Stock stock  )
        {
            Stock = stock;
        }
    }
}
