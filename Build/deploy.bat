@echo off
rem This script is run by AfterBuild events to deploy dll files into
rem Bridge.NET assembly dir.
rem Example usage from VS's PostBuildEvent: 
rem @call $(SolutionDir)\deploy.bat "$(MSBuildProjectName)" "$(SolutionDir)"

rem Project base name (to mount <project>.dll filename)
set projfilename=%~1
rem Solution root directory (and where to locate 'Assemblies/')
set destroot=%~2
rem Additional directory name (if an Extension, for example)
set extdirname=%~3

rem Build full destination path name (appending extension dir name if needed)
set asmpath=%destroot%Build\
if not "%extdirname%" == "" set asmpath=%asmpath%%extdirname%\

if not "%projfilename%" == "" if not "%destroot%" == "" (
 if not exist "%asmpath%" mkdir "%asmpath%"
 for %%x in (exe dll pdb xml) do (
  if exist "%projfilename%.%%x" (
   copy /y "%projfilename%.%%x" "%asmpath%."
  )
 )
)
