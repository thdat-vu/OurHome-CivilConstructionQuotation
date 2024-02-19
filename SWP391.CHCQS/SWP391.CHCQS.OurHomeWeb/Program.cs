using Microsoft.EntityFrameworkCore;
using SWP391.CHCQS.DataAccess.Data;
using SWP391.CHCQS.DataAccess.Repository;

namespace SWP391.CHCQS.OurHomeWeb
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			//builder.Services.AddDbContext<SWP391DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			//TODO: Connect to XuanDat database
            builder.Services.AddDbContext<SWP391DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("XuanDatCon")));
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
                //define the name of role's default page routing

                endpoints.MapControllerRoute(
				  name: "areas",
				  pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}"
				);

                endpoints.MapControllerRoute(
                  name: "defaultManagerRoute",
                  pattern: "{area=Manager}/{controller=Dashboard}/{action=Index}/{id?}"
                );

            });

			app.Run();
		}
	}
}