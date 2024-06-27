using System;
using System.Collections.Generic;

namespace Back.Models;

public partial class ClienteArticulo
{
    public int IdCompra { get; set; }

    public int IdArticulo { get; set; }

    public int Cantidad { get; set; }

    public virtual Articulo IdArticuloNavigation { get; set; } = null!;

    public virtual Compra IdCompraNavigation { get; set; } = null!;
}
