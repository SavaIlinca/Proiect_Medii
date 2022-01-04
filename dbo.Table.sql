CREATE TABLE [dbo].[Client]
(
	[IdClient] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nume] NVARCHAR(50) NULL, 
    [Prenume] NVARCHAR(50) NULL, 
    [Telefon] TEXT NULL
)
