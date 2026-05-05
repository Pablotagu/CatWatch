public class Shelter : Entity, IAggregateRoot
{
    public string Name { get; private set; }

    public Shelter(string name)
    {
        Name = name;
    }
}