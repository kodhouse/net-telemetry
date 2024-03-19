# Use the official .NET Core SDK as the build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "./MetricAggregator/MetricAggregator.csproj" --disable-parallel
RUN dotnet publish "./MetricAggregator/MetricAggregator.csproj" -c release -o /app --no-restore


FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5137

ENTRYPOINT ["dotnet", "MetricAggregator.dll"]