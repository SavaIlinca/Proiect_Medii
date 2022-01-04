CREATE TABLE [dbo].[Programare]
(
	[IdProgramare] INT NOT NULL PRIMARY KEY IDENTITY, 
    [IdClient] INT NULL, 
    [IdHairstylist] INT NULL, 
    [Data] DATETIME NULL

	CONSTRAINT fk_programari_cli_id
 FOREIGN KEY (IdClient)
 REFERENCES Client (IdClient)
 ON DELETE CASCADE
ON UPDATE CASCADE,
CONSTRAINT fk_programari_hair_id
 FOREIGN KEY (IdHairstylist)
 REFERENCES Hairstylist (IdHairstylist)
 ON DELETE CASCADE
ON UPDATE CASCADE
);
