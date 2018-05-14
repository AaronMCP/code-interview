@echo off

echo.
echo ==============================================
echo  XDS Gateway Integration Solution Uninstaller
echo ==============================================
echo.

echo Please enter a path of the Integration Solution to be uninstall.
set /p dest="(For example: C:\EMR_Integration) : "

echo Please enter SQL Server instance name.
set /p dbinstance="(For example: (local)\SQLExpress) : "

echo Please enter SQL Server user name.
set /p dbusername="(For example: sa) : "

echo Please enter SQL Server password.
set /p dbpassword=" : "

echo.
echo Please ensure your input is right, and press any key to continue.
echo Or cancel uninstallation by closing the console window.
echo.

pause

echo Stopping NT Services...

net stop FILEIN_END
net stop FILEIN_PAT
net stop FILEIN_RAD
net stop FILEIN_US
net stop FILEOUT_RHIS
net stop LOGOUT_RHIS
net stop MSGBOX_CDA
net stop MSGBOX_LOG
net stop SCRIPT_SVC

echo Uninstalling NT Services...

sc delete FILEIN_END
sc delete FILEIN_PAT
sc delete FILEIN_RAD
sc delete FILEIN_US
sc delete FILEOUT_RHIS
sc delete LOGOUT_RHIS
sc delete MSGBOX_CDA
sc delete MSGBOX_LOG
sc delete SCRIPT_SVC

echo Dropping Database...

mkdir "%dest%\log"
"%dest%\osql\osql.exe" -S%dbinstance% -U%dbusername% -P%dbpassword% -d master -i "%dest%\DropDB.sql" >> "%dest%\log\DropDB.log"

echo Deleting IIS Virtual Directory...

call "%dest%\DropVirtualPath.bat"

rem mkdir "%dest%\log"
rem Cscript.exe C:\Inetpub\AdminScripts\adsutil.vbs DELETE W3SVC/1/Root/Renji_EMR_Integration >> "%dest%\log\DropVirtualPath.log"

echo.
echo Uninstallation Completed. You can delete the folder manually.
echo.

pause

exit