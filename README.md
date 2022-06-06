# truck-registration-api

Hey guys

this project had the objective of bringing an api construction for a simple purpose, however the architecture and components used were of a higher level, a little to leave common for whenever possible to update the knowledge, because after all we have several ways to solve our problems.

this projects is currently running on the link -> https://truck-registration-api.herokuapp.com/swagger/index.html

with swagger you could test all interactions available in the application.

For you to run this local application you have 2 options:

1 - You can upload a local Mysql database (I recommend uploading with docker) and change the database settings in the appsettings.json file at the root of the api, and then open the "Package manager console" window and run the command " update-database", this will generate all tables in your database, and you can run the system normally!

2 - You can point to the database that is already in the appsettings.json, that is, simply run the application right after cloning it (remembering that I will soon remove the database from the server)
