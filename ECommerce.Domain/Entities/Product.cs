using ECommerce.Domain.Common;
using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities;

public class Product : AuditableEntity
{
    //ctor's
    private Product()
    {

    }

    private Product(string name, string description, Guid brandId, Guid categoryId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name is required.");

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Product description is required.");

        if (brandId == Guid.Empty)
            throw new ArgumentException("Brand ID is required.");

        if (categoryId == Guid.Empty)
            throw new ArgumentException("Category ID is required.");

        Name = name;
        Description = description;
        BrandId = brandId;
        CategoryId = categoryId;
        Status = ProductStatus.Draft;
    }

    //prop's
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;

    public Guid BrandId { get; private set; }
    public Brand? Brand { get; private set; } = null!;

    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;

    public ProductStatus Status { get; private set; } = ProductStatus.Draft;

    private readonly List<ProductVariant> _variants = [];
    public IReadOnlyCollection<ProductVariant> Variants => _variants.AsReadOnly();
    private readonly List<ProductImage> _images = [];
    public IReadOnlyCollection<ProductImage> Images => _images.AsReadOnly();


    //method's
    public static Product Create(string name, string description, Guid brandId, Guid categoryId)
    {
        return new Product
        {
            Name = name,
            Description = description,
            BrandId = brandId,
            CategoryId = categoryId,
            Status = ProductStatus.Draft
        };
    }
}