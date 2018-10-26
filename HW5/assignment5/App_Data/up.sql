CREATE TABLE [dbo].[Requests]
(
    [ID]				INT IDENTITY (0,1)		NOT NULL,
    [FirstName]			NVARCHAR(64)			NOT NULL,
    [LastName]			NVARCHAR(128)		    NOT NULL,
    [PhoneNum]			NVARCHAR(15)            NOT NULL,
	[AptName]			NVARCHAR(40)			NOT NULL,
	[UnitNum]			INT						NOT NULL,
	[Comments]			NVARCHAR(1000)			NOT NULL,
	[Permission]		bit						NOT NULL,
	[SubmissionTime]	DateTime				NOT NULL
	CONSTRAINT [PK_dbo.Requests] PRIMARY KEY CLUSTERED ([ID] ASC)
);

INSERT INTO [dbo].[Requests] (FirstName, LastName, PhoneNum, AptName, UnitNum, Comments, Permission, SubmissionTime) VALUES
    ('Alessa','Gillespie','555-838-3298', 'Silent Hills Apartments', '1', 'There is a hole in the wall.', '1', '09/14/2018 14:24:00'),
    ('Cybil','Bennet','555-838-6701', 'Silent Hills Apartments', '2', 'The walls are rusted...out of nowhere...', '1', '09/14/2018 12:45:00'),
	('Heather','Mason','555-838-8923', 'Silent Hills Apartments', '6', 'I think our refridgerator is broken.', '1', ' 09/14/2018 17:30:00'),
	('Alessa','Gillespie','555-838-1076', 'Silent Hills Apartments', '1', 'The hole is gone now.', '1', '09/14/2018 16:14:00'),
	('Matthew','Mercer','555-838-0000', 'Silent Hills Apartments', '5', 'My next door neighbor is loud at night.', '1', '09/14/2018 13:14:00')
GO