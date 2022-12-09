FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BitSourceMe.WebApi/BitSourceMe.WebApi.csproj", "BitSourceMe.WebApi/"]
COPY ["BitSourceMe.Core/BitSourceMe.Core.csproj", "BitSourceMe.Core/"]
COPY ["BitSourceMe.Core.Abstractions/BitSourceMe.Core.Abstractions.csproj", "BitSourceMe.Core.Abstractions/"]
COPY ["BitSourceMe.Data/BitSourceMe.Data.csproj", "BitSourceMe.Data/"]
COPY ["BitSourceMe.Data.InMemory/BitSourceMe.Data.InMemory.csproj", "BitSourceMe.Data.InMemory/"]
COPY ["BitSourceMe.Data.Sqlite/BitSourceMe.Data.Sqlite.csproj", "BitSourceMe.Data.Sqlite/"]
RUN dotnet restore "BitSourceMe.WebApi/BitSourceMe.WebApi.csproj"
COPY . .
WORKDIR "/src/BitSourceMe.WebApi"
RUN dotnet build "BitSourceMe.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BitSourceMe.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BitSourceMe.WebApi.dll"]
