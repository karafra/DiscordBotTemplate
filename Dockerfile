FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

LABEL maintainer="Karafra"

WORKDIR /src
COPY ["Bot/Bot.csproj", "./"]
RUN dotnet restore "./Bot.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Bot/Bot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bot/Bot.csproj" -c Release -o /app/publish

FROM build AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bot.dll"]