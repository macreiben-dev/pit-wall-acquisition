param(
  [Parameter(Mandatory)] [string] $ArtifactOutputPath,
  [Parameter(Mandatory)] [string] $SimhubInstallationDirectory,
  [Parameter(Mandatory)] [string] $ConfigurationName)

if($ConfigurationName -eq "Release") {
	Write-Host "No copy to SimhHub installation."
	exit 0
}

$isSimhubInstallationDirectoryValid = Test-Path $SimhubInstallationDirectory

$SourceArtifactOutputPath = $ArtifactOutputPath + "\\bin\\" + $ConfigurationName

$isArtifactOutputPathValid = Test-Path $SourceArtifactOutputPath

if ($isSimhubInstallationDirectoryValid -eq $false) {
  Write-Host("Simub installation directory is invalid")
  exit 1
}

if ($isArtifactOutputPathValid -eq $false) {
  Write-Error("Artifact output path is invalid")
  exit 1
}

Write-Host "Simhub installation path is:   $isSimhubInstallationDirectoryValid"
Write-Host "Artifact output path is:       $SourceArtifactOutputPath"

$Source = $SourceArtifactOutputPath + "\\PitWallAcquisitionPlugin.dll"
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
