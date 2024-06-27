using Back.Models;

namespace Back.Services.Contracts;

public interface ITiendumService
{
  Task<List<Tiendum>> GetTiendums();
  Task<Tiendum> GetTiendum(int id);
  Task<bool> InsertTiendum(Tiendum tiendum);
  Task<bool> UpdateTiendum(Tiendum tiendum);
  Task<bool> DeleteTiendum(int id);
}
