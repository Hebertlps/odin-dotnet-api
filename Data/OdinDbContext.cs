using Microsoft.EntityFrameworkCore;
using OdinApi.Models;

namespace OdinApi.Data
{
    public class OdinDbContext : DbContext
    {
        public OdinDbContext(DbContextOptions<OdinDbContext> options) : base(options)
        {
        }

        public DbSet<Operador> Operadores { get; set; }
        public DbSet<Satelite> Satelites { get; set; }
        public DbSet<Detrito> Detritos { get; set; }
        public DbSet<Manobra> Manobras { get; set; }
        public DbSet<Alerta> Alertas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relacionamentos
            modelBuilder.Entity<Satelite>()
                .HasOne(s => s.Operador)
                .WithMany(o => o.Satelites)
                .HasForeignKey(s => s.OperadorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Detrito>()
                .HasOne(d => d.Operador)
                .WithMany(o => o.Detritos)
                .HasForeignKey(d => d.OperadorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Manobra>()
                .HasOne(m => m.Satelite)
                .WithMany(s => s.Manobras)
                .HasForeignKey(m => m.SateliteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Manobra>()
                .HasOne(m => m.Operador)
                .WithMany(o => o.Manobras)
                .HasForeignKey(m => m.OperadorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Alerta>()
                .HasOne(a => a.Satelite)
                .WithMany(s => s.Alertas)
                .HasForeignKey(a => a.SateliteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Alerta>()
                .HasOne(a => a.Detrito)
                .WithMany(d => d.Alertas)
                .HasForeignKey(a => a.DebitoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed inicial de dados
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Operadores
            modelBuilder.Entity<Operador>().HasData(
                new Operador { Id = 1, Nome = "Marcus Vinícius", Email = "marcus@odin.local", NivelAcesso = "ADMIN", DataCriacao = DateTime.UtcNow },
                new Operador { Id = 2, Nome = "Hebert Lopes", Email = "hebert@odin.local", NivelAcesso = "OPERADOR", DataCriacao = DateTime.UtcNow },
                new Operador { Id = 3, Nome = "Nicolas Monteiro", Email = "nicolas@odin.local", NivelAcesso = "OPERADOR", DataCriacao = DateTime.UtcNow },
                new Operador { Id = 4, Nome = "Ana Silva", Email = "ana@odin.local", NivelAcesso = "USUARIO", DataCriacao = DateTime.UtcNow },
                new Operador { Id = 5, Nome = "Carlos Santos", Email = "carlos@odin.local", NivelAcesso = "USUARIO", DataCriacao = DateTime.UtcNow }
            );

            // Satelites
            modelBuilder.Entity<Satelite>().HasData(
                new Satelite { Id = 1, Nome = "INSAT-3D", CombustivelAtual = 500m, StatusOperacional = "ATIVO", DataLancamento = new DateTime(2013, 9, 26), OperadorId = 1 },
                new Satelite { Id = 2, Nome = "CBERS-4", CombustivelAtual = 450m, StatusOperacional = "ATIVO", DataLancamento = new DateTime(2014, 12, 4), OperadorId = 2 },
                new Satelite { Id = 3, Nome = "Amazonia-1", CombustivelAtual = 380m, StatusOperacional = "ATIVO", DataLancamento = new DateTime(2021, 2, 28), OperadorId = 3 },
                new Satelite { Id = 4, Nome = "SGDC-1", CombustivelAtual = 420m, StatusOperacional = "ATIVO", DataLancamento = new DateTime(2017, 5, 9), OperadorId = 1 },
                new Satelite { Id = 5, Nome = "BRASILSAT-B2", CombustivelAtual = 350m, StatusOperacional = "MANUTENCAO", DataLancamento = new DateTime(1998, 3, 4), OperadorId = 2 }
            );

            // Detritos
            modelBuilder.Entity<Detrito>().HasData(
                new Detrito { Id = 1, Identificacao = "DEB-001", Latitude = 0m, Longitude = 79.5m, Altitude = 36000m, Velocidade = 3.07m, NivelRisco = 45, OperadorId = 1, DataDeteccao = DateTime.UtcNow },
                new Detrito { Id = 2, Identificacao = "DEB-002", Latitude = 15.5m, Longitude = 93.5m, Altitude = 35786m, Velocidade = 3.08m, NivelRisco = 65, OperadorId = 2, DataDeteccao = DateTime.UtcNow },
                new Detrito { Id = 3, Identificacao = "DEB-003", Latitude = -10.2m, Longitude = 110.3m, Altitude = 800m, Velocidade = 7.45m, NivelRisco = 85, OperadorId = 3, DataDeteccao = DateTime.UtcNow },
                new Detrito { Id = 4, Identificacao = "DEB-004", Latitude = 28.5m, Longitude = 77.2m, Altitude = 400m, Velocidade = 7.82m, NivelRisco = 90, OperadorId = 1, DataDeteccao = DateTime.UtcNow },
                new Detrito { Id = 5, Identificacao = "DEB-005", Latitude = 5.8m, Longitude = 102.7m, Altitude = 600m, Velocidade = 7.65m, NivelRisco = 55, OperadorId = 2, DataDeteccao = DateTime.UtcNow }
            );

            // Manobras
            modelBuilder.Entity<Manobra>().HasData(
                new Manobra { Id = 1, SateliteId = 1, OperadorId = 1, Tipo = "DESVIO", Status = "EXECUTADA", DataSolicitacao = DateTime.UtcNow.AddDays(-1), DataExecucao = DateTime.UtcNow, CombustivelConsumido = 5m },
                new Manobra { Id = 2, SateliteId = 2, OperadorId = 2, Tipo = "ACELERACAO", Status = "EXECUTADA", DataSolicitacao = DateTime.UtcNow.AddDays(-2), DataExecucao = DateTime.UtcNow.AddDays(-1), CombustivelConsumido = 3m },
                new Manobra { Id = 3, SateliteId = 3, OperadorId = 3, Tipo = "DESACELERACAO", Status = "PENDENTE", DataSolicitacao = DateTime.UtcNow, DataExecucao = null, CombustivelConsumido = 0m },
                new Manobra { Id = 4, SateliteId = 4, OperadorId = 1, Tipo = "DESVIO", Status = "CANCELADA", DataSolicitacao = DateTime.UtcNow.AddDays(-3), DataExecucao = null, CombustivelConsumido = 0m },
                new Manobra { Id = 5, SateliteId = 5, OperadorId = 2, Tipo = "ACELERACAO", Status = "EXECUTADA", DataSolicitacao = DateTime.UtcNow.AddDays(-5), DataExecucao = DateTime.UtcNow.AddDays(-4), CombustivelConsumido = 4m }
            );

            // Alertas
            modelBuilder.Entity<Alerta>().HasData(
                new Alerta { Id = 1, SateliteId = 1, DebitoId = 1, Severidade = "MEDIA", Mensagem = "Aproximação de detrito detectada", Status = "RESOLVIDO", DataCriacao = DateTime.UtcNow.AddDays(-1), DataResolucao = DateTime.UtcNow },
                new Alerta { Id = 2, SateliteId = 2, DebitoId = 2, Severidade = "ALTA", Mensagem = "Risco iminente de colisão", Status = "ATIVO", DataCriacao = DateTime.UtcNow, DataResolucao = null },
                new Alerta { Id = 3, SateliteId = 3, DebitoId = 3, Severidade = "CRITICA", Mensagem = "Alerta crítico - ação imediata necessária", Status = "ATIVO", DataCriacao = DateTime.UtcNow, DataResolucao = null },
                new Alerta { Id = 4, SateliteId = 4, DebitoId = 4, Severidade = "ALTA", Mensagem = "Trajetória de colisão confirmada", Status = "RESOLVIDO", DataCriacao = DateTime.UtcNow.AddDays(-2), DataResolucao = DateTime.UtcNow.AddDays(-1) },
                new Alerta { Id = 5, SateliteId = 5, DebitoId = 5, Severidade = "BAIXA", Mensagem = "Monitoramento de detrito em progresso", Status = "ATIVO", DataCriacao = DateTime.UtcNow.AddDays(-3), DataResolucao = null }
            );
        }
    }
}
