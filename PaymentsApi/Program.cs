using NHibernate;
using PaymentsApi.Gateways;
using PaymentsApi.Services;
using PaymentsApi.Services.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPaymentGateway, PaymentGateway>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = false;
    });
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

var sessionFactory =
    NHibernateHelper.CreateSessionFactory(connectionString);

builder.Services.AddSingleton<ISessionFactory>(sessionFactory);
var app = builder.Build();

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
