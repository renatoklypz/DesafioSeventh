# DesafioSeventh

# Instru��es Gerais
## Configura��o
> DesafioSeventh.Web/appsettings.json

1. Configure o banco de dados
- D� preferencia ao banco de dados SQLServer
- Para fim de teste utilize um arquivo .mdf 


```json
{
  "ConnectionStrings": {
    "Default": "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=_CAMINHO DO ARQUIVO_.mdf;Integrated Security=True;Connect Timeout=30"
  }
}
```

2. Configure as informa��es de v�deos
- Path: Local onde os bin�rios ser�o armazenados
- AcceptedExtensions: Formatos (extens�es) de arquivos aceitos pelo programa

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
> **Domain Driven Design**: Objetiva padronizar o acesso as fun��es de regras de neg�cio
### Camadas
- **Aplica��o**: DesafioSeventh.Web
- **Domain** (Interfaces): DesafioSeventh.Domain
- **Services** (Regra de neg�cio): DesafioSeventh.Service
- **Infrastructure** (Reposit�rio/Outros): DesafioSeventh.Infra.Data
## Configura��o Web
### Tratamento de Erro
> DesafioSeventh.Web.Helpers.ErrorResultHelper

#### Mapeamento de erros e HttpStatus
Os c�digos de erros s�o definidos na classe base `DesafioSeventh.Domain.Global.ExceptionCode`.

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

#### Intercepta��o de Erros
> A configura��o de erros personalizados ocorre em `Program.cs`
```csharp
// Extens�o do mapeamento de erro (dentre outras)
using DesafioSeventh.Web.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Remove a valida��o de entidade autom�tica na controller
// Resumo: Remove valida��o do DataAnnotation
// Com isso toda valida��o dever� ser realizada na classe de neg�cio
builder.Services.AddControllers(opt =>
{
	opt.ModelValidatorProviders.Clear();
})

var app = builder.Build();

// Ativa erros personalizados
// Conferir `DesafioSeventh.Domain.Global.ExceptionCode.UseErrorMap`
app.UseErrorMap();
```

### Inje��o de depend�ncia
> A configura��o de inje��o de depend�ncia ocorre em `Program.cs`
```csharp
// Extens�o do mapeamento de Inje��o (dentre outras)
using DesafioSeventh.Web.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Configura��o de inje��o de depend�ncia
builder.Services.AddInject();
```

### Workers
> Servi�os rodados em background

```csharp
using DesafioSeventh.Web.Workers;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureServices(services =>
{
	//Adiciona servi�o que apaga arquivos por servidor
	services.AddHostedService<DeleteServerWorker>()
	//Adiciona servi�o que apaga arquivos antigos
			.AddHostedService<RecycleWorker>();
});
```

#### Remo��o de arquivos de Servidor Apagado
> Acesse: `DesafioSeventh.Web.Workers.DeleteServerWorker`

#### Remo��o de arquivos antigos (Recycler)
> Acesse: `DesafioSeventh.Web.Workers.RecycleWorker`

# Postman
- Import: `./API Desafio.postman_collection.json`
- Environment: `./Local.postman_environment.json`