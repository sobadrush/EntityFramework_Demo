FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CodeFirst_Demo/CodeFirst_Demo.csproj", "CodeFirst_Demo/"]
RUN dotnet restore "CodeFirst_Demo/CodeFirst_Demo.csproj"
COPY . .
WORKDIR "/src/CodeFirst_Demo"
RUN dotnet build "CodeFirst_Demo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CodeFirst_Demo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CodeFirst_Demo.dll"]
