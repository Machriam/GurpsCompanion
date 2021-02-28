using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GurpsCompanion.Shared.DataModel.DataContext
{
    public partial class DataContext : DbContext
    {
        public virtual DbSet<Advantage> Advantages { get; set; }
        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<CharacterAdvantageAssociation> CharacterAdvantageAssociations { get; set; }
        public virtual DbSet<CharacterItemAssociation> CharacterItemAssociations { get; set; }
        public virtual DbSet<CharacterSkillAssociation> CharacterSkillAssociations { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advantage>(entity =>
            {
                entity.ToTable("advantage");

                entity.HasIndex(e => e.Name, "IX_advantage_name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");
            });

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

            modelBuilder.Entity<CharacterAdvantageAssociation>(entity =>
            {
                entity.ToTable("character_advantage_association");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AdvantageFk).HasColumnName("advantage_fk");

                entity.Property(e => e.CharacterFk).HasColumnName("character_fk");

                entity.HasOne(d => d.AdvantageFkNavigation)
                    .WithMany(p => p.CharacterAdvantageAssociations)
                    .HasForeignKey(d => d.AdvantageFk);

                entity.HasOne(d => d.CharacterFkNavigation)
                    .WithMany(p => p.CharacterAdvantageAssociations)
                    .HasForeignKey(d => d.CharacterFk);
            });

            modelBuilder.Entity<CharacterItemAssociation>(entity =>
            {
                entity.ToTable("character_item_association");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CharacterFk).HasColumnName("character_fk");

                entity.Property(e => e.ItemFk).HasColumnName("item_fk");

                entity.HasOne(d => d.CharacterFkNavigation)
                    .WithMany(p => p.CharacterItemAssociations)
                    .HasForeignKey(d => d.CharacterFk);

                entity.HasOne(d => d.ItemFkNavigation)
                    .WithMany(p => p.CharacterItemAssociations)
                    .HasForeignKey(d => d.ItemFk);
            });

            modelBuilder.Entity<CharacterSkillAssociation>(entity =>
            {
                entity.ToTable("character_skill_association");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CharacterFk).HasColumnName("character_fk");

                entity.Property(e => e.SkillFk).HasColumnName("skill_fk");

                entity.HasOne(d => d.CharacterFkNavigation)
                    .WithMany(p => p.CharacterSkillAssociations)
                    .HasForeignKey(d => d.CharacterFk);

                entity.HasOne(d => d.SkillFkNavigation)
                    .WithMany(p => p.CharacterSkillAssociations)
                    .HasForeignKey(d => d.SkillFk);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("item");

                entity.HasIndex(e => e.Name, "IX_item_name")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "item_name_index")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Weight).HasColumnName("weight");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.ToTable("skill");

                entity.HasIndex(e => e.Name, "IX_skill_name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BaseAttribute)
                    .IsRequired()
                    .HasColumnName("base_attribute");

                entity.Property(e => e.Defaults).HasColumnName("defaults");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Difficulty).HasColumnName("difficulty");

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
