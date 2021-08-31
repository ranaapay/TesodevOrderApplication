#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Order_Api/Order_Api.csproj", "Order_Api/"]
COPY ["LoggerService/LoggerService.csproj", "LoggerService/"]
COPY ["OrderCore/OrderCore.csproj", "OrderCore/"]
RUN dotnet restore "Order_Api/Order_Api.csproj"
COPY . .
WORKDIR "/src/Order_Api"
RUN dotnet build "Order_Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order_Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order_Api.dll"]
