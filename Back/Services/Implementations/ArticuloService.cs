using Microsoft.EntityFrameworkCore;
using Back.Models;
using Back.Services.Contracts;

namespace Back.Services.Implementations;

public class ArticuloService : IArticuloService
{
  private readonly TestMlgContext _dbContext;

  public ArticuloService(TestMlgContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<List<Articulo>> GetArticulos()
  {
    return await _dbContext.Articulos.ToListAsync();
  }

  public async Task<Articulo> GetArticulo(int id)
  {
    return await _dbContext.Articulos.FindAsync(id);
  }

  public async Task<bool> InsertArticulo(Articulo articulo)
  {
    _dbContext.Articulos.Add(articulo);
    return await _dbContext.SaveChangesAsync() > 0;
  }

  public async Task<bool> UpdateArticulo(Articulo articulo)
  {
    //_dbContext.Articulos.Update(articulo);
    _dbContext.Entry(articulo).State = EntityState.Modified;
    return await _dbContext.SaveChangesAsync() > 0;
  }

  public async Task<bool> DeleteArticulo(int id)
  {
    var articulo = await GetArticulo(id);
    if (articulo == null)
      return false;

    _dbContext.Articulos.Remove(articulo);
    return await _dbContext.SaveChangesAsync() > 0;
  }
}
