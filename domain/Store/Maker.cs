using Store.Data;
using System;
using System.Text.RegularExpressions;

namespace Store
{
    public class Maker
    {
        private readonly MakerDto dto;

        public int Id => dto.Id;

        public string Title 
        {
            get => dto.Title;
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("no correct title for Maker"+Id);

                dto.Title = value;
            }
        }

        public string NumberPhone 
        { 
            get => dto.NumberPhone;
            set
            {
                if (value == null || !IsNumberPhone(value))
                    throw new ArgumentException("no correct numberphone for Maker"+ Id.ToString());

                dto.NumberPhone = value;
            }
        }

        public string Email 
        {
            get => dto.NumberPhone;
            set
            {
                if (value == null || !IsEmail(value))
                    throw new ArgumentException("no correct Email for Maker" + Id.ToString());
            }
        }

        public string Addres 
        {
            get => dto.Addres;
            set => dto.Addres = value;    
        }

        public string Description 
        {
            get => dto.Description;
            set => dto.Description = value;
        }

        public Maker(MakerDto dto)
        {
            this.dto = dto;
        }
        

        private static bool IsNumberPhone(string number)
        {
            return Regex.IsMatch(number, @"\+7\d{3}-\d{3}-\d{2}-\d{2}")
                   || Regex.IsMatch(number, @"8\d{3}-\d{3}-\d{2}-\d{2}");
        }

        private static bool IsEmail(string email)
        {
            return Regex.IsMatch(email, @"^[a-z0-9_-]+[a-z0-9_-]@[a-z]{2,20}.[a-z]{2,4}$");
        }

        public static class Mapper
        {
            public static Maker Map(MakerDto dto) => new Maker(dto);

            public static MakerDto Map(Maker domain) => domain.dto;
        }
    }
}
