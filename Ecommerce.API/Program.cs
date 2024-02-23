using Ecommerce.Repositorio.DBContext;
using Microsoft.EntityFrameworkCore;

using Ecommerce.Repositorio.Contrato;
using Ecommerce.Repositorio.Implementacion;

using Ecommerce.Utilidades;

using Ecommerce.Servicio.Contrato;
using Ecommerce.Servicio.Implementacion;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Conexion a base de datos 
builder.Services.AddDbContext<DbecommerceContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL"));
});

builder.Services.AddTransient(typeof(IGenericoRepositorio<>), typeof(GenericoRepositorio<>)); //AddTransient porque sabemos para que entidad sera 

builder.Services.AddScoped<IVentaRepositorio, VentaRepositorio>(); // AddScoped porque ya sabemos que sera en la entidad venta 

builder.Services.AddAutoMapper(typeof(AutoMapperProfile)); //Cambio de modelo a DTO y DTO a modelo

builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddScoped<ICategoriaServicio, CategoriaServicio>();
builder.Services.AddScoped<IProductoServicio, ProductoServicio>();
builder.Services.AddScoped<IVentaServicio, VentaServicio>();
builder.Services.AddScoped<IDashboardServicio, DashboardServicio>();

// Se usa para evitar incovenientes entre las url front y back
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
}

);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();
