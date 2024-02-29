using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witcher.Persistence;

namespace Witcher.Tests.Common
{
    public class EnvironmentContextFactory
    {
        public static Guid EnvironmentIdForDelete = new Guid();
        public static Guid EnvironmentIdForUpdate = new Guid();

        public static WitcherDbContext Create()
        {
            var options = new DbContextOptionsBuilder<WitcherDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new WitcherDbContext(options);
            context.Database.EnsureCreated();
            context.Environments.AddRange(
                new Domain.Environment
                {

                }
            );
            return context;
        }
    }
}
