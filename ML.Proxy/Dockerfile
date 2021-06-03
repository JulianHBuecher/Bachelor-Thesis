FROM mcr.microsoft.com/dotnet/sdk:5.0.300-buster-slim AS build
RUN apt-get update && apt-get install -y locales

WORKDIR /app

# copy csproj and restore as distinct layers
COPY ML.Proxy/*.csproj ./ML.Proxy/
COPY ThrottlR/*.csproj ./ThrottlR/
RUN dotnet restore "./ML.Proxy/ML.Proxy.csproj"
RUN dotnet restore "./ThrottlR/ThrottlR.csproj"

# copy everything else and build app
COPY ML.Proxy/. ./ML.Proxy/
COPY ThrottlR/. ./ThrottlR/
RUN dotnet publish "./ML.Proxy/ML.Proxy.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "ML.Proxy.dll"]