FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base

RUN apt-get update
RUN apt-get install -y gnupg2
RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs

WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build

RUN apt-get update
RUN apt-get install -y gnupg2
RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs

WORKDIR /src
COPY ["src/LuKaSo.RabbitMqSagaExample.Portal/LuKaSo.RabbitMqSagaExample.Portal.csproj", "src/LuKaSo.RabbitMqSagaExample.Portal/"]
RUN dotnet restore "src/LuKaSo.RabbitMqSagaExample.Portal/LuKaSo.RabbitMqSagaExample.Portal.csproj"
COPY . .
WORKDIR "/src/src/LuKaSo.RabbitMqSagaExample.Portal"
RUN dotnet build "LuKaSo.RabbitMqSagaExample.Portal.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "LuKaSo.RabbitMqSagaExample.Portal.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LuKaSo.RabbitMqSagaExample.Portal.dll"]
