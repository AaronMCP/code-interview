-- =============================================

-- Author:		PANDY
-- Create date: 2006-11-6
-- Description:	Script used to uninstall outbound adapter, 
--				delete special record in GWConfigDB

-- Note: %var% represent the variable will be replaced by the configuration
-- %Inbound_IFName%		:Inbound interface Name
-- %Outbound_IFName%	:Outbound interface name

-- =============================================

USE [GWConfigDB]
GO

-- Delete record from table interface
-- DELETE FROM INTERFACE WHERE interface_name = '%Outbound_IFName%'

-- Delete record from table combination
DELETE FROM COMBINATION 
WHERE datain = '%Inbound_IFName%'
AND   dataout= '%Outbound_IFName%'
