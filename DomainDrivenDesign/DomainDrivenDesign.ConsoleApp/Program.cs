using MediatR;

namespace DomainDrivenDesign.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        Order order = new();
        order.CreateOrder(1, "Domates");
        order.CreateOrder(2, "Elma");
        order.CreateOrder(3, "Armut");

        DomainEventDispacther.Dispatch(order.DomainEvents);

        //BenchmarkRunner.Run<BenchMarkService>();
        Console.ReadLine();
    }
}

public class Order
{
    private readonly IMediator _mediator;
    public int Id { get; set; }
    public string ProductName { get; set; }
    public List<IDomainEvent> DomainEvents { get; } = new();
    public void CreateOrder(int id, string productName)
    {
        Id = id;
        ProductName = productName;

        //DomainEvents.Add(new OrderCreatedEvent(id));

        _mediator.Publish(new OrderCompletedEvent(id));
    }
}

public class StockUpdateHandler : INotificationHandler<OrderCompletedEvent>
{
    public Task Handle(OrderCompletedEvent notification, CancellationToken cancellationToken)
    {
        //işlemlerimizi yapabiliriz
        return Task.CompletedTask;
    }
}

public class SendMailHandler : INotificationHandler<OrderCompletedEvent>
{
    public Task Handle(OrderCompletedEvent notification, CancellationToken cancellationToken)
    {
        //mail gönderme işlemlerimizi yapabiliriz
        return Task.CompletedTask;
    }
}

public class SendSmsHandler : INotificationHandler<OrderCompletedEvent>
{
    public Task Handle(OrderCompletedEvent notification, CancellationToken cancellationToken)
    {
        //sms gönderme işlemlerimizi yapabiliriz
        return Task.CompletedTask;
    }
}

public class OrderCompletedEvent : INotification
{
    public int Id { get; }
    public OrderCompletedEvent(int id)
    {
        Id = id;
    }
}

public static class DomainEventDispacther
{
    public static void Dispatch(List<IDomainEvent> events)
    {
        foreach (var domainEvent in events)
        {
            if (domainEvent is OrderCreatedEvent orderEvent)
            {
                Console.WriteLine($"Order Event işlemi başladı, Id: {orderEvent.OrderId}");
            }
        }
    }
}

public interface IDomainEvent
{

}

public class OrderCreatedEvent : IDomainEvent
{
    public int OrderId { get; }
    public OrderCreatedEvent(int orderId)
    {
        OrderId = orderId;
    }
}

public abstract class Entity : IEquatable<Entity>
{
    public Guid Id { get; init; } // init: Id elde edildikten sonra bir daha değiştirilememesini sağlar. 
    protected Entity(Guid id)
    {
        Id = id;
    }

    // maind methodunda id bazlı kontrol yapmamız için equals metodunu override ettik burada.
    // Bu override methodu olmadan çalışırsa false döner. 
    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (obj is not Entity entity)
        {
            return false;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return entity.Id == Id;
    }

    public override int GetHashCode() // Listeler için çalışır
    {
        return Id.GetHashCode();
    }

    public bool Equals(Entity? other) // IEquatable<Entity> Interface'nin bu metodu performans açısından daha iyi
    {
        if (other == null)
        {
            return false;
        }

        if (other is not Entity entity)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return entity.Id == Id;
    }
}

public class A : Entity
{
    public A(Guid id) : base(id)
    {
    }
}
