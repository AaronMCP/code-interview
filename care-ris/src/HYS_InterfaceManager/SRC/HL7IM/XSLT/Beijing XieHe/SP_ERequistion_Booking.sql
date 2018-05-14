USE [GCRIS2]
GO

/****** Object:  StoredProcedure [dbo].[SP_ERequisition_Booking]    Script Date: 10/13/2011 09:29:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--------------------------------------------------------------
--Description: Insert Procedure for the table "dbo.tERequisition"
--Created: 2006-11-10 
--Author: Bruce Deng
-------------------------------------------------------------

CREATE PROCEDURE [dbo].[SP_ERequisition_Booking]
(
	@ERNo                               varchar(32),
	@IsBookins                          varchar(16),
	@ApplyDate                          varchar(16)
)
AS
BEGIN
	BEGIN TRAN
	if (@IsBookins = 'Y')
	begin 
	UPDATE dbo.tERequisition 
	SET IsBooking = @IsBookins,
		ApplyDate = cast(SUBSTRING(@ApplyDate,1,4)+'-'+SUBSTRING(@ApplyDate,5,2)+'-'+ SUBSTRING(@ApplyDate,7,2)+' '+ SUBSTRING(@ApplyDate,9,2)+':'+ SUBSTRING(@ApplyDate,11,2)+':'+ SUBSTRING(@ApplyDate,13,2) as datetime)
	WHERE ERNo = @ERNo
	end
	else
	begin
	UPDATE dbo.tERequisition 
	SET IsBooking = @IsBookins
	WHERE ERNo = @ERNo
	end

    COMMIT TRAN
END

GO

