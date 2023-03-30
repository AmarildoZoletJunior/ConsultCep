#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://+:1234;http://+:80;
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ConsultCep.API/ConsultCep.API.csproj", "ConsultCep.API/"]
COPY ["ConsultCep.Domain/ConsultCep.Domain.csproj", "ConsultCep.Domain/"]
RUN dotnet restore "ConsultCep.API/ConsultCep.API.csproj"
COPY . .
WORKDIR "/src/ConsultCep.API"
RUN dotnet build "ConsultCep.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConsultCep.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsultCep.API.dll"]