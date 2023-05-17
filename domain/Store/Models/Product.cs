using Store.Entities;
using System;

namespace Store;

public class Product
{
    private readonly ProductEntity dto;
    internal Product(ProductEntity dto)
    {
        this.dto = dto;
    }
    public int ID => dto.ID;

    public string Title 
    {
        get => dto.Title;
        set
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("no correct TitleProduct");

            dto.Title = value;
        }
    }
    
    public int PublishHousingID 
    {
        get => dto.PublishHousingID;
        set
        {
            if (value > 0)
                dto.PublishHousingID = value;

            throw new ArgumentException("no correct IdMaker from DB");
        }
    }

    public string Category
    {
        get => dto.Category;
        set
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("no correct Category from DB");

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
                PublishHousingID = idMaker,
                Title = title.Trim(),
                Category = category.Trim(),
                Description = description,
                Price = price
            };
        }
    }

    public Maker Maker => Maker.Mapper.Map(dto.PublishHousing);

    public static class Mapper
    {
        public static Product Map(ProductEntity dto) => new Product(dto);

        public static ProductEntity Map(Product domain) => domain.dto;
    }
}
