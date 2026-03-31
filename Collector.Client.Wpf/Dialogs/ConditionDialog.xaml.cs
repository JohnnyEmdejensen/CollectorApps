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
using System.Windows.Shapes;

namespace Collector.Client.Wpf.Dialogs
{
    /// <summary>
    /// Interaction logic for ConditionDialog.xaml
    /// </summary>
    public partial class ConditionDialog : Window
    {
        public Internals.FormSettingsClass _settings;
        public Entity.Condition? _condition;

        public ConditionDialog()
        {
            InitializeComponent();
            _settings = new FormSettingsClass(this);
            ContentRendered += ConditionDialog_ContentRendered;
            Closing += ConditionDialog_Closing;
            KeyDown += ConditionDialog_KeyDown;
            App.EventHelper.ConditionListChanged += EventHelper_ConditionListChanged;
            FillControls();
            _settings.Load();
        }


        private void FillControls()
        {
            using (new WorkerHandler("FillControls"))
            {
                FillConditionControl();
            }
        }
        private void FillConditionControl()
        {
            using (new WorkerHandler("FillConditionControl"))
            {
                dgConditions.BeginInit();
                dgConditions.ItemsSource = App.DataShare.ConditionList;
                dgConditions.EndInit();
            }
        }
        private void DoAdd()
        {
            _condition = new Entity.Condition();
            DataContext = _condition;
            SetGui();
            txtName.Focus();
        }
        private void DoSave(bool CloseAfter)
        {
            gsMain.Focus();
            _condition = DataContext as Entity.Condition;
            if (_condition != null)
            {
                if (_condition.Id == 0)
                {
                    _condition = App.AppShare.Repositories.ConditionRepository.Create(_condition);
                }
                else
                {
                    _condition = App.AppShare.Repositories.ConditionRepository.Update(_condition);
                }
                DataContext = _condition;
                SetGui();
            }
            if (CloseAfter)
            {
                Close();
            }
        }
        private void SetGui()
        {
            btnAdd.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnDelete.IsEnabled = false;
            if (_condition != null)
            {
                if (_condition.Id == 0)
                {
                    btnSave.IsEnabled = true;
                    txtName.Focus();
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
        private void ConditionDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                if (e.Key == Key.N)
                {
                    DoAdd();
                }
                if (e.Key == Key.S)
                {
                    DoSave(false);
                }
            }
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
            {
                if (e.Key == Key.S)
                {
                    DoSave(true);
                }
            }
        }

        private void ConditionDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            using (new WorkerHandler("ConditionDialog_Closing"))
            {
                _settings.Save();
            }
        }

        private void ConditionDialog_ContentRendered(object? sender, EventArgs e)
        {
            _condition = DataContext as Entity.Condition;
            SetGui();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DoAdd();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DoSave(false);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dgConditions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void EventHelper_ConditionListChanged(object sender, EventArgs e)
        {
            using (new WorkerHandler("EventHelper_ConditionListChanged"))
            {
                FillConditionControl();
            }
        }
    }
}
