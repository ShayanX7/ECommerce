using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities;

public class Category : AuditableEntity
{
    public string Name { get; private set; } = null!;
    
    public Guid? ParentCategoryId { get; private set; }
    public Category? ParentCategory { get; private set; }
    
    private readonly List<Category> _children = [];
    public IReadOnlyCollection<Category> Children => _children.AsReadOnly();
}