# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /app

#Copia el archivo .csproj al directorio de trabajo en el contenedor
COPY MovieAPI/*.csproj ./  
RUN dotnet restore

COPY . . 
RUN dotnet publish -c Release -o out 

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 5000
		
ENTRYPOINT ["dotnet", "MovieAPI.dll"]
