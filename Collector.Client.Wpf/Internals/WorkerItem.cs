using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Client.Wpf.Internals
{
    public enum WorkerItemStatus
    {
        Unknown,
        Startet,
        Ended
    }
    public class WorkerItem
    {
        public string Name { get; set; } = "";
        public WorkerItemStatus Status { get; set; } = WorkerItemStatus.Unknown;
        public DateTime Timer { get; set; }
        public WorkerItem(string name, WorkerItemStatus status)
        {
            Name = name;
            Status = status;
            Timer = DateTime.Now;
        }
    }
}
