# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY ["src/Application/Application.csproj", "./src/Application/"]
COPY ["src/Domain/Domain.csproj", "./src/Domain/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "./src/Infrastructure/"]
COPY ["src/Sdk/Sdk.csproj", "./src/Sdk/"]
COPY ["src/WebApi/WebApi.csproj", "./src/WebApi/"]
COPY ["tests/WebApi.UnitTests/WebApi.UnitTests.csproj", "./tests/WebApi.UnitTests/"]
COPY ["Services.Accounts.slnx", "./"]
COPY ["Directory.Packages.props", "./"]
RUN dotnet restore "./Services.Accounts.slnx"

# Copy source and publish
COPY . .
RUN dotnet publish "./src/WebApi/WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "HomeBudgetManager.Services.Accounts.WebApi.dll"]
