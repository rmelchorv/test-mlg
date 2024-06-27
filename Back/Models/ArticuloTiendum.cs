using System;
using System.Collections.Generic;

namespace Back.Models;

public partial class ArticuloTiendum
{
    public int IdArticulo { get; set; }

    public int IdTienda { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Articulo IdArticuloNavigation { get; set; } = null!;

    public virtual Tiendum IdTiendaNavigation { get; set; } = null!;
}
