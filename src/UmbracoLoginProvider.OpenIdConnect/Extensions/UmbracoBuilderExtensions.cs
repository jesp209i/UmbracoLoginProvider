using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Extensions;
using UmbracoLoginProvider.OpenIdConnect.Configuration;
using UmbracoLoginProvider.OpenIdConnect.Options;

namespace UmbracoLoginProvider.OpenIdConnect.Extensions;

public static class UmbracoBuilderExtensions
{
    public static IUmbracoBuilder AddExternalLoginProviders(this IUmbracoBuilder builder)
    {
        builder.Services.ConfigureOptions<ExternalLoginProviderOptions>();
        builder.Services.ConfigureOptions<ConfigureOpenIdConnectOptions>();

        var serviceProvider = builder.Services.BuildServiceProvider();
        var externalLoginOptions = serviceProvider.GetService<IOptions<ExternalLoginProviderConfiguration>>();
        var optionsSchemeName = externalLoginOptions.Value.LoginProviderAlias;
        
        builder.AddBackOfficeExternalLogins(logins =>
        {
            logins.AddBackOfficeLogin(backOfficeAuthenticationBuilder =>
            {
                var schemeName =
                    backOfficeAuthenticationBuilder.SchemeForBackOffice($"{Constants.Security.BackOfficeExternalAuthenticationTypePrefix}{optionsSchemeName}");

                ArgumentNullException.ThrowIfNull(schemeName);
                backOfficeAuthenticationBuilder.AddOpenIdConnect(
                    schemeName,
                    options =>
                    {
                        // Callback path: Represents the URL to which the browser should be redirected to.
                        // The default value is '/signin-provider'.
                        // The value here should match what you have configured in you external login provider.
                        // The value needs to be unique.

                        // Example: Map Claims
                        // Relevant when using auto-linking.
                        options.GetClaimsFromUserInfoEndpoint = true;
                        options.TokenValidationParameters.NameClaimType = "name";
                        // Example: Add scopes
                        options.Scope.Add("email");
                        
                    });
            });
        });
        return builder;
    }
    
}