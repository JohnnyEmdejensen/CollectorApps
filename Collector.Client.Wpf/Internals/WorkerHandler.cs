using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Collector.Client.Wpf.Internals
{
    public class WorkerHandler : IDisposable
    {
        private bool disposedValue;
        private Cursor _cursor;
        private WorkerItem _item;
        public WorkerHandler(string Name) 
        {
            _item = new WorkerItem(Name, WorkerItemStatus.Startet);
            App.DataShare.WorkerItems.Add(_item);
            _cursor = Mouse.OverrideCursor;
            Mouse.OverrideCursor = Cursors.Wait;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }
                disposedValue = true;
            }
            App.DataShare.WorkerItems.Add(new WorkerItem(_item.Name, WorkerItemStatus.Ended));
            Mouse.OverrideCursor = _cursor;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
