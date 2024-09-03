From the root of the project, run the following commands:

```bash
cd SqlServer
docker build -t rocklogic-sqlserver-image .
docker run -p 1433:1433 --name rocklogic-sqlserver rocklogic-sqlserver-image
```
