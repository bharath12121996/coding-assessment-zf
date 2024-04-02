using CodingAssessment.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

namespace WebApi.EndToEndTests
{
    /// <summary>
    /// 
    /// </summary>
    public class DataBaseFixture : IDisposable
    {
        private bool _isDisposed;

        public CodingAssessment.Infrastructure.DbContext DbContext { get; set; }

        public DataBaseFixture()
        {
            var options = new DbContextOptionsBuilder<CodingAssessment.Infrastructure.DbContext>()
                .UseInMemoryDatabase(databaseName: "inmemdb")
                .Options;

            DbContext = new CodingAssessment.Infrastructure.DbContext(options);
            DbContext.Database.EnsureCreated();

        }

        public void Dispose()
        {
            DbContext?.Dispose();
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

        ~DataBaseFixture()
        {
            Dispose(false);
        }
    }
}
