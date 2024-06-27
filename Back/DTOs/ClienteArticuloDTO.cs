namespace Back.DTOs;

public partial class ClienteArticuloDTO
{
    public int IdCompra { get; set; }

    public int IdArticulo { get; set; }

    public int Cantidad { get; set; }

    public string Articulo { get; set; } = null!;

    public string Compra { get; set; } = null!;
}
