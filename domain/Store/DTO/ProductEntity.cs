namespace Store.Data;

//TODO переделать под книги
public class ProductEntity
{
    public int ID { get; init; }

    public string Title { get; set; }

    public int MakerID { get; set; }

    public string ISBN { get; }

    public string Category { get; set; }

    public decimal Price { get; set; }

    public string Description { get; set; }

    public virtual MakerEntity Maker { get; init; }
}
