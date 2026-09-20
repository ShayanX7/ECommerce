namespace ECommerce.Domain.Enums;

public enum OrderStatus
{
    Pending = 1,
    AwaitingPayment = 2,
    Confirmed = 3,
    Processing = 4,
    Shipped = 5,
    Delivered = 6,
    Cancelled = 7
}