Set-Location -Path "../"
dotnet ef migrations add Init --project CleanArchTemplate.Infrastructure/CleanArchTemplate.Infrastructure.csproj --startup-project CleanArchTemplate.Presentation/CleanArchTemplate.Presentation.csproj
Set-Location -Path "./scripts"