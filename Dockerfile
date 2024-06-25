FROM mcr.microsoft.com/mssql/server:2022-latest

ENV ACCEPT_EULA=Y \
    SA_PASSWORD=MyStrongPassword

COPY init.sql /docker-entrypoint-initdb.d/

EXPOSE 1433