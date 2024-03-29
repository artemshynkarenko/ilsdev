/*
   29. november 200820:59:16
   User: sa
   Server: stranger
   Database: ASH_Trainings_RemoteAdmin
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ClassDefinition
	DROP COLUMN ParentClassDefinitionId
GO
COMMIT
select Has_Perms_By_Name(N'dbo.ClassDefinition', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ClassDefinition', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ClassDefinition', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.ClassInheritance
	(
	ClassDefinitionId int NOT NULL,
	ParentClassDefinitionId int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.ClassInheritance ADD CONSTRAINT
	PK_ClassInheritance PRIMARY KEY CLUSTERED 
	(
	ClassDefinitionId,
	ParentClassDefinitionId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.ClassInheritance ADD CONSTRAINT
	FK_ClassInheritance_ClassDefinition FOREIGN KEY
	(
	ClassDefinitionId
	) REFERENCES dbo.ClassDefinition
	(
	ClassDefinitionId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.ClassInheritance ADD CONSTRAINT
	FK_ClassInheritance_ClassDefinition1 FOREIGN KEY
	(
	ParentClassDefinitionId
	) REFERENCES dbo.ClassDefinition
	(
	ClassDefinitionId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.ClassInheritance', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ClassInheritance', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ClassInheritance', 'Object', 'CONTROL') as Contr_Per 