<#
.SYNOPSIS
A psake bootstraper; This script runs one or more tasks defined in the psake file.

.EXAMPLE
.\build.ps1 -Help;
This example prints a list of all the available tasks.
#>

Param(
	[ValidateNotNullorEmpty()]
	[string[]]$Tasks = @("default"),

	[Alias('f', "desc", "description")]
	[ValidateNotNullorEmpty()]
	[string]$Filter = "*",

	[Alias('s', "secrets")]
	[string]$SecretsFilePath,

	[Alias('c')]
	[string]$Configuration = "Debug",

	[Alias('h')]
	[switch]$Help,

	[Alias('i')]
	[switch]$NonInteractive,

	[switch]$Production,
	[switch]$Preview,
	[switch]$Major,
	[switch]$Minor
)

# Ensuring we have our Dependencies installed.
if(-not ((&npm --version) -match '\d+.\d+')) { throw "'npm' is not accessible on this machine."; }
if(-not ((&dotnet --version) -match '\d+.\d+')) { throw "'dotnet' is not accessible on this machine."; }
if (-not ((&git --version) -match 'git version \d+\.\d+')) { throw "'git' is not accessible on this machine."; }

# Initializing our default variables.
if (($Tasks.Length -gt 0) -and ($Tasks[0] -like "publish")) { $Configuration = "Release"; }
if ($Production -or $Preview) { $Configuration = "Release"; }

$environmentName = "local";
if ($Preview) { $environmentName = "preview"; }
if ($Production) { $environmentName = "production"; }

if ([string]::IsNullOrEmpty($SecretsFilePath)) { $SecretsFilePath = (Join-Path $PSScriptRoot "secrets.json"); }
$msbuild = try { (&cmd /c where msbuild); } catch { return $null; }

# Invoking the Psake tasks.
$toolsFolder = Join-Path $PSScriptRoot "tools";
$psakeModule = Join-Path $toolsFolder "psake/*/*.psd1";
if (-not (Test-Path $psakeModule))
{
	if (-not (Test-Path $toolsFolder)) { New-Item $toolsFolder -ItemType Directory | Out-Null; }
	Save-Module "psake" -Path $toolsFolder;
}
Import-Module $psakeModule -Force;

$taskFile = Join-Path $PSScriptRoot "build/tasks.psake.ps1";
if ($Help) { Invoke-Psake -buildFile $taskFile -docs; }
else
{
	Write-Host -ForegroundColor DarkGray "Host:          $([Environment]::UserName)@$([Environment]::MachineName)";
	Write-Host -ForegroundColor DarkGray "OS:            $([Environment]::OSVersion.Platform)";
	Write-Host -ForegroundColor DarkGray "Configuration: $Configuration";
	Write-Host -ForegroundColor DarkGray "Environment:   $environmentName";
	Write-Host "";
	Invoke-psake $taskFile -nologo -taskList $Tasks -properties @{
		"Filter"=$Filter;
		"MSBuildExe"=$msbuild;
		"Major"=$Major.IsPresent;
		"Minor"=$Minor.IsPresent;
		"ToolsFolder"=$toolsFolder;
		"Configuration"=$Configuration;
		"SolutionFolder"=$PSScriptRoot;
		"SecretsFilePath"=$SecretsFilePath;
		"EnvironmentName"=$environmentName;
		"InPreview"=$Preview.IsPresent;
		"InProduction"=$Production.IsPresent;
		"Interactive"=(-not $NonInteractive.IsPresent);
	}
	if (-not $psake.build_success) { exit 1; }
}