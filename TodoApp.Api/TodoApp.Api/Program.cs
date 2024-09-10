using TodoApp.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// 2. Configurar los servicios de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
// builder.Services.AddRazorPages();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApp API V1");
    c.RoutePrefix = string.Empty; // Para que Swagger UI esté en la raíz
});

// Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }

// app.UseHttpsRedirection();
// app.UseStaticFiles();

app.UseRouting();

// app.UseAuthorization();
//
// app.MapRazorPages();

app.MapGet("api/v1/prueba/{nombreUsuario}", (string nombreUsuario) => $"Hola, {nombreUsuario}");

app.MapGet("sumar/{a}/{b}", (int a, int b) => $"La suma de {a} y {b} es {a + b}");

app.MapPost("tareas", (Tarea tarea) =>
{
    File.AppendAllText("tareas.txt", $"{tarea.Nombre} - {tarea.Completada}");
    return Results.Created($"/tareas/{tarea.Nombre}", tarea);
})
.Produces<Tarea>();

app.Run();