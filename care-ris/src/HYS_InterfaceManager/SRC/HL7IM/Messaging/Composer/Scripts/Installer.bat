@echo off

echo.
echo ============================================
echo  XDS Gateway Integration Solution Installer
echo ============================================
echo.

if "%~1"=="" goto prompt1
set dest=%~1
goto skip1

:prompt1
echo Please enter a path to install the Integration Solution.
set /p dest="(For example: C:\EMR_Integration) : "
:skip1

if "%~2"=="" goto prompt2
set dbinstance=%~2
goto skip2

:prompt2
echo Please enter SQL Server instance name.
set /p dbinstance="(For example: (local)\SQLExpress) : "
:skip2

if "%~3"=="" goto prompt3
set dbusername=%~3
goto skip3

:prompt3
echo Please enter SQL Server user name.
set /p dbusername="(For example: sa) : "
:skip3

if "%~4"=="" goto prompt4
set dbpassword=%~4
goto skip4

:prompt4
echo Please enter SQL Server password.
set /p dbpassword=" : "
:skip4

if "%~5"=="" goto prompt5
set solutionname=%~5
goto skip5

:prompt5
echo Please enter Integration Solution name.
set /p solutionname=" : "
:skip5

if "%~6"=="" goto prompt6
set virtualpathname=%~6
goto skip6

:prompt6
echo Please enter IIS Virutal Path name.
set /p virtualpathname=" : "
:skip6

if "%~7"=="" goto prompt7
set adminusername=%~7
goto skip7

:prompt7
echo Please enter Windows administrator user name.
set /p adminusername=" : "
:skip7

if "%~8"=="" goto prompt8
set adminpassword=%~8
goto skip8

:prompt8
echo Please enter Windows administrator password.
set /p adminpassword=" : "

echo.
echo Please ensure your input is right, and press any key to continue.
echo Or cancel installation by closing the console window.
echo.

pause
:skip8

xcopy "EMR_Integration\*.*" "%dest%\*.*" /E /R /Y

echo Modifying Database Configuration...

"%dest%\HYS.IM.Messaging.Composer.exe" -x SolutionDir.xml /SolutionConfig/DBParameter/OSQLArgument "-S%dbinstance% -U%dbusername% -P%dbpassword%"
"%dest%\HYS.IM.Messaging.Composer.exe" -x MSGBOX_CDA\MessageBox.xml /MessageBoxConfig/DatabaseScript/OSQLArgument "-S%dbinstance% -U%dbusername% -P%dbpassword%"
"%dest%\HYS.IM.Messaging.Composer.exe" -x MSGBOX_CDA\MessageBox.xml /MessageBoxConfig/Database/ConnectionString "Server=%dbinstance%;Database=XDSDataDB;UID=%dbusername%;PWD=%dbpassword%;"
"%dest%\HYS.IM.Messaging.Composer.exe" -x MSGBOX_LOG\MessageBox.xml /MessageBoxConfig/DatabaseScript/OSQLArgument "-S%dbinstance% -U%dbusername% -P%dbpassword%"
"%dest%\HYS.IM.Messaging.Composer.exe" -x MSGBOX_LOG\MessageBox.xml /MessageBoxConfig/Database/ConnectionString "Server=%dbinstance%;Database=XDSDataDB;UID=%dbusername%;PWD=%dbpassword%;"

echo Creating Database...

mkdir "%dest%\log"
"%dest%\osql\osql.exe" -S%dbinstance% -U%dbusername% -P%dbpassword% -d master -i "%dest%\DropDB.sql" >> "%dest%\log\DropDB.log"
"%dest%\osql\osql.exe" -S%dbinstance% -U%dbusername% -P%dbpassword% -d master -i "%dest%\CreateDB.sql" >> "%dest%\log\CreateDB.log"

echo Creating Tables and Storge Procedures...

"%dest%\MSGBOX_CDA\HYS.IM.MessageBox.Config.exe" -g
"%dest%\osql\osql.exe" -S%dbinstance% -U%dbusername% -P%dbpassword% -d master -i "%dest%\MSGBOX_CDA\InstallTable.sql" >> "%dest%\MSGBOX_CDA\Log\osql.log"
"%dest%\osql\osql.exe" -S%dbinstance% -U%dbusername% -P%dbpassword% -d master -i "%dest%\MSGBOX_CDA\InstallSP.sql" >> "%dest%\MSGBOX_CDA\Log\osql.log"
"%dest%\MSGBOX_LOG\HYS.IM.MessageBox.Config.exe" -g
"%dest%\osql\osql.exe" -S%dbinstance% -U%dbusername% -P%dbpassword% -d master -i "%dest%\MSGBOX_LOG\InstallTable.sql" >> "%dest%\MSGBOX_LOG\Log\osql.log"
"%dest%\osql\osql.exe" -S%dbinstance% -U%dbusername% -P%dbpassword% -d master -i "%dest%\MSGBOX_LOG\InstallSP.sql" >> "%dest%\MSGBOX_LOG\Log\osql.log"

echo Installing NT Services...

"%dest%\FILEIN_END\InstallUtil.exe" "%dest%\FILEIN_END\HYS.IM.Messaging.Service.exe" /LogFile="%dest%\FILEIN_END\InstallUtil.InstallLog"
"%dest%\FILEIN_PAT\InstallUtil.exe" "%dest%\FILEIN_PAT\HYS.IM.Messaging.Service.exe" /LogFile="%dest%\FILEIN_PAT\InstallUtil.InstallLog"
"%dest%\FILEIN_RAD\InstallUtil.exe" "%dest%\FILEIN_RAD\HYS.IM.Messaging.Service.exe" /LogFile="%dest%\FILEIN_RAD\InstallUtil.InstallLog"
"%dest%\FILEIN_US\InstallUtil.exe" "%dest%\FILEIN_US\HYS.IM.Messaging.Service.exe" /LogFile="%dest%\FILEIN_US\InstallUtil.InstallLog"
"%dest%\FILEOUT_RHIS\InstallUtil.exe" "%dest%\FILEOUT_RHIS\HYS.IM.Messaging.Service.exe" /LogFile="%dest%\FILEOUT_RHIS\InstallUtil.InstallLog"
"%dest%\LOGOUT_RHIS\InstallUtil.exe" "%dest%\LOGOUT_RHIS\HYS.IM.Messaging.Service.exe" /LogFile="%dest%\LOGOUT_RHIS\InstallUtil.InstallLog"
"%dest%\MSGBOX_CDA\InstallUtil.exe" "%dest%\MSGBOX_CDA\HYS.IM.Messaging.Service.exe" /LogFile="%dest%\MSGBOX_CDA\InstallUtil.InstallLog"
"%dest%\MSGBOX_LOG\InstallUtil.exe" "%dest%\MSGBOX_LOG\HYS.IM.Messaging.Service.exe" /LogFile="%dest%\MSGBOX_LOG\InstallUtil.InstallLog"

"%dest%\Tools\ScriptService\InstallUtil.exe" "%dest%\Tools\ScriptService\HYS.IM.Messaging.Service.exe" /LogFile="%dest%\Tools\ScriptService\InstallUtil.InstallLog"
"%dest%\Tools\ScriptService\HYS.IM.Messaging.ServiceConfig.exe" -auto
net start SCRIPT_SVC

echo Installing IIS Virtual Directory...

"%dest%\HYS.IM.Messaging.Composer.exe" -x SolutionDir.xml /SolutionConfig/Name "%solutionname%"
"%dest%\HYS.IM.Messaging.Composer.exe" -x SolutionDir.xml /SolutionConfig/WebSetting/VirtualPathName "%virtualpathname%"
"%dest%\HYS.IM.Messaging.Composer.exe" -g
call "%dest%\CreateVirtualPath.bat"

"%dest%\HYS.IM.Messaging.Composer.exe" -r Portal\Web.Config "<!-- <identity impersonate=\"true\" userName=\"accountname\" password=\"password\" /> -->" "<identity impersonate=\"true\" userName=\"%adminusername%\" password=\"%adminpassword%\" />"

rem Cscript.exe C:\Inetpub\AdminScripts\mkwebdir.vbs -c localhost -w 1 -v "Renji_EMR_Integration","%dest%\Portal" >> "%dest%\log\CreateVirtualPath.log"
rem Cscript.exe C:\Inetpub\AdminScripts\adsutil.vbs APPCREATEINPROC W3SVC/1/Root/Renji_EMR_Integration >> "%dest%\log\CreateVirtualPath.log"
rem Cscript.exe C:\Inetpub\AdminScripts\chaccess.vbs -a W3SVC/1/Root/Renji_EMR_Integration +script >> "%dest%\log\CreateVirtualPath.log"

echo.
echo Installation Completed.
echo.

pause

exit