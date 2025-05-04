# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy the .csproj and restore dependencies (via NuGet)
COPY ./RFMoneyMatters.csproj ./RFMoneyMatters/

# Restore the dependencies
RUN dotnet restore RFMoneyMatters/RFMoneyMatters.csproj

# Copy the entire project into the container
COPY . .

# Set proper permissions for the app directory
RUN chmod -R 777 /app

# Publish the application in release mode (using a different output directory)
RUN dotnet publish ./RFMoneyMatters.csproj -c Release -o /tmp/publish

# Use the official .NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Set the working directory in the container
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /tmp/publish .

# Expose the port that the app will run on
EXPOSE 80

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "RFMoneyMatters.dll"]
