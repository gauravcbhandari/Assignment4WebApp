using WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;

namespace WebApp.Repository
{
    public class EmployeeContext : DbContext
    {
        private readonly IConfiguration _config;

        private readonly string _connectionString;

        public EmployeeContext(DbContextOptions<EmployeeContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
            _connectionString = GetConnectionStringFromVault().GetAwaiter().GetResult();
        }
        public DbSet<Employee> Employee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connstring = _connectionString;

            optionsBuilder.UseSqlServer(connstring);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        private async Task<string> GetConnectionStringFromVault()
        {
            AzureServiceTokenProvider azureServiceTokenProvider =
               new AzureServiceTokenProvider();

            KeyVaultClient keyVaultClient =
            new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

            var secret = await keyVaultClient
                .GetSecretAsync(_config["DatabaseKey"])
                        .ConfigureAwait(false);

            return secret.Value;
        }
    }
}
