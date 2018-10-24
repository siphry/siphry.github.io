CREATE TABLE [dbo].[Requests]
(
    [ID]        INT IDENTITY (0,1)    NOT NULL,
    [FirstName]    NVARCHAR(64)        NOT NULL,
    [LastName]    NVARCHAR(128)        NOT NULL,
    [PhoneNum]	NVARCHAR(15)            NOT NULL,
	[AptName]	NVARCHAR(40)			NOT NULL,
	[UnitNum]	INT					NOT NULL,
	[Comments]	NVARCHAR(1000)			NOT NULL,
	[Permission]	bit NOT NULL,
	[SubmissionTime] DateTime NOT NULL,
    CONSTRAINT [PK_dbo.OurRequests] PRIMARY KEY CLUSTERED ([ID] ASC)
);

INSERT INTO [dbo].[Requests] (FirstName, LastName, PhoneNum, AptName, UnitNum, Comments, Permission, SubmissionTime) VALUES
    ('Stacia','Fry','555-838-3298', 'Silent Hills Apartments', '1', 'My washing machine is busted.', '1', '02:24:00'),
    ('Angela','Calabrese','555-838-6701', 'Silent Hills Apartments', '2', 'My refridgerator makes a weird noise.', '1', '03:24:00'),
	('Chloe','Takacs','555-838-8923', 'Silent Hills Apartments', '2', 'I think our refridgerator is broken.', '1', '05:30:00'),
	('Randolph','Schuck','555-838-1076', 'Silent Hills Apartments', '1', 'The washing machine is leaking water.', '1', '01:14:00'),
	('Matthew','Mercer','555-838-0000', 'Silent Hills Apartments', '5', 'My next door neighbor is loud at night.', '1', '07:44:00')
GO