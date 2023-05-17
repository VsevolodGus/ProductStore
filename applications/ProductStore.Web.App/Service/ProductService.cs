using ProductStore.Web.App.Service;
using Store;
using Store.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProductStore.Web.App;

internal class ProductService : IProductService
{
    private readonly IReadonlyRepository<ProductEntity> _products;
    public ProductService(IReadonlyRepository<ProductEntity> products)
    {
        _products = products;
    }

    /// <summary>
    /// Получение модели продукта по идентификатору
    /// </summary>
    /// <param name="id">идентификатор продукта</param>
    /// <returns>модель продукта</returns>
    public async Task<ProductModel> GetByIDAsync(int id, CancellationToken cancellationToken = default)
    {
        var product = await _products.With(c=> c.PublishHousing)
            .FirstOrDefaultAsync(c=> c.ID == id, cancellationToken);

        return Map(Product.Mapper.Map(product));
    }

    /// <summary>
    /// Получение продуктов по идентификатору
    /// </summary>
    /// <param name="makerID">идентификатор производителя</param>
    /// <returns>список моделей продуктов</returns>
    public async Task<ProductModel[]> GetAllByIDMakerAsync(int makerID, CancellationToken cancellationToken = default)
    {
        var list = await _products.ToArrayAsync(c => c.PublishHousingID == makerID, cancellationToken);

        return list.Select(Product.Mapper.Map).Select(Map).ToArray();
    }

    /// <summary>
    /// Получение списка продуктов
    /// </summary>
    /// <param name="search">строка поиска</param>
    /// <returns>список моделей продуктов</returns>
    public async Task<ProductModel[]> GetAllByQueryAsync(string search, CancellationToken cancellationToken = default)
    {
        ProductEntity[] array;
        var query = _products.With(c => c.PublishHousing);
        if (string.IsNullOrEmpty(search))
            array = await query.ToArrayAsync(cancellationToken);
        else
            array = await query.ToArrayAsync(c => c.Title.ToLower().Contains(search.ToLower())
                                                    || c.Category.ToLower().Contains(search.ToLower())
                                                    || c.PublishHousing.Title.ToLower().Contains(search.ToLower())
                                , cancellationToken);

        return array.Select(Product.Mapper.Map).Select(Map).ToArray();
    }

    /// <summary>
    /// Метод маппинга сущность в модель
    /// </summary>
    /// <param name="product">сущность</param>
    /// <returns>модель</returns>
    private ProductModel Map(Product product)
        => new ProductModel
        {
            ProductId = product.ID,
            MakerId = product.PublishHousingID,
            ProductTitle = product.Title,
            MakerTitle = product.Maker.Title,
            Category = product.Category,
            Price = product.Price,
            Description = product.Description
        };
}
