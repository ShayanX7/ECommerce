using ECommerce.Domain.Common;
using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities;

public class ProductVariant : AuditableEntity
{
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = null!;
    public string SKU { get; private set; } = null!;
    public string? Name { get; private set; }
    public ProductVariantStatus Status { get; set; }
    
    private readonly List<SellerOffer> _sellerOffers = [];
    public IReadOnlyCollection<SellerOffer> SellerOffers => _sellerOffers.AsReadOnly();
}