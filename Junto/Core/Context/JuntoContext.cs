namespace Junto.Core.Context
{
    using Junto.Core.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class JuntoContext : DbContext
    {
        public JuntoContext(DbContextOptions<JuntoContext> options)
         : base(options)
        {
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserClassMapping());
        }
    }
}
