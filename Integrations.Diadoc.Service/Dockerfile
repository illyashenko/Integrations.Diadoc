FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Integrations.Diadoc.Service/Integrations.Diadoc.Service.csproj", "Integrations.Diadoc.Service/"]
RUN dotnet restore "Integrations.Diadoc.Service/Integrations.Diadoc.Service.csproj"
COPY . .
WORKDIR "/src/Integrations.Diadoc.Service"
RUN dotnet build "Integrations.Diadoc.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Integrations.Diadoc.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Integrations.Diadoc.Service.dll"]
