#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CELSIS.Api/CELSIS.Api.csproj", "CELSIS.Api/"]
RUN dotnet restore "CELSIS.Api/CELSIS.Api.csproj"
COPY . .
WORKDIR "/src/CELSIS.Api"
RUN dotnet build "CELSIS.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CELSIS.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CELSIS.Api.dll"]