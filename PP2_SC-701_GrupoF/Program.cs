using PP1_SC_701_GrupoFBLL;
using PP1_SC_701_GrupoFBLL.Servicios.Cliente;
using PP1_SC_701_GrupoFDAL.Repositorio.Cliente;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(cfg => { }, typeof(MapeoClases));

builder.Services.AddSingleton<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddSingleton<IClienteServicio, ClienteServicio>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
