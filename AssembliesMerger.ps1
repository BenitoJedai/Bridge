param($solutionDir,$projectDir,$outputDir,$targetFileName,$configurationName)
$ILMergeName = "ILMerge.exe"
$nameSuffix = "bundle"
$scriptName = $MyInvocation.MyCommand.Name

function log($msg) {
 if ($debug) {
  if ($isLineNew) { Write-Host -NoNewLine ($scriptName + "[debug]: ") }
  Write-Host $msg
  $isLineNew = $true
 }
}

$isLineNew = $true;
function lognb($msg) {
 if ($debug) {
  if ($isLineNew) { Write-Host -NoNewLine ($scriptName + "[debug]: ") }
  Write-Host -NoNewLine $msg
  $isLineNew = $true
 }
}

$debug = $false
if ( $configurationName -eq "Debug" ) {
 $debug = $true
}
log ("Solution Directory: [" + $solutionDir + "]")
log ("Project Directory: [" + $projectDir + "]")
log ("Output Directory: [" + $outputDir + "]")
log ("Target File: [" + $targetFileName + "]")
log ("Configuration: [" + $configurationName + "]")

lognb ("Getting Target filename's handler: ")
$targetFileHandle = Get-Item $targetFileName
log ("done.")

$outputFileName = ($targetFileHandle.BaseName + "-" + $nameSuffix + `
 $targetFileHandle.Extension)

lognb ("Fetching assemblies list: ")
$dllList = Get-Item *.dll | Where-Object { $_.Name -ne $targetFileName -and $_.Name -ne $outputFileName }
log ("done.")

$ILMergeBin = Get-ChildItem -Path ($solutionDir + "packages") `
 -Filter $ILMergeName -Recurse | `Select-Object -Last 1
if ( $ILMergeBin -eq $null -or -not (Test-Path -PathType Leaf $ILMergeBin.FullName) ) {
 $epfx = $scriptName + "[fatal]: "
 write-output ($epfx + "Unable to find '" + $ILMergeName + "' under NuGet packages for current project.")
 write-output ($epfx + "Please consider installing the 'ILMerge' NuGet package before building.")
 exit 1
}

#ILMerge's target: library or exe
$target = "library"
if ( $TargetFileHandle.Extension -eq ".exe" ) { $target = "exe" }
log ("Chosen target format: " + $target)

# PowerShell intelligently escapes command names arguments. So no need to worry
# with double quotes on file names.
lognb ("Performing merge: ")
& $ILMergeBin.FullName "/target:$target" "/out:$outputFileName" $targetFileName `
 ($dllList | ForEach-Object { $_.Name })
log ("done.")

write-output ($scriptName + ": Bundled '" + $targetFileName + `
 "' and additional assemblies into '" + $outputFileName)