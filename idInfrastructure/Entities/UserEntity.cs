using Microsoft.AspNetCore.Identity;

namespace idInfrastructure.Entities;

public class UserEntity : IdentityUser
{
    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;

    [ProtectedPersonalData]
    public string LastName { get; set; } = null!;

    [ProtectedPersonalData]
    public string? Bio {  get; set; }

    public bool IsExternalAccount { get; set; } = false;

    public ICollection<AddressEntity> Address { get; set; } = [];
}