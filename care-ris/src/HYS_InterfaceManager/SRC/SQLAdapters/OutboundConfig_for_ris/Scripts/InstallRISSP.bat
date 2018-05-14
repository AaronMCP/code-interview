echo off

echo Making Temp Dir
mkdir C:\temp

echo Set SQL Server Path
set path=osql;%path%

echo Creating Table and GC Storage Procedure
osql -S (local)\CSSERVER -d GCRis2 -U sa -P sa -i InstallTable.sql > C:\temp\GCRisTableInstall.log

echo Dropping Old Storage Procedure
osql -S (local)\CSSERVER -d GCRis2 -U sa -P sa -i UninstallSP.sql > C:\temp\GCRisSPUninstall.log

echo Creating New Storage Procedure
osql -S (local)\CSSERVER -d GCRis2 -U sa -P sa -i InstallSP.sql > C:\temp\GCRisSPInstall.log

echo DONE
