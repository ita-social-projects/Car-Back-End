FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app
EXPOSE 80

COPY . ./
RUN dotnet restore Car.Backend.sln
RUN dotnet build ./Car.WebApi -c Release
RUN dotnet publish ./Car.WebApi/Car.WebApi.csproj -c Release -o carAPI --no-restore --no-build

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build-env /app/carAPI .

ENTRYPOINT ["dotnet", "Car.WebApi.dll"]