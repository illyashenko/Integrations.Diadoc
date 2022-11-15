FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Integrations.Diadoc.Data/Integrations.Diadoc.Data.csproj", "Integrations.Diadoc.Data/"]
COPY ["Integrations.Diadoc.Infrastructure/Integrations.Diadoc.Infrastructure.csproj", "Integrations.Diadoc.Infrastructure/"]
COPY ["Integrations.Diadoc.Service/Integrations.Diadoc.Service.csproj", "Integrations.Diadoc.Service/"]
RUN dotnet restore -s "https://api.nuget.org/v3/index.json" -s "http://tfs-ds-01.pickpoint.lcl/nuget" "Integrations.Diadoc.Service/Integrations.Diadoc.Service.csproj"
COPY . .
WORKDIR "/src/Integrations.Diadoc.Service"
RUN dotnet build "Integrations.Diadoc.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Integrations.Diadoc.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN ln -sf /usr/share/zoneinfo/Europe/Moscow /etc/localtime
ENTRYPOINT ["dotnet", "Integrations.Diadoc.Service.dll"]
