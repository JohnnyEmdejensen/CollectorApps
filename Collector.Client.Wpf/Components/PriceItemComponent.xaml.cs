using Collector.Client.Wpf.Internals;
using Microsoft.Identity.Client;
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
    /// Interaction logic for PriceItemComponent.xaml
    /// </summary>
    public partial class PriceItemComponent : UserControl
    {
        Entity.StampPrice _stamprice;
        public PriceItemComponent(long stampId, long conditionId, double price)
        {
            _stamprice = new Entity.StampPrice();
            _stamprice.StampId = stampId;
            _stamprice.ConditionId = conditionId;
            _stamprice.Price = price;
            InitializeComponent();
            FillConditionControl();
            DataContext = _stamprice;
        }

        private void FillConditionControl()
        {
            using (new WorkerHandler("FillConditionControl"))
            {
                var list = App.DataShare.ConditionList;
                if (list != null)
                {
                    list = list.OrderBy(x => x.Sort).ToList();
                }
                cboCondition.BeginInit();
                cboCondition.ItemsSource = list;
                cboCondition.DisplayMemberPath = "Value";
                cboCondition.SelectedValuePath = "Id";
                cboCondition.EndInit();
            }
        }
    }
}
