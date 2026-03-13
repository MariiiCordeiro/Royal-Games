using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RoyalGames.Domains;

namespace RoyalGames.Contexts;

public partial class RoyalGamesContext : DbContext
{
    public RoyalGamesContext()
    {
    }

    public RoyalGamesContext(DbContextOptions<RoyalGamesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClassificacaoIndicativa> ClassificacaoIndicativa { get; set; }

    public virtual DbSet<Genero> Genero { get; set; }

    public virtual DbSet<Log_AlteracaoProduto> Log_AlteracaoProduto { get; set; }

    public virtual DbSet<Plataforma> Plataforma { get; set; }

    public virtual DbSet<Produto> Produto { get; set; }

    public virtual DbSet<ProdutoPromocao> ProdutoPromocao { get; set; }

    public virtual DbSet<Promocao> Promocao { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }
  

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.

        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=RoyalGames;Trusted_Connection=true;TrustServerCertificate=true");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClassificacaoIndicativa>(entity =>
        {

            entity.HasKey(e => e.ClassificacaoID).HasName("PK__Classifi__D1D088EED24C54D4");



            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.GeneroID).HasName("PK__Genero__A99D0268F3282BE6");

            entity.HasIndex(e => e.Nome, "UQ__Genero__7D8FE3B261FE0D4B").IsUnique();

            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Log_AlteracaoProduto>(entity =>
        {
            entity.HasKey(e => e.Log_AlteracaoProdutoID).HasName("PK__Log_Alte__D06C51B7B2F8C697");

            entity.Property(e => e.DataAlteracao).HasPrecision(0);
            entity.Property(e => e.NomeAnterior)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PrecoAnterior).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Produto).WithMany(p => p.Log_AlteracaoProduto)
                .HasForeignKey(d => d.ProdutoID)
                .HasConstraintName("FK__Log_Alter__Produ__5812160E");
        });

        modelBuilder.Entity<Plataforma>(entity =>
        {
            entity.HasKey(e => e.PlataformaID).HasName("PK__Platafor__B835678D1B2C7189");

            entity.HasIndex(e => e.Nome, "UQ__Platafor__7D8FE3B29F8BA612").IsUnique();

            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.ProdutoID).HasName("PK__Produto__9C8800C37F574F08");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("trg_AlteracaoProduto");
                    tb.HasTrigger("trg_ExclusaoProduto");
                });

            entity.HasIndex(e => e.Nome, "UQ__Produto__7D8FE3B2E3FE8BE1").IsUnique();

            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Preco).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StatusProduto).HasDefaultValue(true);

            entity.HasOne(d => d.Classificacao).WithMany(p => p.Produto)
                .HasForeignKey(d => d.ClassificacaoID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Produto_Classificacao");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Produto)
                .HasForeignKey(d => d.UsuarioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Produto_Usuario");

            entity.HasMany(d => d.Genero).WithMany(p => p.Produto)
                .UsingEntity<Dictionary<string, object>>(
                    "ProdutoGenero",
                    r => r.HasOne<Genero>().WithMany()
                        .HasForeignKey("GeneroID")
                        .HasConstraintName("FK_ProdutoGenero_Genero"),
                    l => l.HasOne<Produto>().WithMany()
                        .HasForeignKey("ProdutoID")
                        .HasConstraintName("FK_ProdutoGenero_Produto"),
                    j =>
                    {
                        j.HasKey("ProdutoID", "GeneroID");
                    });

            entity.HasMany(d => d.Plataforma).WithMany(p => p.Produto)
                .UsingEntity<Dictionary<string, object>>(
                    "ProdutoPlataforma",
                    r => r.HasOne<Plataforma>().WithMany()
                        .HasForeignKey("PlataformaID")
                        .HasConstraintName("FK_ProdutoPlataforma_Plataforma"),
                    l => l.HasOne<Produto>().WithMany()
                        .HasForeignKey("ProdutoID")
                        .HasConstraintName("FK_ProdutoPlataforma_Produto"),
                    j =>
                    {
                        j.HasKey("ProdutoID", "PlataformaID");
                    });
        });

        modelBuilder.Entity<ProdutoPromocao>(entity =>
        {
            entity.HasKey(e => new { e.PromocaoId, e.ProdutoId }).HasName("PK_ProdutoPromocao_ProdutoId_PromocaoId");

            entity.Property(e => e.Valor).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Produto).WithMany(p => p.ProdutoPromocao)
                .HasForeignKey(d => d.ProdutoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProdutoPromocao_ProdutoId");

            entity.HasOne(d => d.Promocao).WithMany(p => p.ProdutoPromocao)
                .HasForeignKey(d => d.PromocaoId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Promocao>(entity =>
        {
            entity.HasKey(e => e.PromocaoId).HasName("PK__Promocao__254B581DEC816713");

            entity.HasIndex(e => e.Nome, "UQ__Promocao__7D8FE3B20D8D2358").IsUnique();

            entity.Property(e => e.DataExpiracao).HasColumnType("datetime");
            entity.Property(e => e.Descricao).IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StatusPromocao).HasDefaultValue(true);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioID).HasName("PK__Usuario__2B3DE79866B1BFB1");

            entity.ToTable(tb => tb.HasTrigger("trg_ExclusaoUsuario"));

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D105345FE77F75").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(32);
            entity.Property(e => e.StatusUsuario).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
