using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Data
{
    public class SampleContext :DbContext
    {
        public SampleContext(DbContextOptions<SampleContext> options) : base(options)
        {
        }

        public async Task LongTask(CancellationToken cancellationToken = default)
        {
            await Database.ExecuteSqlRawAsync("WAITFOR DELAY '0:0:2'", cancellationToken);
        }
    }
}
