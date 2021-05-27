namespace StoreManufacture
{
    public class Manufacture
    {
        public int Id { get; }

        public string Title { get; }

        public string NumberPhone { get; }

        public string Email { get; }

        public string Addres { get; }

        public string Description { get; }


        public Manufacture(int id, string title, string numberPhone, string email, string addres, string description)
        {
            Id = id;
            Title = title;
            NumberPhone = numberPhone;
            Email = email;
            Addres = addres;
            Description = description;
        }
    }
}
