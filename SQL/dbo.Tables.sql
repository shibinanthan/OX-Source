﻿CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserName] NCHAR(15) NOT NULL, 
    [Password] NCHAR(10) NOT NULL
)
