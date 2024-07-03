using Back.Models;

namespace Back.Services.Contracts;

public interface ICompraService
{
  Task<List<Compra>> GetCompras();
  Task<Compra> GetCompra(int id);
  Task<bool> InsertCompra(Compra compra);
  Task<bool> UpdateCompra(Compra compra);
  Task<bool> DeleteCompra(int id);
}
