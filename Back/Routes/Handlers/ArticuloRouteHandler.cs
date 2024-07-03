using AutoMapper;
using Back.DTOs;
using Back.Models;
using Back.Services.Contracts;

namespace Back.Routes.Handlers;

public class ArticuloRouteHandler
{
  public static async Task<IResult> GetAll(IArticuloService service, IMapper mapper)
  {
    var articulos = await service.GetArticulos();
    var articulosDTO = mapper.Map<List<ArticuloDTO>>(articulos);

    if (articulosDTO.Count == 0)
      return TypedResults.NotFound();

    return TypedResults.Ok(articulosDTO);
  }

  public static async Task<IResult> GetById(int id, IArticuloService service, IMapper mapper)
  {
    var articulo = await service.GetArticulo(id);
    var articuloDTO = mapper.Map<ArticuloDTO>(articulo);

    if (articuloDTO is null)
      TypedResults.NotFound();

    return TypedResults.Ok(articuloDTO);
  }

  public static async Task<IResult> Create(ArticuloDTO model, IArticuloService service, IMapper mapper) {
    var articulo = mapper.Map<Articulo>(model);
    var respuesta = await service.InsertArticulo(articulo);

    //if (articuloCreado.Id != 0)
    //    Results.Ok(_mapper.Map<ArticuloDTO>(articuloCreado));
    //else
    //    Results.StatusCode(StatusCodes.Status500InternalServerError);
    if (!respuesta)
      return TypedResults.BadRequest();

    model = mapper.Map<ArticuloDTO>(articulo);
    return TypedResults.Created($"/articulo/{model.Id}", model);
  }

  public static async Task<IResult> Update(int id, ArticuloDTO model, IArticuloService service, IMapper mapper) {
    var existente = await service.GetArticulo(id);
    
    if (existente is null)
      TypedResults.NotFound();

    var articulo = mapper.Map<Articulo>(model);
    
    existente.Codigo = articulo.Codigo;
    existente.Descripcion = articulo.Descripcion;
    existente.Precio = articulo.Precio;
    existente.Imagen = articulo.Imagen;
    existente.Stock = articulo.Stock;

    var respuesta = await service.UpdateArticulo(existente);

    if (!respuesta)
      TypedResults.BadRequest();
  
    return TypedResults.NoContent();
  }

  public static async Task<IResult> Delete(int id, IArticuloService service) {
    var respuesta = await service.DeleteArticulo(id);

    if (!respuesta)
      TypedResults.BadRequest();

    return TypedResults.NoContent();
  }
}
