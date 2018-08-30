# Version number
$VERSION = [int]::Parse($Env:APPVEYOR_BUILD_NUMBER).ToString("00000")
$BUILD_SUFFIX = "nightly"
# Environment variables
$Env:ARTIFACT_DIR = ".\artifacts"
# Prepare the environment
Copy-Item .\.nuget.\NuGet.config .\
$dir = New-Item -type directory $env:ARTIFACT_DIR
$dir = $dir.FullName
# Verbosity
Write-Host "Build: $VERSION"
Write-Host "Artifacts will be placed in: $dir"
# Restore NuGet packages
dotnet restore -v Minimal
# Build
dotnet build DSharpPlus.sln -v Minimal -c Release --version-suffix "$BUILD_SUFFIX" -p:BuildNumber="$VERSION"
# Package
dotnet pack DSharpPlus.sln -v Minimal -c Release -o "$dir" --no-build --version-suffix "$BUILD_SUFFIX" -p:BuildNumber="$VERSION"
# Remove Test packages
# Get-ChildItem $env:ARTIFACT_DIR | ? { $_.Name.ToLower().StartsWith("DSharpPlus.Test") } | % { $_.Delete() }
# Rebuild documentation
& .\rebuild-docs.ps1 .\docs "$dir" dsharpplus-docs
