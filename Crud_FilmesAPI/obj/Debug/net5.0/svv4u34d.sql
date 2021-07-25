IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Generos] (
    [Id] int NOT NULL IDENTITY,
    [Nome] varchar(100) NOT NULL,
    [DataCriacao] datetime2 NOT NULL,
    [Ativo] nvarchar(1) NOT NULL,
    CONSTRAINT [PK_Generos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Locacoes] (
    [Id] int NOT NULL IDENTITY,
    [CPF] varchar(14) NOT NULL,
    [DataLocacao] datetime2 NOT NULL,
    CONSTRAINT [PK_Locacoes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Filmes] (
    [Id] int NOT NULL IDENTITY,
    [Nome] varchar(200) NOT NULL,
    [DataCriacao] datetime2 NOT NULL,
    [Ativo] nvarchar(1) NOT NULL,
    [GeneroId] int NOT NULL,
    CONSTRAINT [PK_Filmes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Filmes_Generos_GeneroId] FOREIGN KEY ([GeneroId]) REFERENCES [Generos] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [FilmeLocacao] (
    [FilmesId] int NOT NULL,
    [LocacoesId] int NOT NULL,
    CONSTRAINT [PK_FilmeLocacao] PRIMARY KEY ([FilmesId], [LocacoesId]),
    CONSTRAINT [FK_FilmeLocacao_Filmes_FilmesId] FOREIGN KEY ([FilmesId]) REFERENCES [Filmes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FilmeLocacao_Locacoes_LocacoesId] FOREIGN KEY ([LocacoesId]) REFERENCES [Locacoes] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_FilmeLocacao_LocacoesId] ON [FilmeLocacao] ([LocacoesId]);
GO

CREATE INDEX [IX_Filmes_GeneroId] ON [Filmes] ([GeneroId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210724221223_Inicial', N'5.0.8');
GO

COMMIT;
GO

