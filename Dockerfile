#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
#EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Web.Server/Web.Server.csproj", "Web.Server/"]
COPY ["SourceGeneration_1c8ef23e-6cbb-45cc-8661-72abcef0f120/SourceGeneration_1c8ef23e-6cbb-45cc-8661-72abcef0f120.csproj", "SourceGeneration_1c8ef23e-6cbb-45cc-8661-72abcef0f120/"]
COPY ["Communication/Communication.csproj", "Web.Common/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Business/Business.csproj", "Business/"]
COPY ["Data/Data.csproj", "Data/"]
RUN dotnet restore "Web.Server/Web.Server.csproj"
COPY . .
WORKDIR "/src/Web.Server"
RUN dotnet build "Web.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Server.dll"]