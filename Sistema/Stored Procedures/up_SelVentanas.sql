USE [CentroSalud]
GO
/****** Object:  StoredProcedure [dbo].[up_SelVentanas]    Script Date: 07/12/2011 23:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[up_SelVentanas] (
@idPerfil		INT,
@NomVentana		VARCHAR(40),
@accion			INT 
)
AS
BEGIN
	IF @accion = 0
    BEGIN
		SELECT V.Nombre
		FROM VentanasxPerfil X, Ventana V
		WHERE X.Id_Ventana = V.Id_Ventana AND X.Id_Perfil = @idPerfil
	END
	IF @accion = 1
    BEGIN
		SELECT V.Id_Ventana
		FROM Ventana V
		WHERE V.Nombre = @NomVentana
    END
	
END

		