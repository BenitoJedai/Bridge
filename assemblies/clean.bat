@echo off
rem This script can be used to clean assemblies/ directory from built
rem Bridge.NET files.
rem It is NOT required to run this script every time you clean the solution,
rem as once you rebuild, the files will be overwritten here.

for %%x in (exe dll xml pdb) do (
 if exist "*.%%x" del /q "*.%%x"
)
