using Microsoft.EntityFrameworkCore;
using Back.Models;
using Back.Services.Contracts;

namespace Back.Services.Implementations;

public class CompraService : ICompraService
{
  private readonly TestMlgContext _dbContext;

  public CompraService(TestMlgContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<List<Compra>> GetCompras()
  {
    return await _dbContext.Compras.Include(cliente => cliente.IdClienteNavigation).ToListAsync();
  }

  public async Task<Compra> GetCompra(int id)
  {
    Compra? compra = await _dbContext.Compras
      .Include(cliente => cliente.IdClienteNavigation)
      .FirstOrDefaultAsync(c => c.Id == id);

    return compra;
  }

  public async Task<Compra> InsertCompra(Compra compra)
  {
    _dbContext.Compras.Add(compra);
    await _dbContext.SaveChangesAsync();
    
    return compra;
  }

  public async Task<bool> UpdateCompra(Compra compra)
  {
    _dbContext.Entry(compra).State = EntityState.Modified;
    return await _dbContext.SaveChangesAsync() > 0;
  }

  public async Task<bool> DeleteCompra(int id)
  {
    var compra = await GetCompra(id);
    if (compra == null)
      return false;

    _dbContext.Compras.Remove(compra);
    return await _dbContext.SaveChangesAsync() > 0;
  }
}
