namespace Avaliacao3BimLp3.Models;

public class Product
{
    public Product() {}

    public Product(int id, string name, double price, bool active)
    {
        Id = id;
        Name = name;
        Price = price;
        Active = active;
    }

    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public double Price { get; set; }
    
    public bool Active { get; set; }    
}
