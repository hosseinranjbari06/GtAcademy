FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# copy csproj files and restore
COPY ["GtAcademy.Web/GtAcademy.Web.csproj", "GtAcademy.Web/"]
COPY ["GtAcademy.Application/GtAcademy.Application.csproj", "GtAcademy.Application/"]
COPY ["GtAcademy.Infrastructure/GtAcademy.Infrastructure.csproj", "GtAcademy.Infrastructure/"]
COPY ["GtAcademy.Domain/GtAcademy.Domain.csproj", "GtAcademy.Domain/"]

RUN dotnet restore "GtAcademy.Web/GtAcademy.Web.csproj"

# copy everything else and publish
COPY . .
WORKDIR /src/GtAcademy.Web
RUN dotnet publish -c Release -o /app/publish

# runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80

COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "GtAcademy.Web.dll"]
