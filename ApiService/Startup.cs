using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MoneyMeExam.Repository;
using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace MoneyMeExam.ApiService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        internal static IConfiguration Configuration { get; private set; }
        internal static bool IsDevelopment {get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration)
                    .AddDbContext<MoneyMeExamDbContext>(options => {
                        options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]);
                    })
                    .AddCors(options => {
                        options.AddPolicy("CORS", policy => {
                            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                        });
                    })
                    .Configure<GzipCompressionProviderOptions>(options => {
                        options.Level = System.IO.Compression.CompressionLevel.Optimal;
                    })
                    .Configure<BrotliCompressionProviderOptions>(options => {
                        options.Level = System.IO.Compression.CompressionLevel.Optimal;
                    })
                    .AddResponseCompression(options => {
                        options.EnableForHttps = true;
                        options.Providers.Add<GzipCompressionProvider>();
                        options.Providers.Add<BrotliCompressionProvider>();
                    })
                    .AddSwaggerGen(options => {
                        options.SwaggerDoc("v1", new OpenApiInfo {
                            Title = "Money Me Exam",
                            Version = "v1",
                            Description = "Money Me Exam API",
                            Contact = new OpenApiContact {
                                Name = Configuration.GetValue<string>("AppSettings:API:ContactName"),
                                Email = Configuration.GetValue<string>("AppSettings:API:ContactEmail")
                            }
                        });
                    })
                    .AddFluentValidation()
                    .AddHttpContextAccessor()
                    .AddHealthChecks();

            var mvcBuilder = services.AddControllers();
            mvcBuilder.AddNewtonsoftJson(options => {
                if (Configuration.GetValue<bool>("AppSettings:API:PrettifyJSON"))
                {
                    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;     
                }
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            
            services.AddSpaStaticFiles(options => {
                        options.RootPath = "ClientApp/dist";
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                IsDevelopment = true;
                app.UseDeveloperExceptionPage();
            } 
            else
            {
                IsDevelopment = false;
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiService v1"));
            app.UseRouting();
            app.UseCors("CORS");
            app.UseHttpsRedirection();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions() { });
                endpoints.MapControllers();
            });
            app.UseSpa(spa => {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
