#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["X.Micro.Client.ClientA/X.Micro.Client.ClientA.csproj", "X.Micro.Client.ClientA/"]
RUN dotnet restore "X.Micro.Client.ClientA/X.Micro.Client.ClientA.csproj"
COPY . .
WORKDIR "/src/X.Micro.Client.ClientA"
RUN dotnet build "X.Micro.Client.ClientA.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "X.Micro.Client.ClientA.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "X.Micro.Client.ClientA.dll"]