"C:\Program Files\Microsoft Visual Studio\2022\Community\Msbuild\Current\Bin\msbuild" /p:ContinueOnError=true /p:RunCodeAnalysis=true /t:Clean,Build
if %ErrorLevel% neq 0 (
pause
)