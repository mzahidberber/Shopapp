#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["shopapp.web/shopapp.web.csproj", "shopapp.web/"]
COPY ["shopapp.business/shopapp.business.csproj", "shopapp.business/"]
COPY ["shopapp.core/shopapp.core.csproj", "shopapp.core/"]
COPY ["shopapp.dataaccess/shopapp.dataaccess.csproj", "shopapp.dataaccess/"]
RUN dotnet restore "shopapp.web/shopapp.web.csproj"
COPY . .
WORKDIR "/src/shopapp.web"
RUN dotnet build "shopapp.web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "shopapp.web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "shopapp.web.dll"]