FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/LuKaSo.RabbitMqSagaExample.StrategyB/LuKaSo.RabbitMqSagaExample.StrategyB.csproj", "src/LuKaSo.RabbitMqSagaExample.StrategyB/"]
RUN dotnet restore "src/LuKaSo.RabbitMqSagaExample.StrategyB/LuKaSo.RabbitMqSagaExample.StrategyB.csproj"
COPY . .
WORKDIR "/src/src/LuKaSo.RabbitMqSagaExample.StrategyB"
RUN dotnet build "LuKaSo.RabbitMqSagaExample.StrategyB.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "LuKaSo.RabbitMqSagaExample.StrategyB.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LuKaSo.RabbitMqSagaExample.StrategyB.dll"]