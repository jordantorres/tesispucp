CREATE PROCEDURE up_SelHistoriaClinica
(
	@IdHistoriaClinica	INT
)
AS
BEGIN	
	SELECT IdHistoriaClinica, Numero, IdPaciente, IdAntecedentes
	FROM HistoriaClinica
	WHERE IdHistoriaClinica = @IdHistoriaClinica
END
