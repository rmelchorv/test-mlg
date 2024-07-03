namespace Back.DTOs;

public class ClienteDTO
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Apellidos { get; set; }

    public string? Domicilio { get; set; }
}
