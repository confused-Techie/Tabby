#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Tabby_Docker/Tabby_Docker.csproj", "Tabby_Docker/"]
RUN dotnet restore "Tabby_Docker/Tabby_Docker.csproj"
COPY . .
WORKDIR "/src/Tabby_Docker"
RUN dotnet build "Tabby_Docker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tabby_Docker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tabby_Docker.dll"]