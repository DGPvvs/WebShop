namespace WebShop.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WebShop.Core.Contracts;
    using WebShop.Core.Models;

    /// <summary>
    /// Web Shop products
    /// </summary>
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }


        /// <summary>
        /// List all products
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await this.productService.GetAll();
            ViewData["Title"] = nameof(products);

            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new ProductDto();
            ViewData["Title"] = "Add New Product";

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductDto model)
        {
            ViewData["Title"] = "Add New Product";

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await this.productService.Add(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult>Delete([FromForm] string id)
        {
            Guid guidId = Guid.Parse(id);

            await this.productService.Delete(guidId);

            return RedirectToAction(nameof(Index));
        }

    }
}
