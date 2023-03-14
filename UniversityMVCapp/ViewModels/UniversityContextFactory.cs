using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace UniversityMVCapp.ViewModels
{
	public class UniversityContextFactory : IDesignTimeDbContextFactory<UniversityContext>
	{
		public UniversityContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();

			ConfigurationBuilder builder = new ConfigurationBuilder();
			builder.SetBasePath(Directory.GetCurrentDirectory());
			builder.AddJsonFile("appsettings.json");
			IConfigurationRoot config = builder.Build();

			string connectionString = config.GetConnectionString("DefaultConnection");
			optionsBuilder.UseSqlServer(connectionString);

			return new UniversityContext(optionsBuilder.Options);
		}
	}
}
