using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1
{
    public class MyContext:DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) 
            :base(options){ }

        public DbSet<Lwdz> Lwdzs { get; set; }
    }

    
}
