using FluentValidation;
using MediatR;
using System.Reflection;
using Truck.Registration.Api.GlobalErrorHandler;
using Truck.Registration.Application.Pipeline;
using Truck.Registration.Application.UseCases.Add;
using Truck.Registration.Application.UseCases.Get;
using Truck.Registration.Application.UseCases.GetById;
using Truck.Registration.Application.ValueObject;
using Truck.Registration.MySql.Module;

var builder = WebApplication.CreateBuilder(args);

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
var appSettings = appSettingsSection.Get<AppSettings>();
builder.Services.Configure<AppSettings>(appSettingsSection);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMySqlModule();
builder.Services.AddMySqlContext(appSettings.ConnectionString);

builder.Services.AddMediatR(typeof(Program));

builder.Services.AddMediatR(typeof(AddCommand).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(GetCommand).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(GetByIdCommand).GetTypeInfo().Assembly);

AssemblyScanner.FindValidatorsInAssembly(typeof(AddCommand).Assembly)
    .ForEach(item => builder.Services.AddScoped(item.InterfaceType, item.ValidatorType));

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();

app.Run();