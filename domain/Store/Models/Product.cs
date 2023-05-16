using Store.Data;
using System;

namespace Store
{
    public class Product
    {
        private readonly ProductEntity dto;

        public int Id => dto.ID;

        public string Title 
        {
            get => dto.Title;
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("no correct TitleProduct from DB");

                dto.Title = value;
            }
        }
        
        public int MakerID 
        {
            get => dto.MakerID;
            set
            {
                if (value > 0)
                    dto.MakerID = value;

                throw new ArgumentException("no correct IdMaker from DB");
            }
        }

        public string Category
        {
            get => dto.Category;
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("no correct Catefory from DB");

                dto.Category = value;
            }
        }

        public decimal Price 
        {
            get => dto.Price;
            set => dto.Price = value;
        }

        public string Description 
        {
            get => dto.Description;
            set => dto.Description = value;
        }

        internal Product(ProductEntity dto) 
        {
            this.dto = dto;
        }

        public static class DtoFactory
        {
            public static ProductEntity Create(int idMaker,
                                         string category,
                                         string title,
                                         string description,
                                         decimal price)
            {
                if (string.IsNullOrWhiteSpace(title))
                    throw new ArgumentException(nameof(title));

                return new ProductEntity
                {
                    MakerID = idMaker,
                    Title = title.Trim(),
                    Category = category.Trim(),
                    Description = description,
                    Price = price
                };
            }
        }

        public static class Mapper
        {
            public static Product Map(ProductEntity dto) => new Product(dto);

            public static ProductEntity Map(Product domain) => domain.dto;
        }
    }
}
