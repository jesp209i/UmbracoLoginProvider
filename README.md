# Umbraco External Login Provider
This is just to play around with the Umbraco External Login Provider functionality

## Microsoft Entra ID
Create a new App registration in Microsoft Azure Entra ID.

You will need:
- Application (client) ID
- Directory (tenant) ID
- to generate a Client Secret (Do this in App > Manage > Certificate & secrets )
- Authority Url (find in App > Overview > Endpoints)
  - Remove the guid/tenant id in the end to get the base url needed

You will also need to add redirect urls:
- http://localhost:your-http-port/sigin-oidc
- https://localhost:your-https-port/sigin-oidc
- https://yourdomain.com/signin-oidc

You can change the `/signin-oidc` to something else if you'd like, just remember to update the CallBackPath 

## external-login.json

```json
{
  "External": {
    "CallBackPath" : "/signin-oidc",
    "TenantId" : "my-directory-or-tenant-id",
    "ClientId": "my-application-or-client-id",
    "ClientSecret" : "my-secret-secret",
    "AuthorityBase" : "https://login.microsoftonline.com",
    "LoginProviderAlias" : "LoginProviderAliasNoSpaces",
    "LoginProviderPrettyName" : "Login Provider Pretty Name"
  }
}
```