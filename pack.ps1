#!/usr/bin/env  pwsh

# get latest tag and set as version after stripping v and any prerelease tag
try
{
  $version = git describe --abbrev=0 --tags | % {$_.replace('v', '')} | % {if ($_.Contains('-')) { $_.Substring(0, $_.IndexOf('-'))} else {$_}}
  echo "Initial version string: $version"
}
catch
{
  echo 'Version not found, continuing with 1.0.0'
}

if ($version -eq $null) {
  # if no existing tag set version to 1.0.0
  $version = '1.0.0'
  echo "No git tagged versions exist, setting to 1.0.0"
}
else {
  $split = $version.Split('.')
  if ($split.Length -lt 3) {
    Throw New-Object System.ArgumentOutOfRangeException "Tagged version must be in the format v<major>.<minor>.<patch>-<prereleasetag>"
  }

  $BETA_VERSION_PART = 'patch'

  $major = [int]$split[0]
  $major = if ($BETA_VERSION_PART -eq 'major') {$major + 1} else {$major}
  echo "Major part: $major"

  $minor = [int]$split[1]
  $minor = if ($BETA_VERSION_PART -eq 'minor') {$minor + 1} else {$minor}
  echo "Minor part: $minor"

  $patch = [int]$split[2]
  $patch = if ($BETA_VERSION_PART -eq 'patch') {$patch + 1} else {$patch}
  echo "Patch part: $patch"

  $version = "${major}.${minor}.${patch}"
}

$date = (Get-Date)
$dateString = $date.ToString("yyyyMMdd")

$BUILD_NUMBER = $date.Hour.ToString() + $date.Minute.ToString() + $date.Second.ToString()

# set prerelease tag to beta
$versionPrefix = $version
$versionSuffix = "beta.$dateString.$BUILD_NUMBER"
echo "Final version: $version-$versionSuffix"

$Env:Version = $versionPrefix

dotnet pack --version-suffix $versionSuffix

dotnet nuget push --source ~/.nuget/packages $PSScriptRoot/WaterData/bin/Release/WaterData.NET.Sdk.$Env:Version-$versionSuffix.nupkg
