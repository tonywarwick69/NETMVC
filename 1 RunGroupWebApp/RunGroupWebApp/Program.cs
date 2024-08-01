using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Repository;
// Import the required packages
//==============================

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;
using RunGroupWebApp.Helpers;
using RunGroupWebApp.Services;

// Set your Cloudinary credentials
//=================================

//DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
//Cloudinary cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
//cloudinary.Api.Secure = true;
var builder = WebApplication.CreateBuilder(args);
//Add cloudinary services
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IClubRepository, ClubRepository>();
builder.Services.AddScoped<IRaceRepository, RaceRepository>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//Type "dotnet run seeddata" in PM console to insert data into DB
var app = builder.Build();
if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    //
    Seed.SeedData(app);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
