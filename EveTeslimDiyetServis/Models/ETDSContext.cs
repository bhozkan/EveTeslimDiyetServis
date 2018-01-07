using ETDS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace EveTeslimDiyetServis.Models
{
    public class ETDSContext:DbContext
    {
        public ETDSContext() : base("name=ETDS") { }
        public DbSet<Kullanici> Kullanici { get; set; }
        public DbSet<Il> Il { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Ogun> Ogun { get; set; }
        public DbSet<Paket> Paket { get; set; }
        public DbSet<Asci> Asci { get; set; }
        public DbSet<Kurye> Kurye { get; set; }
        public DbSet<Siparis> Siparis { get; set; }
        public DbSet<Yemek> Yemek { get; set; }
        public DbSet<YemekMenu> YemekMenu { get; set; }
        public DbSet<Diyetisyen> Diyetisyen { get; set; }
        public DbSet<SiparisDurum> SiparisDurum { get; set; }

        //database olusturuldugunda tablo adlarini cogul yapmamak icin gereken method
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Database.SetInitializer<ETDSContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}