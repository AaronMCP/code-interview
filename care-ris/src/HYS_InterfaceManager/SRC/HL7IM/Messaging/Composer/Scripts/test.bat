@echo off

if "%~1"=="" goto prompt
set dest=%~1
goto run

:prompt
set /p dest="(For example: C:\EMR_Integration) : "

:run
echo %dest%
pause