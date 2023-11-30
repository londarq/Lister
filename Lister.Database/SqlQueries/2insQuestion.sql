USE ListerDb;
GO

SET ANSI_NULLS ON;
SET ANSI_PADDING ON;
SET QUOTED_IDENTIFIER ON;
GO

IF NOT EXISTS (SELECT * FROM Question)
BEGIN
	INSERT INTO Question (test_id, question_text, media_link)
	VALUES (1, 'testt?', null),
		   (1, 'testtest?', null),

		   (2, 'testtes?', null),
		   (2, 'esttesttesttesttesttestt?', null)
END
GO

/* -- Rollback

USE ListerDb;
GO

SET ANSI_NULLS ON;
SET ANSI_PADDING ON;
SET QUOTED_IDENTIFIER ON;
GO

DELETE Question;
GO

--*/