# dotnetcore-mongo
My boilerplate dotnet core &amp; mongo solution including domain driven design approach

### MongoDB replica set setup
1. Change your directory  
`cd MongoDBSetup`  
2. Bootstrap replica set with docker-compose  
`docker-compose up`  
3. Configure and initiate replica set  
`docker exec -it mongo1 /scripts/setup.sh `
