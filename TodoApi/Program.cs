using TodoApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(s => s.AddDefaultPolicy(
	p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
builder.Services.AddDbContext<TodoContext>(builder => builder.UseInMemoryDatabase("TodoList"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// DÝZÝNLERDE index.html deafult yapýlýr.
app.UseDefaultFiles();

// STATÝK DOSYALARIN DIÞARIYA AÇILMASI
app.UseStaticFiles();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<TodoContext>();
	db.Database.EnsureCreated();
}

app.Run();


// Install-Package Migrosoft.EntityFrameworkCore
// Install-Package Migrosoft.EntityFrameworkCore.InMemory