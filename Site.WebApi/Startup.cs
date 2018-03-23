using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Site.Lib.Model.DomainModel;
using Site.WebApi.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Autofac;
using System.Reflection;
using Autofac.Extensions.DependencyInjection;

namespace Site.WebApi
{
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// 
        /// </summary>
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opt =>
            {
                opt.Filters.Add(new ApiExceptionFilterAttribute());
                opt.Filters.Add(new JdPayAuthActionFilter(Configuration.GetValue<string>("JdConfig:JdPayAuthToken")));
//                opt.ModelBinderProviders.Insert(0,new JdWithdrawBaseReqBinderProvider());
            });
            
            //jd 配置
            services.Configure<JdConfig>(Configuration.GetSection("JdConfig"));

            
            #region swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info() { Title = "Site", Version = "v1" });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Site.WebApi.xml"));
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Site.Lib.xml"));
                c.AddSecurityDefinition("Jwt", new ApiKeyScheme()
                {
                    Description = "JsonWebToken",
                    Type = "apiKey",
                    In = "header",
                    Name = "Authorization"
                });
            });
            #endregion

            #region Autofac
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.Load(new AssemblyName() { Name = "Site.Lib" }))
                   .Where(w => w.Name.EndsWith("Service", StringComparison.CurrentCulture))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.Populate(services);

            this.ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(this.ApplicationContainer);
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Site api");
            });
            app.UseMvc();

        }
    }
}
