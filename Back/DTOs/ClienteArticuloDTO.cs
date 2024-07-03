namespace Back.DTOs;

public class ClienteArticuloDTO
{
    public int IdCompra { get; set; }

    public int IdArticulo { get; set; }

    public string Articulo { get; set; } = null!;

    public int Cantidad { get; set; }
}
