FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["journey.csproj", "./"]
RUN dotnet restore "journey.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "journey.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "journey.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "journey.dll"]
