IF DB_ID(N'EspeciasSarapiquiDb') IS NULL
	CREATE DATABASE [EspeciasSarapiquiDb];
GO
USE [EspeciasSarapiquiDb];
GO

IF OBJECT_ID(N'dbo.AspNetUsers', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.AspNetRoles (Id nvarchar(450) NOT NULL CONSTRAINT PK_AspNetRoles PRIMARY KEY, Name nvarchar(256) NULL, NormalizedName nvarchar(256) NULL, ConcurrencyStamp nvarchar(max) NULL);
	CREATE UNIQUE INDEX RoleNameIndex ON dbo.AspNetRoles (NormalizedName) WHERE NormalizedName IS NOT NULL;
	CREATE TABLE dbo.AspNetUsers (Id nvarchar(450) NOT NULL CONSTRAINT PK_AspNetUsers PRIMARY KEY, UserName nvarchar(256) NULL, NormalizedUserName nvarchar(256) NULL, Email nvarchar(256) NULL, NormalizedEmail nvarchar(256) NULL, EmailConfirmed bit NOT NULL, PasswordHash nvarchar(max) NULL, SecurityStamp nvarchar(max) NULL, ConcurrencyStamp nvarchar(max) NULL, PhoneNumber nvarchar(max) NULL, PhoneNumberConfirmed bit NOT NULL, TwoFactorEnabled bit NOT NULL, LockoutEnd datetimeoffset NULL, LockoutEnabled bit NOT NULL, AccessFailedCount int NOT NULL, FullName nvarchar(120) NOT NULL DEFAULT N'', IsActive bit NOT NULL DEFAULT 1);
	CREATE INDEX EmailIndex ON dbo.AspNetUsers (NormalizedEmail);
	CREATE UNIQUE INDEX UserNameIndex ON dbo.AspNetUsers (NormalizedUserName) WHERE NormalizedUserName IS NOT NULL;
	CREATE TABLE dbo.AspNetRoleClaims (Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_AspNetRoleClaims PRIMARY KEY, RoleId nvarchar(450) NOT NULL, ClaimType nvarchar(max) NULL, ClaimValue nvarchar(max) NULL, CONSTRAINT FK_AspNetRoleClaims_AspNetRoles FOREIGN KEY (RoleId) REFERENCES dbo.AspNetRoles (Id) ON DELETE CASCADE);
	CREATE INDEX IX_AspNetRoleClaims_RoleId ON dbo.AspNetRoleClaims (RoleId);
	CREATE TABLE dbo.AspNetUserClaims (Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_AspNetUserClaims PRIMARY KEY, UserId nvarchar(450) NOT NULL, ClaimType nvarchar(max) NULL, ClaimValue nvarchar(max) NULL, CONSTRAINT FK_AspNetUserClaims_AspNetUsers FOREIGN KEY (UserId) REFERENCES dbo.AspNetUsers (Id) ON DELETE CASCADE);
	CREATE INDEX IX_AspNetUserClaims_UserId ON dbo.AspNetUserClaims (UserId);
	CREATE TABLE dbo.AspNetUserLogins (LoginProvider nvarchar(128) NOT NULL, ProviderKey nvarchar(128) NOT NULL, ProviderDisplayName nvarchar(max) NULL, UserId nvarchar(450) NOT NULL, CONSTRAINT PK_AspNetUserLogins PRIMARY KEY (LoginProvider, ProviderKey), CONSTRAINT FK_AspNetUserLogins_AspNetUsers FOREIGN KEY (UserId) REFERENCES dbo.AspNetUsers (Id) ON DELETE CASCADE);
	CREATE INDEX IX_AspNetUserLogins_UserId ON dbo.AspNetUserLogins (UserId);
	CREATE TABLE dbo.AspNetUserRoles (UserId nvarchar(450) NOT NULL, RoleId nvarchar(450) NOT NULL, CONSTRAINT PK_AspNetUserRoles PRIMARY KEY (UserId, RoleId), CONSTRAINT FK_AspNetUserRoles_AspNetUsers FOREIGN KEY (UserId) REFERENCES dbo.AspNetUsers (Id) ON DELETE CASCADE, CONSTRAINT FK_AspNetUserRoles_AspNetRoles FOREIGN KEY (RoleId) REFERENCES dbo.AspNetRoles (Id) ON DELETE CASCADE);
	CREATE INDEX IX_AspNetUserRoles_RoleId ON dbo.AspNetUserRoles (RoleId);
	CREATE TABLE dbo.AspNetUserTokens (UserId nvarchar(450) NOT NULL, LoginProvider nvarchar(128) NOT NULL, Name nvarchar(128) NOT NULL, Value nvarchar(max) NULL, CONSTRAINT PK_AspNetUserTokens PRIMARY KEY (UserId, LoginProvider, Name), CONSTRAINT FK_AspNetUserTokens_AspNetUsers FOREIGN KEY (UserId) REFERENCES dbo.AspNetUsers (Id) ON DELETE CASCADE);
END
GO

IF OBJECT_ID(N'dbo.Categories', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.Categories (Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Categories PRIMARY KEY, Name nvarchar(100) NOT NULL, Description nvarchar(300) NULL, IsActive bit NOT NULL DEFAULT 1);
	CREATE TABLE dbo.Products (Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Products PRIMARY KEY, Sku nvarchar(30) NOT NULL, Name nvarchar(150) NOT NULL, Description nvarchar(1000) NULL, Unit nvarchar(50) NOT NULL, PurchasePrice decimal(18,2) NOT NULL, SalePrice decimal(18,2) NOT NULL, MinimumStock decimal(18,3) NOT NULL, Stock decimal(18,3) NOT NULL, IsActive bit NOT NULL DEFAULT 1, CategoryId int NOT NULL, CONSTRAINT FK_Products_Categories FOREIGN KEY (CategoryId) REFERENCES dbo.Categories(Id));
	CREATE UNIQUE INDEX IX_Products_Sku ON dbo.Products(Sku);
	CREATE TABLE dbo.Customers (Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Customers PRIMARY KEY, FullName nvarchar(120) NOT NULL, Identification nvarchar(30) NULL, Email nvarchar(120) NULL, Phone nvarchar(30) NULL, Address nvarchar(300) NULL, IsActive bit NOT NULL DEFAULT 1);
	CREATE UNIQUE INDEX IX_Customers_Identification ON dbo.Customers(Identification) WHERE Identification IS NOT NULL;
	CREATE TABLE dbo.Sales (Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Sales PRIMARY KEY, InvoiceNumber nvarchar(30) NOT NULL, SaleDate datetime2 NOT NULL DEFAULT SYSUTCDATETIME(), Status nvarchar(20) NOT NULL, PaymentMethod nvarchar(30) NOT NULL, Subtotal decimal(18,2) NOT NULL, Tax decimal(18,2) NOT NULL, Total decimal(18,2) NOT NULL, CustomerId int NULL, UserId nvarchar(450) NOT NULL, CONSTRAINT FK_Sales_Customers FOREIGN KEY(CustomerId) REFERENCES dbo.Customers(Id) ON DELETE SET NULL, CONSTRAINT FK_Sales_Users FOREIGN KEY(UserId) REFERENCES dbo.AspNetUsers(Id));
	CREATE UNIQUE INDEX IX_Sales_InvoiceNumber ON dbo.Sales(InvoiceNumber);
	CREATE INDEX IX_Sales_UserId ON dbo.Sales(UserId);
	CREATE TABLE dbo.SaleDetails (Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SaleDetails PRIMARY KEY, SaleId int NOT NULL, ProductId int NOT NULL, Quantity decimal(18,3) NOT NULL, UnitPrice decimal(18,2) NOT NULL, Discount decimal(18,2) NOT NULL, LineTotal decimal(18,2) NOT NULL, CONSTRAINT FK_SaleDetails_Sales FOREIGN KEY(SaleId) REFERENCES dbo.Sales(Id) ON DELETE CASCADE, CONSTRAINT FK_SaleDetails_Products FOREIGN KEY(ProductId) REFERENCES dbo.Products(Id));
	CREATE INDEX IX_SaleDetails_SaleId ON dbo.SaleDetails(SaleId);
	CREATE INDEX IX_SaleDetails_ProductId ON dbo.SaleDetails(ProductId);
	CREATE TABLE dbo.InventoryMovements (Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_InventoryMovements PRIMARY KEY, ProductId int NOT NULL, MovementDate datetime2 NOT NULL DEFAULT SYSUTCDATETIME(), MovementType nvarchar(20) NOT NULL, Quantity decimal(18,3) NOT NULL, Notes nvarchar(300) NULL, UserId nvarchar(450) NOT NULL, CONSTRAINT FK_InventoryMovements_Products FOREIGN KEY(ProductId) REFERENCES dbo.Products(Id), CONSTRAINT FK_InventoryMovements_Users FOREIGN KEY(UserId) REFERENCES dbo.AspNetUsers(Id));
	CREATE INDEX IX_InventoryMovements_ProductId ON dbo.InventoryMovements(ProductId);
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Categories WHERE Name = N'Especias') INSERT dbo.Categories(Name, Description) VALUES (N'Especias', N'Especias secas y molidas');
IF NOT EXISTS (SELECT 1 FROM dbo.Categories WHERE Name = N'Hierbas aromáticas') INSERT dbo.Categories(Name, Description) VALUES (N'Hierbas aromáticas', N'Hierbas para cocina e infusiones');
IF NOT EXISTS (SELECT 1 FROM dbo.Categories WHERE Name = N'Condimentos') INSERT dbo.Categories(Name, Description) VALUES (N'Condimentos', N'Mezclas y condimentos preparados');

IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Sku = N'CAN-001') INSERT dbo.Products(Sku, Name, Description, Unit, PurchasePrice, SalePrice, MinimumStock, Stock, CategoryId) SELECT N'CAN-001', N'Canela molida 100 g', N'Canela de alta calidad', N'Paquete', 900, 1500, 10, 25, Id FROM dbo.Categories WHERE Name = N'Especias';
IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Sku = N'CUR-001') INSERT dbo.Products(Sku, Name, Description, Unit, PurchasePrice, SalePrice, MinimumStock, Stock, CategoryId) SELECT N'CUR-001', N'Cúrcuma 100 g', N'Cúrcuma molida', N'Paquete', 850, 1400, 10, 30, Id FROM dbo.Categories WHERE Name = N'Especias';
IF NOT EXISTS (SELECT 1 FROM dbo.Products WHERE Sku = N'ORE-001') INSERT dbo.Products(Sku, Name, Description, Unit, PurchasePrice, SalePrice, MinimumStock, Stock, CategoryId) SELECT N'ORE-001', N'Orégano seco 50 g', N'Orégano deshidratado', N'Paquete', 700, 1200, 8, 20, Id FROM dbo.Categories WHERE Name = N'Hierbas aromáticas';

IF NOT EXISTS (SELECT 1 FROM dbo.Customers WHERE Identification = N'000000000') INSERT dbo.Customers(FullName, Identification, Email, Phone, Address) VALUES (N'Cliente contado', N'000000000', N'cliente@local.test', N'8888-0000', N'Cliente general');

IF NOT EXISTS (SELECT 1 FROM dbo.AspNetUsers WHERE NormalizedEmail = N'ADMIN@ESPECIAS.LOCAL')
BEGIN
	INSERT dbo.AspNetUsers
	(Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, FullName, IsActive)
	VALUES
	(N'FAAE7E0D-01BE-4948-8F49-F06100D03E68', N'admin@especias.local', N'ADMIN@ESPECIAS.LOCAL', N'admin@especias.local', N'ADMIN@ESPECIAS.LOCAL', 1,
	 N'AQAAAAIAAYagAAAAEJwIyT6mY9V6pSB3jy1YXvVth37kETyhBtbwew5Oewiwipax8IaZDy7I7YEvgHsu+w==',
	 CONVERT(nvarchar(36), NEWID()), CONVERT(nvarchar(36), NEWID()), N'Administrador', 1);
END
GO
PRINT N'Base EspeciasSarapiquiDb creada y datos iniciales cargados. Usuario inicial: admin@especias.local / Admin123!';
