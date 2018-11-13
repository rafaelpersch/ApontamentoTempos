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
            // essa implementação é apenas para tempo de desenvolvimento para criar a base de teste

            this.connectionString = "Host=localhost;Database=tempos;Username=postgres;Password=217799";
        }

        public MyDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<RecuperacaoSenha> RecuperacoesSenha { get; set; }
        public DbSet<ApontamentoTempo> ApontamentosTempo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;");
            //optionsBuilder.UseMySql(@"Server=localhost;User Id=root;Password=;Database=test");
            optionsBuilder.UseNpgsql(this.connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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
