"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\msbuild" /p:ContinueOnError=true /p:RunCodeAnalysis=true /t:Clean,Build
if %ErrorLevel% neq 0 (
pause
)