using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities;

public class ProductImage : Entity
{
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = null!;
    
    public Guid? ProductVariantId { get; private set; }
    public ProductVariant? ProductVariant { get; private set; }

    public string Url { get; private set; } = null!;
    public string? AltText { get; private set; }
    public int SortOrder { get; private set; }
    public bool IsPrimary { get; private set; }
}