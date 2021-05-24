IF EXISTS (SELECT *
           FROM   sys.objects
           WHERE  object_id = OBJECT_ID(N'[dbo].[CheckTableForDataEquality]')
                  AND type IN ( N'FN', N'IF', N'TF', N'FS', N'FT' ))
  DROP FUNCTION [dbo].[CheckTableForDataEquality]

GO 

CREATE FUNCTION [dbo].[CheckTableForDataEquality]
    (
      @t1 [dbo].[MyTableType] READONLY,
	  @t2 [dbo].[MyTableType] READONLY

    )
RETURNS INT
AS
 
    BEGIN
	RETURN (SELECT
	(CASE
		  WHEN NOT EXISTS
		 (SELECT Value1, Value2 FROM @T1
		  EXCEPT
		  SELECT Value1, Value2 FROM @T2)  
		  AND  NOT EXISTS
		  (SELECT Value1, Value2 FROM @T2
		  EXCEPT
		  SELECT Value1, Value2 FROM @T1) THEN 0
		  ELSE 1
	END) As Value)
 

		
	   
    END
GO