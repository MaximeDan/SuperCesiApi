using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SuperCesiApi.Data;
using SuperCesiApi.Models;
using SuperCesiApi.Services;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Default") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<SuperCesiApiDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<SuperCesiApiDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddCors();

builder.Services.AddTransient<IncidentTypeService>();
builder.Services.AddTransient<IncidentService>();
builder.Services.AddTransient<SuperHeroService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Les Super Héros Api", Version = "v1" });
    
    // Set unique schemaIds for input models
    c.CustomSchemaIds(type => type.FullName.Replace("+", "."));
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    
    var identityAssembly = typeof(IdentityUser).Assembly;
    var identityXmlPath = $"{identityAssembly.GetName().Name}.xml";
    var identityXmlFullPath = Path.Combine(AppContext.BaseDirectory, identityXmlPath);
    if (File.Exists(identityXmlFullPath))
    {
        c.IncludeXmlComments(identityXmlFullPath);
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Migrate the database on startup
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<SuperCesiApiDbContext>();
context.Database.Migrate();

IncidentTypeSeeder.Seed(context);
RoleSeeder.Seed(context);

app.UseAuthentication();

app.UseCors(options => options
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()   
);

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Les Super Héros Api v1");
    c.RoutePrefix = string.Empty; // Serve the Swagger UI at the root URL
    c.DocExpansion(DocExpansion.List);
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();