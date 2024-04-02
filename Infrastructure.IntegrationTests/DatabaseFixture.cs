using Microsoft.EntityFrameworkCore;
using CodingAssessment.Infrastructure;
using System;

namespace Infrastructure.IntegrationTests
{
    public class DatabaseFixture: IDisposable
    {
        private bool _isDisposed;
        //public readonly IConfiguration Configuration;

        public CodingAssessment.Infrastructure.DbContext DbContext { get; set; }

        public DatabaseFixture()
        {

            var options = new DbContextOptionsBuilder<CodingAssessment.Infrastructure.DbContext>()
                .UseInMemoryDatabase("in-memory", b => b.EnableNullChecks(false))
                //.UseSqlServer(connectionString)
                .Options;

            DbContext = new CodingAssessment.Infrastructure.DbContext(options);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;


            if (disposing)
            {
                DbContext.Dispose();
            }

            _isDisposed = true;
        }

        ~DatabaseFixture()
        {
            Dispose(false);
        }
    }
}
