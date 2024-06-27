namespace Back.DTOs;

public partial class ArticuloDTO
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public byte[]? Imagen { get; set; }

    public int Stock { get; set; }

    public ICollection<ArticuloTiendumDTO> ArticuloTienda { get; set; } = new List<ArticuloTiendumDTO>();

    public ICollection<ClienteArticuloDTO> ClienteArticulos { get; set; } = new List<ClienteArticuloDTO>();
}
