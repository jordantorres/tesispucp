CREATE PROCEDURE up_AD_LogTableChanges
(
   @tblname VARCHAR(128)
)
AS
BEGIN

CREATE TABLE #colinfo 
(colname VARCHAR(128), 
 datatype VARCHAR(20), 
 coltype VARCHAR(20), 
 orderpos INT, 
 colsupdated varbinary(16) DEFAULT COLUMNS_UPDATED(), 
 cid INT IDENTITY(1,1))


DECLARE @cnti INT, @cntd INT, @maxid int, @minid INT, @pkcols INT, @maxpkcolid INT, @cmd nvarchar(4000), @audittblname VARCHAR(128),
	@cmdINSERT nvarchar(4000), @cmdSELECT nvarchar(4000), @cmdSELECTI nvarchar(4000), @cmdWHERE nvarchar(4000),  
        @modif_id bigint, @cmdFROM VARCHAR(250), @AUDITINSERT VARCHAR(255), @DUI VARCHAR(10), 
        @SelectSeparator VARCHAR(10), @InsertSeparator VARCHAR(10), @WhereSeparator VARCHAR(10), @ActionQuery VARCHAR(4000)
	
SET NOCOUNT ON 

SELECT @ActionQuery = ''

-- Obtiene el query que dispara el trigger
EXEC up_AD_ActionQuery @ActionQuery OUTPUT

-- Obtiene la informacion de todas las columnas de la tabla #colinfo
EXEC up_AD_ColInfo @tblname 
 
SET @audittblname = 'LogAudit'


SELECT @cnti = COUNT(*) FROM #ti
SELECT @cntd = COUNT(*) FROM #td

--select @cnti = 2
--select @cntd = 1

-- check how many rows changed. If 0 then do nothing
IF (@cnti = 0 and @cntd = 0)
   return 0
 
SET @cmdSELECT = ' '''
SET @cmdSELECTI = ''

SET @cmdWHERE = ''

SET @cmdINSERT = ' INSERT INTO ' + @audittblname + ' ( TableName, TableAction, ActionQuery, OldValues, NewValues ) '

SELECT @SelectSeparator = ' + '';' 
SELECT @InsertSeparator = ','  
SELECT @WhereSeparator = ' AND '

SELECT @maxid = MAX(cid), @minid = MIN(cid) FROM #colinfo

SELECT @maxpkcolid = MAX(cid), @pkcols = COUNT(*) FROM #colinfo WHERE coltype = 'PRIMARY KEY'

IF (@cnti = @cntd)
BEGIN

	SELECT
	@cmdWHERE = CASE 
			WHEN coltype = 'PRIMARY KEY' THEN 
				@cmdWHERE + 'd.' + colname + '=' + 'i.' + colname + CASE WHEN cid < @maxpkcolid THEN @WhereSeparator ELSE '' END
			ELSE
				@cmdWHERE + ''
	END,
	@cmdSELECT = CASE 
			WHEN coltype <> 'PRIMARY KEY' THEN 
				@cmdSELECT + colname + '='' + ' + dbo.fn_FieldDelimiter(datatype) + ' + convert(VARCHAR(512),d.' + colname + ') + ' + dbo.fn_FieldDelimiter(datatype) + CASE WHEN cid < @maxid THEN @SelectSeparator ELSE '' END
			ELSE
				@cmdSELECT + ''
	END,
	@cmdSELECTI = CASE 
			WHEN coltype <> 'PRIMARY KEY' THEN 
				@cmdSELECTI + colname + '='' + ' + dbo.fn_FieldDelimiter(datatype) + ' + convert(VARCHAR(512),i.' + colname + ') + ' + dbo.fn_FieldDelimiter(datatype) + CASE WHEN cid < @maxid THEN @SelectSeparator ELSE '' END
			ELSE
				@cmdSELECTI + ''
	END
	FROM  #colinfo

	SELECT @cmdSELECT = @cmdSELECT + ', ''' + @cmdSELECTI

	SELECT @DUI = 'U'
	SELECT @cmdFROM = ' FROM #td d, #ti i '

	IF @pkcols > 0 SELECT @cmdWHERE = ' WHERE ' + @cmdWHERE


END


IF (@cnti < @cntd)
BEGIN

	SELECT
	@cmdSELECT = @cmdSELECT + colname + '='' + ' + dbo.fn_FieldDelimiter(datatype) + ' + convert(VARCHAR(512), d.' + colname + ') + ' + dbo.fn_FieldDelimiter(datatype) + CASE WHEN cid < @maxid THEN @SelectSeparator ELSE '' END
	FROM  #colinfo

	SELECT @cmdSELECT = @cmdSELECT + ','''''
	SELECT @DUI = 'D' 
	SELECT @cmdFROM = ' FROM #td d '

END

IF (@cnti > @cntd)
BEGIN

	SELECT
	@cmdSELECT = @cmdSELECT + colname + '='' + ' + dbo.fn_FieldDelimiter(datatype) + ' + convert(VARCHAR(512), i.' + colname + ') + ' + dbo.fn_FieldDelimiter(datatype) + CASE WHEN cid < @maxid THEN @SelectSeparator ELSE '' END
	FROM #colinfo

	SELECT @cmdSELECT = ''''',' + @cmdSELECT
	SELECT @DUI = 'I' 
	SELECT @cmdFROM = ' FROM #ti i '

END

EXEC (@cmdINSERT + 'SELECT ' + '''' + @tblname + ''',''' + @DUI + ''',''' + @ActionQuery + ''',' + @cmdSELECT + @cmdFROM + @cmdWHERE) 

SET NOCOUNT OFF 

END