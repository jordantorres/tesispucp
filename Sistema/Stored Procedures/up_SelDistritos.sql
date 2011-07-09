USE [CentroSalud]
GO
/****** Object:  StoredProcedure [dbo].[up_SelDistritos]    Script Date: 07/09/2011 14:07:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[up_SelDistritos]
@CodDepartamento      char(2),
@CodProvincia         char(2)
AS

BEGIN	
	SET NOCOUNT ON

		SELECT * 
		FROM Ubigeo 
		WHERE CodDepartamento = @CodDepartamento 
        AND CodProvincia = @CodProvincia and CodDistrito <> '00'
		
	SET NOCOUNT OFF
END
