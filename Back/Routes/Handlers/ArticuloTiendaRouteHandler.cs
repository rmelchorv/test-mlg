using AutoMapper;
using Back.DTOs;
using Back.Models;
using Back.Services.Contracts;

namespace Back.Routes.Handlers;

public class ArticuloTiendaRouteHandler
{
  public static async Task<IResult> GetAll(IArticuloTiendumService service, IMapper mapper)
  {
    var articuloTiendas = await service.GetArticuloTiendums();
    var articuloTiendasDTO = mapper.Map<List<ArticuloTiendumDTO>>(articuloTiendas);

    if (articuloTiendasDTO.Count > 0)
      return TypedResults.Ok(articuloTiendasDTO);
    else
      return TypedResults.NotFound();
  }

  public static async Task<IResult> GetById(int item, int store, IArticuloTiendumService service, IMapper mapper)
  {
    var articuloTienda = await service.GetArticuloTiendum(item, store);
    var articuloTiendaDTO = mapper.Map<ArticuloTiendumDTO>(articuloTienda);

    if (articuloTiendaDTO is null)
      TypedResults.NotFound();

    return TypedResults.Ok(articuloTiendaDTO);
  }

  public static async Task<IResult> Create(ArticuloTiendumDTO model, IArticuloTiendumService service, IMapper mapper) {
    var articuloTienda = mapper.Map<ArticuloTiendum>(model);
    var respuesta = await service.InsertArticuloTiendum(articuloTienda);

    if (!respuesta)
      return TypedResults.BadRequest();

    model = mapper.Map<ArticuloTiendumDTO>(articuloTienda);
    return TypedResults.Created($"/articulo-tienda/{model.IdArticulo}/{model.IdTienda}", model);
  }

  public static async Task<IResult> Update(ArticuloTiendumDTO model, IArticuloTiendumService service, IMapper mapper) {
    var existente = await service.GetArticuloTiendum(model.IdArticulo, model.IdTienda);

    if (existente is null)
      TypedResults.NotFound();

    var articuloTienda = mapper.Map<ArticuloTiendum>(model);

    existente.IdArticulo = articuloTienda.IdArticulo;
    existente.IdTienda = articuloTienda.IdTienda;
    existente.Fecha = articuloTienda.Fecha;

    var respuesta = await service.UpdateArticuloTiendum(existente);

    if (!respuesta)
      TypedResults.BadRequest();

    return TypedResults.NoContent();
  }

  public static async Task<IResult> Delete(int item, int store, IArticuloTiendumService service) {
    var respuesta = await service.DeleteArticuloTiendum(item, store);

    if (!respuesta)
      TypedResults.BadRequest();

    return TypedResults.NoContent();
  }
}
