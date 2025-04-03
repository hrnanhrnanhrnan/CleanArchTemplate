Set-Location -Path "../"
dotnet ef database update --project CleanArchTemplate.Infrastructure/CleanArchTemplate.Infrastructure.csproj --startup-project CleanArchTemplate.Presentation/CleanArchTemplate.Presentation.csproj
Set-Location -Path "./scripts"