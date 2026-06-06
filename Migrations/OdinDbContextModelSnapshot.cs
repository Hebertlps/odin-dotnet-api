using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using OdinApi.Data;

#nullable disable

namespace OdinApi.Migrations
{
    [DbContext(typeof(OdinDbContext))]
    partial class OdinDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            // Operador
            modelBuilder.Entity("OdinApi.Models.Operador", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<DateTime>("DataCriacao")
                    .HasColumnType("datetime2");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<string>("NivelAcesso")
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("nvarchar(20)");

                b.Property<string>("Nome")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.HasKey("Id");

                b.ToTable("Operadores");
            });

            // Satelite
            modelBuilder.Entity("OdinApi.Models.Satelite", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<decimal>("CombustivelAtual")
                    .HasColumnType("decimal(18,2)");

                b.Property<DateTime>("DataLancamento")
                    .HasColumnType("datetime2");

                b.Property<string>("Nome")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<int>("OperadorId")
                    .HasColumnType("int");

                b.Property<string>("StatusOperacional")
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("nvarchar(20)");

                b.HasKey("Id");

                b.HasIndex("OperadorId");

                b.ToTable("Satelites");
            });

            // Detrito
            modelBuilder.Entity("OdinApi.Models.Detrito", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<decimal>("Altitude")
                    .HasColumnType("decimal(18,2)");

                b.Property<DateTime>("DataDeteccao")
                    .HasColumnType("datetime2");

                b.Property<string>("Identificacao")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<decimal>("Latitude")
                    .HasColumnType("decimal(18,2)");

                b.Property<decimal>("Longitude")
                    .HasColumnType("decimal(18,2)");

                b.Property<int>("NivelRisco")
                    .HasColumnType("int");

                b.Property<int>("OperadorId")
                    .HasColumnType("int");

                b.Property<decimal>("Velocidade")
                    .HasColumnType("decimal(18,2)");

                b.HasKey("Id");

                b.HasIndex("OperadorId");

                b.ToTable("Detritos");
            });

            // Manobra
            modelBuilder.Entity("OdinApi.Models.Manobra", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<decimal>("CombustivelConsumido")
                    .HasColumnType("decimal(18,2)");

                b.Property<DateTime?>("DataExecucao")
                    .HasColumnType("datetime2");

                b.Property<DateTime>("DataSolicitacao")
                    .HasColumnType("datetime2");

                b.Property<int>("OperadorId")
                    .HasColumnType("int");

                b.Property<int>("SateliteId")
                    .HasColumnType("int");

                b.Property<string>("Status")
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("nvarchar(20)");

                b.Property<string>("Tipo")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.HasKey("Id");

                b.HasIndex("OperadorId");

                b.HasIndex("SateliteId");

                b.ToTable("Manobras");
            });

            // Alerta
            modelBuilder.Entity("OdinApi.Models.Alerta", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<DateTime>("DataCriacao")
                    .HasColumnType("datetime2");

                b.Property<DateTime?>("DataResolucao")
                    .HasColumnType("datetime2");

                b.Property<int>("DebitoId")
                    .HasColumnType("int");

                b.Property<string>("Mensagem")
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnType("nvarchar(500)");

                b.Property<int>("SateliteId")
                    .HasColumnType("int");

                b.Property<string>("Severidade")
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("nvarchar(20)");

                b.Property<string>("Status")
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("nvarchar(20)");

                b.HasKey("Id");

                b.HasIndex("DebitoId");

                b.HasIndex("SateliteId");

                b.ToTable("Alertas");
            });

            // Relationships
            modelBuilder.Entity("OdinApi.Models.Alerta", b =>
            {
                b.HasOne("OdinApi.Models.Detrito", "Detrito")
                    .WithMany("Alertas")
                    .HasForeignKey("DebitoId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("OdinApi.Models.Satelite", "Satelite")
                    .WithMany("Alertas")
                    .HasForeignKey("SateliteId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Detrito");

                b.Navigation("Satelite");
            });

            modelBuilder.Entity("OdinApi.Models.Detrito", b =>
            {
                b.HasOne("OdinApi.Models.Operador", "Operador")
                    .WithMany("Detritos")
                    .HasForeignKey("OperadorId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.Navigation("Operador");
            });

            modelBuilder.Entity("OdinApi.Models.Manobra", b =>
            {
                b.HasOne("OdinApi.Models.Operador", "Operador")
                    .WithMany("Manobras")
                    .HasForeignKey("OperadorId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.HasOne("OdinApi.Models.Satelite", "Satelite")
                    .WithMany("Manobras")
                    .HasForeignKey("SateliteId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Operador");

                b.Navigation("Satelite");
            });

            modelBuilder.Entity("OdinApi.Models.Satelite", b =>
            {
                b.HasOne("OdinApi.Models.Operador", "Operador")
                    .WithMany("Satelites")
                    .HasForeignKey("OperadorId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.Navigation("Operador");
            });

            modelBuilder.Entity("OdinApi.Models.Detrito", b =>
            {
                b.Navigation("Alertas");
            });

            modelBuilder.Entity("OdinApi.Models.Operador", b =>
            {
                b.Navigation("Detritos");

                b.Navigation("Manobras");

                b.Navigation("Satelites");
            });

            modelBuilder.Entity("OdinApi.Models.Satelite", b =>
            {
                b.Navigation("Alertas");

                b.Navigation("Manobras");
            });
#pragma warning restore 612, 618
        }
    }
}
