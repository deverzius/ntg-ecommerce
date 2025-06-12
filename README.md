# NTG E-commerce

## Migrate Database

To create new migration.

```bash
cd .\src\ResourceServer\CommerceCore.Infrastructure
dotnet ef migrations add InitDatabase --project . --startup-project ../CommerceCore.WebAPI/CommerceCore.WebAPI.csproj
```

To update the database using migration files.

```bash
cd .\src\ResourceServer\CommerceCore.Infrastructure
dotnet ef database update --project . --startup-project ../CommerceCore.WebAPI/CommerceCore.WebAPI.csproj
```

To drop the database.

```bash
cd .\src\ResourceServer\CommerceCore.Infrastructure
dotnet ef database drop --project . --startup-project ../CommerceCore.WebAPI/CommerceCore.WebAPI.csproj
```

## How To Run

Start the Identity Server:

```bash
cd src\CommerceCore.IdentityServer
dotnet run
```

Start the Web API:

```bash
cd src\ResourceServer\CommerceCore.WebAPI
dotnet run
```

Start the Customers Site:

```bash
cd src\WebApp\CommerceCore.Web.CustomersSite
dotnet run
```

Start the Admin Site:

```bash
cd src\WebApp\CommerceCore.Web.AdminSite
pnpm run dev
```
