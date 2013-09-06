USE [CentroSalud]
GO
/****** Object:  StoredProcedure [dbo].[up_ObtenerMenus]    Script Date: 09/06/2013 13:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[up_ObtenerMenus]
(
	@IdEmpleado		INT
)
AS
BEGIN
  SELECT Menu
  FROM MenusxPerfil
  WHERE IdPerfil = (SELECT IdPerfil
   				    FROM Empleado
				    WHERE IdEmpleado = @IdEmpleado)
END