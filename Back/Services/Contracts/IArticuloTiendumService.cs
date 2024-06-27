using Back.Models;

namespace Back.Services.Contracts;

public interface IArticuloTiendumService
{
  Task<List<ArticuloTiendum>> GetArticuloTiendums();
  Task<ArticuloTiendum> GetArticuloTiendum(int idArticulo, int idTiendum);
  Task<bool> InsertArticuloTiendum(ArticuloTiendum articuloTiendum);
  Task<bool> UpdateArticuloTiendum(ArticuloTiendum articuloTiendum);
  Task<bool> DeleteArticuloTiendum(int idArticulo, int idTiendum);
}
