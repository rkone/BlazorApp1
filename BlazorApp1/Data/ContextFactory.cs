using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Data
{
    public class ContextFactory
    {
        private static DbContextOptions<SampleContext> _options;
        public ContextFactory(DbContextOptions<SampleContext> options)
        {
            _options = options;
        }

        public SampleContext Generate()
        {
            return new SampleContext(_options);
        }
    }
}
