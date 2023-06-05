use RompeCabezaPW3;
drop table if exists Sala;
drop table if exists ScoreMap;

go
CREATE TABLE Sala(
	id INT IDENTITY(1,1) PRIMARY KEY,
	nickName VARCHAR(256) unique,
	cant_pieces INT,
	limite_participantes int,
	pin VARCHAR(4),
	nroSala INT,
);
go

go
CREATE TABLE ScoreMap(
	id INT IDENTITY(1,1) PRIMARY KEY,
	nickName VARCHAR(256) unique,
	score float,
	sala int,
	FOREIGN KEY (sala) REFERENCES Sala(id)
);
go

