using Store.Data;
using System;

namespace Store
{
    public class Product
    {
        private readonly ProductOrderItemEntity dto;

        public int Id => dto.Id;

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
        
        public int IdMaker 
        {
            get => dto.IdMaker;
            set
            {
                if (value > 0)
                    dto.IdMaker = value;

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

        internal Product(ProductOrderItemEntity dto) 
        {
            this.dto = dto;
        }

        public static class DtoFactory
        {
            public static ProductOrderItemEntity Create(int idMaker,
                                         string category,
                                         string title,
                                         string description,
                                         decimal price)
            {
                if (string.IsNullOrWhiteSpace(title))
                    throw new ArgumentException(nameof(title));

                return new ProductOrderItemEntity
                {
                    IdMaker = idMaker,
                    Title = title.Trim(),
                    Category = category.Trim(),
                    Description = description,
                    Price = price
                };
            }
        }

        public static class Mapper
        {
            public static Product Map(ProductOrderItemEntity dto) => new Product(dto);

            public static ProductOrderItemEntity Map(Product domain) => domain.dto;
        }
    }
}
