using ApiITProducts.Data;
using ApiITProducts.Datasets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiITProducts.Controllers
{
    public class ShowAllSelectedProductsController : Controller
    {
        private readonly ProductsContext productcontext;


      //  private readonly ProductsContext productcontext;

        public ShowAllSelectedProductsController(ProductsContext context)
        {
            productcontext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet(nameof(GetProductsonPeriod))]
        public async Task<IActionResult> GetProductsonPeriod( string date1, string date2)
        {

            DateTime dt1 = DateTime.ParseExact(date1, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
            DateTime dt2 = DateTime.ParseExact(date2, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);


            //var result1 = await productcontext.Sales.ToListAsync();
            var result= await productcontext.Sales.Where(s => s.SaleDate >= dt1 && s.SaleDate<= dt2)
                .Include(s => s.Product)
                .Select(s=>  s.Product.CategoriesProducts.Select(
                    c=> new TopProductsDto
                    {
                        CategoryName=c.Category.Name,
                        ProductName= c.Product.Name,
                        Dateofsold= s.SaleDate.ToString("dd-MM-yyyy"),
                        TotalSold= s.Quantity

                    })).ToListAsync();

            //    .GroupBy(x => new { x.CategoryName, x.ProductName })
            //.Select(g => new
            //{
            //    g.Key.CategoryName,
            //    g.Key.ProductName,
            //    TotalSold = g.Sum(x => x.Quantity)
            //})
            //.GroupBy(x => x.CategoryName)
            //.Select(g => g.OrderByDescending(x => x.TotalSold).FirstOrDefault())
            //.Select(x => new TopProductDto
            //{
            //    CategoryName = x.CategoryName,
            //    ProductName = x.ProductName,
            //    TotalSold = x.TotalSold
            //}
            // )
            //.ToListAsync();




            var data = JsonConvert.SerializeObject(result);
            return Ok(data);



        }

        [HttpGet(nameof(GetProductsByCategory))]
        public async Task<IActionResult> GetProductsByCategory(string categoryname)
        {

            var categoryfromdb= productcontext.Categories.FirstOrDefault(c=>  c.Name.Equals(categoryname));


            var result2 = await productcontext.Sales.
                Where(s=> s.Product.CategoriesProducts.Any(c => c.Category.Id == categoryfromdb.Id))
                .SelectMany(p=> p.Product.CategoriesProducts
               
                .Select(x => new CategoryFilter()
                {
                    NameProduct = x.Product.Name,
                    Price = x.Product.Price,
                    CategoryName = x.Category.Name

                })).ToListAsync();

           // var data = JsonConvert.SerializeObject(result2);
            return Ok(result2);
        }
        [HttpGet(nameof(GetProductsByTown))]
        public async Task<IActionResult> GetProductsByTown(string town, string date1, string date2)
        {

            DateTime dt1 = DateTime.ParseExact(date1, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
            DateTime dt2 = DateTime.ParseExact(date2, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
             


            var result = await productcontext.Sales.Where(s => s.SaleDate >= dt1 && s.SaleDate <= dt2 && s.User.Town == town)
                .Select(s => new
                {
                   Date= s.SaleDate.ToString(),
                   Product= s.Product.Name,
                   Price= s.TotalPrice

                }).ToListAsync();

            var data = JsonConvert.SerializeObject(result);
            return Ok(data);
        }

        [HttpGet(nameof(GetMaxSoldPeriodByTown))]
        public async Task<IActionResult> GetMaxSoldPeriodByTown( string date1, string date2)
        {

            DateTime dt1 = DateTime.ParseExact(date1, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
            DateTime dt2 = DateTime.ParseExact(date2, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);



            var result = await productcontext.Sales.Where(s => s.SaleDate >= dt1 && s.SaleDate <= dt2)
                .Include(u => u.User)
               // group by town name
                .Select(s => new
                {
                    Town = s.User.Town,
                    DateSale = s.SaleDate,    // sum of products sold
                    TotalSold = s.TotalPrice, // optional: sum of revenue
                    SalesCount = s.Quantity

                    //Town = g.Key,
                    //TotalSold = g.Sum(s => s.Quantity),    // sum of products sold
                    //TotalRevenue = g.Sum(s => s.TotalPrice), // optional: sum of revenue
                    //SalesCount = g.Count()                 // optional: number of sales
                }).ToListAsync();

            var data = JsonConvert.SerializeObject(result);
            return Ok(data);
        }

        [HttpGet(nameof(GetAllProducts))]
        public async Task<IActionResult> GetAllProducts()
        {
            var AllProducts = await productcontext.Products
                .Include(p=> p.CategoriesProducts)
                .ThenInclude(c=> c.Category)
                .Select(p=> new
                {
                    PName=p.Name,
                    Quantity=p.Quantity,
                    PriceProduct=p.Price,
                    CategoryName= p.CategoriesProducts.FirstOrDefault(n => n.Productid== p.Id).Category.Name,
                    PicProduct=p.picurl
                })
                
                .ToListAsync();

            var data = JsonConvert.SerializeObject(AllProducts);

            return Ok(data);

        }

            [HttpGet(nameof(GetByCategoryMaxPrice))]
        public async Task<IActionResult> GetByCategoryMaxPrice( string date1, string date2)
        {


            string str_new = "exec pFindMaxSalesCategory '" +  date1 + "','" + date2 + "'";


            var resultnew = await productcontext.CatMaxPricesDto.FromSqlRaw(str_new).ToListAsync();

            var data = JsonConvert.SerializeObject(resultnew);

            return Ok(data);



        }


        }

}
