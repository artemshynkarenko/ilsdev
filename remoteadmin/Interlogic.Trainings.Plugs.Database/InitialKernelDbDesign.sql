USE [ASH_Trainings_RemoteAdmin]
GO
/****** Object:  UserDefinedDataType [dbo].[path]    Script Date: 07/10/2008 10:27:41 ******/
CREATE TYPE [dbo].[path] FROM [nvarchar](1000) NOT NULL
GO
/****** Object:  Default [dbo].[TRUE]    Script Date: 07/10/2008 10:27:40 ******/
create default [dbo].[TRUE] as 1
GO
/****** Object:  Default [dbo].[FALSE]    Script Date: 07/10/2008 10:27:40 ******/
create default [dbo].[FALSE] as 0
GO
/****** Object:  UserDefinedDataType [dbo].[systemName]    Script Date: 07/10/2008 10:27:41 ******/
CREATE TYPE [dbo].[systemName] FROM [varchar](255) NOT NULL
GO
/****** Object:  UserDefinedDataType [dbo].[description]    Script Date: 07/10/2008 10:27:41 ******/
CREATE TYPE [dbo].[description] FROM [nvarchar](4000) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[name]    Script Date: 07/10/2008 10:27:41 ******/
CREATE TYPE [dbo].[name] FROM [nvarchar](1000) NOT NULL
GO
/****** Object:  Table [dbo].[Binding]    Script Date: 07/10/2008 10:27:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Binding](
	[BindingId] [int] IDENTITY(1,1) NOT NULL,
	[BindablePointId] [int] NOT NULL,
	[ImplementationId] [int] NOT NULL,
 CONSTRAINT [PK_Binding] PRIMARY KEY CLUSTERED 
(
	[BindingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlugLocation]    Script Date: 07/10/2008 10:27:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PlugLocation](
	[PlugLocationId] [int] NOT NULL,
	[PlugLocationName] [dbo].[systemName] NOT NULL,
	[PlugLocationDescription] [dbo].[description] NULL,
	[PlugLocationPath] [dbo].[path] NOT NULL,
	[PlugId] [int] NOT NULL,
 CONSTRAINT [PK_PlugLocation] PRIMARY KEY CLUSTERED 
(
	[PlugLocationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PlugFile]    Script Date: 07/10/2008 10:27:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlugFile](
	[PlugFileId] [int] IDENTITY(1,1) NOT NULL,
	[PlugFileName] [dbo].[name] NOT NULL,
	[RelativeIncomingPath] [dbo].[path] NOT NULL,
	[DestinationLocationId] [int] NOT NULL,
	[RelativeDestinationPath] [dbo].[path] NOT NULL,
	[PlugId] [int] NOT NULL,
 CONSTRAINT [PK_PlugFile] PRIMARY KEY CLUSTERED 
(
	[PlugFileId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedDataType [dbo].[active]    Script Date: 07/10/2008 10:27:41 ******/
CREATE TYPE [dbo].[active] FROM [bit] NOT NULL
GO
EXEC sys.sp_bindefault @defname=N'[dbo].[TRUE]', @objname=N'[dbo].[active]' , @futureonly='futureonly'
GO
/****** Object:  Table [dbo].[PlugIn]    Script Date: 07/10/2008 10:27:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PlugIn](
	[PlugId] [int] IDENTITY(1,1) NOT NULL,
	[PlugName] [dbo].[systemName] NOT NULL,
	[PlugFriendlyName] [dbo].[name] NOT NULL,
	[PlugDescription] [dbo].[description] NULL,
	[PlugVersion] [dbo].[systemName] NULL,
	[Active] [dbo].[active] NOT NULL,
 CONSTRAINT [PK_PlugIn] PRIMARY KEY CLUSTERED 
(
	[PlugId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_bindefault @defname=N'[dbo].[TRUE]', @objname=N'[dbo].[PlugIn].[Active]' , @futureonly='futureonly'
GO
/****** Object:  Table [dbo].[BindablePoint]    Script Date: 07/10/2008 10:27:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BindablePoint](
	[BindablePointId] [int] IDENTITY(1,1) NOT NULL,
	[PlugId] [int] NOT NULL,
	[BindablePointName] [dbo].[systemName] NOT NULL,
	[BindablePointFriendlyName] [dbo].[name] NOT NULL,
	[BindablePointDescription] [dbo].[description] NULL,
	[Active] [dbo].[active] NOT NULL,
	[InterfaceId] [int] NULL,
 CONSTRAINT [PK_BindablePoint] PRIMARY KEY CLUSTERED 
(
	[BindablePointId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_bindefault @defname=N'[dbo].[TRUE]', @objname=N'[dbo].[BindablePoint].[Active]' , @futureonly='futureonly'
GO
/****** Object:  Table [dbo].[ClassDefinition]    Script Date: 07/10/2008 10:27:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassDefinition](
	[ClassDefinitionId] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [dbo].[name] NOT NULL,
	[ClassDefinitionDescrition] [dbo].[description] NOT NULL,
	[Active] [dbo].[active] NOT NULL,
	[FileId] [int] NOT NULL,
	[PlugId] [int] NOT NULL,
 CONSTRAINT [PK_ClassDefinition] PRIMARY KEY CLUSTERED 
(
	[ClassDefinitionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_bindefault @defname=N'[dbo].[TRUE]', @objname=N'[dbo].[ClassDefinition].[Active]' , @futureonly='futureonly'
GO
/****** Object:  ForeignKey [FK_BindablePoint_ClassDefinition]    Script Date: 07/10/2008 10:27:41 ******/
ALTER TABLE [dbo].[BindablePoint]  WITH CHECK ADD  CONSTRAINT [FK_BindablePoint_ClassDefinition] FOREIGN KEY([InterfaceId])
REFERENCES [dbo].[ClassDefinition] ([ClassDefinitionId])
GO
ALTER TABLE [dbo].[BindablePoint] CHECK CONSTRAINT [FK_BindablePoint_ClassDefinition]
GO
/****** Object:  ForeignKey [FK_BindablePoint_PlugIn]    Script Date: 07/10/2008 10:27:41 ******/
ALTER TABLE [dbo].[BindablePoint]  WITH CHECK ADD  CONSTRAINT [FK_BindablePoint_PlugIn] FOREIGN KEY([PlugId])
REFERENCES [dbo].[PlugIn] ([PlugId])
GO
ALTER TABLE [dbo].[BindablePoint] CHECK CONSTRAINT [FK_BindablePoint_PlugIn]
GO
/****** Object:  ForeignKey [FK_Binding_BindablePoint]    Script Date: 07/10/2008 10:27:41 ******/
ALTER TABLE [dbo].[Binding]  WITH CHECK ADD  CONSTRAINT [FK_Binding_BindablePoint] FOREIGN KEY([BindablePointId])
REFERENCES [dbo].[BindablePoint] ([BindablePointId])
GO
ALTER TABLE [dbo].[Binding] CHECK CONSTRAINT [FK_Binding_BindablePoint]
GO
/****** Object:  ForeignKey [FK_Binding_ClassDefinition]    Script Date: 07/10/2008 10:27:41 ******/
ALTER TABLE [dbo].[Binding]  WITH CHECK ADD  CONSTRAINT [FK_Binding_ClassDefinition] FOREIGN KEY([ImplementationId])
REFERENCES [dbo].[ClassDefinition] ([ClassDefinitionId])
GO
ALTER TABLE [dbo].[Binding] CHECK CONSTRAINT [FK_Binding_ClassDefinition]
GO
/****** Object:  ForeignKey [FK_ClassDefinition_PlugFile]    Script Date: 07/10/2008 10:27:41 ******/
ALTER TABLE [dbo].[ClassDefinition]  WITH CHECK ADD  CONSTRAINT [FK_ClassDefinition_PlugFile] FOREIGN KEY([FileId])
REFERENCES [dbo].[PlugFile] ([PlugFileId])
GO
ALTER TABLE [dbo].[ClassDefinition] CHECK CONSTRAINT [FK_ClassDefinition_PlugFile]
GO
/****** Object:  ForeignKey [FK_ClassDefinition_PlugIn]    Script Date: 07/10/2008 10:27:41 ******/
ALTER TABLE [dbo].[ClassDefinition]  WITH CHECK ADD  CONSTRAINT [FK_ClassDefinition_PlugIn] FOREIGN KEY([PlugId])
REFERENCES [dbo].[PlugIn] ([PlugId])
GO
ALTER TABLE [dbo].[ClassDefinition] CHECK CONSTRAINT [FK_ClassDefinition_PlugIn]
GO
/****** Object:  ForeignKey [FK_PlugFile_PlugIn]    Script Date: 07/10/2008 10:27:41 ******/
ALTER TABLE [dbo].[PlugFile]  WITH CHECK ADD  CONSTRAINT [FK_PlugFile_PlugIn] FOREIGN KEY([PlugId])
REFERENCES [dbo].[PlugIn] ([PlugId])
GO
ALTER TABLE [dbo].[PlugFile] CHECK CONSTRAINT [FK_PlugFile_PlugIn]
GO
/****** Object:  ForeignKey [FK_PlugFile_PlugLocation]    Script Date: 07/10/2008 10:27:41 ******/
ALTER TABLE [dbo].[PlugFile]  WITH CHECK ADD  CONSTRAINT [FK_PlugFile_PlugLocation] FOREIGN KEY([DestinationLocationId])
REFERENCES [dbo].[PlugLocation] ([PlugLocationId])
GO
ALTER TABLE [dbo].[PlugFile] CHECK CONSTRAINT [FK_PlugFile_PlugLocation]
GO
/****** Object:  ForeignKey [FK_PlugLocation_PlugIn]    Script Date: 07/10/2008 10:27:41 ******/
ALTER TABLE [dbo].[PlugLocation]  WITH CHECK ADD  CONSTRAINT [FK_PlugLocation_PlugIn] FOREIGN KEY([PlugId])
REFERENCES [dbo].[PlugIn] ([PlugId])
GO
ALTER TABLE [dbo].[PlugLocation] CHECK CONSTRAINT [FK_PlugLocation_PlugIn]
GO
