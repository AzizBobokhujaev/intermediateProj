USE [Clients]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 30.09.2021 22:59:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[Gender] [nvarchar](50) NOT NULL,
	[BirthDate] [nvarchar](50) NOT NULL,
	[FormId] [int] NULL,
	[CreditId] [int] NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Credit]    Script Date: 30.09.2021 22:59:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Credit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ball] [int] NOT NULL,
	[MonthlyPayment] [int] NOT NULL,
	[Sum] [int] NOT NULL,
	[Persent] [int] NOT NULL,
	[ClientId] [int] NOT NULL,
 CONSTRAINT [PK_Credit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Forme]    Script Date: 30.09.2021 22:59:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forme](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Age] [int] NOT NULL,
	[Income] [decimal](18, 2) NOT NULL,
	[CreditSum] [decimal](18, 2) NOT NULL,
	[LoanAmountFromIncome] [int] NOT NULL,
	[CreditHistory] [int] NOT NULL,
	[OverdueCredit] [int] NOT NULL,
	[PurposeCredit] [nvarchar](255) NOT NULL,
	[TermCredit] [int] NOT NULL,
	[CreatedAt] [nvarchar](255) NOT NULL,
	[ClientId] [int] NOT NULL,
 CONSTRAINT [PK_Forme] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Forme]  WITH CHECK ADD  CONSTRAINT [FK_Forme_Clients] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([Id])
GO
ALTER TABLE [dbo].[Forme] CHECK CONSTRAINT [FK_Forme_Clients]
GO
