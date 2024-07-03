namespace Back.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Apellidos { get; set; }

    public string? Domicilio { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
