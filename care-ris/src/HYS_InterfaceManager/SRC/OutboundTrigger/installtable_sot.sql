-- =============================================

-- Author:		PANDY
-- Create date: 2006-11-10
-- Description:	Script used to create outbound database objects, include 
--				tables, trigger, functions

-- Note: %var% represent the variable will be replaced by the configuration
-- %Inbound_IFName%		:Inbound interface Name
-- %Outbound_IFName%	:Outbound interface name

-- =============================================



USE [GWDataDB]
GO

-- Create Tables
/****** Object:  Table [dbo].[DataIndex]    Script Date: 11/02/2006 10:00:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[%Outbound_IFName%_DataIndex](
	[Data_ID] [uniqueidentifier] NOT NULL,
	[Data_DT] [datetime] NOT NULL,
	[EVENT_TYPE] [varchar](max) NOT NULL,
	[RECORD_INDEX_1] [varchar](max) COLLATE Chinese_PRC_CI_AS  NULL,
	[RECORD_INDEX_2] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[RECORD_INDEX_3] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[RECORD_INDEX_4] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[DATA_SOURCE] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[PROCESS_FLAG] [varchar](max) NOT NULL,
	CONSTRAINT [PK_%Outbound_IFName%_DataIndex] PRIMARY KEY CLUSTERED 
(
	[DATA_ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[PATIENT]    Script Date: 11/02/2006 10:00:36 ******/
CREATE TABLE [dbo].[%Outbound_IFName%_PATIENT](
	[DATA_ID] [uniqueidentifier] NOT NULL,
	[DATA_DT] [datetime] NOT NULL,
	[PATIENTID] [varchar](max) COLLATE Chinese_PRC_CI_AS  NULL,
	[PATIETN_OLDID] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[OTHER_PID] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[PATIENT_NAME] [varchar](max) COLLATE Chinese_PRC_CI_AS  NULL,
	[MOTHER_MAIDEN_NAME] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[BIRTHDATE] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[SEX] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[PATIENT_ALIAS] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[RACE] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[ADDRESS] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[COUNTRY_CODE] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[PHONENUMBER_HOME] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[PHONENUMBER_BUSINESS] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[PRIMARY_LANGUAGE] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[MARITAL_STATUS] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[RELIGION] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[ACCOUNT_NUMBER] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[SSN_NUMBER] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[DRIVERLIC_NUMBER] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[ETHNIC_GROUP] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[BIRTH_PLACE] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[CITIZENSHIP] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[VETERANS_MIL_STATUS] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[NATIONALITY] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[PATIENT_TYPE] [varchar](max) COLLATE Chinese_PRC_CI_AS  NULL,
	[PATIENT_LOCATION] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[PATIENT_STATUS] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[VISIT_NUMBER] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[CUSTOMER_1] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[CUSTOMER_2] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[CUSTOMER_3] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[CUSTOMER_4] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_%Outbound_IFName%_PATIENT] PRIMARY KEY CLUSTERED 
(
	[DATA_ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[ORDER]    Script Date: 11/02/2006 10:00:36 ******/
CREATE TABLE [dbo].[%Outbound_IFName%_ORDER](
	[DATA_ID] [uniqueidentifier] NOT NULL,
	[DATA_DT] [datetime] NOT NULL,
	[ORDER_NO] [varchar](max)  NULL,
	[PLACER_NO] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[FILLER_NO] [varchar](max) COLLATE Chinese_PRC_CI_AS  NULL,
	[SERIES_NO] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[PATIENT_ID] [varchar](max) COLLATE Chinese_PRC_CI_AS  NULL,
	[EXAM_STATUS] [varchar](max) COLLATE Chinese_PRC_CI_AS  NULL,
	[PLACER_DEPARTMENT] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[PLACER] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[PLACER_CONTACT] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[FILLER_DEPARTMENT] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[FILLER] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[FILLER_CONTACT] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[REF_ORGANIZATION] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[REF_PHYSICIAN] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[REF_CONTACT] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[REQUEST_REASON] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[REUQEST_COMMENTS] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[EXAM_REQUIREMENT] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[SCHEDULED_DT] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[MODALITY] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[STATION_NAME] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[STATION_AETITLE] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[EXAM_LOCATION] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[EXAM_VOLUME] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[EXAM_DT] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[DURATION] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[TRANSPORT_ARRANGE] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[TECHNICIAN] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[BODY_PART] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[PROCEDURE_NAME] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[PROCEDURE_CODE] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[PROCEDURE_DESC] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[STUDY_INSTANCE_UID] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[STUDY_ID] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[REF_CLASS_UID] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[EXAM_COMMENT] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[CNT_AGENT] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[CHARGE_STATUS] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[CHARGE_AMOUNT] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[CUSTOMER_1] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[CUSTOMER_2] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[CUSTOMER_3] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[CUSTOMER_4] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_%Outbound_IFName%_ORDER] PRIMARY KEY CLUSTERED 
(
	[DATA_ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]



/****** Object:  Table [dbo].[REPORT]    Script Date: 11/02/2006 10:00:36 ******/
CREATE TABLE [dbo].[%Outbound_IFName%_REPORT](
	[DATA_ID] [uniqueidentifier] NOT NULL,
	[DATA_DT] [datetime] NOT NULL,
	[REPORT_NO] [varchar](max)  NULL,
	[ACCESSION_NUMBER] [varchar](max) COLLATE Chinese_PRC_CI_AS  NULL,
	[PATIENT_ID] [varchar](max) COLLATE Chinese_PRC_CI_AS  NULL,
	[REPORT_STATUS] [varchar](max) COLLATE Chinese_PRC_CI_AS  NULL,
	[MODALITY] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[REPORT_TYPE] [varchar](max) COLLATE Chinese_PRC_CI_AS  NULL,
	[REPORT_FILE] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[DIAGNOSE] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[COMMENTS] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[REPORT_WRITER] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[REPORT_INTEPRETER] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[REPORT_APPROVER] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[REPORTDT] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[OBSERVATIONMETHOD] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[CUSTOMER_1] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[CUSTOMER_2] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[CUSTOMER_3] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[CUSTOMER_4] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_%Outbound_IFName%_REPORT] PRIMARY KEY CLUSTERED 
(
	[Data_ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF

