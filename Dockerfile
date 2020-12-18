FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY src/Markisa.Application/*.csproj ./Markisa.HttpApi.Host/
COPY src/Markisa.Application.Contracts/*.csproj ./Markisa.Application.Contracts/
COPY src/Markisa.DbMigrator/*.csproj ./Markisa.DbMigrator/
COPY src/Markisa.Domain/*.csproj ./Markisa.Domain/
COPY src/Markisa.Domain.Shared/*.csproj ./Markisa.Domain.Shared/
COPY src/Markisa.EntityFrameworkCore/*.csproj ./Markisa.EntityFrameworkCore/
COPY src/Markisa.EntityFrameworkCore.DbMigrations/*.csproj ./Markisa.EntityFrameworkCore.DbMigrations/
COPY src/Markisa.HttpApi/*.csproj ./Markisa.HttpApi/
COPY src/Markisa.HttpApi.Client/*.csproj ./Markisa.HttpApi.Client/
COPY src/Markisa.HttpApi.Host/*.csproj ./Markisa.HttpApi.Host/
RUN RUN dotnet restore Markisa.HttpApi.Host/complexapp.csproj

# copy everything else and build app
COPY src/Markisa.Application/. ./Markisa.HttpApi.Host/
COPY src/Markisa.Application.Contracts/. ./Markisa.Application.Contracts/
COPY src/Markisa.DbMigrator/. ./Markisa.DbMigrator/
COPY src/Markisa.Domain/. ./Markisa.Domain/
COPY src/Markisa.Domain.Shared/. ./Markisa.Domain.Shared/
COPY src/Markisa.EntityFrameworkCore/. ./Markisa.EntityFrameworkCore/
COPY src/Markisa.EntityFrameworkCore.DbMigrations/. ./Markisa.EntityFrameworkCore.DbMigrations/
COPY src/Markisa.HttpApi/. ./Markisa.HttpApi/
COPY src/Markisa.HttpApi.Client/. ./Markisa.HttpApi.Client/
COPY src/Markisa.HttpApi.Host/. ./Markisa.HttpApi.Host/

WORKDIR /source/Markisa.HttpApi.Host
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Markisa.HttpApi.Host.dll"]