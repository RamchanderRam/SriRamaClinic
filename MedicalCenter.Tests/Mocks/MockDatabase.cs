using MedicalCenter.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Tests.Mocks
{
    public static class MockDatabase
    {
        public static ApplicationDbContext Instance
        {
            get
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

                var db = new ApplicationDbContext(options);

                return db;
            }
        }
    }
}
