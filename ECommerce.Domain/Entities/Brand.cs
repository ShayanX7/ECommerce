using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities;

public class Brand : AuditableEntity
{
    public string Name { get; private set; } = null!;
}