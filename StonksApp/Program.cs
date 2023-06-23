
using StonksApp.Models;
using StonksApp.Services;
using StonksApp.ServiceContracts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddTransient<GetStockService>();
builder.Services.AddTransient<StockTradeService>();
builder.Services.Configure<Stonk>(builder.Configuration.GetSection("StonkSiteOptions"));
var app = builder.Build();

app.UseRouting();
app.UseStaticFiles();
app.MapControllers();
app.Run();
