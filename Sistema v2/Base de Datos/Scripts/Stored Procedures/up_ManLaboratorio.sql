USE [CentroSalud]
GO
/****** Object:  StoredProcedure [dbo].[up_ManLaboratorio]    Script Date: 09/06/2013 13:04:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[up_ManLaboratorio]
(
	@IdLaboratorio		INT,
	@Codigo				VARCHAR(3),
	@Nombre				VARCHAR(100),
	@Descripcion		VARCHAR(300),
	@Estado				VARCHAR(8),
	@Accion				INT,
	@IdGenerado			INT	OUTPUT --0:INSERT, 1:UPDATE, 2:DELETE, 3:RECOVER, 4:SELECT, 5:SELECTALL, 6:SELECT CUSTOM
)
AS
BEGIN
  BEGIN TRANSACTION
    IF @Accion = 0
    BEGIN
		INSERT INTO Laboratorio(Codigo, Nombre, Descripcion, Estado)
		VALUES (@Codigo, @Nombre, @Descripcion, 'ACTIVO')
		
		SET @IdGenerado = SCOPE_IDENTITY()
    END
    
    IF @Accion = 1
    BEGIN
		UPDATE Laboratorio SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion
		WHERE IdLaboratorio = @IdLaboratorio
							
		SET @IdGenerado = 1
    END
    IF @Accion = 2
    BEGIN
		UPDATE Laboratorio SET Estado = 'INACTIVO'
		WHERE IdLaboratorio = @IdLaboratorio
		
		SET @IdGenerado = 1
    END
    IF @Accion = 3
    BEGIN
		UPDATE Laboratorio SET Estado = 'ACTIVO'
		WHERE IdLaboratorio = @IdLaboratorio
		
		SET @IdGenerado = 1
    END
    IF @Accion = 4
    BEGIN
		SELECT IdLaboratorio, Codigo, Nombre, Descripcion, Estado 
		FROM Laboratorio
		WHERE IdLaboratorio = @IdLaboratorio
		ORDER BY Codigo, Descripcion
	END
    IF @Accion = 5
    BEGIN
		SELECT IdLaboratorio, Codigo, Nombre, Descripcion, Estado 
		FROM Laboratorio
		ORDER BY Codigo, Descripcion
	END
	IF @Accion = 6
	BEGIN
		DECLARE @Query VARCHAR(1000)
		SET @Query = 'SELECT IdLaboratorio, Codigo, Nombre, Descripcion, Estado FROM Laboratorio WHERE IdLaboratorio > 0'
		
		IF @Codigo <> ''
		BEGIN
			SET @Query = @Query + ' AND Codigo = ''' + @Codigo + ''''
		END
		
		IF @Nombre <> ''
		BEGIN
			SET @Query = @Query + ' AND Nombre LIKE ''%' + @Nombre + '%'''
		END
		
		IF @Estado <> 'TODOS'
		BEGIN
			SET @Query = @Query + ' AND Estado = ''' + @Estado + ''''
		END	
		
		SET @Query = @Query + ' ORDER BY Codigo, Nombre'	
						
		EXEC(@Query)
	END
  IF @@ERROR <> 0
  BEGIN
    ROLLBACK TRANSACTION
    
    SET @IdGenerado = 0
	
	RETURN @IdGenerado
  END
  
  COMMIT TRANSACTION
  
  RETURN @IdGenerado
END