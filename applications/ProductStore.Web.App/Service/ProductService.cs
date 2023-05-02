using MoreLinq.Extensions;
using Store;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStore.Web.App
{
    public class ProductService
    {
        private readonly IProductRepository products;
        private readonly IMakerRepository makerRepository;

        public ProductService(IProductRepository products,
                              IMakerRepository makerRepository)
        {
            this.products = products;
            this.makerRepository = makerRepository;
        }

        /// <summary>
        /// Получение модели продукта по идентификатору
        /// </summary>
        /// <param name="id">идентификатор продукта</param>
        /// <returns>модель продукта</returns>
        public async Task<ProductModel> GetByIdAsync(int id)
        {
            var product = await products.GetByIdAsync(id);

            return Map(product);
        }

        /// <summary>
        /// Получение продуктов по идентификатору
        /// </summary>
        /// <param name="makerID">идентификатор производителя</param>
        /// <returns>список моделей продуктов</returns>
        public async Task<List<ProductModel>> GetAllByIdMakerAsync(int makerID)
        {
            var list = await products.GetAllByIdMakerAsync(makerID);

            return list.Select(Map).ToList();
        }

        /// <summary>
        /// Получение списка продуктов
        /// </summary>
        /// <param name="seatch">строка поиска</param>
        /// <returns>список моделей продуктов</returns>
        public async Task<List<ProductModel>> GetAllByQueryAsync(string seatch)
        {
            var listByTitle = await products.GetAllByTitleAsync(seatch);
            var listByCategory = await products.GetAllByCategoryAsync(seatch);
            var listByManufacture = await products.GetAllByManufactureAsync(seatch);


            var list = listByTitle.Union(listByCategory)
                                  .Union(listByManufacture)
                                  .DistinctBy(item => item.Id);
           

            return list.Select(Map).ToList();
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
                MakerId = product.IdMaker,
                ProductTitle = product.Title,
                MakerTitle = makerRepository.GetById(product.IdMaker).Title,
                Category = product.Category,
                Price = product.Price,
                Description = product.Description
            };
        }
    }
}
