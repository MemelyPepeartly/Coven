CREATE SCHEMA app;
GO
CREATE TABLE app.Client
(
    userId UNIQUEIDENTIFIER PRIMARY KEY ,
    username NVARCHAR(50) NOT NULL,
    email NVARCHAR(50) NOT NULL UNIQUE
);
CREATE TABLE app.[Character]
(
    characterId UNIQUEIDENTIFIER PRIMARY KEY,
    characterName NVARCHAR(50) NOT NULL,
    userId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES app.Client(userId) NOT NULL,
);
CREATE TABLE app.FeatureCategory
(
    featureCategoryId UNIQUEIDENTIFIER PRIMARY KEY,
    featureCategoryName NVARCHAR(25) NOT NULL,
    featureCategoryDescription NVARCHAR(150) NOT NULL
);
CREATE TABLE app.Feature
(
    featureId UNIQUEIDENTIFIER PRIMARY KEY,
    featureName NVARCHAR(25) NOT NULL,
    featureCategoryId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES app.FeatureCategory(featureCategoryId) NOT NULL
);
CREATE TABLE app.CustomFeature
(
    customFeatureId UNIQUEIDENTIFIER PRIMARY KEY,
    customFeatureName NVARCHAR(25) NOT NULL,
    featureCategoryId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES app.FeatureCategory(featureCategoryId) NOT NULL
);
CREATE TABLE app.AttributeCategory
(
    attributeCategoryId UNIQUEIDENTIFIER PRIMARY KEY,
    attributeCategoryName NVARCHAR(25) NOT NULL,
    attributeCategoryDescription NVARCHAR(150) NOT NULL
);
CREATE TABLE app.Attribute
(
    attributeId UNIQUEIDENTIFIER PRIMARY KEY,
    attributeName NVARCHAR(25) NOT NULL,
    attributeValue NVARCHAR(35) NOT NULL,
    attributeCategoryId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES app.AttributeCategory(attributeCategoryId) NOT NULL
);