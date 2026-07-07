using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Collector.Client.Wpf.Internals
{

    public class ToolHelperClass
    {
        private List<string> Months = new List<string>() { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};

        public string GetCatalogNumbers(Entity.Stamp Stamp)
        {
            string result = "";
            if (App.DataShare.CatalogList != null)
            {
                var catalog = App.DataShare.CatalogList.FirstOrDefault(x => x.Id == Stamp.CatalogId);
                if (catalog != null)
                {
                    result += catalog.Shortname+":" + Stamp.Number;
                }
                if (App.DataShare.StampNumberList != null)
                {
                    var numbers = App.DataShare.StampNumberList.Where(x => x.StampId == Stamp.Id).ToList();
                    foreach (var number in numbers)
                    {
                        var cat = App.DataShare.CatalogList.FirstOrDefault(x => x.Id == number.CatalogId);
                        if (cat != null)
                        {
                            result += " " + cat.Shortname + ":" + number.Number;
                        }
                    }
                }
            }
            return result;
        }
        public long GetContinentIdFromCountry(long CountryId)
        {
            long result = 0;
            if (App.DataShare.CountryList != null)
            {
                var country = App.DataShare.CountryList.FirstOrDefault(x => x.Id == CountryId);
                if (country != null && country.ContinentId.HasValue)
                {
                    result = country.ContinentId.Value;
                }
            }
            return result;
        }
        public long GetCountryByPublishedId(long PublishedCountryId)
        {
            long result = 0;
            if (App.DataShare.PublishedCountryList != null)
            {
                var published = App.DataShare.PublishedCountryList.FirstOrDefault(x => x.ContryId == PublishedCountryId);
                if (published != null)
                {
                    result = published.ContryId;   
                }
            }
            return result;
        }
        public string GetPublishedCountryName(long? PublishedCountryId)
        {
            if (!PublishedCountryId.HasValue) return string.Empty;
            string result = "";
            using (WorkerHandler worker = new WorkerHandler("GetPublishedCountryName"))
            {
                if (App.DataShare.PublishedCountryList != null)
                {
                    var pub = App.DataShare.PublishedCountryList.FirstOrDefault(x => x.Id == PublishedCountryId);
                    if (pub != null)
                    {
                        result = pub.Name;
                    }
                }
            }
            return result;
        }
        public string GetColorName(long? ColorId)
        {
            if (!ColorId.HasValue) return string.Empty;
            string result = "";
            using (WorkerHandler worker = new WorkerHandler("GetColorName"))
            {
                if (App.DataShare.ColorList != null)
                {
                    var color = App.DataShare.ColorList.FirstOrDefault(x => x.Id == ColorId);
                    if (color != null)
                    {
                        result = color.Value;
                    }
                }
            }
            return result;
        }
        public string GetCoinName(long? CoinId)
        {
            if (!CoinId.HasValue) return string.Empty;
            string result = "";
            using (WorkerHandler worker = new WorkerHandler("GetCoinName"))
            {
                if (App.DataShare.CoinList != null)
                {
                    var coin = App.DataShare.CoinList.FirstOrDefault(x => x.Id == CoinId);
                    if (coin != null)
                    {
                        result = coin.Value;
                    }
                }
            }
            return result;
        }

        public string GetStockQuantities(Entity.Stamp? Stamp)
        {
            string result = "0";
            if (Stamp == null) return result;
            var stocks = App.AppShare.Repositories.StockRepository.GetByStamp(Stamp.Id);
            if (stocks != null)
            {
                var value = stocks.Sum(x => x.Quantity);
                result = string.Format("{0:N0}", value);
            }
            return result;
        }
        public string GetStockValues(Entity.Stamp? Stamp)
        {
            string result = "0";
            double value = 0;
            if (Stamp == null) return result;
            var stocks = App.AppShare.Repositories.StockRepository.GetByStamp(Stamp.Id);
            if (stocks != null && App.DataShare.ConditionList != null && App.DataShare.StampPriceList != null)
            {
                foreach (var stock in stocks)
                {
                    var priceItem = App.DataShare.StampPriceList.FirstOrDefault(x => x.ConditionId == stock.ConditionId && x.StampId == stock.StampId);
                    if (priceItem != null)
                    {
                        value += (stock.Quantity * priceItem.Price);
                    }
                }
            }
            result = string.Format("{0:N2}", value);
            return result;
        }
        public string GetTypeName(long? TypeId)
        {
            string result = "";
            using (new WorkerHandler("GetTypeName"))
            {
                if (App.DataShare.TypeeList != null && TypeId.HasValue)
                {
                    var type = App.DataShare.TypeeList.FirstOrDefault(x => x.Id == TypeId);
                    if (type != null)
                    {
                        result = type.Value;
                    }
                }
            }
            return result;
        }
        public string GetClassName(long? ClassId)
        {
            string result = "";
            using (new WorkerHandler("GetClassName"))
            {
                if (App.DataShare.ClasssList != null && ClassId.HasValue)
                {
                    var cls = App.DataShare.ClasssList.FirstOrDefault(x => x.Id == ClassId);
                    if (cls != null)
                    {
                        result = cls.Value;
                    }
                }
            }
            return result;
        }
        public string GetPerforationName(long? PerforationId)
        {
            string result = "";
            using (new WorkerHandler("GetPerforationName"))
            {
                if (App.DataShare.PerforationList != null && PerforationId.HasValue)
                {
                    var per = App.DataShare.PerforationList.FirstOrDefault(x => x.Id == PerforationId);
                    if (per != null)
                    {
                        result = per.Value;
                    }
                }
            }
            return result;
        }
        public string GetTopicName(long? TopicId)
        {
            string result = "";
            using (new WorkerHandler("GetTopicName"))
            {
                if (App.DataShare.TopicList != null && TopicId.HasValue)
                {
                    var topic = App.DataShare.TopicList.FirstOrDefault(y => y.Id == TopicId);
                    if(topic != null)
                    {
                        result = topic.Value;
                    }
                }
            }
            return result;
        }
        public string GetCategoryName(long CategoryId)
        {
            string result = "";
            using (WorkerHandler worker = new WorkerHandler("GetCategoryName"))
            {
                if (App.DataShare.CategoryList != null)
                {
                    var category = App.DataShare.CategoryList.FirstOrDefault(x => x.Id == CategoryId);
                    if (category != null)
                    {
                        result = category.Value;
                    }
                }
            }
            return result;
        }
        public string GetCatalogName(long CatalogId)
        {
            string result = "";
            using (WorkerHandler worker = new WorkerHandler("GetCatalogName"))
            {
                if (App.DataShare.CatalogList != null)
                {
                    var catalog = App.DataShare.CatalogList.FirstOrDefault(x => x.Id == CatalogId);
                    if (catalog != null)
                    {
                        result = catalog.Value;
                    }
                }
            }
            return result;
        }
        public string FormatIssued(int Issued)
        {
            string result = "";
            string strIssued = Issued.ToString();
            using (WorkerHandler worker = new WorkerHandler("FormatIssued"))
            {
                string y = "";
                string m = "";
                string d = "";
                if (strIssued.Length == 4)
                {
                    y = strIssued;
                }
                if (strIssued.Length == 6)
                {
                    y = strIssued.Substring(0, 4);
                    m = Months[Convert.ToInt32(strIssued.Substring(4, 2))];
                }
                if (strIssued.Length == 8)
                {
                    y = strIssued.Substring(0, 4);
                    m = Months[Convert.ToInt32(strIssued.Substring(4, 2))];
                    d = strIssued.Substring(6, 2).TrimStart('0');
                }
                result = y;
                if (!string.IsNullOrEmpty(m)) result += " " + m;
                if (!string.IsNullOrEmpty(d)) result += " " + d;
            }
            return result;
        }
        public string GetContinentName(long? Id)
        {
            string result = "";
            using (WorkerHandler worker = new WorkerHandler("GetContinentName"))
            {
                if (App.DataShare.ContinentList != null)
                {
                    var continent = App.DataShare.ContinentList.FirstOrDefault(x => x.Id == Id);
                    if (continent != null)
                    {
                        result = continent.Value;
                    }
                }
            }
            return result;
        }

        public byte[] ConvertImageToByteArray(BitmapImage Image)
        {
            using (new WorkerHandler("ConvertImageToByteArray"))
            {
                MemoryStream memoryStream = new MemoryStream();
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(Image));
                encoder.Save(memoryStream);
                return memoryStream.ToArray();
            }
        }
        public BitmapImage ConvertByteArrayToBitmapImage(Byte[] bytes)
        {
            var stream = new MemoryStream(bytes);
            stream.Seek(0, SeekOrigin.Begin);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            return image;
        }
    }
}
