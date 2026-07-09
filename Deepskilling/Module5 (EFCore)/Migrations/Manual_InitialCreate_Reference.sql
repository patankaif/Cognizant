IF DB_ID('Module5EFCoreDb') IS NULL
    CREATE DATABASE Module5EFCoreDb;
GO

USE Module5EFCoreDb;
GO

CREATE TABLE Authors
(
    AuthorId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL
);
GO

CREATE TABLE AuthorProfiles
(
    AuthorId INT PRIMARY KEY,
    Biography NVARCHAR(2000) NOT NULL,
    Website NVARCHAR(300) NULL,
    CONSTRAINT FK_AuthorProfiles_Authors FOREIGN KEY (AuthorId)
        REFERENCES Authors (AuthorId) ON DELETE CASCADE
);
GO

CREATE TABLE Books
(
    BookId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(300) NOT NULL,
    PublishedYear INT NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    AuthorId INT NOT NULL,
    RowVersion ROWVERSION NOT NULL,
    CONSTRAINT FK_Books_Authors FOREIGN KEY (AuthorId)
        REFERENCES Authors (AuthorId) ON DELETE CASCADE
);
GO

CREATE INDEX IX_Books_Title ON Books (Title);
GO

CREATE TABLE Genres
(
    GenreId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);
GO

CREATE UNIQUE INDEX IX_Genres_Name ON Genres (Name);
GO

CREATE TABLE BookGenres
(
    BookId INT NOT NULL,
    GenreId INT NOT NULL,
    CONSTRAINT PK_BookGenres PRIMARY KEY (BookId, GenreId),
    CONSTRAINT FK_BookGenres_Books FOREIGN KEY (BookId)
        REFERENCES Books (BookId) ON DELETE CASCADE,
    CONSTRAINT FK_BookGenres_Genres FOREIGN KEY (GenreId)
        REFERENCES Genres (GenreId) ON DELETE CASCADE
);
GO
