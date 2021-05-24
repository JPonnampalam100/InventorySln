IF EXISTS (SELECT 1 FROM sys.types WHERE is_table_type = 1 AND name ='MyTableType') 
DROP TYPE [MyTableType];
	GO
         
CREATE TYPE [MyTableType] AS TABLE
(
        Id INT NOT NULL PRIMARY KEY CLUSTERED,
        Value1 INT NOT NULL,
        Value2 INT NULL
);

GO
