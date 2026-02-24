using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Collector.Client.Wpf.Internals
{
    public class FormSettingsClass
    {
        private Window _parent;
        public FormSettingsClass(Window Parent)
        {
            _parent = Parent;
            DirectoryInfo dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Emdesoft Developments\\Collector");
            if (!dir.Exists) dir.Create();
        }

        public void Load()
        {
            FileInfo file = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Emdesoft Developments\\Collector\\" + _parent.Tag.ToString() + ".json");
            if (file.Exists)
            {
                string content = "";
                using (StreamReader sr = new StreamReader(file.FullName))
                {
                    content = sr.ReadToEnd();
                    sr.Close();
                }
                var info = Newtonsoft.Json.JsonConvert.DeserializeObject<FormInfo>(content);
                if (info != null)
                {
                    if (!info.IsMaxed)
                    {
                        _parent.Top = info.Top;
                        _parent.Left = info.Left;
                        _parent.Width = info.Width;
                        _parent.Height = info.Height;
                    }
                    else
                    {
                        _parent.WindowState = WindowState.Maximized;
                    }
                }
            }
        }
        public void Save()
        {
            FileInfo file = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Emdesoft Developments\\Collector\\" + _parent.Tag.ToString() + ".json");
            FormInfo info = new FormInfo();
            info.Top = _parent.Top;
            info.Left = _parent.Left;
            info.Width = _parent.Width;
            info.Height = _parent.Height;
            info.IsMaxed = _parent.WindowState == WindowState.Maximized;
            string content = Newtonsoft.Json.JsonConvert.SerializeObject(info);
            using (StreamWriter sw = new StreamWriter(file.FullName))
            {
                sw.Write(content);
                sw.Flush();
                sw.Close();
            }
        }
    }

    public class FormInfo
    {
        public double Top { get; set; }
        public double Left { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool IsMaxed { get; set; }
    }
}
