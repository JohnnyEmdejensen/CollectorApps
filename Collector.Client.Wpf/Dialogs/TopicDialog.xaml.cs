using Collector.Client.Wpf.Internals;
using Collector.Entity;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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
    /// Interaction logic for TopicDialog.xaml
    /// </summary>
    public partial class TopicDialog : Window
    {
        private Internals.FormSettingsClass _settings;
        private Entity.Topic? _topic;

        public TopicDialog()
        {
            InitializeComponent();
            _settings = new Internals.FormSettingsClass(this);
            ContentRendered += TopicDialog_ContentRendered;
            Closing += TopicDialog_Closing;
            KeyDown += TopicDialog_KeyDown;
            App.EventHelper.TopicListChanged += EventHelper_TopicListChanged;
            FillControls();
            _settings.Load();
        }

        private void FillControls()
        {
            using (new WorkerHandler("FillControls"))
            {
                FillTopicsControl();
            }
        }

        private void FillTopicsControl()
        {
            using (new WorkerHandler("FillTopicsControl"))
            {
                dgTopics.BeginInit();
                dgTopics.ItemsSource = App.DataShare.TopicList;
                dgTopics.EndInit();
            }
        }

        private void DoAdd()
        {
            _topic = new Entity.Topic();
            DataContext = _topic;
            SetGui();
        }
        private void DoSave(bool CloseAfter)
        {
            gsMain.Focus();
            _topic = DataContext as Entity.Topic;
            if (_topic != null)
            {
                if (_topic.Id == 0)
                {
                    _topic = App.AppShare.Repositories.TopicRepository.Create(_topic);
                }
                else
                {
                    _topic = App.AppShare.Repositories.TopicRepository.Update(_topic);
                }
                DataContext = _topic;
                SetGui();
            }
            if (CloseAfter)
            {
                Close();
            }
        }
        private void SetGui()
        {
            using (new WorkerHandler("SetGui"))
            {
                btnAdd.IsEnabled = false;
                btnSave.IsEnabled = false;
                btnDelete.IsEnabled = false;
                if (_topic != null)
                {
                    if (_topic.Id == 0)
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
        }
        private void TopicDialog_KeyDown(object sender, KeyEventArgs e)
        {
            using (new WorkerHandler("TopicDialog_KeyDown"))
            {
                if (e.Key == Key.Escape)
                {
                    Close();
                }
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                {
                    if (e.Key == Key.A)
                    {
                        DoAdd();
                    }
                    if (e.Key == Key.S)
                    {
                        DoSave(false);
                    }
                }
            }
        }

        private void TopicDialog_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            using (new WorkerHandler("TopicDialog_Closing"))
            {
                _settings.Save();
            }
        }

        private void TopicDialog_ContentRendered(object? sender, EventArgs e)
        {
            using (new WorkerHandler("TopicDialog_ContentRendered"))
            {
                _topic = DataContext as Entity.Topic;
                SetGui();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            using (new WorkerHandler("btnAdd_Click"))
            {
                DoAdd();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (new WorkerHandler("btnSave_Click"))
            {
                DoSave(false);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            using (new WorkerHandler("btnDelete_Click"))
            {

            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            using (new WorkerHandler("txtFilter_TextChanged"))
            {

            }
        }

        private void dgTopics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (new WorkerHandler("dgTopics_SelectionChanged"))
            {

            }
        }
        private void EventHelper_TopicListChanged(object sender, EventArgs e)
        {
            using (new WorkerHandler("EventHelper_TopicListChanged"))
            {
                FillTopicsControl();
            }
        }
    }
}
