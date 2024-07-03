using AutoMapper;
using Back.DTOs;
using Back.Models;
using Back.Services.Contracts;

namespace Back.Routes.Handlers;

public class TiendaRouteHandler
{
  public static async Task<IResult> GetAll(ITiendumService service, IMapper mapper)
  {
    var tiendas = await service.GetTiendums();
    var tiendasDTO = mapper.Map<List<TiendumDTO>>(tiendas);

    if (tiendasDTO.Count > 0)
      return TypedResults.Ok(tiendasDTO);
    else
      return TypedResults.NotFound();
  }

  public static async Task<IResult> GetById(int id, ITiendumService service, IMapper mapper)
  {
    var tienda = await service.GetTiendum(id);
    var tiendaDTO = mapper.Map<TiendumDTO>(tienda);

    if (tiendaDTO is null)
      TypedResults.NotFound();

    return TypedResults.Ok(tiendaDTO);
  }

  public static async Task<IResult> Create(TiendumDTO model, ITiendumService service, IMapper mapper) {
    var tienda = mapper.Map<Tiendum>(model);
    var respuesta = await service.InsertTiendum(tienda);

    if (!respuesta)
      return TypedResults.BadRequest();

    model = mapper.Map<TiendumDTO>(tienda);
    return TypedResults.Created($"/tienda/{model.Id}", model);
  }

  public static async Task<IResult> Update(int id, TiendumDTO model, ITiendumService service, IMapper mapper) {
    var existente = await service.GetTiendum(id);

    if (existente is null)
      TypedResults.NotFound();

    var tienda = mapper.Map<Tiendum>(model);

    existente.Sucursal = tienda.Sucursal;
    existente.Direccion = tienda.Direccion;

    var respuesta = await service.UpdateTiendum(existente);

    if (!respuesta)
      TypedResults.BadRequest();

    return TypedResults.NoContent();
  }

  public static async Task<IResult> Delete(int id, ITiendumService service) {
    var respuesta = await service.DeleteTiendum(id);

    if (!respuesta)
      TypedResults.BadRequest();

    return TypedResults.NoContent();
  }
}
