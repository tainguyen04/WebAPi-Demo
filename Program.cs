using DemoDangTin.EF;
using DemoDangTin.Infrastructure;
using DemoDangTin.Interface.Repository;
using DemoDangTin.Interface.Service;
using DemoDangTin.MappingProfiles;
using DemoDangTin.Repository;
using DemoDangTin.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Connect to DB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
// Auto Mapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});
// Services
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
//builder.Services.AddScoped<IBaiDangRepository,BaiDangRepository>();
//builder.Services.AddScoped<IBaiDangService, BaiDangService>();
builder.Services.Scan(scan => scan
    .FromAssembliesOf(typeof(Program))
    .AddClasses(classes => classes.InNamespaces("DemoDangTin.Repository"))
        .AsImplementedInterfaces()
        .WithScopedLifetime()
);

// Scan service class
builder.Services.Scan(scan => scan
    .FromAssembliesOf(typeof(Program))
    .AddClasses(classes => classes.InNamespaces("DemoDangTin.Services"))
        .AsImplementedInterfaces()
        .WithScopedLifetime()
);

builder.Services.AddControllers();
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);
var securityKey = new SymmetricSecurityKey(key);
builder.Services.AddSingleton(securityKey);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = securityKey,
                                
            ClockSkew = TimeSpan.Zero
        };
    }
    );
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => 
        options.SwaggerEndpoint("/openapi/v1.json", "api")
    );
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
