using BookStore_Models.DBModels;using Microsoft.EntityFrameworkCore;using Microsoft.Extensions.Configuration;using System;using System.Collections.Generic;using System.Linq;using System.Text;using System.Threading.Tasks;namespace BookStore_Models.Data{    public class BookStoreDbContext : DbContext    {        protected readonly IConfiguration Configuration;        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options, IConfiguration configuration) : base(options)        {            Configuration = configuration;        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("BookStore_APIContext"));
        }

        public DbSet<Users> Users { get; set; }        public DbSet<AdminUsers> AdminUsers { get; set; }
        public DbSet<Shops> Shops { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Books> Books { get; set; }
    }

}
