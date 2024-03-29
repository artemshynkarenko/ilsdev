USE [ASH_Trainings_RemoteAdmin]
/****** Object:  UserDefinedDataType [dbo].[systemName]    Script Date: 12/05/2008 14:50:49 ******/
CREATE TYPE [dbo].[systemName] FROM [varchar](255) NOT NULL
GO
/****** Object:  UserDefinedDataType [dbo].[description]    Script Date: 12/05/2008 14:50:49 ******/
CREATE TYPE [dbo].[description] FROM [nvarchar](4000) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[name]    Script Date: 12/05/2008 14:50:49 ******/
CREATE TYPE [dbo].[name] FROM [nvarchar](1000) NOT NULL
GO
/****** Object:  UserDefinedDataType [dbo].[path]    Script Date: 12/05/2008 14:50:49 ******/
CREATE TYPE [dbo].[path] FROM [nvarchar](1000) NOT NULL
GO
/****** Object:  Default [dbo].[TRUE]    Script Date: 12/05/2008 14:50:18 ******/
create default [dbo].[TRUE] as 1
GO
/****** Object:  Default [dbo].[FALSE]    Script Date: 12/05/2008 14:50:18 ******/
create default [dbo].[FALSE] as 0
GO
/****** Object:  Table [dbo].[Binding]    Script Date: 12/05/2008 14:50:26 ******/
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
/****** Object:  Table [dbo].[WebComponentBinding]    Script Date: 12/05/2008 14:50:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebComponentBinding](
	[BindingId] [int] NOT NULL,
	[Left] [int] NOT NULL,
	[Top] [int] NOT NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassInheritance]    Script Date: 12/05/2008 14:50:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassInheritance](
	[ClassDefinitionId] [int] NOT NULL,
	[ParentClassDefinitionId] [int] NOT NULL,
 CONSTRAINT [PK_ClassInheritance] PRIMARY KEY CLUSTERED 
(
	[ClassDefinitionId] ASC,
	[ParentClassDefinitionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BindablePointDefinition]    Script Date: 12/05/2008 14:50:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BindablePointDefinition](
	[BindablePointDefinitionId] [int] IDENTITY(1,1) NOT NULL,
	[ClassDefinitionId] [int] NOT NULL,
	[BindablePointName] [dbo].[systemName] NOT NULL,
	[BindablePointFriendlyName] [dbo].[name] NOT NULL,
	[BindablePointDescription] [dbo].[description] NULL,
	[InterfaceId] [int] NULL,
 CONSTRAINT [PK_BindablePointDefinition] PRIMARY KEY CLUSTERED 
(
	[BindablePointDefinitionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PlugLocation]    Script Date: 12/05/2008 14:50:43 ******/
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
/****** Object:  Table [dbo].[Instance]    Script Date: 12/05/2008 14:50:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Instance](
	[InstanceId] [int] IDENTITY(1,1) NOT NULL,
	[ClassDefinitionId] [int] NOT NULL,
	[InstanceName] [dbo].[systemName] NOT NULL,
 CONSTRAINT [PK_Intance] PRIMARY KEY CLUSTERED 
(
	[InstanceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StaticTreeviewNode]    Script Date: 12/05/2008 14:50:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StaticTreeviewNode](
	[InstanceId] [int] NOT NULL,
	[ParentInstanceId] [int] NULL,
	[NodeFriendlyName] [dbo].[name] NOT NULL,
	[NodeDescription] [dbo].[description] NULL,
 CONSTRAINT [PK_StaticTreeviewNode] PRIMARY KEY CLUSTERED 
(
	[InstanceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlugFile]    Script Date: 12/05/2008 14:50:37 ******/
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
/****** Object:  UserDefinedDataType [dbo].[active]    Script Date: 12/05/2008 14:50:49 ******/
CREATE TYPE [dbo].[active] FROM [bit] NOT NULL
GO
EXEC sys.sp_bindefault @defname=N'[dbo].[TRUE]', @objname=N'[dbo].[active]' , @futureonly='futureonly'
GO
/****** Object:  Table [dbo].[PlugIn]    Script Date: 12/05/2008 14:50:41 ******/
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
/****** Object:  Table [dbo].[ClassDefinition]    Script Date: 12/05/2008 14:50:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassDefinition](
	[ClassDefinitionId] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [dbo].[name] NOT NULL,
	[ClassDefinitionDescription] [dbo].[description] NOT NULL,
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
/****** Object:  Table [dbo].[BindablePoint]    Script Date: 12/05/2008 14:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BindablePoint](
	[BindablePointId] [int] NOT NULL,
	[BindablePointDefinitonId] [int] NOT NULL,
	[InstanceId] [int] NOT NULL,
	[Active] [dbo].[active] NOT NULL,
 CONSTRAINT [PK_BindablePoint] PRIMARY KEY CLUSTERED 
(
	[BindablePointId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_bindefault @defname=N'[dbo].[TRUE]', @objname=N'[dbo].[BindablePoint].[Active]' , @futureonly='futureonly'
GO
/****** Object:  ForeignKey [FK_BindablePoint_BindablePointDefinition1]    Script Date: 12/05/2008 14:50:21 ******/
ALTER TABLE [dbo].[BindablePoint]  WITH CHECK ADD  CONSTRAINT [FK_BindablePoint_BindablePointDefinition1] FOREIGN KEY([BindablePointDefinitonId])
REFERENCES [dbo].[BindablePointDefinition] ([BindablePointDefinitionId])
GO
ALTER TABLE [dbo].[BindablePoint] CHECK CONSTRAINT [FK_BindablePoint_BindablePointDefinition1]
GO
/****** Object:  ForeignKey [FK_BindablePoint_Intance]    Script Date: 12/05/2008 14:50:21 ******/
ALTER TABLE [dbo].[BindablePoint]  WITH CHECK ADD  CONSTRAINT [FK_BindablePoint_Intance] FOREIGN KEY([InstanceId])
REFERENCES [dbo].[Instance] ([InstanceId])
GO
ALTER TABLE [dbo].[BindablePoint] CHECK CONSTRAINT [FK_BindablePoint_Intance]
GO
/****** Object:  ForeignKey [FK_BindablePointDefinition_ClassDefinition]    Script Date: 12/05/2008 14:50:24 ******/
ALTER TABLE [dbo].[BindablePointDefinition]  WITH CHECK ADD  CONSTRAINT [FK_BindablePointDefinition_ClassDefinition] FOREIGN KEY([ClassDefinitionId])
REFERENCES [dbo].[ClassDefinition] ([ClassDefinitionId])
GO
ALTER TABLE [dbo].[BindablePointDefinition] CHECK CONSTRAINT [FK_BindablePointDefinition_ClassDefinition]
GO
/****** Object:  ForeignKey [FK_BindablePointDefinition_ClassDefinition1]    Script Date: 12/05/2008 14:50:24 ******/
ALTER TABLE [dbo].[BindablePointDefinition]  WITH CHECK ADD  CONSTRAINT [FK_BindablePointDefinition_ClassDefinition1] FOREIGN KEY([InterfaceId])
REFERENCES [dbo].[ClassDefinition] ([ClassDefinitionId])
GO
ALTER TABLE [dbo].[BindablePointDefinition] CHECK CONSTRAINT [FK_BindablePointDefinition_ClassDefinition1]
GO
/****** Object:  ForeignKey [FK_Binding_BindablePoint]    Script Date: 12/05/2008 14:50:26 ******/
ALTER TABLE [dbo].[Binding]  WITH CHECK ADD  CONSTRAINT [FK_Binding_BindablePoint] FOREIGN KEY([BindablePointId])
REFERENCES [dbo].[BindablePoint] ([BindablePointId])
GO
ALTER TABLE [dbo].[Binding] CHECK CONSTRAINT [FK_Binding_BindablePoint]
GO
/****** Object:  ForeignKey [FK_Binding_Intance]    Script Date: 12/05/2008 14:50:27 ******/
ALTER TABLE [dbo].[Binding]  WITH CHECK ADD  CONSTRAINT [FK_Binding_Intance] FOREIGN KEY([ImplementationId])
REFERENCES [dbo].[Instance] ([InstanceId])
GO
ALTER TABLE [dbo].[Binding] CHECK CONSTRAINT [FK_Binding_Intance]
GO
/****** Object:  ForeignKey [FK_ClassDefinition_PlugFile]    Script Date: 12/05/2008 14:50:30 ******/
ALTER TABLE [dbo].[ClassDefinition]  WITH CHECK ADD  CONSTRAINT [FK_ClassDefinition_PlugFile] FOREIGN KEY([FileId])
REFERENCES [dbo].[PlugFile] ([PlugFileId])
GO
ALTER TABLE [dbo].[ClassDefinition] CHECK CONSTRAINT [FK_ClassDefinition_PlugFile]
GO
/****** Object:  ForeignKey [FK_ClassDefinition_PlugIn]    Script Date: 12/05/2008 14:50:30 ******/
ALTER TABLE [dbo].[ClassDefinition]  WITH CHECK ADD  CONSTRAINT [FK_ClassDefinition_PlugIn] FOREIGN KEY([PlugId])
REFERENCES [dbo].[PlugIn] ([PlugId])
GO
ALTER TABLE [dbo].[ClassDefinition] CHECK CONSTRAINT [FK_ClassDefinition_PlugIn]
GO
/****** Object:  ForeignKey [FK_ClassInheritance_ClassDefinition]    Script Date: 12/05/2008 14:50:32 ******/
ALTER TABLE [dbo].[ClassInheritance]  WITH CHECK ADD  CONSTRAINT [FK_ClassInheritance_ClassDefinition] FOREIGN KEY([ClassDefinitionId])
REFERENCES [dbo].[ClassDefinition] ([ClassDefinitionId])
GO
ALTER TABLE [dbo].[ClassInheritance] CHECK CONSTRAINT [FK_ClassInheritance_ClassDefinition]
GO
/****** Object:  ForeignKey [FK_ClassInheritance_ClassDefinition1]    Script Date: 12/05/2008 14:50:32 ******/
ALTER TABLE [dbo].[ClassInheritance]  WITH CHECK ADD  CONSTRAINT [FK_ClassInheritance_ClassDefinition1] FOREIGN KEY([ParentClassDefinitionId])
REFERENCES [dbo].[ClassDefinition] ([ClassDefinitionId])
GO
ALTER TABLE [dbo].[ClassInheritance] CHECK CONSTRAINT [FK_ClassInheritance_ClassDefinition1]
GO
/****** Object:  ForeignKey [FK_Intance_ClassDefinition]    Script Date: 12/05/2008 14:50:34 ******/
ALTER TABLE [dbo].[Instance]  WITH CHECK ADD  CONSTRAINT [FK_Intance_ClassDefinition] FOREIGN KEY([ClassDefinitionId])
REFERENCES [dbo].[ClassDefinition] ([ClassDefinitionId])
GO
ALTER TABLE [dbo].[Instance] CHECK CONSTRAINT [FK_Intance_ClassDefinition]
GO
/****** Object:  ForeignKey [FK_PlugFile_PlugIn]    Script Date: 12/05/2008 14:50:37 ******/
ALTER TABLE [dbo].[PlugFile]  WITH CHECK ADD  CONSTRAINT [FK_PlugFile_PlugIn] FOREIGN KEY([PlugId])
REFERENCES [dbo].[PlugIn] ([PlugId])
GO
ALTER TABLE [dbo].[PlugFile] CHECK CONSTRAINT [FK_PlugFile_PlugIn]
GO
/****** Object:  ForeignKey [FK_PlugFile_PlugLocation]    Script Date: 12/05/2008 14:50:38 ******/
ALTER TABLE [dbo].[PlugFile]  WITH CHECK ADD  CONSTRAINT [FK_PlugFile_PlugLocation] FOREIGN KEY([DestinationLocationId])
REFERENCES [dbo].[PlugLocation] ([PlugLocationId])
GO
ALTER TABLE [dbo].[PlugFile] CHECK CONSTRAINT [FK_PlugFile_PlugLocation]
GO
/****** Object:  ForeignKey [FK_PlugLocation_PlugIn]    Script Date: 12/05/2008 14:50:43 ******/
ALTER TABLE [dbo].[PlugLocation]  WITH CHECK ADD  CONSTRAINT [FK_PlugLocation_PlugIn] FOREIGN KEY([PlugId])
REFERENCES [dbo].[PlugIn] ([PlugId])
GO
ALTER TABLE [dbo].[PlugLocation] CHECK CONSTRAINT [FK_PlugLocation_PlugIn]
GO
/****** Object:  ForeignKey [FK_StaticTreeviewNode_Intance]    Script Date: 12/05/2008 14:50:46 ******/
ALTER TABLE [dbo].[StaticTreeviewNode]  WITH CHECK ADD  CONSTRAINT [FK_StaticTreeviewNode_Intance] FOREIGN KEY([InstanceId])
REFERENCES [dbo].[Instance] ([InstanceId])
GO
ALTER TABLE [dbo].[StaticTreeviewNode] CHECK CONSTRAINT [FK_StaticTreeviewNode_Intance]
GO
/****** Object:  ForeignKey [FK_StaticTreeviewNode_StaticTreeviewNode]    Script Date: 12/05/2008 14:50:46 ******/
ALTER TABLE [dbo].[StaticTreeviewNode]  WITH CHECK ADD  CONSTRAINT [FK_StaticTreeviewNode_StaticTreeviewNode] FOREIGN KEY([ParentInstanceId])
REFERENCES [dbo].[StaticTreeviewNode] ([InstanceId])
GO
ALTER TABLE [dbo].[StaticTreeviewNode] CHECK CONSTRAINT [FK_StaticTreeviewNode_StaticTreeviewNode]
GO
/****** Object:  ForeignKey [FK_WebComponentBinding_Binding]    Script Date: 12/05/2008 14:50:48 ******/
ALTER TABLE [dbo].[WebComponentBinding]  WITH CHECK ADD  CONSTRAINT [FK_WebComponentBinding_Binding] FOREIGN KEY([BindingId])
REFERENCES [dbo].[Binding] ([BindingId])
GO
ALTER TABLE [dbo].[WebComponentBinding] CHECK CONSTRAINT [FK_WebComponentBinding_Binding]
GO
