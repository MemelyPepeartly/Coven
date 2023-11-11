CREATE SCHEMA app;
GO
CREATE TABLE app.[User]
(
    userId UNIQUEIDENTIFIER PRIMARY KEY,
    username NVARCHAR(50) NOT NULL,
    email NVARCHAR(50) NOT NULL UNIQUE,
    worldAnvilUsername NVARCHAR(50) NOT NULL UNIQUE
);
CREATE TABLE app.[World]
(
    worldId UNIQUEIDENTIFIER PRIMARY KEY,
    userId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES app.[User](userId) NOT NULL,
    worldName NVARCHAR(100) NOT NULL
);
CREATE TABLE app.[PineconeVectorMetadata]
(
    entryId UNIQUEIDENTIFIER PRIMARY KEY,
    worldId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES app.[World](worldId) NOT NULL,
    articleId UNIQUEIDENTIFIER NOT NULL,
    characterString NVARCHAR(MAX) NOT NULL
);

CREATE TABLE app.[WorldContent]
(
    worldContentId UNIQUEIDENTIFIER PRIMARY KEY,
    worldId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES app.[World](worldId) NOT NULL,
    articleId UNIQUEIDENTIFIER NOT NULL,
    worldAnvilArticleType NVARCHAR(200),
    author NVARCHAR(200),
    content NVARCHAR(MAX),
    articleTitle NVARCHAR(200)
);


-- DROP TABLE app.PineconeVectorMetadata;
-- DROP TABLE app.[User];
-- DROP SCHEMA app;
