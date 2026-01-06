using LibraryApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();


builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));


var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.MapControllers();
app.Run();
