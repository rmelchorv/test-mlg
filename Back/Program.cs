using Microsoft.EntityFrameworkCore;
using Back.Models;
using Back.Routes.Handlers;
using Back.Services.Contracts;
using Back.Services.Implementations;
using Back.Utils;

var builder = WebApplication.CreateBuilder(args);

#region "Services"
builder.Services.AddDbContext<TestMlgContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IArticuloService, ArticuloService>();
builder.Services.AddScoped<IArticuloTiendumService, ArticuloTiendumService>();
builder.Services.AddScoped<IClienteArticuloService, ClienteArticuloService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ICompraService, CompraService>();
builder.Services.AddScoped<ITiendumService, TiendumService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", builder => {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config => {
    config.DocumentName = "api";
    config.Title = "TestMLG API";
    config.Version = "v1";
});
#endregion

var app = builder.Build();

#region "Swagger"
if (app.Environment.IsDevelopment()) {
    app.UseOpenApi();
    app.UseSwaggerUi(config => {
        config.DocumentTitle = "TestMLG API";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}
#endregion
#region "Routing"
#region Articulos
app.MapGet("/articulos/", ArticuloRouteHandler.GetAll);
app.MapGet("/articulo/{id}", ArticuloRouteHandler.GetById);
app.MapPost("/articulo/", ArticuloRouteHandler.Create);
app.MapPut("/articulo/{id}", ArticuloRouteHandler.Update);
app.MapDelete("/articulo/{id}", ArticuloRouteHandler.Delete);
#endregion
#region Clientes
app.MapGet("/clientes/", ClienteRouteHandler.GetAll);
app.MapGet("/cliente/{id}", ClienteRouteHandler.GetById);
app.MapPost("/cliente/", ClienteRouteHandler.Create);
app.MapPut("/cliente/{id}", ClienteRouteHandler.Update);
app.MapDelete("/cliente/{id}", ClienteRouteHandler.Delete);
#endregion
#region Tienda
app.MapGet("/tiendas/", TiendaRouteHandler.GetAll);
app.MapGet("/tienda/{id}", TiendaRouteHandler.GetById);
app.MapPost("/tienda/", TiendaRouteHandler.Create);
app.MapPut("/tienda/{id}", TiendaRouteHandler.Update);
app.MapDelete("/tienda/{id}", TiendaRouteHandler.Delete);
#endregion
#region Compra
app.MapGet("/compras/", CompraRouteHandler.GetAll);
app.MapGet("/compra/{id}", CompraRouteHandler.GetById);
app.MapPost("/compra/", CompraRouteHandler.Create);
app.MapPut("/compra/{id}", CompraRouteHandler.Update);
app.MapDelete("/commpra/{id}", CompraRouteHandler.Delete);
#endregion
#region Articulo-Tienda
app.MapGet("/articulos-tiendas/", ArticuloTiendaRouteHandler.GetAll);
app.MapGet("/articulo-tienda/{item}/{store}", ArticuloTiendaRouteHandler.GetById);
app.MapPost("/articulo-tienda/", ArticuloTiendaRouteHandler.Create);
app.MapPut("/articulo-tienda/", ArticuloTiendaRouteHandler.Update);
app.MapDelete("/articulo-tienda/{item}/{store}", ArticuloTiendaRouteHandler.Delete);
#endregion
#region Compra-Articulo
app.MapGet("/compras-articulos/", CompraArticuloRouteHandler.GetAll);
app.MapGet("/compra-articulo/{purchase}/{item}", CompraArticuloRouteHandler.GetById);
app.MapPost("/compra-articulo/",  CompraArticuloRouteHandler.Create);
app.MapPut("/compra-articulo/",  CompraArticuloRouteHandler.Update);
app.MapDelete("/compra-articulo/{purchase}/{item}",  CompraArticuloRouteHandler.Delete);
#endregion
#endregion

app.UseCors("AllowAll");
app.Run();
