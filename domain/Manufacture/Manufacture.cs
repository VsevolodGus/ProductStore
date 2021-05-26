namespace Manufacture
{
    public class Manufacture
    {
        public int Id { get; }

        public string Title {get;} 

        public string NumberPhone { get; }

        public string Description { get; }

        public Manufacture(int id,string title, string numberPhone, string description)
        {
            Id = id;
            Title = title;
            NumberPhone = numberPhone;
            Description = description;
        }
    }
}
