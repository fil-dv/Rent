namespace Domain.Concrete
{
    using Domain.Entities;
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EFDbContext : DbContext
    {
        public EFDbContext()
            : base("name=EFDbContext")
        {
        }

        public virtual DbSet<Area> Areas { get; set; }
        //public virtual DbSet<AreaType> AreaType { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>()
                .Property(e => e.SquareArea)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Area>()
                .Property(e => e.MonthPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Area>()
                .Property(e => e.Latitude)
                .HasPrecision(16, 10);

            modelBuilder.Entity<Area>()
                .Property(e => e.Longitude)
                .HasPrecision(16, 10);
         
            modelBuilder.Entity<Photo>()
                .Property(e => e.Latitude)
                .HasPrecision(16, 10);

            modelBuilder.Entity<Photo>()
                .Property(e => e.Longitude)
                .HasPrecision(16, 10);
        }
    }
}
