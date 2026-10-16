param(
	[string]$ConfigFile = "stryker-config.json"
)

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Definition
Set-Location $scriptDir

# Resolve config path: prefer provided, then parent, then current
if (-not (Test-Path $ConfigFile)) {
	$parent = Join-Path $scriptDir "..\$ConfigFile"
	if (Test-Path $parent) { $ConfigFile = $parent }
}

Write-Host "Running Stryker with config: $ConfigFile"

# Ensure tools restored
dotnet tool restore

# Run Stryker with open report
dotnet tool run dotnet-stryker -- --config-file "$ConfigFile" --open-report

Write-Host "Stryker finished. If --open-report didn't open, check StrykerOutput folder."