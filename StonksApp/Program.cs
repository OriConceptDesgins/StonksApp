
using StonksApp.Models;
using StonksApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<GetStockService>();
builder.Services.Configure<Stonk>(builder.Configuration.GetSection("StonkSiteOptions"));
var app = builder.Build();

app.UseRouting();
app.UseStaticFiles();
app.MapControllers();
app.Run();
