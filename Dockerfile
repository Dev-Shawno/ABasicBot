FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ABasicBot/ABasicBot.csproj ABasicBot/
COPY ShawnoStudios.Common.Discord/ShawnoStudios.Common.DiscordUtils.csproj ShawnoStudios.Common.Discord/
RUN dotnet restore ABasicBot/ABasicBot.csproj
COPY . .
RUN dotnet publish ABasicBot/ABasicBot.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ABasicBot.dll"]
