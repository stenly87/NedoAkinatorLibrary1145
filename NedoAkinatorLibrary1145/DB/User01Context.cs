using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NedoAkinatorLibrary1145.DB;

public partial class User01Context : DbContext
{
    public User01Context()
    {
    }

    public User01Context(DbContextOptions<User01Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Character> Characters { get; set; }

    public virtual DbSet<Cross> Crosses { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("database=user01;user=user01;password=83328\n;server=192.168.200.35;trustservercertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>(entity =>
        {
            entity.ToTable("Character");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Image).HasColumnType("image");
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Cross>(entity =>
        {
            entity.HasKey(e => new { e.IdHistory, e.IdQuestion });

            entity.ToTable("Cross");

            entity.Property(e => e.IdHistory).HasColumnName("idHistory");
            entity.Property(e => e.IdQuestion).HasColumnName("idQuestion");
            entity.Property(e => e.Reaction).HasColumnName("reaction");

            entity.HasOne(d => d.IdHistoryNavigation).WithMany(p => p.Crosses)
                .HasForeignKey(d => d.IdHistory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cross_History");

            entity.HasOne(d => d.IdQuestionNavigation).WithMany(p => p.Crosses)
                .HasForeignKey(d => d.IdQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cross_Question");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.ToTable("History");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCharacter).HasColumnName("id_character");

            entity.HasOne(d => d.IdCharacterNavigation).WithMany(p => p.Histories)
                .HasForeignKey(d => d.IdCharacter)
                .HasConstraintName("FK_History_Character");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.ToTable("Question");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Text).HasMaxLength(1000);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
