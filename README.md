# NTG Ecommerce

### Run project

```
cd D:\ASP-repo\ntg-ecommerce\src\ApiServer\CommerceCore.WebApi

cd .\src\ResourceServer\CommerceCore.Infrastructure
dotnet ef migrations add InitDatabase --project . --startup-project ../CommerceCore.WebApi/CommerceCore.WebApi.csproj
dotnet ef database update --project . --startup-project ../CommerceCore.WebApi/CommerceCore.WebApi.csproj
```
