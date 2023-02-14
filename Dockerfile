FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY ["BlogAPI/BlogAPI.csproj", "/app/"]
RUN dotnet restore "/app/BlogAPI.csproj"
COPY . /app

RUN dotnet build BlogAPI -c Release -o /app/build

FROM build AS publish
RUN dotnet publish BlogAPI -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+
ENV CONN_STRING=EMPTY_BY_DEFAULT
ENTRYPOINT ["dotnet", "BlogAPI.dll"]
