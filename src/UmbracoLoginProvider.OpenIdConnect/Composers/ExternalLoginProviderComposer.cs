using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using UmbracoLoginProvider.OpenIdConnect.Configuration;
using UmbracoLoginProvider.OpenIdConnect.Extensions;

namespace UmbracoLoginProvider.OpenIdConnect.Composers;

public class ExternalLoginProviderComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("login-provider.json", true)
            .Build();
        
        builder.Services.AddOptions<ExternalLoginProviderConfiguration>()
            .Bind(configuration.GetSection("External"))
            .ValidateDataAnnotations();
        
        builder.AddExternalLoginProviders();

        // configure this at some point
        // we dont want this logging in a production environment
        IdentityModelEventSource.ShowPII = true;
    }
}