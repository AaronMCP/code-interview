
USE [GWDataDB]
GO

/****** Object:  Table [dbo].[%Report%]    Script Date: 11/02/2006 10:01:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[%OutBound_IFName%_Report]') AND type in (N'U'))
DROP TABLE [dbo].[%OutBound_IFName%_Report]
go

/****** Object:  Table [dbo].[%Order%]    Script Date: 11/02/2006 10:01:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[%OutBound_IFName%_Order]') AND type in (N'U'))
DROP TABLE [dbo].[%OutBound_IFName%_Order]
go
/****** Object:  Table [dbo].[%Patient%]    Script Date: 11/02/2006 10:01:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[%OutBound_IFName%_Patient]') AND type in (N'U'))
DROP TABLE [dbo].[%OutBound_IFName%_Patient]
go
/****** Object:  Table [dbo].[%DataIndex%]    Script Date: 11/02/2006 10:01:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[%OutBound_IFName%_DataIndex]') AND type in (N'U'))
DROP TABLE [dbo].[%OutBound_IFName%_DataIndex]
go