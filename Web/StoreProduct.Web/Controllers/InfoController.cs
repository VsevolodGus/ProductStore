using Microsoft.AspNetCore.Mvc;
using ProductStore.Web.App;
using System.Threading.Tasks;

namespace StoreProduct.Web.Controllers;

public class InfoController : Controller
{
    private readonly ProductService productService;
    private readonly MakerService makerService;
    public InfoController(ProductService productService,
                          MakerService makerService)
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
        var model = await productService.GetByIdAsync(id);

        return View("InfoProduct", model);
    }

    /// <summary>
    /// получение данных о производителя по идентификатору
    /// </summary>
    /// <param name="id">идентификатор производителя</param>
    /// <returns>страница с информацией о производителе</returns>
    public async Task<IActionResult> InfoManufactureAsync(int id)
    {
        var manufacture = makerService.GetByIdAsync(id, HttpContext.RequestAborted);

        return View("InfoManufacture", manufacture);
    }
}
