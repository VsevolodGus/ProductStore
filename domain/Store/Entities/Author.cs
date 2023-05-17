using System.Collections.Generic;

namespace Store.Entities;
public class Author
{
    public int ID { get; init; }
    public string FirstName { get; init; }
    public string SecondName { get; init; }
    public string LastName { get; init; }
    public string Description { get; init; }
    public virtual ICollection<ProductEntity> Books { get; init; }
}