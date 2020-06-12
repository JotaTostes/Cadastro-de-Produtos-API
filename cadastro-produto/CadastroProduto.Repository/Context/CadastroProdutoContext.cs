using CadastroProduto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CadastroProduto.Repository.Context
{
    public class CadastroProdutoContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }


        public CadastroProdutoContext()
        { }

        public CadastroProdutoContext(DbContextOptions<CadastroProdutoContext> options) : base(options)
        {
            Database.SetCommandTimeout(180);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        /// <summary>
        /// Recupera a conexão com banco de dados
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                .UseMySql(config.GetSection("ConnectionStrings")["DefaultConnection"]);
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
