using NGuid;

namespace Domain.Models;

public class Order
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; }
    public Guid OrderStatusId { get; }
    public decimal TotalPrice { get; private set; }
    public DateTime CreatedAt { get; }
    public ICollection<OrderProduct> OrderProducts { get; }

    public Order(){ }

    public Order(Guid id, Guid customerId, decimal totalPrice)
    {
        Id = id;
        CustomerId = customerId;
        OrderStatusId = new Guid("74B41903-687B-4F91-8B8C-BAFA0FFE85F1"); //Pending status
        TotalPrice = totalPrice;
        CreatedAt =  DateTime.Now;
    }

    public void Update(decimal totalPrice)
    {
        TotalPrice =  totalPrice;
    }
}