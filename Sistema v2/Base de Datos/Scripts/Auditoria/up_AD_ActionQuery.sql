CREATE PROCEDURE up_AD_ActionQuery
(
  @ActionQuery varchar(4000) OUTPUT
)
AS

SET NOCOUNT ON 

DECLARE @ExecStr varchar(512)

SET @ActionQuery = ''


CREATE TABLE #inputbuffer
 (
  EventType nvarchar(30), 
  Parameters int, 
  EventInfo nvarchar(255)
 )

SET @ExecStr = 'DBCC INPUTBUFFER(' + RTRIM(LTRIM(STR(@@SPID))) + ') WITH NO_INFOMSGS'

INSERT INTO #inputbuffer
EXEC ('DBCC INPUTBUFFER(@@SPID) WITH NO_INFOMSGS')

SET @ActionQuery = (SELECT EventInfo FROM #inputbuffer)

SET @ActionQuery = REPLACE(@ActionQuery,"'","''")

SET @ActionQuery = REPLACE(@ActionQuery,char(10),"")
SET @ActionQuery = REPLACE(@ActionQuery,char(13)," ")

SET NOCOUNT OFF