using Microsoft.AspNetCore.Mvc;
using Store;
using StoreProduct.Web.Models;
using System.Collections.Generic;

namespace StoreProduct.Web.Controllers
{
    public class InfoController : Controller
    {
        private readonly ProductService productService;
        private readonly IMakerRepository makerRepository;
        public InfoController(ProductService productService,
                              IMakerRepository makerReposytory)
        {
            this.productService = productService;
            this.makerRepository = makerReposytory;
        }


        // получание продукта из репозитория и вывод его полей, запрос по Id
        public IActionResult InfoProduct(int id)
        {
            var product = productService.GetById(id);
            
            var model =new ProductModel
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
        public IActionResult InfoManufactures(int id)
        {
            var manufacture = makerRepository.GetById(id);

            return View("InfoManufacture", manufacture);
        }
    }
}
