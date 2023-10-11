param(
  [Parameter(Mandatory)] [string] $ArtifactOutputPath,
  [Parameter(Mandatory)] [string] $SimhubInstallationDirectory)

$isSimhubInstallationDirectoryValid = Test-Path $SimhubInstallationDirectory

$isArtifactOutputPathValid = Test-Path $ArtifactOutputPath

if ($isSimhubInstallationDirectoryValid -eq $false) {
  Write-Host("Simub installation directory is invalid")
  exit 1
}

if ($isArtifactOutputPathValid -eq $false) {
  Write-Error("Artifact output path is invalid")
  exit 1
}

Write-Host "Simhub installation path is:   $isSimhubInstallationDirectoryValid"
Write-Host "Artifact output path is:       $isArtifactOutputPathValid"

$Source = $ArtifactOutputPath + "\\PitWallAcquisitionPlugin.*"
$Destination = $SimhubInstallationDirectory

Write-Host "-------------------------------------------------------------"
Write-Host "-------------------------------------------------------------"
Write-Host "Source parameter:            $Source"
Write-Host "Destination parameter:       $Destination"

Copy-Item -Verbose -Force $Source $Destination

$Source = $ArtifactOutputPath + "\\Autofac.*"
$Destination = $SimhubInstallationDirectory

Write-Host "-------------------------------------------------------------"
Write-Host "-------------------------------------------------------------"
Write-Host "Source parameter:            $Source"
Write-Host "Destination parameter:       $Destination"

Copy-Item -Verbose -Force $Source $Destination

$Source = $ArtifactOutputPath + "\\System.Diagnostics.DiagnosticSource.*"
$Destination = $SimhubInstallationDirectory

Write-Host "-------------------------------------------------------------"
Write-Host "-------------------------------------------------------------"
Write-Host "Source parameter:            $Source"
Write-Host "Destination parameter:       $Destination"

Copy-Item -Verbose -Force $Source $Destination
