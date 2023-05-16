using Store.Data;
using System.Collections.Generic;

namespace Store.DTO;
public class Author
{
    public int ID { get; init; }
    public string FirstName { get; init; }
    public string SecondName { get; init; }
    public string LastName { get; init; }
    public string Description { get; init; }
    public virtual ICollection<ProductEntity> Books { get; init; }

}