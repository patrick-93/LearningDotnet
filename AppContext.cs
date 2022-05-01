using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Serilog;

namespace DotnetClient
{
	public class AppDbContext : DbContext
	{
		public string DbPath { get; }
		public DbSet<Data> DataTable => Set<Data>();

		// Constructor
		public AppDbContext()
		{
			Log.Information("Constructor for AppDbContext called");
			var home = Environment.GetEnvironmentVariable("HOME");
			DbPath = home + "/client.db";
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlite($"Data Source={DbPath}");
	}
}