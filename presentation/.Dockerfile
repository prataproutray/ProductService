# Use a .NET 9 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /ProductService-1
COPY ["ProductService.presentation.WebApi.csproj", "ProductService.presentation/"]
RUN dotnet restore "ProductService.presentation/ProductService.presentation.WebApi.csproj"
COPY . .
WORKDIR "/ProductService-1/ProductService.presentation"
RUN dotnet build "ProductService.presentation.WebApi.csproj" -c Release -o /app/build

# Use a .NET 9 ASP.NET Core runtime image for the final application
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "ProductService.presentation.WebApi.dll"]