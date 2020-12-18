FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

COPY Markisa.sln NuGet.Config  ./

# Copy the main source project files
COPY src/Markisa.Application/Markisa.Application.csproj src/Markisa.Application/Markisa.Application.csproj
COPY src/Markisa.Application.Contracts/Markisa.Application.Contracts.csproj src/Markisa.Application.Contracts/Markisa.Application.Contracts.csproj
COPY src/Markisa.DbMigrator/Markisa.DbMigrator.csproj src/Markisa.DbMigrator/Markisa.DbMigrator.csproj
COPY src/Markisa.Domain/Markisa.Domain.csproj src/Markisa.Domain/Markisa.Domain.csproj
COPY src/Markisa.Domain.Shared/Markisa.Domain.Shared.csproj src/Markisa.Domain.Shared/Markisa.Domain.Shared.csproj
COPY src/Markisa.EntityFrameworkCore/Markisa.EntityFrameworkCore.csproj src/Markisa.EntityFrameworkCore/Markisa.EntityFrameworkCore.csproj
COPY src/Markisa.EntityFrameworkCore.DbMigrations/Markisa.EntityFrameworkCore.DbMigrations.csproj src/Markisa.EntityFrameworkCore.DbMigrations/Markisa.EntityFrameworkCore.DbMigrations.csproj
COPY src/Markisa.HttpApi/Markisa.HttpApi.csproj src/Markisa.HttpApi/Markisa.HttpApi.csproj
COPY src/Markisa.HttpApi.Client/Markisa.HttpApi.Client.csproj src/Markisa.HttpApi.Client/Markisa.HttpApi.Client.csproj
COPY src/Markisa.HttpApi.Host/Markisa.HttpApi.Host.csproj src/Markisa.HttpApi.Host/Markisa.HttpApi.Host.csproj

RUN dotnet restore

COPY src src  
RUN dotnet build -c Release --no-restore

RUN dotnet publish "src/Markisa.HttpApi.Host/Markisa.HttpApi.Host.csproj" -c Release -o "../../dist" --no-restore

# copy csproj and restore as distinct layers
# COPY src/Markisa.Application/*.csproj ./Markisa.Application/
# COPY src/Markisa.Application.Contracts/*.csproj ./Markisa.Application.Contracts/
# COPY src/Markisa.DbMigrator/*.csproj ./Markisa.DbMigrator/
# COPY src/Markisa.Domain/*.csproj ./Markisa.Domain/
# COPY src/Markisa.Domain.Shared/*.csproj ./Markisa.Domain.Shared/
# COPY src/Markisa.EntityFrameworkCore/*.csproj ./Markisa.EntityFrameworkCore/
# COPY src/Markisa.EntityFrameworkCore.DbMigrations/*.csproj ./Markisa.EntityFrameworkCore.DbMigrations/
# COPY src/Markisa.HttpApi/*.csproj ./Markisa.HttpApi/
# COPY src/Markisa.HttpApi.Client/*.csproj ./Markisa.HttpApi.Client/
# COPY src/Markisa.HttpApi.Host/*.csproj ./Markisa.HttpApi.Host/
# RUN dotnet restore Markisa.HttpApi.Host/Markisa.HttpApi.Host.csproj

# copy everything else and build app
# COPY src/Markisa.Application/. ./Markisa.Application/
# COPY src/Markisa.Application.Contracts/. ./Markisa.Application.Contracts/
# COPY src/Markisa.DbMigrator/. ./Markisa.DbMigrator/
# COPY src/Markisa.Domain/. ./Markisa.Domain/
# COPY src/Markisa.Domain.Shared/. ./Markisa.Domain.Shared/
# COPY src/Markisa.EntityFrameworkCore/. ./Markisa.EntityFrameworkCore/
# COPY src/Markisa.EntityFrameworkCore.DbMigrations/. ./Markisa.EntityFrameworkCore.DbMigrations/
# COPY src/Markisa.HttpApi/. ./Markisa.HttpApi/
# COPY src/Markisa.HttpApi.Client/. ./Markisa.HttpApi.Client/
# COPY src/Markisa.HttpApi.Host/. ./Markisa.HttpApi.Host/

# WORKDIR /source/Markisa.HttpApi.Host
# RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=builder /source/dist .  
ENTRYPOINT ["dotnet", "Markisa.HttpApi.Host.dll"]