echo off

echo Set SQL Server Path
path C:\Program Files\Microsoft SQL Server\90\Tools\Binn

echo Making Temp Dir
mkdir C:\temp
mkdir D:\HYSInterfaceManager\Data

echo Creating SQL IMConfigDB Database...
echo This will take a couple of minutes
echo .

echo Dropping Old Database
osql -d master -E -i DropDB.sql > C:\temp\HYSInterfaceManagerDBInstall_DropDB.log

echo Deleting Old Database
del d:\HYSInterfaceManager\Data\IMConfigDB.mdf > C:\temp\HYSInterfaceManagerDBInstall_DelCfgDB.log
del d:\HYSInterfaceManager\Data\IMConfigDB_log.LDF > C:\temp\HYSInterfaceManagerDBInstall_DelCfgDBLog.log
del d:\HYSInterfaceManager\Data\IMDataDB.mdf > C:\temp\HYSInterfaceManagerDBInstall_DelDatDB.log
del d:\HYSInterfaceManager\Data\IMDataDB_log.LDF > C:\temp\HYSInterfaceManagerDBInstall_DelDatDBLog.log

echo Creating Databases
osql -d master -E -i InstallDB.sql > C:\temp\HYSInterfaceManagerDBInstall_CreateDB.log

echo Creating Tables
osql -d IMConfigDB -E -i GWConfigDB.sql > C:\temp\HYSInterfaceManagerDBInstall_CreateTableCfgDB.log
osql -d IMDataDB -E -i GWDataDB.sql > C:\temp\HYSInterfaceManagerDBInstall_CreateTableDatDB.log

echo DONE