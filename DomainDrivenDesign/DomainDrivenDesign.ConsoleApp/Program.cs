namespace DomainDrivenDesign.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        Guid id = Guid.NewGuid();
        A a1 = new(id);
        A a2 = new(id);

        Console.WriteLine(a1.Equals(a2));
        Console.ReadLine();
    }
}

public abstract class Entity
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
}

public class A : Entity
{
    public A(Guid id) : base(id)
    {
    }
}
