USE master
CREATE DATABASE GWConfigDB ON ( NAME=IMConfigDB, FILENAME='d:\HYSInterfaceManager\Data\IMConfigDB.mdf', SIZE=10MB )
CREATE DATABASE GWDataDB ON ( NAME=IMDataDB , FILENAME='d:\HYSInterfaceManager\Data\IMDataDB.mdf', SIZE=10MB )
go
