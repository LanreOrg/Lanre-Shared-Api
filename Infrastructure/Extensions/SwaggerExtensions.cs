// Copyright (c) Lanre. All rights reserved.

namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.OpenApi.Models;

    public static class SwaggerExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, string title, string version)
        {
            services.AddSwaggerGen(setup =>
                {
                    setup.DescribeAllParametersInCamelCase();
                    setup.DescribeStringEnumsInCamelCase();
                    setup.SwaggerDoc(version, new OpenApiInfo
                    {
                        Title = title,
                        Version = version,
                    });

                    setup.CustomSchemaIds(x => x.FullName);
                });

            return services;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, string displayText)
        {
            return app.UseSwagger()
                      .UseSwaggerUI(setup =>
                      {
                          setup.SwaggerEndpoint("/swagger/v1/swagger.json", displayText);
                          setup.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                      });
        }
    }
}