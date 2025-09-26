using LeaguesApi.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LeaguesApi.Filters;

public class ConditionalJwtFilter  : IOperationFilter
   
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasJwtAttr = context.MethodInfo.DeclaringType?.GetCustomAttributes(true)
                             .OfType<SwaggerJwtAuthAttribute>().Any() == true
                         || context.MethodInfo.GetCustomAttributes(true)
                             .OfType<SwaggerJwtAuthAttribute>().Any();

        if (hasJwtAttr)
        {
            new JwtHeaderOperationFilter().Apply(operation, context);
        }
    }
}
