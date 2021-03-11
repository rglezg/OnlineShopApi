CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;

CREATE TABLE `AspNetRoles` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(256) CHARACTER SET utf8mb4 NULL,
    `NormalizedName` varchar(256) CHARACTER SET utf8mb4 NULL,
    `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_AspNetRoles` PRIMARY KEY (`Id`)
);

CREATE TABLE `AspNetUsers` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `FirstName` longtext CHARACTER SET utf8mb4 NULL,
    `LastName` longtext CHARACTER SET utf8mb4 NULL,
    `UserName` varchar(256) CHARACTER SET utf8mb4 NULL,
    `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 NULL,
    `Email` varchar(256) CHARACTER SET utf8mb4 NULL,
    `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 NULL,
    `EmailConfirmed` tinyint(1) NOT NULL,
    `PasswordHash` longtext CHARACTER SET utf8mb4 NULL,
    `SecurityStamp` longtext CHARACTER SET utf8mb4 NULL,
    `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL,
    `PhoneNumber` longtext CHARACTER SET utf8mb4 NULL,
    `PhoneNumberConfirmed` tinyint(1) NOT NULL,
    `TwoFactorEnabled` tinyint(1) NOT NULL,
    `LockoutEnd` datetime(6) NULL,
    `LockoutEnabled` tinyint(1) NOT NULL,
    `AccessFailedCount` int NOT NULL,
    CONSTRAINT `PK_AspNetUsers` PRIMARY KEY (`Id`)
);

CREATE TABLE `AspNetRoleClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `RoleId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ClaimType` longtext CHARACTER SET utf8mb4 NULL,
    `ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_AspNetRoleClaims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ClaimType` longtext CHARACTER SET utf8mb4 NULL,
    `ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_AspNetUserClaims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserLogins` (
    `LoginProvider` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ProviderKey` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ProviderDisplayName` longtext CHARACTER SET utf8mb4 NULL,
    `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_AspNetUserLogins` PRIMARY KEY (`LoginProvider`, `ProviderKey`),
    CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserRoles` (
    `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `RoleId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_AspNetUserRoles` PRIMARY KEY (`UserId`, `RoleId`),
    CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserTokens` (
    `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `LoginProvider` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Value` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_AspNetUserTokens` PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
    CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Products` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Description` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Count` int NOT NULL,
    `Slug` longtext CHARACTER SET utf8mb4 NULL,
    `Price` float NOT NULL,
    `OwnerId` varchar(255) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Products` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Products_AspNetUsers_OwnerId` FOREIGN KEY (`OwnerId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Orders` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Date` datetime(6) NOT NULL,
    `State` int NOT NULL,
    `Count` int NOT NULL,
    `UserId` varchar(255) CHARACTER SET utf8mb4 NULL,
    `ProductId` int NOT NULL,
    CONSTRAINT `PK_Orders` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Orders_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Orders_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `Products` (`Id`) ON DELETE CASCADE
);

INSERT INTO `AspNetRoles` (`Id`, `ConcurrencyStamp`, `Name`, `NormalizedName`)
VALUES ('93957408-18ec-41c3-a888-1e549cc95a97', 'c6444f65-da46-4927-b97f-06b268a20b3c', 'Admin', 'Admin');
INSERT INTO `AspNetRoles` (`Id`, `ConcurrencyStamp`, `Name`, `NormalizedName`)
VALUES ('5cc4f2f4-c7e1-4176-9c27-4fa7e1aa371a', '30870c9a-d493-49ab-9306-bdfdd8daaadc', 'Seller', 'Seller');
INSERT INTO `AspNetRoles` (`Id`, `ConcurrencyStamp`, `Name`, `NormalizedName`)
VALUES ('1dab83bc-28ca-40de-a798-af5fe295c689', '4d9f94dd-bfac-49b3-86f9-0fd070cb366c', 'Client', 'Client');

INSERT INTO `AspNetUsers` (`Id`, `AccessFailedCount`, `ConcurrencyStamp`, `Email`, `EmailConfirmed`, `FirstName`, `LastName`, `LockoutEnabled`, `LockoutEnd`, `NormalizedEmail`, `NormalizedUserName`, `PasswordHash`, `PhoneNumber`, `PhoneNumberConfirmed`, `SecurityStamp`, `TwoFactorEnabled`, `UserName`)
VALUES ('621fbe35-5437-45f1-952e-a74565ccf51a', 0, '24cac7ea-570f-44eb-91cd-013bf95a3c60', 'admin@mail.com', TRUE, NULL, NULL, FALSE, NULL, 'ADMIN@MAIL.COM', 'ADMIN', 'AQAAAAEAACcQAAAAECZowIW250N7fTo4SON3mdLOVnstwf0Tkof2+m1bjG5yYD4qXk4VtEcHbuiwnuL6OA==', NULL, FALSE, '', FALSE, 'admin');
INSERT INTO `AspNetUsers` (`Id`, `AccessFailedCount`, `ConcurrencyStamp`, `Email`, `EmailConfirmed`, `FirstName`, `LastName`, `LockoutEnabled`, `LockoutEnd`, `NormalizedEmail`, `NormalizedUserName`, `PasswordHash`, `PhoneNumber`, `PhoneNumberConfirmed`, `SecurityStamp`, `TwoFactorEnabled`, `UserName`)
VALUES ('18fe1a9e-b94d-431a-a317-9ab0a9975494', 0, 'bf288d16-ca4c-4b22-aec6-65d31f6def13', 'seller@mail.com', TRUE, NULL, NULL, FALSE, NULL, 'SELLER@MAIL.COM', 'SELLER', 'AQAAAAEAACcQAAAAEHSBqclpVrIJW1/bhUKounx+Ybj3Av7B3LlKAWJvWbz3o0itMCTY51MlbX6ctikbEw==', NULL, FALSE, '', FALSE, 'seller');
INSERT INTO `AspNetUsers` (`Id`, `AccessFailedCount`, `ConcurrencyStamp`, `Email`, `EmailConfirmed`, `FirstName`, `LastName`, `LockoutEnabled`, `LockoutEnd`, `NormalizedEmail`, `NormalizedUserName`, `PasswordHash`, `PhoneNumber`, `PhoneNumberConfirmed`, `SecurityStamp`, `TwoFactorEnabled`, `UserName`)
VALUES ('bd9cb947-4bc2-4393-ba7d-d57333aa7338', 0, '1192c9b9-0888-46c3-a354-cc66b307f64b', 'client@mail.com', TRUE, NULL, NULL, FALSE, NULL, 'CLIENT@MAIL.COM', 'CLIENT', 'AQAAAAEAACcQAAAAEBQfxPnKn4xcSctUzB9Ey80IosC7VQFG9IrV8i59Td87JRHBkDyx4WjBYUMmRY79vQ==', NULL, FALSE, '', FALSE, 'client');

INSERT INTO `AspNetUserRoles` (`RoleId`, `UserId`)
VALUES ('93957408-18ec-41c3-a888-1e549cc95a97', '621fbe35-5437-45f1-952e-a74565ccf51a');

INSERT INTO `AspNetUserRoles` (`RoleId`, `UserId`)
VALUES ('5cc4f2f4-c7e1-4176-9c27-4fa7e1aa371a', '18fe1a9e-b94d-431a-a317-9ab0a9975494');

INSERT INTO `AspNetUserRoles` (`RoleId`, `UserId`)
VALUES ('1dab83bc-28ca-40de-a798-af5fe295c689', 'bd9cb947-4bc2-4393-ba7d-d57333aa7338');

CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON `AspNetRoleClaims` (`RoleId`);

CREATE UNIQUE INDEX `RoleNameIndex` ON `AspNetRoles` (`NormalizedName`);

CREATE INDEX `IX_AspNetUserClaims_UserId` ON `AspNetUserClaims` (`UserId`);

CREATE INDEX `IX_AspNetUserLogins_UserId` ON `AspNetUserLogins` (`UserId`);

CREATE INDEX `IX_AspNetUserRoles_RoleId` ON `AspNetUserRoles` (`RoleId`);

CREATE INDEX `EmailIndex` ON `AspNetUsers` (`NormalizedEmail`);

CREATE UNIQUE INDEX `UserNameIndex` ON `AspNetUsers` (`NormalizedUserName`);

CREATE INDEX `IX_Orders_ProductId` ON `Orders` (`ProductId`);

CREATE INDEX `IX_Orders_UserId` ON `Orders` (`UserId`);

CREATE INDEX `IX_Products_OwnerId` ON `Products` (`OwnerId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210311043853_Initial', '5.0.4');

COMMIT;

