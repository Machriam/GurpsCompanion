using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GurpsCompanion.Server.DataContext
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<CharacterItemAssociation> CharacterItemAssociations { get; set; }
        public virtual DbSet<CharacterSkillAssociation> CharacterSkillAssociations { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("DataSource=C:\\Users\\Leckmich\\Desktop\\Repos\\GurpsCompanion\\data.sqlite3");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>(entity =>
            {
                entity.ToTable("character");

                entity.Property(e => e.BasicMoveMod).HasColumnName("basic_move_mod");

                entity.Property(e => e.BasicSpeedMod).HasColumnName("basic_speed_mod");

                entity.Property(e => e.Dexterity)
                    .HasColumnName("dexterity")
                    .HasDefaultValueSql("6");

                entity.Property(e => e.Health)
                    .HasColumnName("health")
                    .HasDefaultValueSql("6");

                entity.Property(e => e.HitPointsMod).HasColumnName("hit_points_mod");

                entity.Property(e => e.Intelligence)
                    .HasColumnName("intelligence")
                    .HasDefaultValueSql("6");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.PerceptionMod).HasColumnName("perception_mod");

                entity.Property(e => e.RadexFavor).HasColumnName("radex_favor");

                entity.Property(e => e.Strength)
                    .HasColumnName("strength")
                    .HasDefaultValueSql("6");

                entity.Property(e => e.VagrexFavor).HasColumnName("vagrex_favor");

                entity.Property(e => e.WillMod).HasColumnName("will_mod");
            });

            modelBuilder.Entity<CharacterItemAssociation>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("character_item_association");

                entity.Property(e => e.CharacterFk).HasColumnName("character_fk");

                entity.Property(e => e.ItemFk).HasColumnName("item_fk");

                entity.HasOne(d => d.CharacterFkNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CharacterFk);

                entity.HasOne(d => d.ItemFkNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.ItemFk);
            });

            modelBuilder.Entity<CharacterSkillAssociation>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("character_skill_association");

                entity.Property(e => e.CharacterFk).HasColumnName("character_fk");

                entity.Property(e => e.SkillFk).HasColumnName("skill_fk");

                entity.HasOne(d => d.CharacterFkNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CharacterFk);

                entity.HasOne(d => d.SkillFkNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.SkillFk);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("item");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Weight).HasColumnName("weight");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.ToTable("skill");

                entity.HasIndex(e => e.Name, "IX_skill_name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
