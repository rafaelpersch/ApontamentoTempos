using ApontamentoTempos.API.Model;
using ApontamentoTempos.API.Tools;
using Microsoft.EntityFrameworkCore;

namespace ApontamentoTempos.API.Data
{
    public class MyDbContext : DbContext
    {
        private string connectionString;

        public MyDbContext()
        {
            this.connectionString = "Host=localhost;Database=tempos;Username=postgres;Password=217799";
        }

        public MyDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<RecuperacaoSenha> RecuperacaoSenhas { get; set; }
        public DbSet<ApontamentoTempo> ApontamentoTempos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseNpgsql(this.connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /*builder.Entity<RefreshToken>()
                 .HasOne(a => a.Usuario)
                 .WithMany(c => c.RefreshTokens);*/

            foreach (var entity in builder.Model.GetEntityTypes())
            {
                // Replace table names
                entity.Relational().TableName = entity.Relational().TableName.ToSnakeCase();

                // Replace column names            
                foreach (var property in entity.GetProperties())
                {
                    property.Relational().ColumnName = property.Name.ToSnakeCase();
                }

                foreach (var key in entity.GetKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.Relational().Name = index.Relational().Name.ToSnakeCase();
                }
            }
        }
    }
}
