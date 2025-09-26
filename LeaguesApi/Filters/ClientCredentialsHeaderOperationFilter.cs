using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LeaguesApi.Filters;

public class ClientCredentialsHeaderOperationFilter  : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-Client-Id",
            In = ParameterLocation.Header,
            Description = "Client Id",
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = "string"
            }
        });

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-Client-Secret",
            In = ParameterLocation.Header,
            Description = "Client Secret",
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = "string"
            }
        });
    }
}