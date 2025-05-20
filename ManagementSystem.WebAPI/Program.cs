using FluentValidation.AspNetCore;
using ManagementSystem.BLL.DependencyResolvers;
using ManagementSystem.BLL.MappingProfiles;
using ManagementSystem.BLL.ValidationRules.AppRoleValidations;
using ManagementSystem.BLL.ValidationRules.AppUserValidations;
using ManagementSystem.BLL.ValidationRules.CategoryValidations;
using ManagementSystem.BLL.ValidationRules.ProductValidations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// serilog configuration
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext() // Optional context information for each log record
    .WriteTo.Console()
    .CreateLogger();

// integrate serilog to host
builder.Host.UseSerilog();
/* With the log configuration and adding the log to the host, .net core directly installs the di mechanism, and there is no need to write log di additionally. */

// adding memory cache
builder.Services.AddMemoryCache();

// adding redis cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "RedisCache";
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextService();
builder.Services.AddRepositoryManagerServices();
builder.Services.AddIdentityService();
builder.Services.AddAutoMapper(typeof(GeneralMapping).Assembly);
builder.Services.AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>(); });
builder.Services.AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<UpdateProductValidator>(); });
builder.Services.AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<CreateCategoryValidator>(); });
builder.Services.AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<UpdateCategoryValidator>(); });
builder.Services.AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<AssignRoleDtoValidator>(); });
builder.Services.AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<CreateAppRoleRequestDtoValidator>(); });
builder.Services.AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<UpdateAppRoleRequestDtoValidator>(); });
builder.Services.AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<ChangePasswordRequestDtoValidator>(); });
builder.Services.AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<CreateAppUserLoginRequestDtoValidator>(); });
builder.Services.AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<CreateAppUserRequestDtoValidator>(); });
builder.Services.AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<UpdateAppUserRequestDtoValidator>(); });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// http request logging http method - url - http status - default log level automatically
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();