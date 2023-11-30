USE ListerDb;
GO

SET ANSI_NULLS ON;
SET ANSI_PADDING ON;
SET QUOTED_IDENTIFIER ON;
GO

IF NOT EXISTS (SELECT * FROM Test)
BEGIN
	INSERT INTO Test (name, description, imagesrc, timelimitsec)
	VALUES ('Test1', 'testtesttesttesttesttesttesttesttesttest', null, 180),
		   ('Test2', 'testtesttesttesttesttesttesttesttesttest', null, 60)
END
GO

/* -- Rollback

USE ListerDb;
GO

SET ANSI_NULLS ON;
SET ANSI_PADDING ON;
SET QUOTED_IDENTIFIER ON;
GO

DELETE Test;
GO

--*/