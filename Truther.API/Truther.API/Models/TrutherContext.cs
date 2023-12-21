using Microsoft.EntityFrameworkCore;

namespace Truther.API.Models
{
    public partial class TrutherContext : DbContext
    {
        public TrutherContext()
        {
        }

        public TrutherContext(DbContextOptions<TrutherContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Like> Likes { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Share> Shares { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Comments__postId__36B12243");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Comments__userId__35BCFE0A");
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasIndex(e => new { e.PostId, e.UserId }, "UQ__Likes__31B5D254F799991B")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Count)
                    .HasColumnName("count")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Likes__postId__300424B4");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Likes__userId__30F848ED");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Posts__userId__2A4B4B5E");
            });

            modelBuilder.Entity<Share>(entity =>
            {
                entity.HasIndex(e => new { e.PostId, e.UserId }, "UQ__Shares__31B5D254EBC0F654")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Shares)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Shares__postId__3B75D760");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Shares)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Shares__userId__3C69FB99");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Users__AB6E6164099BF677")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "UQ__Users__F3DBC57230ED6C74")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
