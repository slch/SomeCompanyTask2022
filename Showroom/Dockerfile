FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Showroom/Showroom.csproj", "Showroom/"]
RUN dotnet restore "Showroom/Showroom.csproj"
COPY . .
WORKDIR "/src/Showroom"
RUN dotnet build "Showroom.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Showroom.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Showroom.dll"]
