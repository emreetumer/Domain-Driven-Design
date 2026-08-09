using BenchmarkDotNet.Running;

namespace DomainDrivenDesign.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        BenchmarkRunner.Run<BenchMarkService>();
        Console.ReadLine();
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
