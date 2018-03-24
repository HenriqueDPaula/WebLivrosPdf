using Front.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Front.Contexts
{
    public class ECommerceDbContext :DbContext
    {
        #region Properties

        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Sell> Sells { get; set; }
        public DbSet<SellItem> SellItems { get; set; }

        #endregion Properties

        public ECommerceDbContext()
            : base("Asp_Net_MVC_CS")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // codigo para e
            /* modelBuilder.Entity<Sell>()
                        .HasOptional(s => s.SellItems)
                        .WithOptionalDependent()
                        .WillCascadeOnDelete(true); */
                        
        }
    }
}