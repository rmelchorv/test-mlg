namespace Back.DTOs;

public partial class TiendumDTO
{
    public int Id { get; set; }

    public string Sucursal { get; set; } = null!;

    public string? Direccion { get; set; }
}
