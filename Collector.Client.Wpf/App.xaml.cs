using System.Configuration;
using System.Data;
using System.Windows;

namespace Collector.Client.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Internals.AppShareCLass AppShare = new Internals.AppShareCLass();
        public static Internals.DataShareClass DataShare = new Internals.DataShareClass();
        public static Internals.EventHelperClass EventHelper = new Internals.EventHelperClass();
        public static Internals.FormHelperClass FormHelper = new Internals.FormHelperClass();
        public static Internals.ToolHelperClass ToolHelper = new Internals.ToolHelperClass();
    }

}
