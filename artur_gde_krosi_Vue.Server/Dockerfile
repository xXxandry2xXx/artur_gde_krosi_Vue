#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["artur_gde_krosi_vue.client/nuget.config", "artur_gde_krosi_vue.client/"]
COPY ["artur_gde_krosi_Vue.Server/artur_gde_krosi_Vue.Server.csproj", "artur_gde_krosi_Vue.Server/"]
COPY ["artur_gde_krosi_vue.client/artur_gde_krosi_vue.client.esproj", "artur_gde_krosi_vue.client/"]
RUN dotnet restore "./artur_gde_krosi_Vue.Server/artur_gde_krosi_Vue.Server.csproj"
COPY . .
WORKDIR "/src/artur_gde_krosi_Vue.Server"
RUN dotnet build "./artur_gde_krosi_Vue.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./artur_gde_krosi_Vue.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "artur_gde_krosi_Vue.Server.dll"]