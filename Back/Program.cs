using Microsoft.EntityFrameworkCore;
using AutoMapper;
using NSwag.AspNetCore;
using Back.DTOs;
using Back.Models;
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
    options.AddPolicy("AllowAll", builder =>
    {
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
if (app.Environment.IsDevelopment())
{
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
app.MapGet("/", () => Results.Content($"""
    <!DOCTYPE html>
    <html>
        <head>
            <meta charset=""utf-8"" />
            <title>TestMLG API</title>
        </head>
        <body>
            <h1>TestMLG API</h1>
            <ul>
                <li>
                    <a href="swagger">Show API</a>
                </li>
            </ul>
        </body>
    </html>
""", "text/html"));

var r = app.MapGet("/articulo/lista", 
    async(IArticuloService _service, IMapper _mapper) => {
        var articulos = await _service.GetArticulos();
        var articulosDTO = _mapper.Map<List<ArticuloDTO>>(articulos);

        if (articulosDTO.Count > 0)
            Results.Ok(articulosDTO);
        else
            Results.NotFound();
    }
);

int i = 0;

app.MapGet("/articulo/{idArticulo}", 
    async(int idArticulo, IArticuloService _service, IMapper _mapper) => {
        var articulo = await _service.GetArticulo(idArticulo);
        var articuloDTO = _mapper.Map<ArticuloDTO>(articulo);

        if (articuloDTO != null)
            Results.Ok(articuloDTO);
        else
            Results.NotFound();
    }
);
app.MapPost("/articulo/insertar", 
    async(ArticuloDTO _model, IArticuloService _service, IMapper _mapper) => {
        var articulo = _mapper.Map<Articulo>(_model);
        var respuesta = await _service.InsertArticulo(articulo);

        //if (articuloCreado.Id != 0)
        //    Results.Ok(_mapper.Map<ArticuloDTO>(articuloCreado));
        //else
        //    Results.StatusCode(StatusCodes.Status500InternalServerError);
        if (respuesta)
            Results.Created($"/articulo/{articulo.Id}", _model);
        else
            Results.BadRequest();
    }
);
app.MapPut("/articulo/actualizar/{idArticulo}", 
    async(int idArticulo, ArticuloDTO _model, IArticuloService _service, IMapper _mapper) => {
        var articuloExistente = await _service.GetArticulo(idArticulo);
        
        if (articuloExistente == null)
            Results.NotFound();

        var articulo = _mapper.Map<Articulo>(_model);
        
        articuloExistente.Codigo = articulo.Codigo;
        articuloExistente.Descripcion = articulo.Descripcion;
        articuloExistente.Precio = articulo.Precio;
        articuloExistente.Imagen = articulo.Imagen;
        articuloExistente.Stock = articulo.Stock;

        var respuesta = await _service.UpdateArticulo(articuloExistente);

        if (respuesta)
            Results.Ok();
        else
            Results.BadRequest();
    }
);
app.MapDelete("/articulo/eliminar/{idArticulo}",
    async(int idArticulo, IArticuloService _service) => {
        var articuloExistente = await _service.GetArticulo(idArticulo);

        if (articuloExistente == null)
            Results.NotFound();

        var respuesta = await _service.DeleteArticulo(articuloExistente.Id);

        if (respuesta)
            Results.Ok();
        else
            Results.BadRequest();
    }
);

app.MapGet("/cliente/lista", 
    async(IClienteService _service, IMapper _mapper) => {
        var clientes = await _service.GetClientes();
        var clientesDTO = _mapper.Map<List<ClienteDTO>>(clientes);

        if (clientesDTO.Count > 0)
            Results.Ok(clientesDTO);
        else
            Results.NotFound();
    }
);

app.MapGet("/tienda/lista", 
    async(ITiendumService _service, IMapper _mapper) => {
        var tiendas = await _service.GetTiendums();
        var tiendasDTO = _mapper.Map<List<ArticuloDTO>>(tiendas);

        if (tiendasDTO.Count > 0)
            Results.Ok(tiendasDTO);
        else
            Results.NotFound();
    }
);
#endregion

app.UseCors("AllowAll");
app.Run();
 