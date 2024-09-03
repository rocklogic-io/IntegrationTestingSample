#!/bin/bash

# Set variables for the SA password, database username, and database name
DB_USERNAME='rocklogic'
DB_NAME='rocklogic'

# Start SQL Server in the background
/opt/mssql/bin/sqlservr &

# Wait for SQL Server to start up
sleep 60s

# Create the login and user using the SA credentials
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "$SA_PASSWORD" -d master -Q "CREATE LOGIN $DB_USERNAME WITH PASSWORD='$SA_PASSWORD';"
sleep 1s
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "$SA_PASSWORD" -d master -Q "CREATE DATABASE $DB_NAME;"
sleep 1s
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "$SA_PASSWORD" -d master -Q "USE $DB_NAME; CREATE USER $DB_USERNAME FOR LOGIN $DB_USERNAME; ALTER ROLE db_owner ADD MEMBER $DB_USERNAME;"

# Wait for SQL Server to stop
wait
