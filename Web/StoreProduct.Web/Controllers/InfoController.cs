using Microsoft.AspNetCore.Mvc;
using ProductStore.Web.App.Service;
using System.Threading.Tasks;

namespace StoreProduct.Web.Controllers;

public class InfoController : Controller
{
    private readonly IProductService productService;
    private readonly IMakerService makerService;
    public InfoController(IProductService productService,
                          IMakerService makerService)
    {
        this.productService = productService;
        this.makerService = makerService;
    }

    /// <summary>
    /// получение данных о продукте по идентификатору
    /// </summary>
    /// <param name="id">идентификатор продукта</param>
    /// <returns>страница с информацией о продукте</returns>
    public async Task<IActionResult> InfoProduct(int id)
    {
        var model = await productService.GetByIDAsync(id, HttpContext.RequestAborted);

        return View("InfoProduct", model);
    }

    /// <summary>
    /// получение данных о производителя по идентификатору
    /// </summary>
    /// <param name="id">идентификатор производителя</param>
    /// <returns>страница с информацией о производителе</returns>
    public async Task<IActionResult> InfoManufacture(int id)
    {
        var manufacture = await makerService.GetByIdAsync(id, HttpContext.RequestAborted);

        return View("InfoManufacture", manufacture);
    }
}
