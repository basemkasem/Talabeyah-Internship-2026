namespace Domain.Models;

public class Example
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public Example(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public void Rename(string name)
    {
        Name = name;
    }
}
