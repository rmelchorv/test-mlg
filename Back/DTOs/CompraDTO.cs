namespace Back.DTOs;

public partial class CompraDTO
{
    public int Id { get; set; }

    public int? IdCliente { get; set; }

    public decimal Total { get; set; }

    public string? Fecha { get; set; }

    public string? Cliente { get; set; }
}
