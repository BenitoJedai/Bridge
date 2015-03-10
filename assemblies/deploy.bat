@echo off
rem This script is run by AfterBuild events to deploy dll files into
rem Bridge.NET assembly dir.
rem Example usage from VS's PostBuildEvent: 
rem @call $(SolutionDir)\assemblies\deploy.bat "$(MSBuildProjectName)" "$(SolutionDir)\assemblies"

if not "%1" == "" if not "%2" == "" (
 for %%x in (exe dll pdb xml) do (
  rem echo %1.%%x
  if exist "%1.%%x" (
   copy /y "%1.%%x" "%2assemblies\."
  )
 )
)
