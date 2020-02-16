namespace Microsoft.Extensions.Configuration
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Hosting;

    public static class ConfigureBuilder<TStartup> where TStartup : class
    {
        private static IConfiguration _configuration = null;

        public static IConfiguration GetConfiguration(IHostEnvironment env)
        {
            if (_configuration == null)
            {
                var configBuilder = new ConfigurationBuilder()
                   .SetBasePath(env.ContentRootPath)
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                   .AddEnvironmentVariables()
                    ;

                if (env.IsDevelopment())
                {
                    configBuilder.AddUserSecrets<TStartup>(optional: true);
                }

                _configuration = configBuilder.Build();
            }

            return _configuration;
        }
    }
}
