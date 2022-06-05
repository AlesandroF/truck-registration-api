FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Adapters/Http/Truck.Registration.Api/Truck.Registration.Api.csproj", "src/Adapters/Http/Truck.Registration.Api/"]
COPY ["src/Adapters/Infrastructure/MySql/Truck.Registration.MySql/Truck.Registration.MySql.csproj", "src/Adapters/Infrastructure/MySql/Truck.Registration.MySql/"]
COPY ["src/Core/Truck.Registration.Domain/Truck.Registration.Domain.csproj", "src/Core/Truck.Registration.Domain/"]
COPY ["src/Core/Truck.Registration.Application/Truck.Registration.Application.csproj", "src/Core/Truck.Registration.Application/"]
RUN dotnet restore "src/Adapters/Http/Truck.Registration.Api/Truck.Registration.Api.csproj"
COPY . .
WORKDIR "/src/src/Adapters/Http/Truck.Registration.Api"
RUN dotnet build "Truck.Registration.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Truck.Registration.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Truck.Registration.Api.dll