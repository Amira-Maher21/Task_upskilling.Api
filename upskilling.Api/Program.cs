
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using upskilling.Api.Infrastructure;
using upskilling.Repository.Data;
using upskilling.Repository.UnitOfWork;

namespace upskilling.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddDbContext<upskillingDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
				options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
			});
			builder.Services.AddAutoMapper(typeof(MappingProfile));
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}