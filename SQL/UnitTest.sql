
DECLARE @T1  AS dbo.MyTableType, @T2 As dbo.MyTableType, @RETVAL INT;

-- Test 1- both tables empty
SELECT @RETVAL = dbo.CheckTableForDataEquality(@T1, @T2)
if (@RETVAL != 0)
  raiserror('Test 1 failed', -20,1);

-- Test 2 - one entry in @T1
INSERT INTO @T1 VALUES(1,2,5)
SELECT @RETVAL = dbo.CheckTableForDataEquality(@T1, @T2)
if (@RETVAL != 1)
BEGIN
  raiserror('Test 2 failed', -20,1);
  return;
END;
DELETE FROM @T1;
DELETE FROM @T2;

-- Test 3 - one entry in @T1 & @T2
INSERT INTO @T1 VALUES(1,2,5)
INSERT INTO @T2 VALUES(1,2,5)

SELECT @RETVAL = dbo.CheckTableForDataEquality(@T1, @T2)
if (@RETVAL != 0)
BEGIN
  raiserror('Test 3 failed', -20,1);
  return;
END;
DELETE FROM @T1;
DELETE FROM @T2;


PRINT('Tests passed');