using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individuellt_programmeringsprojekt
{
    public class BibliotekContext : DbContext
    {
            public DbSet<Bibliotek> bibliotek { get; set; }

            public BibliotekContext() : base("name=UserDatabaseConnectionString")
            {

            }
        
    }
}
