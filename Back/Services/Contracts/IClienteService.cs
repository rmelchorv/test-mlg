using Back.Models;

namespace Back.Services.Contracts;

public interface IClienteService
{
  Task<List<Cliente>> GetClientes();
  Task<Cliente> GetCliente(int id);
  Task<bool> InsertCliente(Cliente cliente);
  Task<bool> UpdateCliente(Cliente cliente);
  Task<bool> DeleteCliente(int id);
}
