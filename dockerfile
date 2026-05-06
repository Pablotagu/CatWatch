FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["CatWatch/CatWatch.csproj", "CatWatch/"]
RUN dotnet restore "CatWatch/CatWatch.csproj"
COPY . .
WORKDIR "/src/CatWatch"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CatWatch.dll"]
