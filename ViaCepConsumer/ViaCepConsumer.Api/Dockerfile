FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /build
COPY . ./

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /runtime
COPY --from=build /build/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "ViaCepConsumer.Api.dll"]