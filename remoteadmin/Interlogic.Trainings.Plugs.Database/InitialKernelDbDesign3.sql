USE [ASH_Trainings_RemoteAdmin]
GO
/****** Object:  UserDefinedDataType [dbo].[systemName]    Script Date: 09/05/2008 20:26:31 ******/
CREATE TYPE [dbo].[systemName] FROM [varchar](255) NOT NULL
GO
/****** Object:  UserDefinedDataType [dbo].[description]    Script Date: 09/05/2008 20:26:31 ******/
CREATE TYPE [dbo].[description] FROM [nvarchar](4000) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[name]    Script Date: 09/05/2008 20:26:31 ******/
CREATE TYPE [dbo].[name] FROM [nvarchar](1000) NOT NULL
GO
/****** Object:  UserDefinedDataType [dbo].[path]    Script Date: 09/05/2008 20:26:31 ******/
CREATE TYPE [dbo].[path] FROM [nvarchar](1000) NOT NULL
GO
/****** Object:  Default [dbo].[TRUE]    Script Date: 09/05/2008 20:26:31 ******/
create default [dbo].[TRUE] as 1
GO
/****** Object:  Default [dbo].[FALSE]    Script Date: 09/05/2008 20:26:31 ******/
create default [dbo].[FALSE] as 0
GO
/****** Object:  Table [dbo].[Binding]    Script Date: 09/05/2008 20:26:31 ******/
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
/****** Object:  Table [dbo].[TreeviewNodeBinding]    Script Date: 09/05/2008 20:26:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreeviewNodeBinding](
	[BindingId] [int] NOT NULL,
	[ParentBindingId] [int] NULL,
	[OnClickBindablePointId] [int] NULL,
	[OnContentDataBindBindablePointId] [int] NULL,
 CONSTRAINT [PK_TreeviewNodeBinding] PRIMARY KEY CLUSTERED 
(
	[BindingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'If value = null then we use parent''s  implementations of OnClickBindablePoint and OnContentDatabindBindablePoint' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TreeviewNodeBinding', @level2type=N'COLUMN',@level2name=N'OnClickBindablePointId'
GO
/****** Object:  Table [dbo].[WebComponentBinding]    Script Date: 09/05/2008 20:26:32 ******/
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
/****** Object:  Table [dbo].[ComponentDefiniton]    Script Date: 09/05/2008 20:26:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComponentDefiniton](
	[ClassDefinitionId] [int] NOT NULL,
 CONSTRAINT [PK_ComponentDefiniton] PRIMARY KEY CLUSTERED 
(
	[ClassDefinitionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlugLocation]    Script Date: 09/05/2008 20:26:32 ******/
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
/****** Object:  Table [dbo].[Intance]    Script Date: 09/05/2008 20:26:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Intance](
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
/****** Object:  Table [dbo].[BindablePointDefinition]    Script Date: 09/05/2008 20:26:31 ******/
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
/****** Object:  Table [dbo].[StaticTreeviewNode]    Script Date: 09/05/2008 20:26:32 ******/
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
/****** Object:  Table [dbo].[PlugFile]    Script Date: 09/05/2008 20:26:32 ******/
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
/****** Object:  UserDefinedDataType [dbo].[active]    Script Date: 09/05/2008 20:26:31 ******/
CREATE TYPE [dbo].[active] FROM [bit] NOT NULL
GO
EXEC sys.sp_bindefault @defname=N'[dbo].[TRUE]', @objname=N'[dbo].[active]' , @futureonly='futureonly'
GO
/****** Object:  Table [dbo].[PlugIn]    Script Date: 09/05/2008 20:26:32 ******/
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
/****** Object:  Table [dbo].[ClassDefinition]    Script Date: 09/05/2008 20:26:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassDefinition](
	[ClassDefinitionId] [int] IDENTITY(1,1) NOT NULL,
	[ParentClassDefinitionId] [int] NULL,
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
/****** Object:  Table [dbo].[BindablePoint]    Script Date: 09/05/2008 20:26:31 ******/
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
/****** Object:  ForeignKey [FK_BindablePoint_BindablePointDefinition1]    Script Date: 09/05/2008 20:26:31 ******/
ALTER TABLE [dbo].[BindablePoint]  WITH CHECK ADD  CONSTRAINT [FK_BindablePoint_BindablePointDefinition1] FOREIGN KEY([BindablePointDefinitonId])
REFERENCES [dbo].[BindablePointDefinition] ([BindablePointDefinitionId])
GO
ALTER TABLE [dbo].[BindablePoint] CHECK CONSTRAINT [FK_BindablePoint_BindablePointDefinition1]
GO
/****** Object:  ForeignKey [FK_BindablePoint_Intance]    Script Date: 09/05/2008 20:26:31 ******/
ALTER TABLE [dbo].[BindablePoint]  WITH CHECK ADD  CONSTRAINT [FK_BindablePoint_Intance] FOREIGN KEY([InstanceId])
REFERENCES [dbo].[Intance] ([InstanceId])
GO
ALTER TABLE [dbo].[BindablePoint] CHECK CONSTRAINT [FK_BindablePoint_Intance]
GO
/****** Object:  ForeignKey [FK_BindablePointDefinition_ClassDefinition]    Script Date: 09/05/2008 20:26:31 ******/
ALTER TABLE [dbo].[BindablePointDefinition]  WITH CHECK ADD  CONSTRAINT [FK_BindablePointDefinition_ClassDefinition] FOREIGN KEY([ClassDefinitionId])
REFERENCES [dbo].[ClassDefinition] ([ClassDefinitionId])
GO
ALTER TABLE [dbo].[BindablePointDefinition] CHECK CONSTRAINT [FK_BindablePointDefinition_ClassDefinition]
GO
/****** Object:  ForeignKey [FK_BindablePointDefinition_ClassDefinition1]    Script Date: 09/05/2008 20:26:31 ******/
ALTER TABLE [dbo].[BindablePointDefinition]  WITH CHECK ADD  CONSTRAINT [FK_BindablePointDefinition_ClassDefinition1] FOREIGN KEY([InterfaceId])
REFERENCES [dbo].[ClassDefinition] ([ClassDefinitionId])
GO
ALTER TABLE [dbo].[BindablePointDefinition] CHECK CONSTRAINT [FK_BindablePointDefinition_ClassDefinition1]
GO
/****** Object:  ForeignKey [FK_Binding_BindablePoint]    Script Date: 09/05/2008 20:26:31 ******/
ALTER TABLE [dbo].[Binding]  WITH CHECK ADD  CONSTRAINT [FK_Binding_BindablePoint] FOREIGN KEY([BindablePointId])
REFERENCES [dbo].[BindablePoint] ([BindablePointId])
GO
ALTER TABLE [dbo].[Binding] CHECK CONSTRAINT [FK_Binding_BindablePoint]
GO
/****** Object:  ForeignKey [FK_Binding_Intance]    Script Date: 09/05/2008 20:26:31 ******/
ALTER TABLE [dbo].[Binding]  WITH CHECK ADD  CONSTRAINT [FK_Binding_Intance] FOREIGN KEY([ImplementationId])
REFERENCES [dbo].[Intance] ([InstanceId])
GO
ALTER TABLE [dbo].[Binding] CHECK CONSTRAINT [FK_Binding_Intance]
GO
/****** Object:  ForeignKey [FK_ClassDefinition_PlugFile]    Script Date: 09/05/2008 20:26:31 ******/
ALTER TABLE [dbo].[ClassDefinition]  WITH CHECK ADD  CONSTRAINT [FK_ClassDefinition_PlugFile] FOREIGN KEY([FileId])
REFERENCES [dbo].[PlugFile] ([PlugFileId])
GO
ALTER TABLE [dbo].[ClassDefinition] CHECK CONSTRAINT [FK_ClassDefinition_PlugFile]
GO
/****** Object:  ForeignKey [FK_ClassDefinition_PlugIn]    Script Date: 09/05/2008 20:26:31 ******/
ALTER TABLE [dbo].[ClassDefinition]  WITH CHECK ADD  CONSTRAINT [FK_ClassDefinition_PlugIn] FOREIGN KEY([PlugId])
REFERENCES [dbo].[PlugIn] ([PlugId])
GO
ALTER TABLE [dbo].[ClassDefinition] CHECK CONSTRAINT [FK_ClassDefinition_PlugIn]
GO
/****** Object:  ForeignKey [FK_ComponentDefiniton_ClassDefinition]    Script Date: 09/05/2008 20:26:32 ******/
ALTER TABLE [dbo].[ComponentDefiniton]  WITH CHECK ADD  CONSTRAINT [FK_ComponentDefiniton_ClassDefinition] FOREIGN KEY([ClassDefinitionId])
REFERENCES [dbo].[ClassDefinition] ([ClassDefinitionId])
GO
ALTER TABLE [dbo].[ComponentDefiniton] CHECK CONSTRAINT [FK_ComponentDefiniton_ClassDefinition]
GO
/****** Object:  ForeignKey [FK_Intance_ClassDefinition]    Script Date: 09/05/2008 20:26:32 ******/
ALTER TABLE [dbo].[Intance]  WITH CHECK ADD  CONSTRAINT [FK_Intance_ClassDefinition] FOREIGN KEY([ClassDefinitionId])
REFERENCES [dbo].[ClassDefinition] ([ClassDefinitionId])
GO
ALTER TABLE [dbo].[Intance] CHECK CONSTRAINT [FK_Intance_ClassDefinition]
GO
/****** Object:  ForeignKey [FK_PlugFile_PlugIn]    Script Date: 09/05/2008 20:26:32 ******/
ALTER TABLE [dbo].[PlugFile]  WITH CHECK ADD  CONSTRAINT [FK_PlugFile_PlugIn] FOREIGN KEY([PlugId])
REFERENCES [dbo].[PlugIn] ([PlugId])
GO
ALTER TABLE [dbo].[PlugFile] CHECK CONSTRAINT [FK_PlugFile_PlugIn]
GO
/****** Object:  ForeignKey [FK_PlugFile_PlugLocation]    Script Date: 09/05/2008 20:26:32 ******/
ALTER TABLE [dbo].[PlugFile]  WITH CHECK ADD  CONSTRAINT [FK_PlugFile_PlugLocation] FOREIGN KEY([DestinationLocationId])
REFERENCES [dbo].[PlugLocation] ([PlugLocationId])
GO
ALTER TABLE [dbo].[PlugFile] CHECK CONSTRAINT [FK_PlugFile_PlugLocation]
GO
/****** Object:  ForeignKey [FK_PlugLocation_PlugIn]    Script Date: 09/05/2008 20:26:32 ******/
ALTER TABLE [dbo].[PlugLocation]  WITH CHECK ADD  CONSTRAINT [FK_PlugLocation_PlugIn] FOREIGN KEY([PlugId])
REFERENCES [dbo].[PlugIn] ([PlugId])
GO
ALTER TABLE [dbo].[PlugLocation] CHECK CONSTRAINT [FK_PlugLocation_PlugIn]
GO
/****** Object:  ForeignKey [FK_StaticTreeviewNode_Intance]    Script Date: 09/05/2008 20:26:32 ******/
ALTER TABLE [dbo].[StaticTreeviewNode]  WITH CHECK ADD  CONSTRAINT [FK_StaticTreeviewNode_Intance] FOREIGN KEY([InstanceId])
REFERENCES [dbo].[Intance] ([InstanceId])
GO
ALTER TABLE [dbo].[StaticTreeviewNode] CHECK CONSTRAINT [FK_StaticTreeviewNode_Intance]
GO
/****** Object:  ForeignKey [FK_StaticTreeviewNode_StaticTreeviewNode]    Script Date: 09/05/2008 20:26:32 ******/
ALTER TABLE [dbo].[StaticTreeviewNode]  WITH CHECK ADD  CONSTRAINT [FK_StaticTreeviewNode_StaticTreeviewNode] FOREIGN KEY([ParentInstanceId])
REFERENCES [dbo].[StaticTreeviewNode] ([InstanceId])
GO
ALTER TABLE [dbo].[StaticTreeviewNode] CHECK CONSTRAINT [FK_StaticTreeviewNode_StaticTreeviewNode]
GO
/****** Object:  ForeignKey [FK_TreeviewNodeBinding_BindablePoint]    Script Date: 09/05/2008 20:26:32 ******/
ALTER TABLE [dbo].[TreeviewNodeBinding]  WITH CHECK ADD  CONSTRAINT [FK_TreeviewNodeBinding_BindablePoint] FOREIGN KEY([OnClickBindablePointId])
REFERENCES [dbo].[BindablePoint] ([BindablePointId])
GO
ALTER TABLE [dbo].[TreeviewNodeBinding] CHECK CONSTRAINT [FK_TreeviewNodeBinding_BindablePoint]
GO
/****** Object:  ForeignKey [FK_TreeviewNodeBinding_BindablePoint1]    Script Date: 09/05/2008 20:26:32 ******/
ALTER TABLE [dbo].[TreeviewNodeBinding]  WITH CHECK ADD  CONSTRAINT [FK_TreeviewNodeBinding_BindablePoint1] FOREIGN KEY([OnContentDataBindBindablePointId])
REFERENCES [dbo].[BindablePoint] ([BindablePointId])
GO
ALTER TABLE [dbo].[TreeviewNodeBinding] CHECK CONSTRAINT [FK_TreeviewNodeBinding_BindablePoint1]
GO
/****** Object:  ForeignKey [FK_TreeviewNodeBinding_Binding]    Script Date: 09/05/2008 20:26:32 ******/
ALTER TABLE [dbo].[TreeviewNodeBinding]  WITH CHECK ADD  CONSTRAINT [FK_TreeviewNodeBinding_Binding] FOREIGN KEY([BindingId])
REFERENCES [dbo].[Binding] ([BindingId])
GO
ALTER TABLE [dbo].[TreeviewNodeBinding] CHECK CONSTRAINT [FK_TreeviewNodeBinding_Binding]
GO
/****** Object:  ForeignKey [FK_TreeviewNodeBinding_TreeviewNodeBinding]    Script Date: 09/05/2008 20:26:32 ******/
ALTER TABLE [dbo].[TreeviewNodeBinding]  WITH CHECK ADD  CONSTRAINT [FK_TreeviewNodeBinding_TreeviewNodeBinding] FOREIGN KEY([ParentBindingId])
REFERENCES [dbo].[TreeviewNodeBinding] ([BindingId])
GO
ALTER TABLE [dbo].[TreeviewNodeBinding] CHECK CONSTRAINT [FK_TreeviewNodeBinding_TreeviewNodeBinding]
GO
/****** Object:  ForeignKey [FK_WebComponentBinding_Binding]    Script Date: 09/05/2008 20:26:32 ******/
ALTER TABLE [dbo].[WebComponentBinding]  WITH CHECK ADD  CONSTRAINT [FK_WebComponentBinding_Binding] FOREIGN KEY([BindingId])
REFERENCES [dbo].[Binding] ([BindingId])
GO
ALTER TABLE [dbo].[WebComponentBinding] CHECK CONSTRAINT [FK_WebComponentBinding_Binding]
GO
