using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dot_Net_7_jumpstart.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        
       public DbSet<Character> Characters {get;set;}
       public DbSet<User> Users { get; set;}
    
    }
}