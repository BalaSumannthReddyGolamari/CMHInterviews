FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CMHInterviews/CMHInterviews.csproj", "CMHInterviews/"]
RUN dotnet restore "CMHInterviews/CMHInterviews.csproj"
COPY . .
WORKDIR "/src/CMHInterviews"
RUN dotnet build "CMHInterviews.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CMHInterviews.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CMHInterviews.dll"]