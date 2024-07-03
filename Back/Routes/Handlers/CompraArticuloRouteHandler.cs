using AutoMapper;
using Back.DTOs;
using Back.Models;
using Back.Services.Contracts;

namespace Back.Routes.Handlers;

public class CompraArticuloRouteHandler
{
  public static async Task<IResult> GetAll(IClienteArticuloService service, IMapper mapper)
  {
    var compraArticulos = await service.GetClientesArticulos();
    var compraArticulosDTO = mapper.Map<List<ClienteArticuloDTO>>(compraArticulos);

    if (compraArticulosDTO.Count > 0)
      return TypedResults.Ok(compraArticulosDTO);
    else
      return TypedResults.NotFound();
  }

  public static async Task<IResult> GetById(int purchase, int item, IClienteArticuloService service, IMapper mapper)
  {
    var compraArticulo = await service.GetClienteArticulo(purchase, item);
    var compraArticuloDTO = mapper.Map<ClienteArticuloDTO>(compraArticulo);

    if (compraArticuloDTO is null)
      TypedResults.NotFound();

    return TypedResults.Ok(compraArticuloDTO);
  }

  public static async Task<IResult> Create(ClienteArticuloDTO model, IClienteArticuloService service, IMapper mapper) {
    var compraArticulo = mapper.Map<ClienteArticulo>(model);
    var respuesta = await service.InsertClienteArticulo(compraArticulo);

    if (!respuesta)
      return TypedResults.BadRequest();

    model = mapper.Map<ClienteArticuloDTO>(compraArticulo);
    return TypedResults.Created($"/compra-articulo/{model.IdCompra}/{model.IdArticulo}", model);
  }

  public static async Task<IResult> Update(ClienteArticuloDTO model, IClienteArticuloService service, IMapper mapper) {
    var existente = await service.GetClienteArticulo(model.IdCompra, model.IdArticulo);

    if (existente is null)
      TypedResults.NotFound();

    var compraArticulo = mapper.Map<ClienteArticulo>(model);

    existente.IdCompra = compraArticulo.IdCompra;
    existente.IdArticulo = compraArticulo.IdArticulo;
    existente.Cantidad = compraArticulo.Cantidad;

    var respuesta = await service.UpdateClienteArticulo(existente);

    if (!respuesta)
      TypedResults.BadRequest();

    return TypedResults.NoContent();
  }

  public static async Task<IResult> Delete(int purchase, int item, IClienteArticuloService service) {
    var respuesta = await service.DeleteClienteArticulo(purchase, item);

    if (!respuesta)
      TypedResults.BadRequest();

    return TypedResults.NoContent();
  }
}
