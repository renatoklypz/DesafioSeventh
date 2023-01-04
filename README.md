# DesafioSeventh

# Instruções Gerais
## Configuração
> DesafioSeventh.Web/appsettings.json

1. Configure o banco de dados
- Dê preferencia ao banco de dados SQLServer
- Para fim de teste utilize um arquivo .mdf 


```json
{
  "ConnectionStrings": {
    "Default": "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=_CAMINHO DO ARQUIVO_.mdf;Integrated Security=True;Connect Timeout=30"
  }
}
```

2. Configure as informações de vídeos
- Path: Local onde os binários serão armazenados
- AcceptedExtensions: Formatos (extensões) de arquivos aceitos pelo programa

```json
{
  "VideoConfig": {
    "Path": "__LOCAL_PATH__",
    "AcceptedExtensions": ["mp4", "avi", "wmv", "avchd"]
  }
}
```

# Explicando Arquitetura
## Modelo
> **Domain Driven Design**: Objetiva padronizar o acesso as funções de regras de negócio
### Camadas
- **Aplicação**: DesafioSeventh.Web
- **Domain** (Interfaces): DesafioSeventh.Domain
- **Services** (Regra de negócio): DesafioSeventh.Service
- **Infrastructure** (Repositório/Outros): DesafioSeventh.Infra.Data
## Configuração Web
### Tratamento de Erro
> DesafioSeventh.Web.Helpers.ErrorResultHelper

#### Mapeamento de erros e HttpStatus
Os códigos de erros são definidos na classe base `DesafioSeventh.Domain.Global.ExceptionCode`.

```csharp
// ...
public static class ErrorResultHelper
{
	private static Dictionary<string, HttpStatusCode> errorCodes = new Dictionary<string, HttpStatusCode>()
	{
		{"err_conflict", HttpStatusCode.Conflict },
		{"server_not_found", HttpStatusCode.NotFound },
		{"err_not_exists", HttpStatusCode.NotFound },
		{"err_validation", HttpStatusCode.BadRequest },
		{"recycle_invalid_day", HttpStatusCode.BadRequest },
		{"vid_invalid_file", HttpStatusCode.BadRequest }
	};
}
```

#### Interceptação de Erros
> A configuração de erros personalizados ocorre em `Program.cs`
```csharp
// Extensão do mapeamento de erro (dentre outras)
using DesafioSeventh.Web.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Remove a validação de entidade automática na controller
// Resumo: Remove validação do DataAnnotation
// Com isso toda validação deverá ser realizada na classe de negócio
builder.Services.AddControllers(opt =>
{
	opt.ModelValidatorProviders.Clear();
})

var app = builder.Build();

// Ativa erros personalizados
// Conferir `DesafioSeventh.Domain.Global.ExceptionCode.UseErrorMap`
app.UseErrorMap();
```

### Injeção de dependência
> A configuração de injeção de dependência ocorre em `Program.cs`
```csharp
// Extensão do mapeamento de Injeção (dentre outras)
using DesafioSeventh.Web.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Configuração de injeção de dependência
builder.Services.AddInject();
```

### Workers
> Serviços rodados em background

```csharp
using DesafioSeventh.Web.Workers;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureServices(services =>
{
	//Adiciona serviço que apaga arquivos por servidor
	services.AddHostedService<DeleteServerWorker>()
	//Adiciona serviço que apaga arquivos antigos
			.AddHostedService<RecycleWorker>();
});
```

#### Remoção de arquivos de Servidor Apagado
> Acesse: `DesafioSeventh.Web.Workers.DeleteServerWorker`

#### Remoção de arquivos antigos (Recycler)
> Acesse: `DesafioSeventh.Web.Workers.RecycleWorker`

# Postman
- Import: `./API Desafio.postman_collection.json`
- Environment: `./Local.postman_environment.json`