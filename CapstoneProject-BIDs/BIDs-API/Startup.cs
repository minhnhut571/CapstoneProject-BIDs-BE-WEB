using Business_Logic.Modules.BanHistoryModule;
using Business_Logic.Modules.BanHistoryModule.Interface;
using Business_Logic.Modules.ItemModule;
using Business_Logic.Modules.ItemModule.Interface;
using Business_Logic.Modules.ItemTypeModule;
using Business_Logic.Modules.ItemTypeModule.Interface;
using Business_Logic.Modules.LoginModule;
using Business_Logic.Modules.LoginModule.InterFace;
using Business_Logic.Modules.PaymentModule;
using Business_Logic.Modules.PaymentModule.Interface;
using Business_Logic.Modules.RoleModule;
using Business_Logic.Modules.RoleModule.Interface;
using Business_Logic.Modules.SessionModule;
using Business_Logic.Modules.SessionModule.Interface;
using Business_Logic.Modules.StaffModule;
using Business_Logic.Modules.StaffModule.Interface;
using Business_Logic.Modules.UserModule;
using Business_Logic.Modules.UserModule.Interface;
using Data_Access.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Newtonsoft.Json;
using BIDs_API.Mapper;

namespace BIDs_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BIDs", Version = "v1" });

                // hiển thị khung authorize điền token
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \\n\\n
                      Enter your token in the text input below.
                      \\n\\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
            });
            services.AddDbContext<BIDsContext>(
                opt => opt.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")
                )
            );
            
            //services.AddIdentity<Staff, Role>()
            //    .AddEntityFrameworkStores<BIDsContext>()
            //    .AddDefaultTokenProviders(); ;
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(otp =>
                {
                    otp.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityKey"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            //User Module
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            //Staff Module
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IStaffService, StaffService>();
            //Role Module
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();
            //Role Module
            services.AddScoped<IBanHistoryRepository, BanHistoryRepository>();
            services.AddScoped<IBanHistoryService, BanHistoryService>();
            //Role Module
            services.AddScoped<IItemTypeRepository, ItemTypeRepository>();
            services.AddScoped<IItemTypeService, ItemTypeService>();
            //Role Module
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<ISessionService, SessionService>();
            //Role Module
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IItemService, ItemService>();
            //Role Module
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentService, PaymentService>();
            //Login Module
            services.AddScoped<ILoginService, LoginService>();

            services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
            services.AddAutoMapper(typeof(Mapping));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BIDs v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

