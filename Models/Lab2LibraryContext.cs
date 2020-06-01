using Microsoft.EntityFrameworkCore;

namespace lab2V1.Models
{
	public class Lab2LibraryContext : DbContext
	{
		public virtual DbSet<Class> Classes{ get; set; }
		public virtual DbSet<Family> Famlies { get; set; }
		public virtual DbSet<Genus> Genuses { get; set; }
		public virtual DbSet<Kingdom> Kingdoms { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<Phylum> Phylumes { get; set; }
		public virtual DbSet<Species> Specieses { get; set; }
		public Lab2LibraryContext() { }
		public Lab2LibraryContext(DbContextOptions<Lab2LibraryContext> options):base(options)
		{
			Database.EnsureCreated();
		}
	}
}
