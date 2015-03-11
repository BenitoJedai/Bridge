@echo off
rem This script is run by AfterBuild events to deploy dll files into
rem Bridge.NET assembly dir.
rem Example usage from VS's PostBuildEvent: 
rem @call $(SolutionDir)\assemblies\deploy.bat "$(MSBuildProjectName)" "$(SolutionDir)\assemblies"

rem Project base name (to mount <project>.dll filename)
set projfilename=%~1
rem Solution root directory (and where to locate 'assemblies/')
set destroot=%~2
rem Additional directory name (if an Extension, for example)
set extdirname=%~3

rem Build full destination path name (appending extension dir name if needed)
set asmpath=%destroot%assemblies\
if not "%extdirname%" == "" set asmpath=%asmpath%%extdirname%\

if not "%projfilename%" == "" if not "%destroot%" == "" (
 if not exist "%asmpath%" mkdir "%asmpath%"
 for %%x in (exe dll pdb xml) do (
  if exist "%projfilename%.%%x" (
   copy /y "%projfilename%.%%x" "%asmpath%."
  )
 )
)
