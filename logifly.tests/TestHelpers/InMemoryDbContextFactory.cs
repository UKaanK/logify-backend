using logifly.persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.InMemory; // Bu satırı ekleyi
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logifly.tests.TestHelpers
{
    public static class InMemoryDbContextFactory
    {
        public static LogiflyDbContext Create()
        {
            var options = new DbContextOptionsBuilder<LogiflyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // her test için farklı db
                .Options;

            return new LogiflyDbContext(options);
        }
    }
}
