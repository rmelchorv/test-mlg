namespace Back.DTOs;

public class ArticuloTiendumDTO
{
    public int IdArticulo { get; set; }

    public string Articulo { get; set; } = null!;

    public int IdTienda { get; set; }

    public string Tienda { get; set; } = null!;

    public string? Fecha { get; set; }
}
