CREATE DATABASE ClientesDB;
GO
USE ClientesDB;
GO

CREATE TABLE tblGrupo(
	Id int PRIMARY KEY IDENTITY NOT NULL,
	Nombre varchar(100) NOT NULL
);

CREATE TABLE tblPais(
	Id int PRIMARY KEY IDENTITY NOT NULL,
	Nombre varchar(100) NOT NULL
);

CREATE TABLE tblCliente(
	Id int PRIMARY KEY IDENTITY NOT NULL,
	IdPais int NOT NULL,
	IdGrupo int NOT NULL,
	Nombre varchar(100) NOT NULL,
	RNC char(11) NOT NULL UNIQUE,
	FOREIGN KEY (IdPais) REFERENCES tblPais(Id),
	FOREIGN KEY (IdGrupo) REFERENCES tblGrupo(Id),
);

CREATE TABLE tblArticulo(
	Id int PRIMARY KEY IDENTITY NOT NULL,
	Nombre varchar(100) NOT NULL,
	Codigo char(6) NOT NULL UNIQUE,
	Precio decimal(9,2)
);

CREATE TABLE tblClienteArticulo(
	Id int PRIMARY KEY IDENTITY NOT NULL,
	IdCliente int NOT NULL,
	IdArticulo int NOT NULL
	FOREIGN KEY (IdCliente) REFERENCES tblCliente(Id),
	FOREIGN KEY (IdArticulo) REFERENCES tblArticulo(Id),
);

INSERT INTO tblPais (Nombre) VALUES
	('REPUBLICA DOMINICANA'),
	('MEXICO'),
	('COSTA RICA');

INSERT INTO tblGrupo (Nombre) VALUES
	('REGULAR'),
	('NUEVO'),
	('TEMPORAL');

INSERT INTO tblCliente (IdGrupo,IdPais,Nombre,RNC) VALUES
	(1,1,'SUPER MERCADO UNIVERSAL','23013085075'),
	(1,2,'FERRETERIA DIAMANTE','88838052599'),
	(1,1,'ALMACENES SANCHEZ','86577087161'),
	(2,1,'MUEBLES DEL ESTE','37505244879'),
	(3,3,'PLAZA IMPERIAL','51399349790');

INSERT INTO tblArticulo (Nombre,Codigo,Precio) VALUES
	('CLORO BOTELLA 1 LITRO','397593',101.99),
	('JABON BARRA 250 GRAMOS','998174',70.00),
	('JABON BARRA 100 GRAMOS','340649',49.98),
	('CLORO BOTELLA 1 GALON','621595',200.00),
	('DETERGENTE DE LAVAR 400 GRAMOS','813666',150.00),
	('DETERGENTE DE LAVAR 1000 GRAMOS','655771',229.50),
	('LIMPIA VIDRIOS 1 LITRO','553555',112.00),
	('DETERGENTE DE FREGAR 2 LITROS','863468',280.11);