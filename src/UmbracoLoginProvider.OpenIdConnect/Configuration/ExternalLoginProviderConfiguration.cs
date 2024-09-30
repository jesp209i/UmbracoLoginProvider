using System.ComponentModel.DataAnnotations;

namespace UmbracoLoginProvider.OpenIdConnect.Configuration;

public class ExternalLoginProviderConfiguration
{
    public ExternalLoginProviderConfiguration()
    {
        
    }
    [Required]
    public string CallBackPath { get; set; }
    [Required]
    public string TenantId { get; set; }
    [Required]
    public string ClientId { get; set; }
    [Required]
    public string ClientSecret { get; set; }
    [Required]
    public string AuthorityBase { get; set; } = "https://login.microsoftonline.com";

    public string LoginProviderAlias { get; set; }
    public string LoginProviderPrettyName { get; set; }

    public string GetAuthority()
    {
        return $"{AuthorityBase.TrimEnd('/')}/{TenantId}/v2.0";
    }
}