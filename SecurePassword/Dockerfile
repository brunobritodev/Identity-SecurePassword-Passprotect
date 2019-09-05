FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["SecurePassword/SecurePassword.csproj", "SecurePassword/"]
RUN dotnet restore "SecurePassword/SecurePassword.csproj"
COPY . .
WORKDIR "/src/SecurePassword"
RUN dotnet build "SecurePassword.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SecurePassword.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SecurePassword.dll"]