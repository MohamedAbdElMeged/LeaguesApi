using LeaguesApi.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LeaguesApi.Filters;

public class ConditionalClientFilter: IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasClientAttr = context.MethodInfo.DeclaringType?.GetCustomAttributes(true)
                                .OfType<SwaggerClientAuthAttribute>().Any() == true
                            || context.MethodInfo.GetCustomAttributes(true)
                                .OfType<SwaggerClientAuthAttribute>().Any();

        if (hasClientAttr)
        {
            new ClientCredentialsHeaderOperationFilter().Apply(operation, context);
        }
    }
}