Cloudinary keys will be saved through dotnet user-secret so to get Cloudninary keys:
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
