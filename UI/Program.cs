using ElectronNET.API;

var builder = WebApplication.CreateBuilder(args);

// Add configuration to use Electron
builder.WebHost.UseElectron(args);
builder.WebHost.UseEnvironment("Development");

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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

if (HybridSupport.IsElectronActive)
{
   CreateElectronWindow();
}

app.Run();

async void CreateElectronWindow()
{
   var window = await ElectronNET.API.Electron.WindowManager.CreateWindowAsync();
   window.OnClose += () => ElectronNET.API.Electron.App.Quit();
}
