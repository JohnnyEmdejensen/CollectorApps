using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Collector.Data
{

    public class ApiContextFactory : IDesignTimeDbContextFactory<ApiContext>
    {
        public ApiContext CreateDbContext(string[]? args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApiContext>();
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Collector;Trusted_Connection=True;TrustServerCertificate=True;");
            return new ApiContext(optionsBuilder.Options);
        }
    }
}
