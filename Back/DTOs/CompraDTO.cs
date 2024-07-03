namespace Back.DTOs;

public class CompraDTO
{
    public int Id { get; set; }

    public int? IdCliente { get; set; }

    public string? Cliente { get; set; }

    public decimal Total { get; set; }

    public string? Fecha { get; set; }
}
