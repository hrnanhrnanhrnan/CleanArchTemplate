param(
    [string]$CurrentName = $(Split-Path -Leaf (Get-Location)),
    [Parameter(Mandatory = $true)]
    [string]$NewName,

    [switch]$ReinitializeGit
)

Write-Host "Starting rename: '$CurrentName' => '$NewName'" -ForegroundColor Cyan

# ✅ Filtyper vi bryr oss om
$targetExtensions = @(".csproj", ".sln", ".cs", ".json", ".ps1")

# 🔍 Steg 1: Byt namn på undermappar först (djupast först)
$foldersToRename = Get-ChildItem -Recurse -Directory | Where-Object {
    $_.FullName -notmatch "\\(bin|obj)\\"
} | Sort-Object FullName -Descending

foreach ($folder in $foldersToRename) {
    if ($folder.Name -like "*$CurrentName*") {
        $newFolderName = $folder.Name -replace [Regex]::Escape($CurrentName), $NewName
        try {
            Rename-Item -Path $folder.FullName -NewName $newFolderName -ErrorAction Stop
            Write-Host "Renamed folder: $($folder.Name) => $newFolderName"
        } catch {
            Write-Warning "Could not rename folder '$($folder.FullName)': $($_.Exception.Message)"
        }
    }
}

# 🔍 Steg 2: Byt namn på toppnivå-mappar
$topLevelFolders = Get-ChildItem -Directory | Where-Object {
    $_.Name -like "*$CurrentName*"
}

foreach ($folder in $topLevelFolders) {
    $newFolderName = $folder.Name -replace [Regex]::Escape($CurrentName), $NewName
    try {
        Rename-Item -Path $folder.FullName -NewName $newFolderName -ErrorAction Stop
        Write-Host "Renamed top-level folder: $($folder.Name) => $newFolderName"
    } catch {
        Write-Warning "Could not rename top-level folder '$($folder.FullName)': $($_.Exception.Message)"
    }
}

# 📄 Steg 3: Samla alla relevanta filer för innehålls- och filnamnsändringar
$filesToProcess = Get-ChildItem -Recurse -File | Where-Object {
    ($_.FullName -notmatch "\\(bin|obj)\\") -and
    ($targetExtensions -contains $_.Extension) -and
    ($_.Name -ne "rename-template.ps1")
} | Sort-Object FullName

# ✏️ Steg 4: Byt innehåll i filer
foreach ($file in $filesToProcess) {
    try {
        $content = Get-Content $file.FullName -Raw -Encoding UTF8
        if ($content -like "*$CurrentName*") {
            $newContent = $content -replace [Regex]::Escape($CurrentName), $NewName
            Set-Content $file.FullName -Value $newContent -Encoding UTF8
            Write-Host "Updated content in $($file.FullName)"
        }
    } catch {
        Write-Warning "Could not update content in '$($file.FullName)': $($_.Exception.Message)"
    }
}

# 🏷️ Steg 5: Byt namn på filer
foreach ($file in $filesToProcess) {
    if ($file.Name -like "*$CurrentName*") {
        $newFileName = $file.Name -replace [Regex]::Escape($CurrentName), $NewName
        try {
            Rename-Item -Path $file.FullName -NewName $newFileName -ErrorAction Stop
            Write-Host "Renamed file: $($file.Name) => $newFileName"
        } catch {
            Write-Warning "Could not rename file '$($file.FullName)': $($_.Exception.Message)"
        }
    }
}

# 🌀 Steg 6: Git reset om flaggan är satt
if ($ReinitializeGit) {
    if (Test-Path ".git") {
        Write-Host "Removing existing .git folder..."
        Remove-Item -Recurse -Force ".git"
    }

    Write-Host "Initializing new Git repository..."
    git init
    git add .
    git commit -m "Initialized renamed project: $NewName"
}

Write-Host ""
Write-Host "Done! You can now run 'dotnet build'." -ForegroundColor Green