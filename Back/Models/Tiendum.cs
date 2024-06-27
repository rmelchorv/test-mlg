using System;
using System.Collections.Generic;

namespace Back.Models;

public partial class Tiendum
{
    public int Id { get; set; }

    public string Sucursal { get; set; } = null!;

    public string? Direccion { get; set; }

    public virtual ICollection<ArticuloTiendum> ArticuloTienda { get; set; } = new List<ArticuloTiendum>();
}
