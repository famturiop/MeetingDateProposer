#FROM mcr.microsoft.com/mssql/server:2019-latest AS sql
#ENV ACCEPT_EULA=Y
#ENV SA_PASSWORD=!234Qwer

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
EXPOSE 80
EXPOSE 443
EXPOSE 44343
EXPOSE 44344

COPY *.sln .
COPY MeetingDateProposer/*.csproj ./MeetingDateProposer/
COPY MeetingDateProposer.BusinessLayer/*.csproj ./MeetingDateProposer.BusinessLayer/
COPY MeetingDateProposer.DataLayer/*.csproj ./MeetingDateProposer.DataLayer/
COPY MeetingDateProposer.Domain/*.csproj ./MeetingDateProposer.Domain/
COPY MeetingDateProposer.Tests.xUnit/*.csproj ./MeetingDateProposer.Tests.xUnit/

RUN dotnet restore

COPY . .
WORKDIR /source/MeetingDateProposer

COPY MeetingDateProposer/secrets-tmp.json .
RUN cat secrets-tmp.json | dotnet user-secrets set

RUN dotnet build "MeetingDateProposer.csproj" -c Release -o /app 

# FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS release
# WORKDIR /app
# COPY --from=build /app ./
# ENTRYPOINT ["dotnet", "MeetingDateProposer.dll"]
RUN dotnet dev-certs https --clean
RUN dotnet dev-certs https -t
CMD ["dotnet", "run", "--launch-profile", "MeetingDateProposerProd"]