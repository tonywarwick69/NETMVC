Check users-secrets:
dotnet user-secrets list
I. Cloudinary keys will be saved through dotnet user-secret so to get Cloudninary keys:
1. Sign up for Cloudinary
2. Look for these params then save it somewhere secure: 
- API Key
- API Secret
- CloudName
3. Add Cloudinary settings in appsettings.json
    "CloudinarySettings": {
    "CloudName": "",
    "ApiKey": "",
    "ApiSecret": ""
  }
4. Add user-secret
- init in console with command: dotnet user-secret init
- dotnet user-secrets set "CloudinarySettings:CloudName" "YOURCloudName"
- dotnet user-secrets set "CloudinarySettings:ApiKey" "YOURApiKey"
- dotnet user-secrets set "CloudinarySettings:ApiSecret" "YOURApiSecret"
- dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YourDBConnectionString"

II. Migrations
Open nu console
To start migration for dotnet mvc with entity framework type:
add-migration InitialSchoolDB
To update database type:
Update-database migration_file_name
Add migration for identity framework (apply User & Role) type:
Add-migration identity
Then update SQL database by typing:
Update-Database
