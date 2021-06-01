FROM mcr.microsoft.com/dotnet/sdk:5.0.300-buster-slim AS build
RUN apt-get update && apt-get install -y locales

WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore "ML.Proxy.csproj"

# copy everything else and build app
COPY . ./
RUN dotnet publish "ML.Proxy.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "ML.Proxy.dll"]