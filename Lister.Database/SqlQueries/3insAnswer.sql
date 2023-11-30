USE ListerDb;
GO

SET ANSI_NULLS ON;
SET ANSI_PADDING ON;
SET QUOTED_IDENTIFIER ON;
GO

IF NOT EXISTS (SELECT * FROM Answer)
BEGIN
	INSERT INTO Answer (question_id, answer_text)
	VALUES (1, 'wrong'),
		   (1, 'right'),
		   (1, 'wrong'),

		   (2, 'wrong'),
		   (2, 'right'),

		   (3, 'right'),
		   (3, 'right'),

		   (4, 'right'),
		   (4, 'wrong')
END
GO

/* -- Rollback

USE ListerDb;
GO

SET ANSI_NULLS ON;
SET ANSI_PADDING ON;
SET QUOTED_IDENTIFIER ON;
GO

DELETE Answer;
GO

--*/