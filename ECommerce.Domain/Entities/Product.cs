using ECommerce.Domain.Common;
using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities;

public class Product : AuditableEntity
{
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    
    public Guid BrandId { get; private set; }
    public Brand? Brand { get; private set; } = null!;
    
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;

    public ProductStatus Status { get; private set; } =  ProductStatus.Draft;

    private readonly List<ProductVariant> _variants = [];
    public IReadOnlyCollection<ProductVariant> Variants => _variants.AsReadOnly();
    private readonly List<ProductImage> _images = [];
    public IReadOnlyCollection<ProductImage> Images => _images.AsReadOnly();

}