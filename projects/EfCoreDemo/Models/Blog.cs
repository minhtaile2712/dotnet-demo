namespace EfCoreDemo.Models;

public class Blog
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<Post> Posts { get; set; } = [];

    public Blog(string name)
    {
        Name = name;
    }
}
