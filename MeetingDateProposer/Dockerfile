FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
COPY *.sln .
COPY MeetingDateProposer/*.csproj ./MeetingDateProposer/
COPY MeetingDateProposer.BusinessLayer/*.csproj ./MeetingDateProposer.BusinessLayer/
COPY MeetingDateProposer.DataLayer/*.csproj ./MeetingDateProposer.DataLayer/
COPY MeetingDateProposer.Domain/*.csproj ./MeetingDateProposer.Domain/
COPY MeetingDateProposer.Tests.xUnit/*.csproj ./MeetingDateProposer.Tests.xUnit/
RUN dotnet restore "MeetingDateProposer/MeetingDateProposer.csproj"
COPY . .
WORKDIR /source/MeetingDateProposer
RUN dotnet build "MeetingDateProposer.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR /source/MeetingDateProposer
RUN apt-get update && \
apt-get -y install curl gnupg && \
curl -sL https://deb.nodesource.com/setup_16.x  | bash - && \
apt-get -y install build-essential nodejs
RUN dotnet publish "MeetingDateProposer.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS release
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet",  "MeetingDateProposer.dll" ]