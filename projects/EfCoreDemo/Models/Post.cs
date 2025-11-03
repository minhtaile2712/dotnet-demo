using System.Text.Json.Serialization;

namespace EfCoreDemo.Models;

public class Post
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int BlogId { get; set; }

    [JsonIgnore]
    public Blog? Blog { get; set; }

    public Post(string name)
    {
        Name = name;
    }
}
