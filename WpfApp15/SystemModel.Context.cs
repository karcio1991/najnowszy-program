//------------------------------------------------------------------------------
// <auto-generated>
//    Ten kod źródłowy został wygenerowany na podstawie szablonu.
//
//    Ręczne modyfikacje tego pliku mogą spowodować nieoczekiwane zachowanie aplikacji.
//    Ręczne modyfikacje tego pliku zostaną zastąpione w przypadku ponownego wygenerowania kodu.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp15
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BazaDanychOkazjaEntities2 : DbContext
    {
        public BazaDanychOkazjaEntities2()
            : base("name=BazaDanychOkazjaEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<C__EFMigrationsHistory> C__EFMigrationsHistory { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
