#nullable disable

using Microsoft.EntityFrameworkCore;

namespace GurpsCompanion.Shared.DataModel.DataContext
{
    public partial class DataContext
    {
        private readonly string _databaseConnection;

        public DataContext(string databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(_databaseConnection);
            }
        }
    }
}
