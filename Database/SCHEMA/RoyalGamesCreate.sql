CREATE DATABASE RoyalGames;
GO

USE RoyalGames;
GO

------------------------
-- Criando as tableas --
------------------------

CREATE TABLE Usuario(
	UsuarioID INT IDENTITY(1,1) PRIMARY KEY,
	Nome VARCHAR(60) NOT NULL,
	Email VARCHAR(150) UNIQUE NOT NULL,
	Senha VARBINARY(32) NOT NULL,
	StatusUsuario BIT DEFAULT 1
);
GO

CREATE TABLE ClassificacaoIndicativa(
	ClassificacaoID INT IDENTITY(1,1) PRIMARY KEY,
	Nome VARCHAR(50) NOT NULL
);
GO

CREATE TABLE Produto(
	ProdutoID INT IDENTITY(1,1) PRIMARY KEY,
	Nome VARCHAR(100) UNIQUE NOT NULL,
	Preco DECIMAL(10,2) NOT NULL,
	Descricao NVARCHAR(MAX) NOT NULL,
	Imagem VARBINARY(MAX) NOT NULL,
	StatusProduto BIT DEFAULT 1,
	UsuarioID INT NOT NULL,
    	ClassificacaoID INT NOT NULL,
    	CONSTRAINT FK_Produto_Usuario
        	FOREIGN KEY (UsuarioID)
        	REFERENCES Usuario(UsuarioID),
    	CONSTRAINT FK_Produto_Classificacao
        FOREIGN KEY (ClassificacaoID)
        REFERENCES ClassificacaoIndicativa(ClassificacaoID)
);
GO

CREATE TABLE Genero(
    GeneroID INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(50) UNIQUE NOT NULL 
);
GO

CREATE TABLE Plataforma(
    PlataformaID INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(50) UNIQUE NOT NULL 
);
GO

CREATE TABLE Promocao(
	PromocaoId INT IDENTITY(1,1) PRIMARY KEY,
	Nome VARCHAR(50) UNIQUE NOT NULL,
	DataExpiracao DATETIME NOT NULL,
	StatusPromocao BIT DEFAULT 1
);
GO

CREATE TABLE ProdutoPromocao(
PromocaoId INT NOT NULL,
ProdutoId INT NOT NULL,
Valor DECIMAL,
CONSTRAINT PK_ProdutoPromocao_ProdutoId_PromocaoId PRIMARY KEY (PromocaoId, ProdutoId), 
CONSTRAINT FK_ProdutoPromocao_ProdutoId FOREIGN KEY (ProdutoId) REFERENCES Produto (ProdutoId),
CONSTRAINT FK_ProdutoPromocao_Promocao_PromocaoId FOREIGN KEY (PromocaoId) REFERENCES Promocao (PromocaoId)
);
GO

CREATE TABLE ProdutoGenero(
    ProdutoID INT NOT NULL,
    GeneroID INT NOT NULL,

	CONSTRAINT PK_ProdutoGenero PRIMARY KEY (ProdutoID, GeneroID),
	CONSTRAINT FK_ProdutoGenero_Produto FOREIGN KEY (ProdutoID)
        REFERENCES Produto(ProdutoID) ON DELETE CASCADE,
	CONSTRAINT FK_ProdutoGenero_Genero FOREIGN KEY (GeneroID)
        REFERENCES Genero(GeneroID) ON DELETE CASCADE
);
GO

CREATE TABLE ProdutoPlataforma(
    ProdutoID INT NOT NULL,
    PlataformaID INT NOT NULL,

    CONSTRAINT PK_ProdutoPlataforma PRIMARY KEY (ProdutoID, PlataformaID),
	CONSTRAINT FK_ProdutoPlataforma_Produto FOREIGN KEY (ProdutoID)
        REFERENCES Produto(ProdutoID) ON DELETE CASCADE,
    CONSTRAINT FK_ProdutoPlataforma_Plataforma FOREIGN KEY (PlataformaID)
        REFERENCES Plataforma(PlataformaID)ON DELETE CASCADE
);
GO


CREATE TABLE Log_AlteracaoProduto(
	Log_AlteracaoProdutoID INT PRIMARY KEY IDENTITY,
	DataAlteracao DATETIME2(0) NOT NULL,
	NomeAnterior VARCHAR(100),
	PrecoAnterior DECIMAL(10, 2),

	ProdutoID INT FOREIGN KEY REFERENCES Produto(ProdutoID)
);
GO

-------------------------
-- Criação da Triggers --
-------------------------
CREATE TRIGGER trg_ExclusaoUsuario
ON Usuario
INSTEAD OF DELETE
AS
BEGIN
	UPDATE a SET StatusUsuario = 0
	FROM Usuario a
	INNER JOIN deleted d 
		ON d.UsuarioID = a.UsuarioID;
END
GO

CREATE TRIGGER trg_AlteracaoProduto
ON Produto
AFTER UPDATE
AS
BEGIN
	INSERT INTO Log_AlteracaoProduto
	(
		DataAlteracao, 
		ProdutoID, 
		NomeAnterior, 
		PrecoAnterior
	)
	SELECT GETDATE(), ProdutoID, Nome, Preco FROM deleted 
END
GO

CREATE TRIGGER trg_ExclusaoProduto
ON Produto
INSTEAD OF DELETE
AS
BEGIN
	UPDATE p SET StatusProduto = 0
	FROM Produto p
	INNER JOIN deleted d 
		ON d.ProdutoID = p.ProdutoID;
END
GO

-------------------------
-- Inserção de valores --
-------------------------

INSERT INTO ClassificacaoIndicativa (Nome)
VALUES ('Livre'),('10'),('12'),('14'),('16'),('18');
GO

INSERT INTO Usuario (Nome, Email, Senha)
VALUES
('Carlos Silva', 'carlos@email.com', HASHBYTES('SHA2_256','123456')),
('Juliana Souza', 'juliana@email.com', HASHBYTES('SHA2_256','123456'));
GO

INSERT INTO Genero (Nome)
VALUES('Ação'),('Aventura'),('RPG'),('Esporte'),('Suspense');
GO

INSERT INTO Plataforma (Nome)
VALUES('PC'),('PlayStation 5'),('Xbox Series X'),('Nintendo Switch'),('Super Nintendo'),('Mega Drive'),('PlayStation 2');
GO

INSERT INTO Produto 
(Nome, Preco, Descricao, Imagem, UsuarioID, ClassificacaoID)
VALUES
('Super Mario World', 199.90, 
'Clássico jogo de plataforma do Super Nintendo.', 
CONVERT(VARBINARY(MAX), 'mario.jpg'), 2, 1),

('Sonic The Hedgehog', 149.90, 
'Clássico da SEGA em cartucho original.', 
CONVERT(VARBINARY(MAX), 'sonic.jpg'), 2, 1),

('God of War II', 89.90, 
'Versão original para PlayStation 2.', 
CONVERT(VARBINARY(MAX), 'gow2.jpg'), 2, 5),

('The Last of Us Part II', 249.90, 
'Versão remasterizada para nova geração.', 
CONVERT(VARBINARY(MAX), 'tlou2.jpg'), 1, 5),

('Elden Ring', 279.90, 
'RPG de ação premiado.', 
CONVERT(VARBINARY(MAX), 'eldenring.jpg'), 1, 5),

('FIFA 24', 199.90, 
'Simulador de futebol atualizado.', 
CONVERT(VARBINARY(MAX), 'fifa24.jpg'), 2, 1),

('PlayStation 2 Slim', 499.90, 
'Console usado em ótimo estado.', 
CONVERT(VARBINARY(MAX), 'ps2.jpg'), 2, 1),

('Super Nintendo Original', 799.90, 
'Console retrô funcionando perfeitamente.', 
CONVERT(VARBINARY(MAX), 'snes.jpg'), 2, 1),

('PlayStation 5', 3899.90, 
'Console novo lacrado.', 
CONVERT(VARBINARY(MAX), 'ps5.jpg'), 1, 1);
GO

INSERT INTO ProdutoGenero (ProdutoID, GeneroID)
VALUES
(1, 2),
(2, 2), 
(3, 1), 
(4, 1), 
(5, 3),
(6, 4);
GO

INSERT INTO ProdutoPlataforma (ProdutoID, PlataformaID)
VALUES
(1, 5),
(2, 6),
(3, 7),
(4, 2), 
(5, 3),
(6, 2), 
(7, 5),
(8, 5),
(9, 2); 
GO

