namespace idInfrastructure.Entities;

public class AddressEntity
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public string AdressLine_1 { get; set; } = null!;
    public string? AdressLine_2 { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;

    public ICollection<UserEntity> Users { get; set; } = [];
}
