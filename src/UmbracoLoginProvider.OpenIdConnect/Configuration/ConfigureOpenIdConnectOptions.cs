using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;

namespace UmbracoLoginProvider.OpenIdConnect.Configuration;

public class ConfigureOpenIdConnectOptions : IPostConfigureOptions<OpenIdConnectOptions>
{
    private readonly ExternalLoginProviderConfiguration _externalLoginProviderConfiguration;

    public ConfigureOpenIdConnectOptions(IOptions<ExternalLoginProviderConfiguration> externalLoginProviderConfiguration)
    {
        _externalLoginProviderConfiguration = externalLoginProviderConfiguration.Value;
    }
    public void PostConfigure(string? name, OpenIdConnectOptions options)
    {
        if (name != "Hest")
        {
            return;
        }
        
        // Callback path: Represents the URL to which the browser should be redirected to.
        // The default value is '/signin-provider'.
        // The value here should match what you have configured in you external login provider.
        // The value needs to be unique.
        options.CallbackPath = _externalLoginProviderConfiguration.CallBackPath;
        options.ClientId = _externalLoginProviderConfiguration.ClientId; // Replace with your client id generated while creating OAuth client ID
        options.ClientSecret = _externalLoginProviderConfiguration.ClientSecret; // Replace with your client secret generated while creating OAuth client ID

        options.Authority = _externalLoginProviderConfiguration.GetAuthority();
        // Example: Map Claims
        // Relevant when using auto-linking.
        options.GetClaimsFromUserInfoEndpoint = true;
        options.TokenValidationParameters.NameClaimType = "name";
        // Example: Add scopes
        options.Scope.Add("email");
    }
}