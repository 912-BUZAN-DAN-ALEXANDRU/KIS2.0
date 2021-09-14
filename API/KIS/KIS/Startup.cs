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
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddDbContext<KISContext>(ServiceLifetime.Transient);
            services.AddTransient<UserRepository>();
            services.AddTransient<PostRepository>();
            services.AddTransient<ReactionRepository>();
            services.AddTransient<CommentRepository>();
            services.AddTransient<UserManager>();
            services.AddTransient<PostManager>();
            services.AddTransient<ReactionManager>();
            services.AddTransient<CommentManager>();
            services.AddTransient<AuthorizeAttribute>();
            //services.AddTransient<JwtMiddleware>();
            services.AddTransient<UnitOfWork>();
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
