REQUISITOS
IDE: Visual Studio 2022

CONFIGURAÇÃO (Configuration)
ARQUIVO: appsettings.json
AÇÃO: Modificar a Url com a URL da sua API.

{
  "ApiSettings": {
    "Url": "https://api.com/endpoint"
  }
}
ARQUIVO: Secrets.cs
AÇÃO: Criar o arquivo Secrets.cs na raiz do projeto com o seguinte conteúdo.

namespace MauiApp2;

internal static class Secrets
{
    internal const string ApiKey = "CHAVE_DA_API";
}

EXECUÇÃO (Run)
ABRIR a solução MauiApp2.sln no Visual Studio.

SELECIONAR o dispositivo de destino na barra de ferramentas

INICIAR a depuração
