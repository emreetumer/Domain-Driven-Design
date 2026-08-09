namespace DomainDrivenDesign.Domain.Abstractions;

public abstract class Entity
{
    public Guid Id { get; init; } // init: Id elde edildikten sonra bir daha değiştirilememesini sağlar. 
    protected Entity(Guid id)
    {
        Id = id;
    }

    // TestMethod methodunda id bazlı kontrol yapmamız için equals metodunu override ettik burada.
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
}

#region SORUN => Id'ler burada eşit değil fakat biz eşit olmasını istiyoruz çünkü A sınıfı Entity'e bağlı.
// Equals metodu burada ezmemiz gerekiyor equals bazlı değil de id bazlı kontrol olması lazım.
public class A : Entity
{
    public A(Guid id) : base(id)
    {
    }
}

public class Test
{
    public void TestMethod()
    {
        Guid id = Guid.NewGuid();
        A a1 = new(id);
        A a2 = new(id);

        Console.WriteLine(a1.Equals(a2)); // equals override metodunu yazmasaydık burası false dönerdi.
    }
}
#endregion