using Microsoft.EntityFrameworkCore;
using SWP391.CHCQS.DataAccess.Data;
using SWP391.CHCQS.DataAccess.Repository;
using SWP391.CHCQS.DataAccess.Repository.IRepository;

namespace SWP391.CHCQS.OurHomeWeb
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<SWP391DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
				  name: "areas",
				  pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}"
				);
			});

			app.Run();
		}
	}
}