# Emulate AppVeyor environment
$Env:APPVEYOR_BUILD_NUMBER = "315"
$Env:APPVEYOR_BUILD_VERSION = "3.2.3-beta-$Env:APPVEYOR_BUILD_NUMBER"
$Env:APPVEYOR_REPO_BRANCH = "master"
$Env:APPVEYOR_PULL_REQUEST_NUMBER = ""

# Version number
$VERSION_PREFIX = "$Env:APPVEYOR_BUILD_VERSION".Split("-")[0]
$VERSION_SUFFIX = [int]::Parse($Env:APPVEYOR_BUILD_NUMBER).ToString("00000")

# Branch
$BRANCH = "$Env:APPVEYOR_REPO_BRANCH"
$Env:DOCFX_SOURCE_BRANCH_NAME = "$BRANCH"

# Output directory
$Env:ARTIFACT_DIR = ".\artifacts"
$dir = New-Item -type directory $env:ARTIFACT_DIR
$dir = $dir.FullName

# Verbosity
Write-Host "Build: $VERSION_SUFFIX / Branch: $BRANCH"
Write-Host "Artifacts will be placed in: $dir"

# Check if this is a PR
if (-not $Env:APPVEYOR_PULL_REQUEST_NUMBER)
{
	# Rebuild documentation
	Write-Host "Commencing complete build"
	& .\rebuild-all.ps1 -ArtifactLocation "$dir" -VersionSuffix "$VERSION_SUFFIX" -DocsPath ".\docs" -DocsPackageName "dsharpplus-docs"
}
else
{
	# Skip documentation
	Write-Host "Building from PR ($Env:APPVEYOR_PULL_REQUEST_NUMBER); skipping docs build"
	& .\rebuild-all.ps1 -ArtifactLocation "$dir" -VersionSuffix "$VERSION_SUFFIX"
}