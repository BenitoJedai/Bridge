SET _=%~dp0
SET mode=Release
copy /Y "%_%..\bin\%mode%\*.dll" "%_%"
