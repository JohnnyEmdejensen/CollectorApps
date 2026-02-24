using Collector.Data;

namespace Collector.Repository
{
    public class BaseRepository
    {
        public ApiContext ApiContext { get; set; }
        public BaseRepository()
        {
            ApiContextFactory factory = new ApiContextFactory();
            ApiContext = factory.CreateDbContext(null);
        }
    }
}
