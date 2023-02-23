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
RUN dotnet new tool-manifest
RUN dotnet tool install dotnet-ef
WORKDIR /app
RUN dotnet build
#RUN dotnet ef migrations add InitialCreate
RUN dotnet ef database update

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+
ENV CONN_STRING=EMPTY_BY_DEFAULT
#RUN dotnet add package Microsoft.EntityFrameworkCore.Sqlite
#RUN dotnet add package Microsoft.EntityFrameworkCore.Design
#RUN dotnet ef database update BlogAPI.dll
ENTRYPOINT ["dotnet", "BlogAPI.dll"]
