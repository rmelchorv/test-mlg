using Microsoft.EntityFrameworkCore;
using Back.Models;
using Back.Services.Contracts;

namespace Back.Services.Implementations;

public class ClienteService : IClienteService
{
  private readonly TestMlgContext _dbContext;

  public ClienteService(TestMlgContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<List<Cliente>> GetClientes()
  {
    return await _dbContext.Clientes.ToListAsync();
  }

  public async Task<Cliente> GetCliente(int id)
  {
    return await _dbContext.Clientes.FindAsync(id);
  }

  public async Task<bool> InsertCliente(Cliente cliente)
  {
    _dbContext.Clientes.Add(cliente);
    return await _dbContext.SaveChangesAsync() > 0;
  }

  public async Task<bool> UpdateCliente(Cliente cliente)
  {
    _dbContext.Entry(cliente).State = EntityState.Modified;
    return await _dbContext.SaveChangesAsync() > 0;
  }

  public async Task<bool> DeleteCliente(int id)
  {
    var cliente = await GetCliente(id);
    if (cliente is null)
      return false;

    _dbContext.Clientes.Remove(cliente);
    return await _dbContext.SaveChangesAsync() > 0;
  }
}
