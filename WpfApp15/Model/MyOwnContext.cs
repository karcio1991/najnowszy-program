
using System;

using System.Linq;

using Microsoft.EntityFrameworkCore;


namespace finalny_program_managementSystem
{
    using Microsoft.EntityFrameworkCore;
    using finalny_program_managementSystem.Model;


    public class MyOwnContext : DbContext
    {

        // Your context has been configured to use a 'Entities' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'WpfApp11_odpodstaw.Entities' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Entities' 
        // connection string in the application configuration file.
        public MyOwnContext(DbContextOptions<MyOwnContext> options)
            : base(options)
        {

        }



        public MyOwnContext() : base()
        {

        }


        //DbSet<IChangeOrder> changeOrders { get; }
        public DbSet<Users1> Users { get; set; }
        public DbSet<Products1> Products { get; set; }
        public DbSet<Customers1> Customers { get; set; }

        public DbSet<Categories1> Categories { get; set; }
        public DbSet<Orders1> Orders { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=BazaDanychOkazja;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            //modelBuilder.Entity<Categories>().HasKey(x => x.Id);


            modelBuilder.Entity<Products1>()
                .HasOne<Categories1>(x => x.Categories)
                .WithMany(y => y.Products);
          


            modelBuilder.Entity<Products1>()
                .HasOne<Orders1>(x=>x.Orders)
                .WithMany(z=>z.Products)
                .HasForeignKey(c=>c.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Orders1>()
            .HasOne<Users1>(x => x.Users)
            .WithMany(z => z.Orders)
            .HasForeignKey(c => c.UserssID)
            .OnDelete(DeleteBehavior.ClientSetNull);


            //zle
            /*
            modelBuilder.Entity<Products1>()
              .HasOne<Customers1>(x => x.Customers)
              .WithMany(z => z.Products)
              .HasForeignKey(o => o.CustomerssID)
              .OnDelete(DeleteBehavior.ClientSetNull);
            */

            //poprawna wersja 
            modelBuilder.Entity<Orders1>()
             .HasOne<Customers1>(x => x.Customers)
             .WithMany(z => z.Orders)
             .HasForeignKey(o => o.CustomerssID)
             .OnDelete(DeleteBehavior.ClientSetNull);

        }


        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}