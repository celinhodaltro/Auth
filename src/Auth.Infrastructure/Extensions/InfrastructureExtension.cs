using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Core.Infrastructure.Extensions;

public static class InfrastructureExtension
{


    public static string GetConnectionString(this IConfiguration configuration)
    {
        // Carregar a string de conexão
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Substituir as variáveis de ambiente
        connectionString = connectionString
            .Replace("${DB_HOST}", Environment.GetEnvironmentVariable("DB_HOST"))
            .Replace("${DB_PORT}", Environment.GetEnvironmentVariable("DB_PORT"))
            .Replace("${DB_NAME}", Environment.GetEnvironmentVariable("DB_NAME"))
            .Replace("${DB_USER}", Environment.GetEnvironmentVariable("DB_USER"))
            .Replace("${DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD"));

        // Verificar se a string de conexão foi carregada corretamente
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("A string de conexão não foi encontrada.");
        }


        return connectionString;
    }

}

