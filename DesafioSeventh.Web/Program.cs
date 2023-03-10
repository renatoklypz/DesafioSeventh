using DesafioSeventh.Web.Helpers;
using DesafioSeventh.Web.Workers;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureServices(services =>
{
	services.AddHostedService<DeleteServerWorker>()
			.AddHostedService<RecycleWorker>();
});

SetupHelper.Configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers(opt =>
{
	opt.ModelValidatorProviders.Clear();
})
	.AddNewtonsoftJson(a =>
	{
		var jsonFormatting = SetupHelper.JSONFormatSetting;
		a.SerializerSettings.ContractResolver = jsonFormatting.ContractResolver;
		a.SerializerSettings.Formatting = jsonFormatting.Formatting;
		a.SerializerSettings.NullValueHandling = jsonFormatting.NullValueHandling;
		a.SerializerSettings.TypeNameAssemblyFormatHandling = jsonFormatting.TypeNameAssemblyFormatHandling;
		a.AllowInputFormatterExceptionMessages = true;
	});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
	s.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DesafioSeventh.Web.xml"), true);
	s.EnableAnnotations();
});

builder.Services.AddInject();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

//Erros personalizados
app.UseErrorMap();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
