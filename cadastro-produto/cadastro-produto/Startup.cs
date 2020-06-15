using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CadastroProduto.InfraEstrutura.Security;
using CadastroProduto.Ioc;
using CadastroProduto.Repository.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace cadastro_produto
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "CorsPolicy";

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHttpContextAccessor();
            services.AddMvc().AddControllersAsServices();

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    builder
                    .AllowCredentials()
                    .SetIsOriginAllowed(hostName => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            // Adicionando servicos do framework
            services.AddMvc().AddNewtonsoftJson(options =>
            {
                // handle loops correctly
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                // use standard name conversion of properties
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                // include $id property in the output
                //options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            });

            //Set string connection
            var sqlConnectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CadastroProdutoContext>(options =>
                  options.UseMySql(
                      sqlConnectionString
                      //b => b.MigrationsAssembly("CadastroProduto.Api")
                  )
              );


            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Documentação de Apis",
                        Version = "v1.0",
                        Description = "# Introducão\nSeja bem-vindo a documentação da API Cadastro de Produto!\n",
                        Contact = new OpenApiContact
                        {
                            Name = "João Guilherme"
                        }
                    });

                c.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    Description = "Por favor, insira no campo a palavra 'Bearer' espaço JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

                //var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                //var commentsFileName = "OsContrato.Ui.Api" + ".XML";
                //var commentsFile = Path.Combine(baseDirectory, commentsFileName);

                //c.IncludeXmlComments(commentsFile);
            });

            services.AddMvcCore(options => options.EnableEndpointRouting = false);

            services.AddApiVersioning(
                options =>
                {
                    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                    options.ReportApiVersions = true;

                    options.DefaultApiVersion = new ApiVersion(new DateTime(2019, 2, 20));

                    // automatically applies an api version based on the name of the defining controller's namespace
                    options.Conventions.Add(new VersionByNamespaceConvention());
                });


            services.AddAuthentication();
            //services.AddMvc().AddWebApiConventions();

            //InjecaoDependenciaServicos(ref services);
            services.AddSingleton<IConfiguration>(Configuration);
            DependencyInjection.InjecaoDependenciaRepositorios(ref services);
            DependencyInjection.InjecaoDependenciaServicos(ref services);
            DependencyInjection.InjecaoDependenciaConverters(ref services);

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            //var tokenConfigurations = new TokenConfigurations();
            //new ConfigureFromConfigurationOptions<TokenConfigurations>(
            //    Configuration.GetSection("TokenConfigurations"))
            //        .Configure(tokenConfigurations);
            //services.AddSingleton(tokenConfigurations);

            //services.AddAuthentication(authOptions =>
            //{
            //    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(bearerOptions =>
            //{
            //    var paramsValidation = bearerOptions.TokenValidationParameters;
            //    paramsValidation.IssuerSigningKey = signingConfigurations.Key;
            //    paramsValidation.ValidAudience = tokenConfigurations.Audience;
            //    paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

            //    // Valida a assinatura de um token recebido
            //    paramsValidation.ValidateIssuerSigningKey = false;

            //    // Verifica se um token recebido ainda é válido
            //    paramsValidation.ValidateLifetime = false;

            //    // Tempo de tolerância para a expiração de um token
            //    paramsValidation.ClockSkew = TimeSpan.Zero;
            //});

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
                {
                    var userLangs = context.Request.Headers["Accept-Language"].ToString();
                    var firstLang = userLangs.Split(',').FirstOrDefault();
                    var defaultLang = string.IsNullOrEmpty(firstLang) ? "pt-BR" : firstLang;
                    return Task.FromResult(new ProviderCultureResult(defaultLang, defaultLang));
                }));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var supportedCultures = new[] {
                new CultureInfo("en-US")            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(MyAllowSpecificOrigins);

            // Ativando middlewares para uso do Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api de integração - Versão 1.0");

                c.RoutePrefix = string.Empty;
            });

            // app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "DefaultApi", template: "{controller=Values}/{id?}");
            });

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
