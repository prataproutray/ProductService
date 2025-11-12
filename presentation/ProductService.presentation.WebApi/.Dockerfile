# Use a .NET 9 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["presentation/ProductService.presentation.WebApi/ProductService.presentation.WebApi.csproj", "presentation/ProductService.presentation.WebApi/"]
RUN dotnet restore "presentation/ProductService.presentation.WebApi/ProductService.presentation.WebApi.csproj"
COPY . .
WORKDIR "/src/presentation/ProductService.presentation.WebApi"
RUN dotnet build "ProductService.presentation.WebApi.csproj" -c Release -o /app/build

# Use a .NET 9 ASP.NET Core runtime image for the final application
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "ProductService.presentation.WebApi.dll"]