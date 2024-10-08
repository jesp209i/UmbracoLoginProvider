using Microsoft.Extensions.Options;
using Umbraco.Cms.Core;
using Umbraco.Cms.Web.BackOffice.Security;
using UmbracoLoginProvider.OpenIdConnect.Configuration;

namespace UmbracoLoginProvider.OpenIdConnect.Options;

public class ExternalLoginProviderOptions : IConfigureNamedOptions<BackOfficeExternalLoginProviderOptions>
{
    public string SchemeName { get; set; } = "ExternalLoginProvider";

    public ExternalLoginProviderOptions(IOptions<ExternalLoginProviderConfiguration> externalLoginProviderConfiguration)
    {
        SchemeName = externalLoginProviderConfiguration.Value.LoginProviderAlias;
    }
    public void Configure(string? name, BackOfficeExternalLoginProviderOptions options)
    {
        if (name != Constants.Security.BackOfficeExternalAuthenticationTypePrefix + SchemeName)
        {
            return;
        }
        Configure(options);
    }
    
    public void Configure(BackOfficeExternalLoginProviderOptions options)
    {
        // Customize the login button
        options.Icon = "icon-cloud";
        

        // The following options are relevant if you
        // want to configure auto-linking on the authentication.
        options.AutoLinkOptions = new ExternalSignInAutoLinkOptions(

            // Set to true to enable auto-linking
            autoLinkExternalAccount: true,

            // [OPTIONAL]
            // Default: "Editor"
            // Specify User Group.
            defaultUserGroups: new[] { Constants.Security.EditorGroupAlias },

            // [OPTIONAL]
            // Default: The culture specified in appsettings.json.
            // Specify the default culture to create the User as.
            // It can be dynamically assigned in the OnAutoLinking callback.
            defaultCulture: null,

            // [OPTIONAL]
            // Disable the ability to link/unlink manually from within
            // the Umbraco backoffice.
            // Set this to false if you don't want the user to unlink
            // from this external provider.
            allowManualLinking: false
        )
        {
            // [OPTIONAL] Callback
            OnAutoLinking = (autoLinkUser, loginInfo) =>
            {
                // Customize the user before it's linked.
                // Modify the User's groups based on the Claims returned
                // in the external login info.
                autoLinkUser.IsApproved = true;
            },

            // [OPTIONAL] Callback
            OnExternalLogin = (user, loginInfo) =>
            {
                // Customize the User before it is saved whenever they have
                // logged in with the external provider.
                // Sync the Users name based on the Claims returned
                // in the external login info

                // Returns a boolean indicating if sign-in should continue or not.
                return true;
            }
        };

        // [OPTIONAL]
        // Disable the ability for users to login with a username/password.
        // If set to true, it will disable username/password login
        // even if there are other external login providers installed.
        options.DenyLocalLogin = false;

        // [OPTIONAL]
        // Choose to automatically redirect to the external login provider
        // effectively removing the login button.
        options.AutoRedirectLoginToExternalProvider = false;
    }
}