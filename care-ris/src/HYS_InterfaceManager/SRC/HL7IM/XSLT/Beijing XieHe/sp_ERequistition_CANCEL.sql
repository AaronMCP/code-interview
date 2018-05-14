USE [GCRIS2]
GO

/****** Object:  StoredProcedure [dbo].[SP_ERequisition_CANCEL]    Script Date: 10/12/2011 15:54:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--------------------------------------------------------------
--Description: Insert Procedure for the table "dbo.tERequisition"
--Created: 2006-11-10 
--Author: Bruce Deng
-------------------------------------------------------------

CREATE PROCEDURE [dbo].[SP_ERequisition_CANCEL]
(
	@ERNo                               varchar(32),
	@PatientID                          varchar(16),
	@Status                             varchar(16)
)
AS
BEGIN
	BEGIN TRAN
	UPDATE dbo.tERequisition SET [Status] = @Status
	WHERE ERNo = @ERNo AND PatientID = @PatientID;

    COMMIT TRAN
END

GO

