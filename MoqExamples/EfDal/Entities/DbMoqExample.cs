namespace EfDal.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Diagnostics;

    public partial class DbMoqExample : DbContext
    {
        public DbMoqExample()
            : base("name=DbMoqExample")
        {

            Database.Log = x => Debug.WriteLine(x);

        }

        public virtual DbSet<MoqExample> MoqExamples { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoqExample>()
                .Property(e => e.Column3)
                .HasPrecision(18, 0);
        }
    }
}
