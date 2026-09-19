using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities;

public class Address : AuditableEntity
{
    public Guid UserId { get; private set; }
    public string Title { get; private set; } = null!;
    public string RecipientName { get; private set; } = null!;
    public string PhoneNumber { get; private set; } = null!;
    public string Province { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string PostalCode { get; private set; } = null!;
    public string AddressLine { get; private set; } = null!;
    public bool IsDefault { get; private set; }
}