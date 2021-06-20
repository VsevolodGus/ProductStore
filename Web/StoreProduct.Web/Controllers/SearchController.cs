using Microsoft.AspNetCore.Mvc;
using Store;
using System.Collections.Generic;
using StoreProduct.Web.Models;

namespace StoreProduct.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ProductService productService;
        private readonly IMakerRepository makerRepository;
        public SearchController(ProductService productService,
                                IMakerRepository makerRepository)
        {
            this.productService = productService;
            this.makerRepository = makerRepository;
        }

        // получение из репозитория список продуктов по запросу и вывод данных на страницу
        public IActionResult Index(string query)
        {
            var products = new List<Product>(productService.GetAllByQuery(query));
            if (products == null || products.Count == 0)
                return View("EmptySearch");

            var model = new List<ProductModel>();

            foreach (var product in products)
            {
                model.Add(new ProductModel
                {
                    ProductId = product.Id,
                    MakerId = product.IdMaker,
                    ProductTitle = product.Title,
                    MakerTitle = makerRepository.GetById(product.IdMaker).Title,
                    Category = product.Category,
                    Price = product.Price,
                    Description = product.Description
                });
            }

            return View(model);
        }

        // получение списка продуктов определнного производителя, по Id Maker/Manifacture
        public IActionResult AllProductsMakers(int IdManufacture)
        {
            var products = new List<Product>(productService.GetAllByIdManufacture(IdManufacture));
            if (products == null || products.Count == 0)
                return View("EmptySearch");

            var model = new List<ProductModel>();

            foreach (var product in products)
            {
                model.Add(new ProductModel
                {
                    ProductId = product.Id,
                    MakerId = product.IdMaker,
                    ProductTitle = product.Title,
                    MakerTitle = makerRepository.GetById(product.IdMaker).Title,
                    Category = product.Category,
                    Price = product.Price,
                    Description = product.Description
                });
            }

            return View("Index", model);
        }



        // получание продукта из репозитория и вывод его полей, запрос по Id
        public IActionResult InfoProduct(int id)
        {
            var product = productService.GetById(id);

            var model = new ProductModel
            {
                ProductId = product.Id,
                MakerId = product.IdMaker,
                ProductTitle = product.Title,
                MakerTitle = makerRepository.GetById(product.IdMaker).Title,
                Category = product.Category,
                Price = product.Price,
                Description = product.Description
            };

            return View("InfoProduct", model);
        }

        // получание производителя из репозитория и вывод его полей, запрос по Id
        public IActionResult InfoManufacture(int id)
        {
            var manufacture = makerRepository.GetById(id);

            return View("InfoManufacture", manufacture);
        }
    }
}
