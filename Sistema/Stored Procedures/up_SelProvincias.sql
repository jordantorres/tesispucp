USE [CentroSalud]
GO
/****** Object:  StoredProcedure [dbo].[up_SelProvincias]    Script Date: 07/09/2011 14:09:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[up_SelProvincias]
@CodDepartamento		char(2)
AS

BEGIN	
	SET NOCOUNT ON									
		
		SELECT * 
		FROM Ubigeo
		WHERE CodDepartamento = @CodDepartamento AND CodProvincia <> '00' AND CodDistrito = '00'
						
	SET NOCOUNT OFF
END
