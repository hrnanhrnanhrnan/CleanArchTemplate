Set-Location -Path "../"
dotnet ef migrations remove --project CleanArchTemplate.Infrastructure/CleanArchTemplate.Infrastructure.csproj --startup-project CleanArchTemplate.Presentation/CleanArchTemplate.Presentation.csproj
Set-Location -Path "./scripts"