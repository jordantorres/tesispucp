USE [CentroSalud]
GO
/****** Object:  StoredProcedure [dbo].[up_ManEnfermera]    Script Date: 07/09/2011 14:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[up_ManEnfermera] (
@idPersona			INT,
@paterno			VARCHAR(30),
@materno			VARCHAR(30),
@nombres			VARCHAR(60),
@sexo				CHAR(1),
@fechaNacimiento	DATETIME,
@idTipoDocumento	INT,
@numeroDocumento	VARCHAR(20),
@estadoCivil		VARCHAR(10),
@idLugarNacimiento	INT,
@idDomicilio		INT,
@telefono			VARCHAR(8),
@celular			VARCHAR(10),
@email				VARCHAR(60),
@fechaRegistro		DATETIME,
@tipoPersona		VARCHAR(8),
@usuario			VARCHAR(20),
@contrasena			VARCHAR(20),
@tipoPersonal		VARCHAR(14),
@idHorario			INT,
@estado				VARCHAR(8),
@numerolicenciatura	VARCHAR(5),
@idAreaMedica		INT,
@accion				INT --0: Insertar, 1: Modificar, 2: Eliminar, 3: Recuperar
)
AS

BEGIN
  BEGIN TRANSACTION
	DECLARE @id INT, @output INT
    IF @accion = 0
    BEGIN
		
		EXEC @id = up_ManPersona @idPersona, @paterno, @materno, @nombres, @sexo, 
								 @fechaNacimiento, @idTipoDocumento, @numeroDocumento, 
								 @estadoCivil, @idLugarNacimiento, @idDomicilio, 
								 @telefono, @celular, @email, @fechaRegistro, @tipoPersona, 
								 @accion, @output OUTPUT
		
		EXEC up_ManPersonal @id, @usuario, @contrasena, @tipoPersonal, @idHorario, 
							'ACTIVO', @accion
		
		INSERT INTO Enfermera (Id_Enfermera, Id_AreaMedica, NumeroLicenciatura)
		VALUES (@id, @idAreaMedica, @numerolicenciatura)
	END
    
    IF @accion = 1
    BEGIN
		
		EXEC @id = up_ManPersona @idPersona, @paterno, @materno, @nombres, @sexo, 
								 @fechaNacimiento, @idTipoDocumento, @numeroDocumento, 
								 @estadoCivil, @idLugarNacimiento, @idDomicilio, 
								 @telefono, @celular, @email, @fechaRegistro, @tipoPersona, 
								 @accion, @output OUTPUT
		
		EXEC up_ManPersonal @id, @usuario, @contrasena, @tipoPersonal, @idHorario, 
							'ACTIVO', @accion
		
		UPDATE Enfermera
		SET	  Id_AreaMedica = @idAreaMedica, NumeroLicenciatura = @numerolicenciatura
		WHERE Id_Enfermera = @id
	END
    
  IF @@ERROR <> 0
  BEGIN
    ROLLBACK TRANSACTION
	RETURN
  END
  COMMIT TRANSACTION
END