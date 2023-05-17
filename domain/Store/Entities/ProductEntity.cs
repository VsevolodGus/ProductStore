namespace Store.Entities;

//TODO переделать под книги
public class ProductEntity
{
    public int ID { get; init; }
    public string Title { get; set; }
    public int PublishHousingID { get; set; }
    public string ISBN { get; }
    public string Category { get; set; }
    public int AuthorID { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public virtual PublishingHouseEntity PublishHousing { get; init; }
    public virtual Author Author { get; init; }
}
