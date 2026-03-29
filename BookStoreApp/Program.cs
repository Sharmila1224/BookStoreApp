using BookStoreApp.Data;
using BookStoreApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllersWithViews();

// Register DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


// 👉 SEED DATA (AUTO INSERT)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();

    if (!db.Authors.Any())
    {
        // Authors
        var author1 = new Author { Name = "Chetan Bhagat", Biography = "Popular Indian author" };
        var author2 = new Author { Name = "A.P.J Abdul Kalam", Biography = "Scientist and former President of India" };
        var author3 = new Author { Name = "R.K. Narayan", Biography = "Famous Indian writer" };

        db.Authors.AddRange(author1, author2, author3);
        db.SaveChanges();

        // Books
        db.Books.AddRange(
            new Book
            {
                Title = "2 States",
                ISBN = "111111",
                PublishedDate = DateTime.Now,
                Price = 300,
                AuthorId = author1.Id
            },
            new Book
            {
                Title = "Half Girlfriend",
                ISBN = "222222",
                PublishedDate = DateTime.Now,
                Price = 350,
                AuthorId = author1.Id
            },
            new Book
            {
                Title = "Five Point Someone",
                ISBN = "333333",
                PublishedDate = DateTime.Now,
                Price = 250,
                AuthorId = author1.Id
            },
            new Book
            {
                Title = "Wings of Fire",
                ISBN = "444444",
                PublishedDate = DateTime.Now,
                Price = 400,
                AuthorId = author2.Id
            },
            new Book
            {
                Title = "Ignited Minds",
                ISBN = "555555",
                PublishedDate = DateTime.Now,
                Price = 320,
                AuthorId = author2.Id
            },
            new Book
            {
                Title = "Malgudi Days",
                ISBN = "666666",
                PublishedDate = DateTime.Now,
                Price = 280,
                AuthorId = author3.Id
            }
        );

        db.SaveChanges();
    }
}

app.Run();