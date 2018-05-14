USE master
go
if exists(select 1 from master..sysdatabases where name='CareRis')
begin
       PRINT '<<<DROP DATABASE CareRis>>>'
	drop database CareRis
end

go

CREATE DATABASE CareRis 
ON PRIMARY
(   NAME='CareRis',
    FILENAME='D:\care-ris\DB\DATA\CareRisDB.mdf',
    SIZE=128MB,
    MAXSIZE=102400MB,
    FILEGROWTH=20% )
LOG ON
(   NAME='CareRisLOG',
    FILENAME='D:\care-ris\DB\LOG\CareRisLOG.ldf',
    SIZE=64MB,
    MAXSIZE=20480MB,
    FILEGROWTH=20% )
go