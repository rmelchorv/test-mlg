using AutoMapper;
using Back.DTOs;
using Back.Models;
using Back.Services.Contracts;

namespace Back.Routes.Handlers;

public class CompraRouteHandler
{
  public static async Task<IResult> GetAll(ICompraService service, IMapper mapper)
  {
    var compras = await service.GetCompras();
    var comprasDTO = mapper.Map<List<CompraDTO>>(compras);

    if (comprasDTO.Count > 0)
      return TypedResults.Ok(comprasDTO);
    else
      return TypedResults.NotFound();
  }

  public static async Task<IResult> GetById(int id, ICompraService service, IMapper mapper)
  {
    var compra = await service.GetCompra(id);
    var compraDTO = mapper.Map<CompraDTO>(compra);

    if (compraDTO is null)
      TypedResults.NotFound();

    return TypedResults.Ok(compraDTO);
  }

  public static async Task<IResult> Create(CompraDTO model, ICompraService service, IMapper mapper) {
    var compra = mapper.Map<Compra>(model);
    var respuesta = await service.InsertCompra(compra);

    if (!respuesta)
      return TypedResults.BadRequest();

    model = mapper.Map<CompraDTO>(compra);
    return TypedResults.Created($"/compra/{model.Id}", model);
  }

  public static async Task<IResult> Update(int id, CompraDTO model, ICompraService service, IMapper mapper) {
    var existente = await service.GetCompra(id);

    if (existente is null)
      TypedResults.NotFound();

    var compra = mapper.Map<Compra>(model);

    existente.IdCliente = compra.IdCliente;
    existente.Total = compra.Total;
    existente.Fecha = compra.Fecha;

    var respuesta = await service.UpdateCompra(existente);

    if (!respuesta)
      TypedResults.BadRequest();
  
    return TypedResults.NoContent();
  }

  public static async Task<IResult> Delete(int id, ICompraService service) {
    var respuesta = await service.DeleteCompra(id);

    if (!respuesta)
      TypedResults.BadRequest();

    return TypedResults.NoContent();
  }
}
