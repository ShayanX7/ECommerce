using ECommerce.Domain.Common;
using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities;

public class ProductVariant : AuditableEntity
{
    //ctor's
    private ProductVariant()
    {
        
    }

    private ProductVariant(Guid productId, string sku, string? name)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException("Product ID is required.");
        
        if(string.IsNullOrWhiteSpace(sku))
            throw new ArgumentException("SKU is required.");
        
        if(sku.Length > 100)
            throw new ArgumentException("SKU cannot be exceed 100 characters.");
        
        if(name is not null && name.Length > 150)
            throw new ArgumentException("Variant name cannot be exceed 150 characters.");
        
        ProductId = productId;
        SKU = sku;
        Name = name;
        Status = ProductVariantStatus.Draft;
    }

    
    //prop's
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = null!;
    public string SKU { get; private set; } = null!;
    public string? Name { get; private set; }
    public ProductVariantStatus Status { get; private set; }
    
    private readonly List<SellerOffer> _sellerOffers = [];
    public IReadOnlyCollection<SellerOffer> SellerOffers => _sellerOffers.AsReadOnly();
    
    
    //method's
    public static ProductVariant Create(Guid productId, string sku, string? name)
    {
        return new ProductVariant(productId, sku, name);
    }
}