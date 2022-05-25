using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using microsoft_lms_backend.IdentityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using microsoft_lms_backend.Data;
using microsoft_lms_backend.Interfaces.v1;
using microsoft_lms_backend.Services.v1;

namespace microsoft_lms_backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container..
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));//ProjectLMSDb
            services.AddTransient<IBusinessProfile, BusinessProfileService>();

            services.AddTransient<IWebinar, WebinarService>();
            services.AddTransient<ICategory, CategoryService>();
            services.AddTransient<ICourse, CourseService>();
            services.AddTransient<ILearningTrack, LearningTrackService>();
            services.AddTransient<IModule, ModuleService>();
            services.AddTransient<IArticle, ArticleService>();
            services.AddTransient<INews, NewsService>();
            services.AddTransient<IProductUpload, ProductUploadService>();
            services.AddTransient<IWebinarAttendee, WebinarAttendeeService>();

            services.AddTransient<ITemplateService, TemplatesService>();
            services.AddTransient<ISupportTicket, SupportTicketsService>();
            services.AddTransient<IBusinessContact, BusinessContactService>();
            services.AddTransient<IOfferService, OfferService>();
            services.AddTransient<IBusinessExpertise, BusinessExpertiseService>();

            services.AddControllers();

            var appSettingsSection = Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<JwtSettings>();
            //var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        //IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            AddApiVersioning(services);
            AddSwaggerGen(services);
        }

            private void AddSwaggerGen(IServiceCollection services)
            {

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "MS LMS API",
                        Version = "v1",
                        Description = "A Cloud Base Learning Management Solution",
                        TermsOfService = new Uri("https://wragbyrestored.azurewebsites.net/terms-of-use/"),
                        Contact = new OpenApiContact
                        {
                            Name = "Wragby Business Solutions and Technologies Limited",
                            Email = string.Empty,
                            Url = new Uri("https://wragbysolutions.com"),
                        },
                    });
                });
            }

        private void AddApiVersioning(IServiceCollection services)
        {
            services.AddApiVersioning(o =>
              {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(majorVersion: 1, minorVersion: 0);
              });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microsoft LMS API  v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
