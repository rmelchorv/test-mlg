namespace Back.DTOs;

public partial class ArticuloTiendumDTO
{
    public int IdArticulo { get; set; }

    public int IdTienda { get; set; }

    public string? Fecha { get; set; }

    public string Articulo { get; set; } = null!;

    public string Tiendum { get; set; } = null!;
}
