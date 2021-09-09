using KIS.Helpers;
using KIS.Managers;
using KIS.Models;
using KIS.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KIS
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddCors();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddDbContext<KISContext>(ServiceLifetime.Singleton);
            services.AddSingleton<UserRepository>();
            services.AddSingleton<PostRepository>();
            services.AddSingleton<ReactionRepository>();
            services.AddSingleton<CommentRepository>();
            services.AddSingleton<UserManager>();
            services.AddSingleton<PostManager>();
            services.AddSingleton<ReactionManager>();
            services.AddSingleton<CommentManager>();
            services.AddSingleton<AuthorizeAttribute>();
            //services.AddSingleton<JwtMiddleware>();
            services.AddSingleton<UnitOfWork>();
            // In production, the React files will be served from this directory

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

              
            app.UseMiddleware<JwtMiddleware>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            
        }
    }
}
