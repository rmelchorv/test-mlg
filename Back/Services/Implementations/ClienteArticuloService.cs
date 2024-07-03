using Microsoft.EntityFrameworkCore;
using Back.Models;
using Back.Services.Contracts;

namespace Back.Services.Implementations;

public class ClienteArticuloService : IClienteArticuloService
{
  private readonly TestMlgContext _dbContext;

  public ClienteArticuloService(TestMlgContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<List<ClienteArticulo>> GetClientesArticulos()
  {
    return await _dbContext.ClienteArticulos
                  .Include(compra => compra.IdCompraNavigation)
                  .Include(articulo => articulo.IdArticuloNavigation).ToListAsync();
  }

  public async Task<ClienteArticulo> GetClienteArticulo(int idCompra, int idArticulo)
  {
    ClienteArticulo? clienteArticulo = await _dbContext.ClienteArticulos
      .Include(compra => compra.IdCompraNavigation)
      .Include(articulo => articulo.IdArticuloNavigation)
      .FirstOrDefaultAsync(ca => ca.IdCompra == idCompra && ca.IdArticulo == idArticulo);

    return clienteArticulo;
  }

  public async Task<bool> InsertClienteArticulo(ClienteArticulo clienteArticulo)
  {
    _dbContext.ClienteArticulos.Add(clienteArticulo);
    return await _dbContext.SaveChangesAsync() > 0;
  }

  public async Task<bool> UpdateClienteArticulo(ClienteArticulo clienteArticulo)
  {
    _dbContext.Entry(clienteArticulo).State = EntityState.Modified;
    return await _dbContext.SaveChangesAsync() > 0;
  }

  public async Task<bool> DeleteClienteArticulo(int idCompra, int idArticulo)
  {
    var clienteArticulo = await GetClienteArticulo(idCompra, idArticulo);
    if (clienteArticulo is null)
      return false;

    _dbContext.ClienteArticulos.Remove(clienteArticulo);
    return await _dbContext.SaveChangesAsync() > 0;
  }
}
