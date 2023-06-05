use RompeCabezaPW3;

go
CREATE TABLE ScoreMap(
	id INT IDENTITY(1,1) PRIMARY KEY,
	nickName VARCHAR(256) unique,
	score float
);
go

go
CREATE TABLE Sala(
	id INT IDENTITY(1,1) PRIMARY KEY,
	nickName VARCHAR(256) unique,
	cant_pieces INT,
	pin VARCHAR(4),
	score_map INT,
	nroSala INT,
	FOREIGN KEY (score_map) REFERENCES ScoreMap(id)
);
go