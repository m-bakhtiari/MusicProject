using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TopLearn.Core.Convertors;
using TopLearn.Core.Services;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.Web.Controllers;

namespace TopLearn.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //services.Configure<FormOptions>(options => { options.MultipartBodyLengthLimit = 6000000; });

            #region Authentication

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddCookie(options =>
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.SlidingExpiration = true;
            });


            #endregion

            #region DataBase Context

            services.AddDbContext<TopLearnContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("MusicConnection"));
                }
            );

            #endregion

            #region IoC

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IViewRenderService, RenderViewToString>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IAcademyService, AcademyService>();
            services.AddTransient<IInstrumentService, InstrumentService>();
            services.AddTransient<IStudentConcertService, StudentConcertService>();
            services.AddTransient<IMusicNoteService, MusicNoteService>();
            services.AddTransient<ILogger<HomeController>, Logger<HomeController>>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IMenuItemService, MenuItemService>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<ICertificateService, CertificateService>();
            services.AddTransient<IConcertPrizeService, ConcertPrizeService>();
            services.AddTransient<ISharedService, SharedService>();
            services.AddTransient<ISubscriberService, SubscriberService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IGalleryImageService, GalleryImageService>();
            services.AddTransient<IVideoService, VideoService>();
            services.AddTransient<IContactMessageService, ContactMessageService>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"

                );
                routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run(async (context) =>
            {
                context.Response.Redirect("/notfound", permanent: false);
            });

        }
    }
}
