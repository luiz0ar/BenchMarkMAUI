using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Text;

namespace MauiApp2;

public class BenchmarkService
{
    private readonly IConfiguration _configuration;
    private static readonly HttpClient Client = new();

    public BenchmarkService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> RunFibonacciBenchmarkAsync(int number)
    {
        var stopwatch = Stopwatch.StartNew();

        long result = await Task.Run(() => Fibonacci(number));

        stopwatch.Stop();

        var sb = new StringBuilder();
        sb.AppendLine("Cálculo Finalizado!");
        sb.AppendLine($"Número: {number}");
        sb.AppendLine("------------------------------------------");
        sb.AppendLine($"O Resultado: {result}");
        sb.AppendLine("------------------------------------------");
        sb.AppendLine($"Tempo de execução: {stopwatch.ElapsedMilliseconds} ms");
        return sb.ToString();
    }

    public async Task<string> RunApiBenchmarkAsync()
    {
        var url = _configuration["ApiSettings:Url"];
        var apiKey = Secrets.ApiKey;

        var stopwatch = Stopwatch.StartNew();
        var sb = new StringBuilder();

        try
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new InvalidOperationException("URL da API não configurada");
            }
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("Chave da API não configurada");
            }

            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("X-API-Key", apiKey);

            var response = await Client.SendAsync(request);
            stopwatch.Stop();

            if (response.IsSuccessStatusCode)
            {
                sb.AppendLine("Requisição finalizada com SUCESSO!");
                sb.AppendLine($"Status Code: {(int)response.StatusCode}");
            }
            else
            {
                sb.AppendLine("Requisição FALHOU!");
                sb.AppendLine($"Status Code: {(int)response.StatusCode}");
                sb.AppendLine("------------------------------------------");
                sb.AppendLine($"Motivo: {response.ReasonPhrase}");
            }
        }
        catch (Exception ex)
        {
            if (stopwatch.IsRunning) stopwatch.Stop();
            sb.AppendLine("Requisição FALHOU!");
            sb.AppendLine("------------------------------------------");
            sb.AppendLine("Detalhes do Erro:");
            sb.AppendLine(ex.Message);
        }

        sb.AppendLine("------------------------------------------");
        sb.AppendLine($"Tempo de execução: {stopwatch.ElapsedMilliseconds} ms");
        return sb.ToString();
    }

    private static long Fibonacci(int n)
    {
        return n <= 1 ? n : Fibonacci(n - 1) + Fibonacci(n - 2);
    }
}