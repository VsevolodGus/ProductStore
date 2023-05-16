using ProductStore.Web.App.Service;
using Store;
using Store.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProductStore.Web.App;

internal class ProductService : IProductService
{
    private readonly IReadonlyRepository<ProductEntity> _products;
    private readonly IReadonlyRepository<MakerEntity> _makers;
    public ProductService(IReadonlyRepository<ProductEntity> products
        , IReadonlyRepository<MakerEntity> makers)
    {
        _makers = makers;
        _products = products;
    }

    /// <summary>
    /// Получение модели продукта по идентификатору
    /// </summary>
    /// <param name="id">идентификатор продукта</param>
    /// <returns>модель продукта</returns>
    public async Task<ProductModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var product = await _products.FirstOrDefaultAsync(c=> c.ID == id, cancellationToken);

        return Map(Product.Mapper.Map(product));
    }

    /// <summary>
    /// Получение продуктов по идентификатору
    /// </summary>
    /// <param name="makerID">идентификатор производителя</param>
    /// <returns>список моделей продуктов</returns>
    public async Task<ProductModel[]> GetAllByIdMakerAsync(int makerID, CancellationToken cancellationToken = default)
    {
        var list = await _products.ToArrayAsync(c => c.MakerID == makerID, cancellationToken);


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
        if(string.IsNullOrEmpty(search))
            array = await _products.ToArrayAsync(cancellationToken);
        else
            array = await _products.ToArrayAsync(c => c.Title.ToLower().Contains(search.ToLower())
                                                    || c.Category.ToLower().Contains(search.ToLower())
                                                    || c.Maker.Title.ToLower().Contains(search.ToLower())
                                , cancellationToken);

        return array.Select(Product.Mapper.Map).Select(Map).ToArray();
    }

    /// <summary>
    /// Метод маппинга сущность в модель
    /// </summary>
    /// <param name="product">сущность</param>
    /// <returns>модель</returns>
    private ProductModel Map(Product product)
    {
        return new ProductModel
        {
            ProductId = product.Id,
            MakerId = product.MakerID,
            ProductTitle = product.Title,
            MakerTitle = _makers.FirstOrDefaultAsync(c=> c.ID == product.MakerID).Result.Title,
            Category = product.Category,
            Price = product.Price,
            Description = product.Description
        };
    }
}
