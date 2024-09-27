USE [master]
GO

-- Verificar si la base de datos 'Investment' existe
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Investment')
BEGIN
    CREATE DATABASE Investment
    PRINT 'Base de datos Investment creada.'
END
ELSE
BEGIN
    PRINT 'La base de datos Investment ya existe.'
END
GO

-- Verificar si el login 'srv_user_investment' existe
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'srv_user_investment')
BEGIN
    CREATE LOGIN srv_user_investment WITH PASSWORD=N'Pass1234', DEFAULT_DATABASE=[Investment], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
    PRINT 'Login srv_user_investment creado.'
END
ELSE
BEGIN
    PRINT 'El login srv_user_investment ya existe.'
END
GO

USE [Investment]
GO

-- Verificar si el usuario 'srv_user_investment' existe
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'srv_user_investment')
BEGIN
    CREATE USER srv_user_investment FOR LOGIN srv_user_investment
    ALTER USER srv_user_investment WITH DEFAULT_SCHEMA=[dbo]
    ALTER ROLE [db_owner] ADD MEMBER srv_user_investment
    PRINT 'Usuario srv_user_investment creado y asignado al rol db_owner.'
END
ELSE
BEGIN
    PRINT 'El usuario srv_user_investment ya existe.'
END
GO


-- Verificar si la tabla 'StockOperations' existe
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'StockOperations' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    SET ANSI_NULLS ON
    SET QUOTED_IDENTIFIER ON

	CREATE TABLE [dbo].[StockOperations](
		[Id] [int] NOT NULL,
		[Quantity] [int] NOT NULL,
		[InstrumentId] [int] NOT NULL,
		[TradeAgentId] [int] NOT NULL,
		[TradeDate] [datetime2](7) NOT NULL,
		[SettlementDate] [datetime2](7) NOT NULL,
	 CONSTRAINT [PK_StockOperations_Id] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
	
    PRINT 'Tabla StockOperations creada.'
END
ELSE
BEGIN
    PRINT 'La tabla StockOperations ya existe.'
END
GO


-- Verificar si la tabla 'MonetaryOperations' existe
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MonetaryOperations' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    SET ANSI_NULLS ON
    SET QUOTED_IDENTIFIER ON

	CREATE TABLE [dbo].[MonetaryOperations](
		[Id] [int] NOT NULL,
		[Comment] [nvarchar](max) NULL,
	 CONSTRAINT [PK_MonetaryOperations_Id] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
	
    PRINT 'Tabla MonetaryOperations creada.'
END
ELSE
BEGIN
    PRINT 'La tabla MonetaryOperations ya existe.'
END
GO


-- Verificar si la tabla 'Operations' existe
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Operations' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    SET ANSI_NULLS ON
    SET QUOTED_IDENTIFIER ON

	CREATE TABLE [dbo].[Operations](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[TypeId] [int] NULL,
		[UserId] [int] NOT NULL,
		[PortfolioId] [int] NOT NULL,
		[Amount] [float] NULL,
		[Currency] [int] NULL,
		[Type_Name] [nvarchar](max) NULL,
		[CreatedAt] [datetime2](7) NOT NULL,
	 CONSTRAINT [PK_Operation_Id] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
	
	ALTER TABLE [dbo].[MonetaryOperations]  WITH CHECK ADD  CONSTRAINT [FK_MonetaryOperations_Operations_Id] FOREIGN KEY([Id]) 
	REFERENCES [dbo].[Operations] ([Id]) ON DELETE CASCADE

	ALTER TABLE [dbo].[StockOperations]  WITH CHECK ADD  CONSTRAINT [FK_StockOperations_Operations_Id] FOREIGN KEY([Id])
	REFERENCES [dbo].[Operations] ([Id]) ON DELETE CASCADE
	
    PRINT 'Tabla Operations creada.'
END
ELSE
BEGIN
    PRINT 'La tabla Operations ya existe.'
END
GO


-- Verificar si la tabla 'Portfolios' existe
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Portfolios' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    SET ANSI_NULLS ON
    SET QUOTED_IDENTIFIER ON

	CREATE TABLE [dbo].[Portfolios](
		[Id] [int] NOT NULL,
		[Amount] [float] NULL,
		[Currency] [int] NULL,
	 CONSTRAINT [PK_Portfolio_Id] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
	
    PRINT 'Tabla Portfolios creada.'
END
ELSE
BEGIN
    PRINT 'La tabla Portfolios ya existe.'
END
GO


-- Verificar si la tabla 'PortfolioInstruments' existe
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PortfolioInstruments' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    SET ANSI_NULLS ON
    SET QUOTED_IDENTIFIER ON

	CREATE TABLE [dbo].[PortfolioInstruments](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[InstrumentId] [int] NOT NULL,
		[Quantity] [int] NULL,
		[Amount] [float] NULL,
		[Currency] [int] NULL,
		[PortfolioId] [int] NULL,
	 CONSTRAINT [PK_PortfolioInstrument_Id] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
	
	ALTER TABLE [dbo].[PortfolioInstruments]  WITH CHECK ADD CONSTRAINT [FK_PortfolioInstruments_Portfolios_PortfolioId] FOREIGN KEY([PortfolioId])
	REFERENCES [dbo].[Portfolios] ([Id])
	
    PRINT 'Tabla PortfolioInstruments creada.'
END
ELSE
BEGIN
    PRINT 'La tabla PortfolioInstruments ya existe.'
END
GO


-- Verificar si la tabla 'PortfolioInstrumentHistory' existe
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PortfolioInstrumentHistory' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    SET ANSI_NULLS ON
    SET QUOTED_IDENTIFIER ON

	CREATE TABLE [dbo].[PortfolioInstrumentHistory](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[InstrumentId] [int] NOT NULL,
		[Quantity] [int] NOT NULL,
		[Amount] [float] NOT NULL,
		[Currency] [int] NOT NULL,
		[CreatedAt] [datetime2](7) NOT NULL,
		[PortfolioInstrumentId] [int] NULL,
	 CONSTRAINT [PK_PortfolioInstrumentHistory_Id] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
	
	ALTER TABLE [dbo].[PortfolioInstrumentHistory]  WITH CHECK ADD CONSTRAINT [FK_PortfolioInstrumentHistory_PortfolioInstruments_PortfolioInstrumentId] FOREIGN KEY([PortfolioInstrumentId])
	REFERENCES [dbo].[PortfolioInstruments] ([Id])
	
    PRINT 'Tabla PortfolioInstrumentHistory creada.'
END
ELSE
BEGIN
    PRINT 'La tabla PortfolioInstrumentHistory ya existe.'
END
GO
