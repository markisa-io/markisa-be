FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build-env
WORKDIR /app

COPY Markisa.sln NuGet.Config common.props  ./
COPY modules/ ./modules/

# Markisa.sln NuGet.Config
# Copy csproj and restore as distinct layers
COPY src/Markisa.Application/*.csproj ./src/Markisa.Application/
COPY src/Markisa.Application.Contracts/*.csproj ./src/Markisa.Application.Contracts/
COPY src/Markisa.DbMigrator/*.csproj ./src/Markisa.DbMigrator/
COPY src/Markisa.Domain/*.csproj ./src/Markisa.Domain/
COPY src/Markisa.Domain.Shared/*.csproj ./src/Markisa.Domain.Shared/
COPY src/Markisa.EntityFrameworkCore/*.csproj ./src/Markisa.EntityFrameworkCore/
COPY src/Markisa.EntityFrameworkCore.DbMigrations/*.csproj ./src/Markisa.EntityFrameworkCore.DbMigrations/
COPY src/Markisa.HttpApi/*.csproj ./src/Markisa.HttpApi/
COPY src/Markisa.HttpApi.Client/*.csproj ./src/Markisa.HttpApi.Client/
COPY src/Markisa.HttpApi.Host/*.csproj ./src/Markisa.HttpApi.Host/

# Copy the test project files
COPY test/Markisa.Application.Tests/*.csproj test/Markisa.Application.Tests/
COPY test/Markisa.Domain.Tests/*.csproj test/Markisa.Domain.Tests/
COPY test/Markisa.EntityFrameworkCore.Tests/*.csproj test/Markisa.EntityFrameworkCore.Tests/
COPY test/Markisa.HttpApi.Client.ConsoleTestApp/*.csproj test/Markisa.HttpApi.Client.ConsoleTestApp/
COPY test/Markisa.TestBase/*.csproj test/Markisa.TestBase/

# Copy Conf 
COPY conf/dev/appsettings.json ./conf/dev/appsettings.json
COPY conf/prod/appsettings.json ./conf/prod/appsettings.json

RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app

COPY --from=build-env /app/out/ .
COPY conf/dev/appsettings.json /app/out/ .

ENTRYPOINT ["dotnet", "Markisa.HttpApi.Host.dll"]
