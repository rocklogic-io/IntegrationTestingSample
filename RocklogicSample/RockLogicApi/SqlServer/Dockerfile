# Use the official Microsoft SQL Server 2019 image as the base image
FROM mcr.microsoft.com/mssql/server:2019-latest

# Set environment variables for SQL Server
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=Passw0rd
ENV MSSQL_PID=Express

# Switch to root to install the necessary tools
USER root

# Install curl and the SQL Server tools
RUN apt-get update && apt-get install -y \
    curl \
    gnupg \
    gnupg2 \
    gnupg1 \
    apt-transport-https \
    && curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - \
    && curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list | tee /etc/apt/sources.list.d/msprod.list \
    && apt-get update && apt-get install -y mssql-tools unixodbc-dev \
    && echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc \
    && apt-get clean && rm -rf /var/lib/apt/lists/*

# Switch back to the SQL Server user
USER mssql

# Expose the SQL Server port
EXPOSE 1433

# Create an entrypoint script to set up the user
COPY entrypoint.sh /usr/src/app/entrypoint.sh

# Set the entrypoint to the script
ENTRYPOINT ["/usr/src/app/entrypoint.sh"]
