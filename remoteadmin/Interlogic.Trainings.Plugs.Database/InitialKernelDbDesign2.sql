IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'path' AND ss.name = N'dbo')
CREATE TYPE [dbo].[path] FROM [nvarchar](1000) NOT NULL
GO
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'systemName' AND ss.name = N'dbo')
CREATE TYPE [dbo].[systemName] FROM [varchar](255) NOT NULL
GO
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'description' AND ss.name = N'dbo')
CREATE TYPE [dbo].[description] FROM [nvarchar](4000) NULL
GO
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'name' AND ss.name = N'dbo')
CREATE TYPE [dbo].[name] FROM [nvarchar](1000) NOT NULL
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TRUE]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
EXEC dbo.sp_executesql N'/****** Object:  Default [dbo].[TRUE]    Script Date: 07/10/2008 10:27:40 ******/
create default [dbo].[TRUE] as 1
'
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FALSE]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
EXEC dbo.sp_executesql N'/****** Object:  Default [dbo].[FALSE]    Script Date: 07/10/2008 10:27:40 ******/
create default [dbo].[FALSE] as 0
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [PlugName_PlugIn] UNIQUE NONCLUSTERED 
(
	[PlugName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_bindefault @defname=N'[dbo].[TRUE]', @objname=N'[dbo].[PlugIn].[Active]' , @futureonly='futureonly'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BindablePoint]') AND type in (N'U'))
BEGIN
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
END
GO
EXEC sys.sp_bindefault @defname=N'[dbo].[TRUE]', @objname=N'[dbo].[BindablePoint].[Active]' , @futureonly='futureonly'
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
	[ClassDefinitionDescrition] [dbo].[description] NOT NULL,
	[Active] [dbo].[active] NOT NULL,
	[FileId] [int] NOT NULL,
	[PlugId] [int] NOT NULL,
 CONSTRAINT [PK_ClassDefinition] PRIMARY KEY CLUSTERED 
(
	[ClassDefinitionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_bindefault @defname=N'[dbo].[TRUE]', @objname=N'[dbo].[ClassDefinition].[Active]' , @futureonly='futureonly'
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Binding_BindablePoint]') AND parent_object_id = OBJECT_ID(N'[dbo].[Binding]'))
ALTER TABLE [dbo].[Binding]  WITH CHECK ADD  CONSTRAINT [FK_Binding_BindablePoint] FOREIGN KEY([BindablePointId])
REFERENCES [dbo].[BindablePoint] ([BindablePointId])
GO
ALTER TABLE [dbo].[Binding] CHECK CONSTRAINT [FK_Binding_BindablePoint]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Binding_ClassDefinition]') AND parent_object_id = OBJECT_ID(N'[dbo].[Binding]'))
ALTER TABLE [dbo].[Binding]  WITH CHECK ADD  CONSTRAINT [FK_Binding_ClassDefinition] FOREIGN KEY([ImplementationId])
REFERENCES [dbo].[ClassDefinition] ([ClassDefinitionId])
GO
ALTER TABLE [dbo].[Binding] CHECK CONSTRAINT [FK_Binding_ClassDefinition]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlugLocation_PlugIn]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlugLocation]'))
ALTER TABLE [dbo].[PlugLocation]  WITH CHECK ADD  CONSTRAINT [FK_PlugLocation_PlugIn] FOREIGN KEY([PlugId])
REFERENCES [dbo].[PlugIn] ([PlugId])
GO
ALTER TABLE [dbo].[PlugLocation] CHECK CONSTRAINT [FK_PlugLocation_PlugIn]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlugFile_PlugIn]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlugFile]'))
ALTER TABLE [dbo].[PlugFile]  WITH CHECK ADD  CONSTRAINT [FK_PlugFile_PlugIn] FOREIGN KEY([PlugId])
REFERENCES [dbo].[PlugIn] ([PlugId])
GO
ALTER TABLE [dbo].[PlugFile] CHECK CONSTRAINT [FK_PlugFile_PlugIn]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PlugFile_PlugLocation]') AND parent_object_id = OBJECT_ID(N'[dbo].[PlugFile]'))
ALTER TABLE [dbo].[PlugFile]  WITH CHECK ADD  CONSTRAINT [FK_PlugFile_PlugLocation] FOREIGN KEY([DestinationLocationId])
REFERENCES [dbo].[PlugLocation] ([PlugLocationId])
GO
ALTER TABLE [dbo].[PlugFile] CHECK CONSTRAINT [FK_PlugFile_PlugLocation]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BindablePoint_ClassDefinition]') AND parent_object_id = OBJECT_ID(N'[dbo].[BindablePoint]'))
ALTER TABLE [dbo].[BindablePoint]  WITH CHECK ADD  CONSTRAINT [FK_BindablePoint_ClassDefinition] FOREIGN KEY([InterfaceId])
REFERENCES [dbo].[ClassDefinition] ([ClassDefinitionId])
GO
ALTER TABLE [dbo].[BindablePoint] CHECK CONSTRAINT [FK_BindablePoint_ClassDefinition]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BindablePoint_PlugIn]') AND parent_object_id = OBJECT_ID(N'[dbo].[BindablePoint]'))
ALTER TABLE [dbo].[BindablePoint]  WITH CHECK ADD  CONSTRAINT [FK_BindablePoint_PlugIn] FOREIGN KEY([PlugId])
REFERENCES [dbo].[PlugIn] ([PlugId])
GO
ALTER TABLE [dbo].[BindablePoint] CHECK CONSTRAINT [FK_BindablePoint_PlugIn]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClassDefinition_PlugFile]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClassDefinition]'))
ALTER TABLE [dbo].[ClassDefinition]  WITH CHECK ADD  CONSTRAINT [FK_ClassDefinition_PlugFile] FOREIGN KEY([FileId])
REFERENCES [dbo].[PlugFile] ([PlugFileId])
GO
ALTER TABLE [dbo].[ClassDefinition] CHECK CONSTRAINT [FK_ClassDefinition_PlugFile]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClassDefinition_PlugIn]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClassDefinition]'))
ALTER TABLE [dbo].[ClassDefinition]  WITH CHECK ADD  CONSTRAINT [FK_ClassDefinition_PlugIn] FOREIGN KEY([PlugId])
REFERENCES [dbo].[PlugIn] ([PlugId])
GO
ALTER TABLE [dbo].[ClassDefinition] CHECK CONSTRAINT [FK_ClassDefinition_PlugIn]
