# Will use connectionstring from Presentation appsettings for scaffolding 
$settings = Get-Content "../CleanArchTemplate.Presentation/appsettings.json" | ConvertFrom-Json
$cs = $settings.ConnectionStrings.Sqlite

Set-Location -Path "../CleanArchTemplate.Infrastructure"
dotnet ef dbcontext scaffold "$cs" Microsoft.EntityFrameworkCore.SqlServer --context-dir Contexts --output-dir ../CleanArchTemplate.Domain/Entities --context ApplicationDbContext --no-onconfiguring -f
Set-Location -Path "../scripts"