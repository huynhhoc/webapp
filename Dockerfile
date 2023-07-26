# Set the base image to use for the container
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Set the working directory in the container
WORKDIR /app

# Copy the .csproj and .sln files to the container
COPY *.csproj .
COPY *.sln .

# Restore the NuGet packages
RUN dotnet restore

# Copy the rest of the source code to the container
COPY . .

# Build the application
RUN dotnet publish -c Release -o out

# Set the base image to use for the final runtime container
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Set the working directory in the container
WORKDIR /app

# Copy the output files from the build-env container to the final runtime container
COPY --from=build-env /app/out .

# Set the entry point for the container to start the web application
ENTRYPOINT ["dotnet", "webapp.dll"]
