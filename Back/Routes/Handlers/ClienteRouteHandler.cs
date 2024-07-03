using AutoMapper;
using Back.DTOs;
using Back.Models;
using Back.Services.Contracts;

namespace Back.Routes.Handlers;

public class ClienteRouteHandler
{
  public static async Task<IResult> GetAll(IClienteService service, IMapper mapper)
  {
    var clientes = await service.GetClientes();
    var clientesDTO = mapper.Map<List<ClienteDTO>>(clientes);

    if (clientesDTO.Count > 0)
      return TypedResults.Ok(clientesDTO);
    else
      return TypedResults.NotFound();
  }

  public static async Task<IResult> GetById(int id, IClienteService service, IMapper mapper)
  {
    var cliente = await service.GetCliente(id);
    var clienteDTO = mapper.Map<ClienteDTO>(cliente);

    if (clienteDTO is null)
      TypedResults.NotFound();

    return TypedResults.Ok(clienteDTO);
  }

  public static async Task<IResult> Create(ClienteDTO model, IClienteService service, IMapper mapper) {
    var cliente = mapper.Map<Cliente>(model);
    var respuesta = await service.InsertCliente(cliente);

    if (!respuesta)
      return TypedResults.BadRequest();

    model = mapper.Map<ClienteDTO>(cliente);
    return TypedResults.Created($"/cliente/{model.Id}", model);
  }

  public static async Task<IResult> Update(int id, ClienteDTO model, IClienteService service, IMapper mapper) {
    var existente = await service.GetCliente(id);

    if (existente is null)
      TypedResults.NotFound();

    var cliente = mapper.Map<Cliente>(model);

    existente.Nombre = cliente.Nombre;
    existente.Apellidos = cliente.Apellidos;
    existente.Domicilio = cliente.Domicilio;

    var respuesta = await service.UpdateCliente(existente);

    if (!respuesta)
      TypedResults.BadRequest();

    return TypedResults.NoContent();
  }

  public static async Task<IResult> Delete(int id, IClienteService service) {
    var respuesta = await service.DeleteCliente(id);

    if (!respuesta)
      TypedResults.BadRequest();

    return TypedResults.NoContent();
  }
}
