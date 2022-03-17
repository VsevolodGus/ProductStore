using PhoneNumbers;
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
                if (value == null || !IsNumberPhone(value, out string cellPhone))
                    throw new ArgumentException("no correct numberphone for Maker"+ Id.ToString());

                dto.NumberPhone = cellPhone;
            }
        }

        public string Email 
        {
            get => dto.Email;
            set
            {
                if (value == null || !IsEmail(value))
                    throw new ArgumentException("no correct Email for Maker" + Id.ToString());

                dto.Email = value;
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

        public  static  readonly PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
        public static bool IsNumberPhone(string cellPhone, out string formattedPhone)
        {
            try
            {
                var phoneNumber = phoneNumberUtil.Parse(cellPhone, "ru");
                formattedPhone = phoneNumberUtil.Format(phoneNumber, PhoneNumberFormat.INTERNATIONAL);
                return true;
            }
            catch (NumberParseException)
            {
                formattedPhone = null;
                return false;
            }
        }

        public static bool IsEmail(string email)
        {
            if (email == null)
                return false;

            return Regex.IsMatch(email, @"^[a-zA-Z0-9]+\@[a-z]{2,10}\.[a-z]{2,5}$");
        }

        public static class Mapper
        {
            public static Maker Map(MakerDto dto) => new Maker(dto);

            public static MakerDto Map(Maker domain) => domain.dto;
        }
    }
}
