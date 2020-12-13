using System;
using System.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;

#nullable disable

namespace XmlForEinvoicing.Models
{
    public partial class MyyntiDBContext : DbContext
    {
        public MyyntiDBContext()
        {
        }

        public MyyntiDBContext(DbContextOptions<MyyntiDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asiakas> Asiakas { get; set; }
        public virtual DbSet<Tilaus> Tilaus { get; set; }
        public virtual DbSet<TilausRivi> TilausRivi { get; set; }
        public virtual DbSet<Tuote> Tuote { get; set; }

        private string connString = XmlForEinvoicingLib.DbConnection.ConnString();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Asiakas>(entity =>
            {
                entity.HasKey(e => e.AsiakasId)
                    .HasName("PK__Asiakas__A15F30BBECA154CC");

                entity.Property(e => e.AsiakasId)
                    .ValueGeneratedNever()
                    .HasColumnName("asiakas_id");

                entity.Property(e => e.Etunimi)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("etunimi");

                entity.Property(e => e.Luottoraja).HasColumnName("luottoraja");

                entity.Property(e => e.Osoite)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("osoite");

                entity.Property(e => e.Postinumero)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("postinumero")
                    .IsFixedLength(true);

                entity.Property(e => e.Postitoimipaikka)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("postitoimipaikka");

                entity.Property(e => e.Sukunimi)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("sukunimi");

                entity.Property(e => e.Syntymäaika)
                    .HasColumnType("date")
                    .HasColumnName("syntymäaika");
            });

            modelBuilder.Entity<Tilaus>(entity =>
            {
                entity.HasKey(e => e.TilausId)
                    .HasName("PK__Tilaus__0775FE4DF3BC676D");

                entity.Property(e => e.TilausId)
                    .ValueGeneratedNever()
                    .HasColumnName("tilaus_id");

                entity.Property(e => e.AsiakasId).HasColumnName("asiakas_id");

                entity.Property(e => e.TilausPvm)
                    .HasColumnType("date")
                    .HasColumnName("tilausPvm");

                entity.Property(e => e.Tilaussumma)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("tilaussumma");

                entity.Property(e => e.ToimitusPvm)
                    .HasColumnType("date")
                    .HasColumnName("toimitusPvm");
            });

            modelBuilder.Entity<TilausRivi>(entity =>
            {
                entity.HasKey(e => new { e.TilausId, e.Rivinro });

                entity.ToTable("TilausRivi");

                entity.Property(e => e.TilausId).HasColumnName("tilaus_id");

                entity.Property(e => e.Rivinro).HasColumnName("rivinro");

                entity.Property(e => e.Ahinta)
                    .HasColumnType("decimal(7, 2)")
                    .HasColumnName("ahinta");

                entity.Property(e => e.Alennus)
                    .HasColumnType("decimal(7, 2)")
                    .HasColumnName("alennus");

                entity.Property(e => e.TilausLkm).HasColumnName("tilausLkm");

                entity.Property(e => e.TuoteId).HasColumnName("tuote_id");
            });

            modelBuilder.Entity<Tuote>(entity =>
            {
                entity.ToTable("Tuote");

                entity.Property(e => e.TuoteId)
                    .ValueGeneratedNever()
                    .HasColumnName("tuote_id");

                entity.Property(e => e.Hinta)
                    .HasColumnType("decimal(7, 2)")
                    .HasColumnName("hinta");

                entity.Property(e => e.Nimi)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nimi");

                entity.Property(e => e.Tuoteryhmä)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("tuoteryhmä");

                entity.Property(e => e.Tyyppi)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("tyyppi");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
