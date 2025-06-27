using MediatRApi.Application;
using MediatRApi.Data;
using Microsoft.EntityFrameworkCore;
using Hellang.Middleware.ProblemDetails;
using MediatRApi.Application.Common.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddMediatR(x =>
{
    x.RegisterServicesFromAssemblyContaining<GetDemoEntitiesQuery>();
});

builder.Services.AddProblemDetails(options =>
{
    options.IncludeExceptionDetails = (ctx, ex) => true;
    options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
    options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);
    options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
});

builder.Services.AddCors();

var app = builder.Build();
app.UseProblemDetails();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt =>
{
    opt.AllowAnyHeader();
    opt.AllowAnyMethod();
    opt.AllowAnyOrigin();
});

app.UseAuthorization();

using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
context.Database.EnsureCreated();

app.MapControllers();

app.Run();
