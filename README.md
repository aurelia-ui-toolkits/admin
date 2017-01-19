## Components

### API 
- dotnet core (API only, no views or anything)
- ASP.Net Identity
- IdentityServer4 (so we can do OAuth)
- [ASP.Net identity mongo connector](https://github.com/g0t4/aspnet-identity-mongo)
- works in a docker container

There is currently just one controller (CategoryController) that has a Get() and a Post() action. The Post() action requires authentication, the Get() does not.

### Database
MongoDB, also through docker

---------------------

I made a docker-compose.yml file which runs these two docker containers. I really like that you can create a docker-compose configuration file, as I easily forget the exact `docker run` command equivalents.

Basically what needs to be done is:

### API
- we should be able to create a user in the development database and live database. not sure yet how we are going to do that. ASP.NET Identity's UserManager can create a user (I verified that this works) but I do not know yet at which point to create the user (we may do this manually in mongodb)

- In addition to the existing CategoryController we need a SampleController, through which we can get the gist url's. We should also be able to add gists (and metadata such as name, tags and categories) to this database, through a POST request (authenticated, of course)

- This API should also have an endpoint equivalent to https://gist-serve.jeroenvinke.nl/files/c2ffde936c8caee5c35b3c08a596b410. This service (currently running on my VPS) serves files from GitHub bypassing:
  - rate limit restrictions
  - incorrect mime types sent by GitHub

### Frontend
There is no frontend yet, but an aurelia-cli app will do I think

### Database
- I have not thought about the schema's of Categories and Samples (what do we need to save of a sample in the database)

---------------------------


### Run docker containers
1. `docker-compose up` to run both the API and the database containers (or `docker-compose up database` for just the mongodb database)

### Sample requests
**Get all categories:**
GET http://localhost:5000/api/category/

**Create a category:**
POST http://localhost:5000/api/category/
body:
```
{
  "name": "foo"
}
```

**Get OAuth access token in postman:**
![img](http://i.imgur.com/kGB9oce.png)


