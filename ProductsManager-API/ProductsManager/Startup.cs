using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.DependencyInjection;
using ProductsManager.Managers;
using ProductsManager.Models;
using Serilog;

namespace ProductsManager
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddSingleton<ApplicationState>();
			services.AddScoped<OrderItemManager>();
			services.AddScoped<ProductManager>();
			services.AddScoped<UserManager>();
			services.AddScoped<ShopManager>();
			services.AddScoped<OrderManager>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseSerilogRequestLogging();
			app.UseMvcWithDefaultRoute();
			app.UseMvc(routes =>
			{
				routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
			});
			app.Use(async (context, next) => {
				context.Request.EnableRewind();
				await next();
			});
		}
	}
}
