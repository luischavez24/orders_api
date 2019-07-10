FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
# COPY ["DistributedSystems.Orders.Api/DistributedSystems.Orders.Api.csproj", "DistributedSystems.Orders.Api/"]
COPY ["./", "DistributedSystems.Orders.Api/"]

RUN dotnet restore "DistributedSystems.Orders.Api/DistributedSystems.Orders.Api.csproj"
COPY . .
WORKDIR "/src/DistributedSystems.Orders.Api"
RUN dotnet build "DistributedSystems.Orders.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DistributedSystems.Orders.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DistributedSystems.Orders.Api.dll"]