using Back.Models;

namespace Back.Services.Contracts;

public interface IArticuloService
{
  Task<List<Articulo>> GetArticulos();
  Task<Articulo> GetArticulo(int id);
  Task<bool> InsertArticulo(Articulo articulo);
  Task<bool> UpdateArticulo(Articulo articulo);
  Task<bool> DeleteArticulo(int id);
}
