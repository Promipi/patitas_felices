#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["patitas_felices.API/patitas_felices.API.csproj", "patitas_felices.API/"]
RUN dotnet restore "patitas_felices.API/patitas_felices.API.csproj"
COPY . .
WORKDIR "/src/patitas_felices.API"
RUN dotnet build "patitas_felices.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "patitas_felices.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "patitas_felices.API.dll"]