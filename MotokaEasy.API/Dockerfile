FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MotokaEasy.API/MotokaEasy.API.csproj", "MotokaEasy.API/"]
COPY ["MotokaEasy.Application/MotokaEasy.Application.csproj", "MotokaEasy.Application/"]
COPY ["MotokaEasy.Core/MotokaEasy.Core.csproj", "MotokaEasy.Core/"]
COPY ["MotokaEasy.Domain/MotokaEasy.Domain.csproj", "MotokaEasy.Domain/"]
COPY ["MotokaEasy.Infrastructure/MotokaEasy.Infrastructure.csproj", "MotokaEasy.Infrastructure/"]
COPY ["MotokaEasy.Consumers/MotokaEasy.Consumers.csproj", "MotokaEasy.Consumers/"]
RUN dotnet restore "MotokaEasy.API/MotokaEasy.API.csproj"
COPY . .
WORKDIR "/src/MotokaEasy.API"
RUN dotnet build "MotokaEasy.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MotokaEasy.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MotokaEasy.API.dll"]
