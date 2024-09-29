namespace UmbracoLoginProvider.OpenIdConnect.Configuration;

public class ExternalLoginProviderConfiguration
{
    public string CallBackPath { get; set; }
    public string TenantId { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string AuthorityBase { get; set; } = "https://login.microsoftonline.com";

    public string GetAuthority()
    {
        return $"{AuthorityBase}/{TenantId}/v2.0";
    }
}