using Back.Models;

namespace Back.Services.Contracts;

public interface IClienteArticuloService
{
  Task<List<ClienteArticulo>> GetClientesArticulos();
  Task<ClienteArticulo> GetClienteArticulo(int idCompra, int idArticulo);
  Task<bool> InsertClienteArticulo(ClienteArticulo clienteArticulo);
  Task<bool> UpdateClienteArticulo(ClienteArticulo clienteArticulo);
  Task<bool> DeleteClienteArticulo(int idCompra, int idArticulo);
}
