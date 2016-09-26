dotnet restore
dotnet build .\cn.jpush.api

#dotnet test .\cn.jpush.api.test

dotnet pack .\cn.jpush.api
$project = Get-Content .\cn.jpush.api\project.json | ConvertFrom-Json
$version = $project.version.Trim("-*")
nuget push .\cn.jpush.api\bin\Debug\binbin.cn.jpush.api.$version.nupkg -source nuget -apikey $env:NUGET_API_KEY
