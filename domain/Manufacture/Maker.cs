using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;


namespace StoreManufacture
{
    //[Keyless]

    [Table("ProductMaker")]
    public class Maker
    {
        public int Id { get; }

        public string Title { get; }

        public string NumberPhone { get; }

        public string Email { get; }

        public string Addres { get; }


        public string Description { get; }

        public Maker()
        {}

        public Maker(int id, string title, string numberPhone, string email, string addres, string description)
        {
            Id = id;
            Title = title;

            if (IsNumberPhone(numberPhone))
                NumberPhone = numberPhone;
            else
                NumberPhone = "";

            if (IsEmail(email))
                Email = email;
            else
                Email = "";

            Addres = addres;
            Description = description;
        }
        
        public Maker( string title, string numberPhone, string email, string addres, string description)
        {
            Title = title;

            if (IsNumberPhone(numberPhone))
                NumberPhone = numberPhone;
            else
                NumberPhone = "";

            if (IsEmail(email))
                Email = email;
            else
                Email = "";

            Addres = addres;
            Description = description;
        }
       
        public static bool IsNumberPhone(string number)
        {
            return Regex.IsMatch(number, @"\+79\d{2}-\d{3}-\d{2}-\d{2}")
                   || Regex.IsMatch(number, @"89\d{2}-\d{3}-\d{2}-\d{2}");
        }

        public static bool IsEmail(string email)
        {
            return Regex.IsMatch(email, @"^[a-z0-9_-]+[a-z0-9_-]@[a-z]{2,6}.[a-z]{2,4}$");
        }
    }
}
