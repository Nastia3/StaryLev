using Application.Options;
using Application.Services;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDb;
using MongoDb.repository;
using System;
using System.Text;
using WebApplication.GraphQL.Mutations;
using WebApplication.GraphQL.Queries;
using WebApplication.GraphQL.Types;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.Configure<MongoSettings>(
                Configuration.GetSection(nameof(MongoSettings)));

            var authoptionConfiguration = Configuration.GetSection("Auth");
            services.Configure<AuthOption>(authoptionConfiguration);

            var authOptions = authoptionConfiguration.Get<AuthOption>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = authOptions.Audience,

                        ValidateLifetime = true,

                        IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });

            services.AddSingleton<IMongoSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoSettings>>().Value);

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IFeelingService, BookService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserService, UserService>();

            services.AddHttpContextAccessor();

            services.AddGraphQLServer()
                .AddAuthorization()
                .AddQueryType(d => d.Name("Query"))
                    .AddTypeExtension<BookQuery>()
                    .AddTypeExtension<UserQuery>()
                .AddMutationType(d => d.Name("Mutation"))
                    .AddTypeExtension<BookMutation>()
                    .AddTypeExtension<UserMutation>()
                .AddType<BookType>()
                .AddType<UserRegistrationType>()
                .AddType<AuthorizeRequestType>()
                .AddType<UserInfoType>()
                .AddSorting()
                .AddFiltering()
                .AddProjections();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL("/graphql");
            });
            
        }
    }
}
