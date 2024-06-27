using System;
using System.Collections.Generic;

namespace Back.Models;

public partial class Compra
{
    public int Id { get; set; }

    public int? IdCliente { get; set; }

    public decimal Total { get; set; }

    public DateTime Fecha { get; set; }

    public virtual ICollection<ClienteArticulo> ClienteArticulos { get; set; } = new List<ClienteArticulo>();

    public virtual Cliente? IdClienteNavigation { get; set; }
}
