IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'systemName' AND ss.name = N'dbo')
CREATE TYPE [dbo].[systemName] FROM [varchar](255) NOT NULL
GO
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'description' AND ss.name = N'dbo')
CREATE TYPE [dbo].[description] FROM [nvarchar](4000) NULL
GO
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'name' AND ss.name = N'dbo')
CREATE TYPE [dbo].[name] FROM [nvarchar](1000) NOT NULL
GO
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'path' AND ss.name = N'dbo')
CREATE TYPE [dbo].[path] FROM [nvarchar](1000) NOT NULL
GO
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'measureUnit' AND ss.name = N'dbo')
CREATE TYPE [dbo].[measureUnit] FROM [int] NOT NULL
GO
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'positioningType' AND ss.name = N'dbo')
CREATE TYPE [dbo].[positioningType] FROM [int] NOT NULL
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[TRUE]') AND OBJECTPROPERTY(id, N'IsDefault') = 1)
EXEC dbo.sp_executesql N'/****** Object:  Default [dbo].[TRUE]    Script Date: 12/05/2008 14:50:18 ******/
create default [dbo].[TRUE] as 1
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Binding]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Binding](
	[BindingId] [int] IDENTITY(1,1) NOT NULL,
	[BindablePointId] [int] NOT NULL,
	[ImplementationId] [int] NOT NULL,
 CONSTRAINT [PK_Binding] PRIMARY KEY CLUSTERED 
(
	[BindingId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClassInheritance]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ClassInheritance](
	[ClassDefinitionId] [int] NOT NULL,
	[ParentClassDefinitionId] [int] NOT NULL,
 CONSTRAINT [PK_ClassInheritance] PRIMARY KEY CLUSTERED 
(
	[ClassDefinitionId] ASC,
	[ParentClassDefinitionId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AbstractContainer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AbstractContainer](
	[InstanceId] [int] NOT NULL,
	[UseLazyLoading] [bit] NOT NULL,
 CONSTRAINT [PK_AbstractContainer] PRIMARY KEY CLUSTERED 
(
	[InstanceId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Instance]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Instance](
	[InstanceId] [int] IDENTITY(1,1) NOT NULL,
	[ClassDefinitionId] [int] NOT NULL,
	[InstanceName] [dbo].[systemName] NOT NULL,
 CONSTRAINT [PK_Intance] PRIMARY KEY CLUSTERED 
(
	[InstanceId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BindablePointDefinition]') AND type in (N'U'))
BEGIN
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
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PlugLocation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PlugLocation](
	[PlugLocationId] [int] IDENTITY(1,1) NOT NULL,
	[PlugLocationName] [dbo].[systemName] NOT NULL,
	[PlugLocationDescription] [dbo].[description] NULL,
	[PlugLocationPath] [dbo].[path] NOT NULL,
	[PlugId] [int] NOT NULL,
 CONSTRAINT [PK_PlugLocation] PRIMARY KEY CLUSTERED 
(
	[PlugLocationId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RootContent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RootContent](
	[InstanceId] [int] NOT NULL,
	[ParentInstanceId] [int] NULL,
	[ContentFriendlyName] [dbo].[name] NOT NULL,
	[ContentDescription] [dbo].[description] NULL,
	[ContentImageSrc] [dbo].[name] NULL,
 CONSTRAINT [PK_RootContent] PRIMARY KEY CLUSTERED 
(
	[InstanceId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PlugFile]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PlugFile](
	[PlugFileId] [int] IDENTITY(1,1) NOT NULL,
	[RelativeIncomingPath] [dbo].[path] NOT NULL,
	[DestinationLocationId] [int] NOT NULL,
	[PlugId] [int] NOT NULL,
	[PlugFileName] [dbo].[name] NOT NULL,
 CONSTRAINT [PK_PlugFile] PRIMARY KEY CLUSTERED 
(
	[PlugFileId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AbstractComponent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AbstractComponent](
	[InstanceId] [int] NOT NULL,
	[ParentComponentId] [int] NULL,
	[TopVal] [int] NOT NULL,
	[TopUnit] [dbo].[measureUnit] NOT NULL,
	[LeftVal] [int] NOT NULL,
	[LeftUnit] [dbo].[measureUnit] NOT NULL,
	[HeightVal] [int] NOT NULL,
	[HeightUnit] [dbo].[measureUnit] NOT NULL,
	[WidthVal] [int] NOT NULL,
	[WidthUnit] [dbo].[measureUnit] NOT NULL,
	[PositioningMethod] [dbo].[positioningType] NULL,
 CONSTRAINT [PK_AbstractComponent] PRIMARY KEY CLUSTERED 
(
	[InstanceId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'active' AND ss.name = N'dbo')
CREATE TYPE [dbo].[active] FROM [bit] NOT NULL
GO
EXEC sys.sp_bindefault @defname=N'[dbo].[TRUE]', @objname=N'[dbo].[active]' , @futureonly='futureonly'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PlugIn]') AND type in (N'U'))
BEGIN
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
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_bindefault @defname=N'[dbo].[TRUE]', @objname=N'[dbo].[PlugIn].[Active]' , @futureonly='futureonly'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClassDefinition]') AND type in (N'U'))
BEGIN
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
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_bindefault @defname=N'[dbo].[TRUE]', @objname=N'[dbo].[ClassDefinition].[Active]' , @futureonly='futureonly'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BindablePoint]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BindablePoint](
	[BindablePointId] [int] IDENTITY(1,1) NOT NULL,
	[BindablePointDefinitonId] [int] NOT NULL,
	[InstanceId] [int] NOT NULL,
	[Active] [dbo].[active] NOT NULL,
 CONSTRAINT [PK_BindablePoint] PRIMARY KEY CLUSTERED 
(
	[BindablePointId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_bindefault @defname=N'[dbo].[TRUE]', @objname=N'[dbo].[BindablePoint].[Active]' , @futureonly='futureonly'
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FALSE]') AND OBJECTPROPERTY(id, N'IsDefault') = 1)
EXEC dbo.sp_executesql N'/****** Object:  Default [dbo].[FALSE]    Script Date: 12/05/2008 14:50:18 ******/
create default [dbo].[FALSE] as 0
'
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Binding_BindablePoint]') AND parent_object_id = OBJECT_ID(N'[dbo].[Binding]'))
ALTER TABLE [dbo].[Binding]  WITH CHECK ADD  CONSTRAINT [FK_Binding_BindablePoint] FOREIGN KEY([BindablePointId])
REFERENCES [dbo].[BindablePoint] ([BindablePointId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Binding_Intance]') AND parent_object_id = OBJECT_ID(N'[dbo].[Binding]'))
ALTER TABLE [dbo].[Binding]  WITH CHECK ADD  CONSTRAINT [FK_Binding_Intance] FOREIGN KEY([ImplementationId])
REFERENCES [dbo].[Instance] ([InstanceId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClassInheritance_ClassDefinition]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClassInheritance]'))
ALTER TABLE [dbo].[ClassInheritance]  WITH CHECK ADD  CONSTRAINT [FK_ClassInheritance_ClassDefinition] FOREIGN KEY([ClassDefinitionId])
REFERENCES [dbo].[ClassDefinition] ([ClassDefinitionId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClassInheritance_ClassDefinition1]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClassInheritance]'))
ALTER TABLE [dbo].[ClassInheritance]  WITH CHECK ADD  CONSTRAINT [FK_ClassInheritance_ClassDefinition1] FOREIGN KEY([ParentClassDefinitionId])
REFERENCES [dbo].[ClassDefinition] ([ClassDefinitionId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AbstractContainer_AbstractComponent]') AND parent_object_id = OBJECT_ID(N'[dbo].[AbstractContainer]'))
ALTER TABLE [dbo].[AbstractContainer]  WITH CHECK ADD  CONSTRAINT [FK_AbstractContainer_AbstractComponent] FOREIGN KEY([InstanceId])
REFERENCES [dbo].[AbstractComponent] ([InstanceId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instance_RootContent]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instance]'))
ALTER TABLE [dbo].[Instance]  WITH CHECK ADD  CONSTRAINT [FK_Instance_RootContent] FOREIGN KEY([InstanceId])
REFERENCES [dbo].[RootContent] ([InstanceId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Intance_ClassDefinition]') AND parent_object_id = OBJECT_ID(N'[dbo].[Instance]'))
ALTER TABLE [dbo].[Instance]  WITH CHECK ADD  CONSTRAINT [FK_Intance_ClassDefinition] FOREIGN KEY([ClassDefinitionId])
REFERENCES [dbo].[ClassDefinition] ([ClassDefinitionId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BindablePointDefinition_ClassDefinition]') AND parent_object_id = OBJECT_ID(N'[dbo].[BindablePointDefinition]'))
ALTER TABLE [dbo].[BindablePointDefinition]  WITH CHECK ADD  CONSTRAINT [FK_BindablePointDefinition_ClassDefinition] FOREIGN KEY([ClassDefinitionId])
REFERENCES [dbo].[ClassDefinition] ([ClassDefinitionId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BindablePointDefinition_ClassDefinition1]') AND parent_object_id = OBJECT_ID(N'[dbo].[BindablePointDefinition]'))
ALTER TABLE [dbo].[BindablePointDefinition]  WITH CHECK ADD  CONSTRAINT [FK_BindablePointDefinition_ClassDefinition1] FOREIGN KEY([InterfaceId])
REFERENCES [dbo].[ClassDefinition] ([ClassDefinitionId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlugLocation_PlugIn]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlugLocation]'))
ALTER TABLE [dbo].[PlugLocation]  WITH CHECK ADD  CONSTRAINT [FK_PlugLocation_PlugIn] FOREIGN KEY([PlugId])
REFERENCES [dbo].[PlugIn] ([PlugId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RootContent_RootContent]') AND parent_object_id = OBJECT_ID(N'[dbo].[RootContent]'))
ALTER TABLE [dbo].[RootContent]  WITH CHECK ADD  CONSTRAINT [FK_RootContent_RootContent] FOREIGN KEY([ParentInstanceId])
REFERENCES [dbo].[RootContent] ([InstanceId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlugFile_PlugIn]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlugFile]'))
ALTER TABLE [dbo].[PlugFile]  WITH CHECK ADD  CONSTRAINT [FK_PlugFile_PlugIn] FOREIGN KEY([PlugId])
REFERENCES [dbo].[PlugIn] ([PlugId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlugFile_PlugLocation]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlugFile]'))
ALTER TABLE [dbo].[PlugFile]  WITH CHECK ADD  CONSTRAINT [FK_PlugFile_PlugLocation] FOREIGN KEY([DestinationLocationId])
REFERENCES [dbo].[PlugLocation] ([PlugLocationId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AbstractComponent_AbstractComponent]') AND parent_object_id = OBJECT_ID(N'[dbo].[AbstractComponent]'))
ALTER TABLE [dbo].[AbstractComponent]  WITH CHECK ADD  CONSTRAINT [FK_AbstractComponent_AbstractComponent] FOREIGN KEY([ParentComponentId])
REFERENCES [dbo].[AbstractComponent] ([InstanceId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AbstractComponent_Instance]') AND parent_object_id = OBJECT_ID(N'[dbo].[AbstractComponent]'))
ALTER TABLE [dbo].[AbstractComponent]  WITH CHECK ADD  CONSTRAINT [FK_AbstractComponent_Instance] FOREIGN KEY([InstanceId])
REFERENCES [dbo].[Instance] ([InstanceId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClassDefinition_PlugFile]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClassDefinition]'))
ALTER TABLE [dbo].[ClassDefinition]  WITH CHECK ADD  CONSTRAINT [FK_ClassDefinition_PlugFile] FOREIGN KEY([FileId])
REFERENCES [dbo].[PlugFile] ([PlugFileId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClassDefinition_PlugIn]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClassDefinition]'))
ALTER TABLE [dbo].[ClassDefinition]  WITH CHECK ADD  CONSTRAINT [FK_ClassDefinition_PlugIn] FOREIGN KEY([PlugId])
REFERENCES [dbo].[PlugIn] ([PlugId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BindablePoint_BindablePointDefinition1]') AND parent_object_id = OBJECT_ID(N'[dbo].[BindablePoint]'))
ALTER TABLE [dbo].[BindablePoint]  WITH CHECK ADD  CONSTRAINT [FK_BindablePoint_BindablePointDefinition1] FOREIGN KEY([BindablePointDefinitonId])
REFERENCES [dbo].[BindablePointDefinition] ([BindablePointDefinitionId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BindablePoint_Intance]') AND parent_object_id = OBJECT_ID(N'[dbo].[BindablePoint]'))
ALTER TABLE [dbo].[BindablePoint]  WITH CHECK ADD  CONSTRAINT [FK_BindablePoint_Intance] FOREIGN KEY([InstanceId])
REFERENCES [dbo].[Instance] ([InstanceId])
