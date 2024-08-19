# Use the ASP.NET 5.0 runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 9100

# Use the .NET SDK 8.0 for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["*.csproj", "./"]
RUN dotnet restore "*.csproj"

# Copy the entire source code into the container
COPY . .

# Build the application
WORKDIR "/src"
RUN dotnet build "C#_Project.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "C#_Project.csproj" -c Release -o /app/publish

# Use the base image for the final stage
FROM base AS final
WORKDIR /app

# Copy the published output to the final image
COPY --from=publish /app/publish .

# Specify the command to run the application
ENTRYPOINT ["dotnet", "C#_Project.dll"]
