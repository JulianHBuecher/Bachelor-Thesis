FROM mcr.microsoft.com/dotnet/sdk:5.0.102-ca-patch-buster-slim AS build
RUN apt-get update && apt-get install -y locales

WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore "WeatherApi.csproj"

# copy everything else and build app
COPY . ./
RUN dotnet publish "WeatherApi.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
ADD ./myRootCA.pem /usr/local/share/ca-certificates/cert.crt
RUN apt-get update && \
	chmod 644 /usr/local/share/ca-certificates/cert.crt && \ 
	update-ca-certificates
RUN echo "Own Root CA is trusted: " && openssl verify /usr/local/share/ca-certificates/cert.crt

WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "WeatherApi.dll"]