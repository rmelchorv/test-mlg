using Microsoft.EntityFrameworkCore;
using Back.Models;
using Back.Services.Contracts;

namespace Back.Services.Implementations;

public class TiendumService : ITiendumService
{
  private readonly TestMlgContext _dbContext;

  public TiendumService(TestMlgContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<List<Tiendum>> GetTiendums()
  {
    return await _dbContext.Tienda.ToListAsync();
  }

  public async Task<Tiendum> GetTiendum(int id)
  {
    return await _dbContext.Tienda.FindAsync(id);
  }

  public async Task<bool> InsertTiendum(Tiendum tiendum)
  {
    _dbContext.Tienda.Add(tiendum);
    return await _dbContext.SaveChangesAsync() > 0;
  }

  public async Task<bool> UpdateTiendum(Tiendum tiendum)
  {
    _dbContext.Entry(tiendum).State = EntityState.Modified;
    return await _dbContext.SaveChangesAsync() > 0;
  }

  public async Task<bool> DeleteTiendum(int id)
  {
    var tiendum = await GetTiendum(id);
    if (tiendum is null)
      return false;

    _dbContext.Tienda.Remove(tiendum);
    return await _dbContext.SaveChangesAsync() > 0;
  }
}
