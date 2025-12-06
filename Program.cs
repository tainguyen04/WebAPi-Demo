using DemoDangTin.EF;
using DemoDangTin.Infrastructure;
using DemoDangTin.Interface.Repository;
using DemoDangTin.Interface.Service;
using DemoDangTin.MappingProfiles;
using DemoDangTin.Repository;
using DemoDangTin.Services;
using Microsoft.EntityFrameworkCore;

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
