using Microsoft.EntityFrameworkCore;
using Back.Models;
using Back.Services.Contracts;

namespace Back.Services.Implementations;

public class ArticuloTiendumService : IArticuloTiendumService
{
  private readonly TestMlgContext _dbContext;

  public ArticuloTiendumService(TestMlgContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<List<ArticuloTiendum>> GetArticuloTiendums()
  {
    return await _dbContext.ArticuloTienda
                  .Include(articulo => articulo.IdArticuloNavigation)
                  .Include(tienda => tienda.IdTiendaNavigation).ToListAsync();
  }

  public async Task<ArticuloTiendum> GetArticuloTiendum(int idArticulo, int idTiendum)
  {
    ArticuloTiendum? articuloTiendum = await _dbContext.ArticuloTienda
      .Include(articulo => articulo.IdArticuloNavigation)
      .Include(tienda => tienda.IdTiendaNavigation)
      .FirstOrDefaultAsync(at => at.IdArticulo == idArticulo && at.IdTienda == idTiendum);

    return articuloTiendum;
  }

  public async Task<bool> InsertArticuloTiendum(ArticuloTiendum articuloTiendum)
  {
    _dbContext.ArticuloTienda.Add(articuloTiendum);
    return await _dbContext.SaveChangesAsync() > 0;
  }

  public async Task<bool> UpdateArticuloTiendum(ArticuloTiendum articuloTiendum)
  {
    _dbContext.Entry(articuloTiendum).State = EntityState.Modified;
    return await _dbContext.SaveChangesAsync() > 0;
  }

  public async Task<bool> DeleteArticuloTiendum(int idArticulo, int idTiendum)
  {
    var articuloTiendum = await GetArticuloTiendum(idArticulo, idTiendum);
    if (articuloTiendum is null)
      return false;

    _dbContext.ArticuloTienda.Remove(articuloTiendum);
    return await _dbContext.SaveChangesAsync() > 0;
  }
}
