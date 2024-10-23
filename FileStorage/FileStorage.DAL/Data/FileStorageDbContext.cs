using FileStorage.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.DAL.Data
{
	public class FileStorageDbContext : DbContext
	{
        public FileStorageDbContext(DbContextOptions<FileStorageDbContext> options)
           : base(options)
        {
        }


        public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<StoredFile> StoredFiles { get; set; }
		public virtual DbSet<StoredFileDetails> StoredFilesDetails { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasKey(u => u.Id);
			modelBuilder.Entity<StoredFile>().HasKey(sf => sf.Id);
			modelBuilder.Entity<StoredFileDetails>().HasKey(sfd => sfd.Id);

			modelBuilder.Entity<User>()
				.Property(u => u.Name)
				.IsRequired()
				.HasMaxLength(20);

			modelBuilder.Entity<User>()
				.Property(u => u.Login)
				.IsRequired()
				.HasMaxLength(20);

			modelBuilder.Entity<User>()
				.Property(u => u.Email)
				.IsRequired()
				.HasMaxLength(64);

			modelBuilder.Entity<User>()
				.Property(u => u.Password)
				.IsRequired()
				.HasMaxLength(24);

			modelBuilder.Entity<StoredFile>()
				.Property(sf => sf.Name)
				.IsRequired()
				.HasMaxLength(64);

			modelBuilder.Entity<StoredFile>()
				.Property(sf => sf.Type)
				.IsRequired()
				.HasMaxLength(24);

			modelBuilder.Entity<StoredFile>()
				.Property(sf => sf.Size)
				.IsRequired();

			modelBuilder.Entity<StoredFile>()
				.Property(sf => sf.Path)
				.IsRequired();

			modelBuilder.Entity<StoredFileDetails>()
				.Property(sfd => sfd.IsPublic)
				.IsRequired();

			modelBuilder.Entity<StoredFileDetails>()
				.Property(sfd => sfd.IsDeleted)
				.IsRequired();

			modelBuilder.Entity<StoredFileDetails>()
				.Property(sfd => sfd.UploadDateTime)
				.IsRequired();

			modelBuilder.Entity<StoredFileDetails>()
				.Property(sfd => sfd.ExpireDateTime)
				.IsRequired();


			modelBuilder.Entity<User>()
				.HasMany<StoredFile>(u => u.StoredFiles)
				.WithOne(sf => sf.User)
				.HasForeignKey(u => u.UserId);
		}
	}
}
