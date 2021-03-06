-- create Look up tables

USE [GWDataDB]
GO
/****** Object:  Table [dbo].[lut_Demo]    Script Date: 11/11/2006 16:14:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[lut_Demo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SourceValue] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[TargetValue] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF