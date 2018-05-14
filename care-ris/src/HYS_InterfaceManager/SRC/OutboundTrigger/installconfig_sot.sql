-- ========================================================================

-- Author:		PANDY
-- Create date: 2006-11-6
-- Description:	Script used to install outbound adapter, 
--				delete special record in GWConfigDB

-- Note: %var% represent the variable will be replaced by the configuration
-- %Inbound_IFName%		:Inbound interface Name
-- %Outbound_IFName%	:Outbound interface name
-- %Outbound_Description% :Outbound interface description
-- %Outbound_EventTypeListStr% :Outbound Event type list
-- %Outbound_Device_ID%        :outbound interface matched device_id

-- =========================================================================

USE [GWConfigDB]
GO

-- Delete record from table interface in first
-- DELETE FROM INTERFACE WHERE interface_name = '%Outbound_IFName%'

-- Add New record, on secord
--INSERT INTO INTERFACE
--      ([INTERFACE_NAME] 
--      ,[INTERFACE_DEVICE_ID] 
--      ,[DEVICE_TYPE] 
--      ,[INTERFACE_DIRECTION] 
--      ,[INTERFACE_DESC] 
--      ,[INTERFACE_STATUS] 
--      ,[INDEX_FILE] 
--      ,[EVENT_TYPE]) 
--
--       SELECT   '%Outbound_IFName%' 
--       ,DEVICE_ID, DEVICE_TYPE, DEVICE_DIRECT 
--       ,'%Outbound_Description%',1, '' 
--       , '%Outbound_EventTypeListStr%'
--       FROM DEVICE  
--       WHERE DEVICE_ID =%Outbound_DEVICE_ID%

UPDATE interface 
SET    INTERFACE_DESC = '%Outbound_Description%',
	   EVENT_TYPE     = '%Outbound_EventTypeListStr%'
WHERE  Interface_name = '%Outbound_IFName%' 	

-- Delete record from table combination
DELETE FROM COMBINATION 
WHERE datain = '%Inbound_IFName%'
AND   dataout= '%Outbound_IFName%'

-- Add New record, on secord
INSERT INTO COMBINATION
(datain,dataout,data_Mapping_File)
values
('%Inbound_IFName%','%Outbound_IFName%','')
