SET _=%~dp0
SET mode=Debug
copy /Y "%_%..\..\Builder\Build\bin\%mode%\Bridge.Build.dll" "%_%"
copy /Y "%_%..\..\Builder\Builder\bin\%mode%\Bridge.Builder.exe" "%_%"
copy /Y "%_%..\Core\Foundation\bin\%mode%\Bridge.Foundation.dll" "%_%"
copy /Y "%_%..\Core\Foundation\bin\%mode%\Bridge.Foundation.xml" "%_%"
copy /Y "%_%..\Core\Html5\bin\%mode%\Bridge.Html5.dll" "%_%"
copy /Y "%_%..\Core\Html5\bin\%mode%\Bridge.Html5.xml" "%_%"
copy /Y "%_%..\Core\Contract\bin\%mode%\Bridge.Contract.dll" "%_%"
copy /Y "%_%..\Core\Translator\bin\%mode%\AjaxMin.dll" "%_%"
copy /Y "%_%..\Core\Translator\bin\%mode%\Ext.Net.Utilities.dll" "%_%"
copy /Y "%_%..\Core\Translator\bin\%mode%\ICSharpCode.NRefactory.dll" "%_%"
copy /Y "%_%..\Core\Translator\bin\%mode%\ICSharpCode.NRefactory.CSharp.dll" "%_%"
copy /Y "%_%..\Core\Translator\bin\%mode%\ICSharpCode.NRefactory.Cecil.dll" "%_%"
copy /Y "%_%..\Core\Translator\bin\%mode%\Mono.Cecil.dll" "%_%"
copy /Y "%_%..\Core\Translator\bin\%mode%\Mono.Cecil.Mdb.dll" "%_%"
copy /Y "%_%..\Core\Translator\bin\%mode%\Mono.Cecil.Pdb.dll" "%_%"
copy /Y "%_%..\Core\Translator\bin\%mode%\Mono.Cecil.Rocks.dll" "%_%"
copy /Y "%_%..\Core\Translator\bin\%mode%\Newtonsoft.Json.dll" "%_%"
copy /Y "%_%..\Core\Translator\bin\%mode%\Bridge.Translator.dll" "%_%"
if not exist "%_%plugins\" mkdir "%_%plugins\"
copy /Y "%_%..\..\Plugins\Aspects\bin\%mode%\Bridge.Aspects.dll" "%_%plugins\"

pause