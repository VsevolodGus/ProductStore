using Microsoft.AspNetCore.Mvc;
using ProductStore.Web.App;
using System.Threading.Tasks;

namespace StoreProduct.Web.Controllers;

public class SearchController : Controller
{
    private readonly ProductService productService;

    public SearchController(ProductService productService)
    {
        this.productService = productService;
    }

    /// <summary>
    /// Получение списка продуктов
    /// </summary>
    /// <param name="search">строка поиска</param>
    /// <returns>страница списка продуктов</returns>
    public async Task<IActionResult> Index(string search)
    {
        var models = await productService.GetAllByQueryAsync(search);
        if (models == null || models.Length == 0)
            return View("EmptySearch");

        return View(models);
    }

    /// <summary>
    /// получение списка продуктов по идентификатору производителя
    /// </summary>
    /// <param name="makerID">идентификатор производителя</param>
    /// <returns>страница списка продуктов</returns>
    public async Task<IActionResult> GetAllProductsMakers(int makerID)
    {
        var model = await productService.GetAllByIdMakerAsync(makerID);
        if (model == null || model.Length == 0)
            return View("EmptySearch");

        return View("Index", model);
    }
}
