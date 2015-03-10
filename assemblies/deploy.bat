@echo off
rem This script is run by AfterBuild events to deploy dll files into
rem Bridge.NET assembly dir.
rem Example usage from VS's PostBuildEvent: 
rem @call $(SolutionDir)\assemblies\deploy.bat "$(MSBuildProjectName)" "$(SolutionDir)\assemblies"

set par1=%~1
set par2=%~2
if not "%par1%" == "" if not "%par2%" == "" (
 echo "noif" >> debug.txt
 for %%x in (exe dll pdb xml) do (
  if exist "%par1%.%%x" (
   copy /y "%par1%.%%x" "%par2%assemblies\."
  )
 )
)
