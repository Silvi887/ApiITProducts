using ApiITProducts.Data;
using ApiITProducts.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddDbContext<ProductsContext>(options => options
         .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Import JSON after app starts
//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<ProductsContext>();

//    var projectDir = Directory.GetCurrentDirectory();
//    var usersJson = File.ReadAllText(Path.Combine(projectDir, "Datasets", "users.json"));
//    Console.WriteLine(ImportUsers(context, usersJson));

//    var categoriesJson = File.ReadAllText(Path.Combine(projectDir, "Datasets", "categories.json"));
//    Console.WriteLine(ImportCategories(context, categoriesJson));

//    var productsJson = File.ReadAllText(Path.Combine(projectDir, "Datasets", "products.json"));
//    Console.WriteLine(ImportProducts(context, productsJson));

//    var categoryProductsJson = File.ReadAllText(Path.Combine(projectDir, "Datasets", "categoriesproducts.json"));
//    Console.WriteLine(ImportCategoryProducts(context, categoryProductsJson));

//    var salesJson = File.ReadAllText(Path.Combine(projectDir, "Datasets", "sales.json"));
//    Console.WriteLine(ImportSalesProducts(context, salesJson));
//}





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

 static string ImportUsers(ProductsContext context, string inputJson)
{
    var Users = JsonConvert.DeserializeObject<User[]>(inputJson);
    context.Users.AddRange(Users);
    context.SaveChanges();

    return $"Successfully imported {Users.Count()}"; ;
}

 static string ImportProducts(ProductsContext context, string inputJson)
{
    var products = JsonConvert.DeserializeObject<Product[]>(inputJson);

    context.Products.AddRange(products);
    context.SaveChanges();

    return $"Successfully imported {products.Length}";
}

 static string ImportCategories(ProductsContext context, string inputJson)
{
    var Categories1 = JsonConvert.DeserializeObject<Category[]>(inputJson);

    int countc = 0;
    foreach (var cat in Categories1)
    {
        if (cat.Name != null)
        {
            context.Categories.Add(cat);
            countc++;
        }
    }


    context.SaveChanges();
    return $"Successfully imported {countc}";

}

 static string ImportCategoryProducts(ProductsContext context, string inputJson)
{
    var categoryProducts1 = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);
    context.Categoriesproducts.AddRange(categoryProducts1);

    context.SaveChanges();
    return $"Successfully imported {categoryProducts1.Count()}";


}

 static string ImportSalesProducts(ProductsContext context, string inputJson)
{
    var Salesproducts = JsonConvert.DeserializeObject<Sale[]>(inputJson);
    context.Sales.AddRange(Salesproducts);

    context.SaveChanges();
    return $"Successfully imported {Salesproducts.Count()}";


}

