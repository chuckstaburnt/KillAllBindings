# File: kab_build.ps1
# KillAllBindings PowerShell build script.
# PowerShell

# NOTE: It's assumed that devenv is registered in path.
# NOTE: Tested only with VS2015.

$scriptPath = split-path -parent $MyInvocation.MyCommand.Definition

$slnFile = "KillAllBindings.sln"
$listBuildModes = "Release^|x86", "Release^|x64"

Set-Location $scriptPath

Write-Host "**     Deleting previous build output."
Remove-Item .\KillAllBindings\bin\* -force -recurse

ForEach ($config in $listBuildModes)
{
    # NOTE: Script assumes devenv is registered in the path.
    Write-Host "**     Building with $config Configuration."
    cmd /c "devenv.exe $slnFile /Build $config"
}

Write-Host "**     Done!"

