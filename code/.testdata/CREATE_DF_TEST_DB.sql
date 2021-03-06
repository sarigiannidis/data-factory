﻿CREATE DATABASE [$(DB)] ON PRIMARY(NAME= DB_DATA, FILENAME = '$(MDF)', FILEGROWTH = 512MB) LOG ON (NAME= DB_LOG, FILENAME = '$(LDF)', FILEGROWTH = 512MB)
GO
USE [$(DB)]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SQLTYPE_TABLE_1](
	[RECID] [int] IDENTITY(1,1) NOT NULL,
	[VALUE001] [bigint] NULL,
	[VALUE002] [binary](50) NULL,
	[VALUE003] [bit] NULL,
	[VALUE004] [char](10) NULL,
	[VALUE005] [date] NULL,
	[VALUE006] [datetime] NULL,
	[VALUE007] [datetime2](7) NULL,
	[VALUE008] [datetimeoffset](7) NULL,
	[VALUE009] [decimal](18, 0) NULL,
	[VALUE010] [float] NULL,
	[VALUE011] [geography] NULL,
	[VALUE012] [geometry] NULL,
	[VALUE013] [hierarchyid] NULL,
	[VALUE014] [image] NULL,
	[VALUE015] [int] NULL,
	[VALUE016] [money] NULL,
	[VALUE017] [ntext] NULL,
	[VALUE018] [numeric](18, 0) NULL,
	[VALUE019] [nvarchar](50) NULL,
	[VALUE020] [nvarchar](max) NULL,
	[VALUE021] [real] NULL,
	[VALUE022] [smalldatetime] NULL,
	[VALUE023] [sql_variant] NULL,
	[VALUE024] [text] NULL,
	[VALUE025] [time](7) NULL,
	[VALUE026] [timestamp] NULL,
	[VALUE027] [tinyint] NULL,
	[VALUE028] [uniqueidentifier] NULL,
	[VALUE029] [varbinary](50) NULL,
	[VALUE030] [varbinary](max) NULL,
	[VALUE031] [varchar](50) NULL,
	[VALUE032] [varchar](max) NULL,
	[VALUE033] [xml] NULL,
 CONSTRAINT [PK_SQLTYPE_TABLE_1] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SQLTYPE_TABLE_2](
	[RECID] [int] IDENTITY(1,1) NOT NULL,
	[VALUE001] [bigint] NOT NULL,
	[VALUE002] [binary](50) NOT NULL,
	[VALUE003] [bit] NOT NULL,
	[VALUE004] [char](10) NOT NULL,
	[VALUE005] [date] NOT NULL,
	[VALUE006] [datetime] NOT NULL,
	[VALUE007] [datetime2](7) NOT NULL,
	[VALUE008] [datetimeoffset](7) NOT NULL,
	[VALUE009] [decimal](18, 0) NOT NULL,
	[VALUE010] [float] NOT NULL,
	[VALUE011] [geography] NOT NULL,
	[VALUE012] [geometry] NOT NULL,
	[VALUE013] [hierarchyid] NOT NULL,
	[VALUE014] [image] NOT NULL,
	[VALUE015] [int] NOT NULL,
	[VALUE016] [money] NOT NULL,
	[VALUE017] [ntext] NOT NULL,
	[VALUE018] [numeric](18, 0) NOT NULL,
	[VALUE019] [nvarchar](50) NOT NULL,
	[VALUE020] [nvarchar](max) NOT NULL,
	[VALUE021] [real] NOT NULL,
	[VALUE022] [smalldatetime] NOT NULL,
	[VALUE023] [sql_variant] NOT NULL,
	[VALUE024] [text] NOT NULL,
	[VALUE025] [time](7) NOT NULL,
	[VALUE026] [timestamp] NOT NULL,
	[VALUE027] [tinyint] NOT NULL,
	[VALUE028] [uniqueidentifier] NOT NULL,
	[VALUE029] [varbinary](50) NOT NULL,
	[VALUE030] [varbinary](max) NOT NULL,
	[VALUE031] [varchar](50) NOT NULL,
	[VALUE032] [varchar](max) NOT NULL,
	[VALUE033] [xml] NOT NULL,
 CONSTRAINT [PK_SQLTYPE_TABLE_2] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SQLTYPE_TABLE_3](
	[RECID] [int] IDENTITY(1,1) NOT NULL,
	[VALUE002] [binary](50) NOT NULL,
	[VALUE004] [char](10) NOT NULL,
	[VALUE007] [datetime2](7) NOT NULL,
	[VALUE008] [datetimeoffset](7) NOT NULL,
	[VALUE009] [decimal](18, 0) NOT NULL,
	[VALUE018] [numeric](18, 0) NOT NULL,
	[VALUE019] [nvarchar](50) NOT NULL,
	[VALUE025] [time](7) NOT NULL,
	[VALUE030] [varbinary](max) NOT NULL,
	[VALUE032] [varchar](max) NOT NULL,
 CONSTRAINT [PK_SQLTYPE_TABLE_3] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SQLTYPE_TABLE_4](
	[RECID] [int] IDENTITY(1,1) NOT NULL,
	[VAL]  AS (getdate()),
	[VAL2] [int] NULL,
 CONSTRAINT [PK_SQLTYPE_TABLE_4] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TABLE_0](
	[RECID] [bigint] IDENTITY(1,1) NOT NULL,
	[Value1] [uniqueidentifier] NULL,
	[FK_TABLE_00] [bigint] NULL,
	[FK_TABLE_01] [bigint] NULL,
	[FK_TABLE_02] [bigint] NULL,
 CONSTRAINT [PK_TABLE_0] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TABLE_00](
	[RECID] [bigint] IDENTITY(1,1) NOT NULL,
	[Value1] [money] NULL,
	[FK_TABLE_000] [bigint] NULL,
	[FK_TABLE_001] [bigint] NULL,
 CONSTRAINT [PK_TABLE_00] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TABLE_000](
	[RECID] [bigint] IDENTITY(1,1) NOT NULL,
	[Value1] [nvarchar](8) NULL,
 CONSTRAINT [PK_TABLE_000] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TABLE_001](
	[RECID] [bigint] IDENTITY(1,1) NOT NULL,
	[Value1] [datetime2](7) NULL,
 CONSTRAINT [PK_TABLE_001] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TABLE_01](
	[RECID] [bigint] IDENTITY(1,1) NOT NULL,
	[Value1] [datetimeoffset](7) NULL,
	[FK_TABLE_010] [bigint] NULL,
	[FK_TABLE_011] [bigint] NULL,
 CONSTRAINT [PK_TABLE_01] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TABLE_010](
	[RECID] [bigint] IDENTITY(1,1) NOT NULL,
	[Value1] [int] NULL,
 CONSTRAINT [PK_TABLE_010] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TABLE_011](
	[RECID] [bigint] IDENTITY(1,1) NOT NULL,
	[Value1] [bit] NULL,
 CONSTRAINT [PK_TABLE_011] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TABLE_02](
	[RECID] [bigint] IDENTITY(1,1) NOT NULL,
	[Value1] [bigint] NULL,
	[FK_TABLE_020] [bigint] NULL,
	[FK_TABLE_021] [bigint] NULL,
 CONSTRAINT [PK_TABLE_02] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TABLE_020](
	[RECID] [bigint] IDENTITY(1,1) NOT NULL,
	[Value1] [real] NULL,
 CONSTRAINT [PK_TABLE_020] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TABLE_021](
	[RECID] [bigint] IDENTITY(1,1) NOT NULL,
	[Value1] [decimal](18, 0) NULL,
 CONSTRAINT [PK_TABLE_021] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TABLE_SOURCE](
	[RECID] [int] IDENTITY(1,1) NOT NULL,
	[TRECID1] [int] NOT NULL,
	[TRECID2] [int] NOT NULL,
	[VALUE] [int] NOT NULL,
 CONSTRAINT [PK_TABLE_SOURCE] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TABLE_TARGET](
	[RECID1] [int] IDENTITY(1,1) NOT NULL,
	[RECID2] [int] NOT NULL,
	[VALUE] [int] NOT NULL,
 CONSTRAINT [PK_TABLE_TARGET] PRIMARY KEY CLUSTERED 
(
	[RECID1] ASC,
	[RECID2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TABLE_0]  WITH CHECK ADD  CONSTRAINT [FK_TABLE_0_TABLE_00] FOREIGN KEY([FK_TABLE_00])
REFERENCES [dbo].[TABLE_00] ([RECID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TABLE_0] CHECK CONSTRAINT [FK_TABLE_0_TABLE_00]
GO
ALTER TABLE [dbo].[TABLE_0]  WITH CHECK ADD  CONSTRAINT [FK_TABLE_0_TABLE_01] FOREIGN KEY([FK_TABLE_01])
REFERENCES [dbo].[TABLE_01] ([RECID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TABLE_0] CHECK CONSTRAINT [FK_TABLE_0_TABLE_01]
GO
ALTER TABLE [dbo].[TABLE_0]  WITH CHECK ADD  CONSTRAINT [FK_TABLE_0_TABLE_02] FOREIGN KEY([FK_TABLE_02])
REFERENCES [dbo].[TABLE_02] ([RECID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TABLE_0] CHECK CONSTRAINT [FK_TABLE_0_TABLE_02]
GO
ALTER TABLE [dbo].[TABLE_00]  WITH CHECK ADD  CONSTRAINT [FK_TABLE_00_TABLE_000] FOREIGN KEY([FK_TABLE_000])
REFERENCES [dbo].[TABLE_000] ([RECID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TABLE_00] CHECK CONSTRAINT [FK_TABLE_00_TABLE_000]
GO
ALTER TABLE [dbo].[TABLE_00]  WITH CHECK ADD  CONSTRAINT [FK_TABLE_00_TABLE_001] FOREIGN KEY([FK_TABLE_001])
REFERENCES [dbo].[TABLE_001] ([RECID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TABLE_00] CHECK CONSTRAINT [FK_TABLE_00_TABLE_001]
GO
ALTER TABLE [dbo].[TABLE_01]  WITH CHECK ADD  CONSTRAINT [FK_TABLE_01_TABLE_010] FOREIGN KEY([FK_TABLE_010])
REFERENCES [dbo].[TABLE_010] ([RECID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TABLE_01] CHECK CONSTRAINT [FK_TABLE_01_TABLE_010]
GO
ALTER TABLE [dbo].[TABLE_01]  WITH CHECK ADD  CONSTRAINT [FK_TABLE_01_TABLE_011] FOREIGN KEY([FK_TABLE_011])
REFERENCES [dbo].[TABLE_011] ([RECID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TABLE_01] CHECK CONSTRAINT [FK_TABLE_01_TABLE_011]
GO
ALTER TABLE [dbo].[TABLE_02]  WITH CHECK ADD  CONSTRAINT [FK_TABLE_02_TABLE_020] FOREIGN KEY([FK_TABLE_020])
REFERENCES [dbo].[TABLE_020] ([RECID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TABLE_02] CHECK CONSTRAINT [FK_TABLE_02_TABLE_020]
GO
ALTER TABLE [dbo].[TABLE_02]  WITH CHECK ADD  CONSTRAINT [FK_TABLE_02_TABLE_021] FOREIGN KEY([FK_TABLE_021])
REFERENCES [dbo].[TABLE_021] ([RECID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TABLE_02] CHECK CONSTRAINT [FK_TABLE_02_TABLE_021]
GO
ALTER TABLE [dbo].[TABLE_SOURCE]  WITH CHECK ADD  CONSTRAINT [FK_TABLE_SOURCE_TABLE_TARGET] FOREIGN KEY([TRECID1], [TRECID2])
REFERENCES [dbo].[TABLE_TARGET] ([RECID1], [RECID2])
GO
ALTER TABLE [dbo].[TABLE_SOURCE] CHECK CONSTRAINT [FK_TABLE_SOURCE_TABLE_TARGET]
GO
