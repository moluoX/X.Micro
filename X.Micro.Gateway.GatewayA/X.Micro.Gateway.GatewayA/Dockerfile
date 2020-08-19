#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.azk8s.cn/dotnet/core/runtime:3.1-buster-slim AS build
WORKDIR /src
COPY ["X.Micro.Gateway.GatewayA/X.Micro.Gateway.GatewayA.csproj", "X.Micro.Gateway.GatewayA/"]
RUN dotnet restore "X.Micro.Gateway.GatewayA/X.Micro.Gateway.GatewayA.csproj"
COPY . .
WORKDIR "/src/X.Micro.Gateway.GatewayA"
RUN dotnet build "X.Micro.Gateway.GatewayA.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "X.Micro.Gateway.GatewayA.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "X.Micro.Gateway.GatewayA.dll"]